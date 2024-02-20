using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Domain;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.Models;
using HouseExpenseTracker.ViewControls;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace HouseExpenseTracker.ViewModels;

public partial class AddExpensePageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;
    readonly SnackbarService _alertService;

    [ObservableProperty]
    NewExpenseDto _newExpense;

    [ObservableProperty] ObservableCollection<PickerItemDto> _paidToPersons;
    [ObservableProperty] ObservableCollection<PickerItemDto> _paidByPersons;

    public NewExpenseDtoValidator NewExpenseValidator { get; init; } = new();

    public AddExpensePageViewModel(IDbContextFactory<AppDbContext> dbContext, SnackbarService alertService)
    {
        _dbContext = dbContext.CreateDbContext();
        _newExpense = new();
        InitCommand = new AsyncRelayCommand(Init);
        _alertService = alertService;
    }

    public IAsyncRelayCommand InitCommand { get; }

    async Task Init(CancellationToken cancellationToken)
    {
        var persons = await _dbContext.Persons
            .Select(x => new PickerItemDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync(cancellationToken);

        // This option can be used to show special popup where user can create new person data.
        persons.Add(new PickerItemDto
        {
            Id = -100,
            Name = "ADD NEW"
        });

        PaidByPersons = new ObservableCollection<PickerItemDto>(persons);
        PaidToPersons = new ObservableCollection<PickerItemDto>(persons);

        _newExpense.PaidBy = GetDefaultPayer();
    }

    [RelayCommand]
    async Task AddExpense()
    {
        try
        {
            if (!NewExpenseValidator.Validate(NewExpense).IsValid)
            {
                return;
            }
            Expense newExpense = new Expense()
            {
                Title = NewExpense.Title.ToUpper(),
                Amount = NewExpense.Amount.Value,
                Description = NewExpense.Description?.Humanize(),
                ExpenseAddedOn = NewExpense.ExpenseAddedOn,
                PaidToId = NewExpense.PaidTo?.Id,
                PaidById = NewExpense.PaidBy.Id
            };

            _dbContext.Expenses.Add(newExpense);
            await _dbContext.SaveChangesAsync();
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await _alertService.Error();
        }
    }

    [RelayCommand]
    async Task PaidToItemSelected(PickerItemDto selectedItem)
    {
        if (selectedItem == null) return;

        if (selectedItem.Id == -100)
        {
            await AddNewPayee();
        }
    }

    [RelayCommand]
    async Task PaidByItemSelected(PickerItemDto selectedItem)
    {
        if (selectedItem == null) return;

        if (selectedItem.Id == -100)
        {
            await AddNewPayer();
        }
    }

    private async Task AddNewPayee()
    {
        string paieeName = await Application.Current.MainPage.DisplayPromptAsync("Payee Name", "", "Save", maxLength: 30, keyboard: Keyboard.Text);
        if (!string.IsNullOrEmpty(paieeName))
        {
            PickerItemDto selectedPerson = await AddPersonToDb(paieeName);
            PaidToPersons.Add(selectedPerson);
            OnPropertyChanged(nameof(PaidToPersons));
            NewExpense.PaidTo = selectedPerson;
        }
        else
        {
            // Reset the paid to field
            NewExpense.PaidTo = null;
        }
    }

    private async Task AddNewPayer()
    {
        string payerName = await Application.Current.MainPage.DisplayPromptAsync("Payer Name", "", "Save", maxLength: 30, keyboard: Keyboard.Text);
        if (!string.IsNullOrEmpty(payerName))
        {
            PickerItemDto selectedPerson = await AddPersonToDb(payerName); 
            var persons = PaidByPersons;
            persons.Add(selectedPerson);
            PaidByPersons = persons;

            NewExpense.PaidBy = selectedPerson;
        }
        else
        {
            // Reset the paid to field
            NewExpense.PaidBy = GetDefaultPayer();
        }
    }

    private PickerItemDto GetDefaultPayer()
    {
        return PaidByPersons.First(x => x.Id == -1);
    }

    private async Task<PickerItemDto> AddPersonToDb(string paieeName)
    {
        var newPerson = new Person()
        {
            Name = paieeName.Pascalize()
        };
        _dbContext.Persons.Add(newPerson);
        await _dbContext.SaveChangesAsync();

        var selectedPerson = new PickerItemDto
        {
            Id = newPerson.Id,
            Name = newPerson.Name,
        };
        return selectedPerson;
    }
}
