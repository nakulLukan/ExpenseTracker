using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Helpers;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HouseExpenseTracker.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;

    [ObservableProperty]
    IList<ExpenseGroupListItemDto> _monthlyExpenses;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    float _totalAmount = 0;

    [ObservableProperty] private string _searchText;

    [ObservableProperty] IList<PickerItemDto> _persons;

    [ObservableProperty] private PickerItemDto _paidByFilter;
    [ObservableProperty] private PickerItemDto _paidToFilter;

    public MainPageViewModel(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext.CreateDbContext();
        InitCommand = new AsyncRelayCommand(Init, AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
        MonthlyExpenses = new List<ExpenseGroupListItemDto>();
    }

    public IAsyncRelayCommand InitCommand { get; }
    async Task Init(CancellationToken cancellationToken)
    {
        IsRefreshing = true;
        await GetExpenses(cancellationToken);
        await CheckAndUpdatePersons();
        IsRefreshing = false;
    }

    private async Task CheckAndUpdatePersons()
    {
        var persons = await _dbContext.Persons
            .Select(x => new PickerItemDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
        if (persons.Count != Persons?.Count)
        {
            Persons = persons;
        }
    }

    private async Task GetExpenses(CancellationToken cancellationToken)
    {
        var currentExpensesQuery = _dbContext.Expenses
                    .Include(x => x.PaidTo)
                    .Include(x => x.PaidBy)
                    .AsQueryable();

        if (!string.IsNullOrEmpty(SearchText))
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.Title.ToUpper().Contains(SearchText.ToUpper()));
        }

        if (PaidByFilter != null)
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.PaidById == PaidByFilter.Id);
        }

        if (PaidToFilter != null)
        {
            currentExpensesQuery = currentExpensesQuery.Where(x => x.PaidToId == PaidToFilter.Id);
        }

        var currentExpenses = await currentExpensesQuery
            .OrderByDescending(x => x.ExpenseAddedOn)
            .ToListAsync(cancellationToken);

        var result = new List<ExpenseGroupListItemDto>();
        foreach (var expense in currentExpenses.GroupBy(x => new { x.ExpenseAddedOn.Year, x.ExpenseAddedOn.Month }))
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(expense.Key.Month);

            result.Add(new ExpenseGroupListItemDto($"{month} {expense.Key.Year}",
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
                    item.PaidBy = x.PaidBy.Name;
                    item.Amount = x.Amount;
                    item.Id = x.Id;
                    return item;
                }).ToList()
                )
            );

            TotalAmount = result.Sum(x => x.TotalAmount);
        }

        MonthlyExpenses = result;
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
