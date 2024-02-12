using HouseExpenseTracker.ViewControls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace HouseExpenseTracker.ViewHandlers;

public partial class DatePickerHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(CustomDatePicker), (h, v) =>
        {
            if (h.VirtualView is CustomDatePicker customDatePicker)
            {
                // Set Tint Color to transparent
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            }
        });
    }
}
