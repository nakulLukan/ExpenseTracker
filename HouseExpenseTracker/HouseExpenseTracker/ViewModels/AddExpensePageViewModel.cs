using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HouseExpenseTracker.Domain;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace HouseExpenseTracker.ViewModels;

public partial class AddExpensePageViewModel : ObservableObject
{
    readonly AppDbContext _dbContext;

    [ObservableProperty]
    NewExpenseDto _newExpense;

    [ObservableProperty]
    ObservableCollection<PersonPickerItemDto> _persons;

    public AddExpensePageViewModel(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext.CreateDbContext();
        _newExpense = new();
        _persons = new();
        InitCommand = new AsyncRelayCommand(Init);
    }

    public IAsyncRelayCommand InitCommand { get; }

    async Task Init(CancellationToken cancellationToken)
    {
        var persons = await _dbContext.Persons
            .Select(x => new PersonPickerItemDto
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToListAsync(cancellationToken);
        persons.ForEach(x => Persons.Add(x));

        // This option can be used to show special popup where user can create new person data.
        Persons.Add(new PersonPickerItemDto
        {
            Id = 0,
            Name = "ADD NEW"
        });
    }

    [RelayCommand]
    async Task AddExpense()
    {
        Expense newExpense = new Expense()
        {
            Title = NewExpense.Title,
            Amount = float.Parse(NewExpense.Amount),
            Description = NewExpense.Description,
            ExpenseAddedOn = NewExpense.ExpenseAddedOn,
            PaidToId = NewExpense.PaidTo?.Id
        };

        _dbContext.Expenses.Add(newExpense);
        await _dbContext.SaveChangesAsync();
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task PaidToItemSelected(PersonPickerItemDto selectedItem)
    {
        if (selectedItem == null) return;

        if (selectedItem.Id == 0)
        {
            await AddNewPerson();
        }
    }

    private async Task AddNewPerson()
    {
        string paieeName = await Application.Current.MainPage.DisplayPromptAsync("Paiee Name", "", "Save", maxLength: 30, keyboard: Keyboard.Text);
        if (!string.IsNullOrEmpty(paieeName))
        {
            var newPerson = new Person()
            {
                Name = paieeName.Pascalize()
            };
            _dbContext.Persons.Add(newPerson);
            await _dbContext.SaveChangesAsync();

            var selectedPerson = new PersonPickerItemDto
            {
                Id = newPerson.Id,
                Name = newPerson.Name,
            };
            Persons.Add(selectedPerson);
            NewExpense.PaidTo = selectedPerson;
        }
    }
}
