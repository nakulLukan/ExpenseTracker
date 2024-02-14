using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Extensions;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.ViewControls;
using Microsoft.EntityFrameworkCore;

namespace HouseExpenseTracker.ViewModels;

[QueryProperty(nameof(ExpenseId), "ExpenseId")]
public partial class ExpenseDetailPageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;
    readonly SnackbarService _alertService;

    [ObservableProperty]
    private int _expenseId;

    [ObservableProperty] private string _expenseTitle;
    [ObservableProperty] private string _expenseDescription;
    [ObservableProperty] private string _expensePaidTo;
    [ObservableProperty] private string _expenseAddedOn;
    [ObservableProperty] private string _amount;

    public ExpenseDetailPageViewModel(AppDbContext dbContext, SnackbarService alertService)
    {
        _dbContext = dbContext;
        InitCommand = new AsyncRelayCommand(Init);
        _alertService = alertService;
    }

    public IAsyncRelayCommand InitCommand { get; }
    async Task Init(CancellationToken cancellationToken)
    {
        var expense = await _dbContext.Expenses
            .Include(x => x.PaidTo)
            .FirstOrDefaultAsync(x => x.Id == ExpenseId, cancellationToken);
        ExpenseTitle = expense.Title;
        ExpenseDescription = expense.Description;
        ExpensePaidTo = expense.PaidTo?.Name ?? "not mentioned";
        Amount = expense.Amount.ToCurrency();
        ExpenseAddedOn = expense.ExpenseAddedOn.ToString("ddd, dd MMM yyyy");
    }

    [RelayCommand]
    async Task DeleteExpense()
    {
        var response = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to delete the record?", "Yes", "Cancel");
        if (response)
        {
            var deleted = await _dbContext.Expenses
                .Where(x => x.Id == ExpenseId)
                .ExecuteDeleteAsync();
            if (deleted == 0)
            {
                await _alertService.Error("Failed to delete the record.");
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
