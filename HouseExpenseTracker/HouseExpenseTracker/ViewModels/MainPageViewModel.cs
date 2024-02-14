using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Helpers;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Globalization;

namespace HouseExpenseTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;

    public ObservableCollection<ExpenseGroupListItemDto> MonthlyExpenses { get; } = new();

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    float _totalAmount = 0;

    public MainPageViewModel(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext.CreateDbContext();
        InitCommand = new AsyncRelayCommand(Init);
    }

    public IAsyncRelayCommand InitCommand { get; }
    async Task Init(CancellationToken cancellationToken)
    {
        IsRefreshing = true;
        var currentExpenses = await _dbContext.Expenses
            .Include(x => x.PaidTo)
            .OrderByDescending(x => x.ExpenseAddedOn)
            .ToListAsync(cancellationToken);
        if (currentExpenses.Count == MonthlyExpenses.Count)
        {
            return;
        }

        MonthlyExpenses.Clear();
        foreach (var expense in currentExpenses.GroupBy(x => new { x.ExpenseAddedOn.Year, x.ExpenseAddedOn.Month }))
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(expense.Key.Month);

            MonthlyExpenses.Add(new ExpenseGroupListItemDto($"{month} {expense.Key.Year}",
                expense.Sum(x => x.Amount),
                expense.Select(x =>
                        {
                            var item = new ExpenseListItemDto();
                            item.AddedMonth = month;
                            item.AddedDate = x.ExpenseAddedOn.Day.ToString();
                            item.AddedMonth = month;
                            item.ExpenseName = x.Title;
                            item.Description = x.Description;
                            item.PaidTo = x.PaidTo?.Name ?? "-";
                            item.Amount = x.Amount;
                            item.Id = x.Id;
                            return item;
                        }).ToList()
                )
            );

            TotalAmount = MonthlyExpenses.Sum(x => x.TotalAmount);
        }

        IsRefreshing = false;
    }

    [RelayCommand]
    async Task AddExpense()
    {
        await Shell.Current.GoToAsync(PageRoutePath.AddExpensePage);
    }

    [RelayCommand]
    async Task OnExpenseSelected(int selectedExpenseId)
    {
        await Shell.Current.GoToAsync(PageRoutePath.ExpenseDetailPage + $"?ExpenseId={selectedExpenseId}");
    }
}
