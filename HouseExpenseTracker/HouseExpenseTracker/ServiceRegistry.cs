using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.ViewControls;
using HouseExpenseTracker.ViewHandlers;
using HouseExpenseTracker.ViewModels;
using HouseExpenseTracker.Views;

namespace HouseExpenseTracker;

public static class ServiceRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        RegisterViewsAndViewModels(services);
        AddDbContext(services);

        services.AddSingleton<SnackbarService>();

        RegisterHandlersAndMappers();

        return services;
    }

    private static void AddDbContext(IServiceCollection services)
    {
        string dbBasePath = FileSystem.AppDataDirectory;
        AppDbContext.Init(dbBasePath);
        services.AddDbContextFactory<AppDbContext>();
        services.AddDbContext<AppDbContext>(opt => { }, contextLifetime: ServiceLifetime.Scoped);
    }

    private static void RegisterHandlersAndMappers()
    {
        EntryHandler.UseHandler();
        EditorHandler.UseHandler();
        PickerHandler.UseHandler();
        DatePickerHandler.UseHandler();
    }

    private static void RegisterViewsAndViewModels(IServiceCollection services)
    {
        services.AddTransient<MainPage>();
        services.AddTransient<MainPageViewModel>();

        services.AddTransient<AddExpensePage>();
        services.AddTransient<AddExpensePageViewModel>();

        services.AddTransient<ExpenseDetailPage>();
        services.AddTransient<ExpenseDetailPageViewModel>();
    }
}
