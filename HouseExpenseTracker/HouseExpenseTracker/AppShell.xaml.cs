using HouseExpenseTracker.Views;
namespace HouseExpenseTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoute<AddExpensePage>();
            RegisterRoute<ExpenseDetailPage>();
        }

        private void RegisterRoute<T>()
        {
            Routing.RegisterRoute(typeof(T).Name, typeof(T));
        }
    }
}
