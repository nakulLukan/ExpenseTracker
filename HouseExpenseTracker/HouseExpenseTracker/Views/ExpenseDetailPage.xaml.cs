using HouseExpenseTracker.ViewModels;

namespace HouseExpenseTracker.Views;

public partial class ExpenseDetailPage : ContentPage
{
    ExpenseDetailPageViewModel vm;
    public ExpenseDetailPage(ExpenseDetailPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = vm = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitCommand.ExecuteAsync(this);
    }
}