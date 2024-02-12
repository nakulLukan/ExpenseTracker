using CommunityToolkit.Mvvm.ComponentModel;
using HouseExpenseTracker.Utilities;

namespace HouseExpenseTracker.Models;

public partial class NewExpenseDto : ObservableObject
{
    [ObservableProperty] string _title;
    [ObservableProperty] string _description;
    [ObservableProperty] float? _amount;
    [ObservableProperty] PersonPickerItemDto _paidTo;
    [ObservableProperty] DateTime _expenseAddedOn = AppDateTime.LocalNow.DateTime;
}
