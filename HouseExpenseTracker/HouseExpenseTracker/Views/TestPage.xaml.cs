using HouseExpenseTracker.ViewModels;

namespace HouseExpenseTracker.Views;

public partial class TestPage : ContentPage
{
	public TestPage(AddExpensePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}