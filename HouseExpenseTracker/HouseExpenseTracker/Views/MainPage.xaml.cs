using HouseExpenseTracker.ViewModels;

namespace HouseExpenseTracker.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;

        public MainPage(MainPageViewModel viewModel)
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
}
