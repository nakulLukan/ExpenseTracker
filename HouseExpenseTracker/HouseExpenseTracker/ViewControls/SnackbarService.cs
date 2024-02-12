using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace HouseExpenseTracker.ViewControls;

public class SnackbarService
{
    public async Task Error(string message = "Oops, something went wrong.")
    {
        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(14).WithAttributes(FontAttributes.Bold),
            ActionButtonFont = Font.SystemFontOfSize(14).WithAttributes(FontAttributes.Bold).WithWeight(FontWeight.Heavy),
            ActionButtonTextColor = Colors.White,
        };

        string actionButtonText = "Ok";
        TimeSpan duration = TimeSpan.FromSeconds(3);

        using var snackbar = Snackbar.Make(message, duration: duration, visualOptions: snackbarOptions);
        await snackbar.Show();
    }
}
