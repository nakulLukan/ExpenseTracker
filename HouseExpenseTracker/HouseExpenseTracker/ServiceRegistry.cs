using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.ViewControls;
using HouseExpenseTracker.ViewModels;
using HouseExpenseTracker.Views;

namespace HouseExpenseTracker;

public static class ServiceRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        RegisterViewsAndViewModels(services);
        string dbBasePath = FileSystem.AppDataDirectory;
        AppDbContext.Init(FileSystem.AppDataDirectory);
        services.AddDbContextFactory<AppDbContext>();
        services.AddDbContext<AppDbContext>(opt => { }, contextLifetime: ServiceLifetime.Scoped);

        services.AddSingleton<SnackbarService>();
        return services;
    }

    private static void RegisterViewsAndViewModels(IServiceCollection services)
    {
        services.AddTransient<MainPage>();
        services.AddTransient<MainPageViewModel>();

        services.AddTransient<AddExpensePage>();
        services.AddTransient<AddExpensePageViewModel>();

        services.AddTransient<TestPage>();
        services.AddTransient<TestViewModel>();
    }
}
