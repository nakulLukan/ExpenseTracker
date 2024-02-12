using HouseExpenseTracker.ViewControls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace HouseExpenseTracker.ViewHandlers;

public partial class PickerHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(CustomPicker), (h, v) =>
        {
            if (h.VirtualView is CustomPicker customPicker)
            {
                // Set Tint Color to transparent
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            }
        });
    }
}
