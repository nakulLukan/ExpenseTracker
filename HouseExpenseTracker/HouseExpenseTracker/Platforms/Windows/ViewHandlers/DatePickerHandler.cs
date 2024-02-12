using HouseExpenseTracker.ViewControls;

namespace HouseExpenseTracker.ViewHandlers;

public partial class DatePickerHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(CustomDatePicker), (h, v) =>
        {
            if (h.VirtualView is CustomDatePicker customDatePicker)
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            }
        });
    }
}
