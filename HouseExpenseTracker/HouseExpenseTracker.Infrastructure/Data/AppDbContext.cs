using HouseExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseExpenseTracker.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Person> Persons { get; set; }

    const string _databaseFilename = "TrackerDb.db3";
    static string _databaseFilePath = string.Empty;

    const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    static string _databasePath =>
        Path.Combine(_databaseFilePath, _databaseFilename);

    public AppDbContext()
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public static void Init(string dbRelativePath)
    {
        _databaseFilePath = dbRelativePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        BuildModel(modelBuilder.Entity<Expense>());
        BuildModel(modelBuilder.Entity<Person>());
    }

    void BuildModel(EntityTypeBuilder<Expense> modelBuilder)
    {
        modelBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Property(x => x.Title).IsRequired();
        modelBuilder.Property(x => x.Description).IsRequired(false);

        modelBuilder.HasOne(x => x.PaidTo)
            .WithMany()
            .HasForeignKey(x => x.PaidToId);
    }

    void BuildModel(EntityTypeBuilder<Person> modelBuilder)
    {
        modelBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Property(x => x.Name).IsRequired();
    }
}
