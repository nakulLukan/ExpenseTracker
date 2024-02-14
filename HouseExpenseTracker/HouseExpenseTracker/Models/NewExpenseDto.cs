using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;
using HouseExpenseTracker.Utilities;
using HouseExpenseTracker.Utilities.Validation;

namespace HouseExpenseTracker.Models;

public partial class NewExpenseDto : ObservableObject
{
    [ObservableProperty] string _title;
    [ObservableProperty] string _description;
    [ObservableProperty] float? _amount;
    [ObservableProperty] PersonPickerItemDto _paidTo;
    [ObservableProperty] DateTime _expenseAddedOn = AppDateTime.LocalNow.DateTime;
}

public class NewExpenseDtoValidator : AppAbstractValidator<NewExpenseDto>
{
    public NewExpenseDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(100);

        RuleFor(x => x.ExpenseAddedOn)
            .LessThan(DateTime.Now.AddDays(1).Date);

        RuleFor(x => x.Amount)
            .NotNull()
            .GreaterThan(0);
    }
}
