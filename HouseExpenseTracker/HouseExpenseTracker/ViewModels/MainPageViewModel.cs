using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Domain;
using HouseExpenseTracker.Extensions;
using HouseExpenseTracker.Helpers;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace HouseExpenseTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;

    public ObservableCollection<ExpenseGroupListItemDto> MonthlyExpenses { get; }

    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private bool _canExport = true;

    [ObservableProperty]
    float _totalAmount = 0;

    [ObservableProperty] private string _searchText;

    [ObservableProperty] ObservableCollection<PickerItemDto> _persons;

    [ObservableProperty] private PickerItemDto _paidByFilter;
    [ObservableProperty] private PickerItemDto _paidToFilter;

    public MainPageViewModel(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext.CreateDbContext();
        InitCommand = new AsyncRelayCommand(Init, AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
        MonthlyExpenses = new ObservableCollection<ExpenseGroupListItemDto>();
    }

    public IAsyncRelayCommand InitCommand { get; }
    async Task Init(CancellationToken cancellationToken)
    {
        IsRefreshing = true;
        await GetExpenses(cancellationToken);
        await CheckAndUpdatePersons();
        IsRefreshing = false;
    }

    private async Task CheckAndUpdatePersons()
    {
        var persons = await _dbContext.Persons
            .Select(x => new PickerItemDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        if (persons.Count != Persons?.Where(x => x.Id != 0)?.Count())
        {
            Persons = new ObservableCollection<PickerItemDto>(persons);
        }
    }

    private async Task GetExpenses(CancellationToken cancellationToken)
    {
        var currentExpensesQuery = _dbContext.Expenses
                    .Include(x => x.PaidTo)
                    .Include(x => x.PaidBy)
                    .AsQueryable();

        if (!string.IsNullOrEmpty(SearchText))
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.Title.ToUpper().Contains(SearchText.ToUpper()));
        }

        if (PaidByFilter != null)
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.PaidById == PaidByFilter.Id);
        }

        if (PaidToFilter != null)
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.PaidToId == PaidToFilter.Id);
        }

        var currentExpenses = await currentExpensesQuery
            .OrderByDescending(x => x.ExpenseAddedOn)
            .ToListAsync(cancellationToken);

        MonthlyExpenses.Clear();
        foreach (var expense in currentExpenses.GroupBy(x => new { x.ExpenseAddedOn.Year, x.ExpenseAddedOn.Month }))
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(expense.Key.Month);

            var item = (new ExpenseGroupListItemDto($"{month} {expense.Key.Year}",
                expense.Sum(x => x.Amount),
                expense.Select(x =>
                {
                    var item = new ExpenseListItemDto();
                    item.AddedMonth = month;
                    item.AddedDate = x.ExpenseAddedOn.Day.ToString();
                    item.AddedMonth = month;
                    item.ExpenseName = x.Title;
                    item.Description = x.Description;
                    item.PaidTo = x.PaidTo?.Name ?? "-";
                    item.PaidBy = x.PaidBy.Name;
                    item.Amount = x.Amount;
                    item.Id = x.Id;
                    return item;
                }).ToList()
                )
            );

            MonthlyExpenses.Add(item);
            TotalAmount = MonthlyExpenses.Sum(x => x.TotalAmount);
        }
    }

    [RelayCommand]
    async Task AddExpense()
    {
        await Shell.Current.GoToAsync(PageRoutePath.AddExpensePage);
    }

    [RelayCommand]
    async Task OnExpenseSelected(int selectedExpenseId)
    {
        await Shell.Current.GoToAsync(PageRoutePath.ExpenseDetailPage + $"?ExpenseId={selectedExpenseId}");
    }

    [RelayCommand(CanExecute = nameof(CanExport))]
    async Task OnExport()
    {
        CanExport = false;
        var expenses = await _dbContext.Expenses
            .OrderByDescending(x => x.ExpenseAddedOn)
            .Include(x => x.PaidTo)
            .Include(x => x.PaidBy)
            .ToListAsync();
        StringBuilder csv = GenerateCsvFileContent(expenses);
        string path = await SaveCsvFileToLocalStorage(csv);
        await EmailExpenses(path, "House Construction Cost", expenses.Sum(x => x.Amount));
        CanExport = true;
    }

    private static async Task<string> SaveCsvFileToLocalStorage(StringBuilder csv)
    {
        var fileName = "Expenses.csv";
        // Store in storage
        var path = Path.Combine(FileSystem.AppDataDirectory, fileName);
        await File.WriteAllTextAsync(path, csv.ToString());
        return path;
    }

    private static StringBuilder GenerateCsvFileContent(List<Expense> expenses)
    {
        // Map each column to to string
        var csv = new StringBuilder();
        // Humanize column name for readability
        csv.AppendLine("House Construction Cost");
        csv.AppendLine($"Total Cost: {expenses.Sum(x => x.Amount).ToCurrency()}");
        csv.AppendLine();
        csv.AppendLine("Date,Title,Description,Amount,Paid To,Paid By");
        foreach (var expense in expenses)
        {
            var newLine = $"{expense.ExpenseAddedOn},{expense.Title},{expense.Description},{expense.Amount},{expense.PaidTo?.Name ?? "-"},{expense.PaidBy.Name}";
            csv.AppendLine(newLine);
        }

        return csv;
    }

    async Task EmailExpenses(string attachmentPath, string expenseGroupName, float totalCost)
    {
        if (Email.Default.IsComposeSupported)
        {
            string subject = "House Construction Expenses";
            StringBuilder emailBodyContent = new StringBuilder(expenseGroupName).AppendLine();
            emailBodyContent.AppendLine($"Total Cost: {totalCost.ToCurrency()}");
            string body = emailBodyContent.ToString();
            string[] recipients = new[] { "matrixlukan@gmail.com" };

            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };
            message.Attachments.Add(new EmailAttachment(attachmentPath));
            await Email.Default.ComposeAsync(message);
        }
    }
}
