using CommunityToolkit.Mvvm.ComponentModel;
using HouseExpenseTracker.Infrastructure.Data;

namespace HouseExpenseTracker.ViewModels;

public partial class TestViewModel : ObservableObject
{
    [ObservableProperty] string _testMessage = "HElooooooooooooooooooo";
    readonly AppDbContext _dbContext;

    public TestViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


}
