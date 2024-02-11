using HouseExpenseTracker.ViewModels;

namespace HouseExpenseTracker.Views;

public partial class AddExpensePage : ContentPage
{
    AddExpensePageViewModel vm;
    public AddExpensePage(AddExpensePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = vm = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.InitCommand.ExecuteAsync(null);
    }
}