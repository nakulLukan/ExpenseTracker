using HouseExpenseTracker.ViewControls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace HouseExpenseTracker.ViewHandlers;

public partial class EntryHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntry), (h, v) =>
        {
            if (h.VirtualView is CustomEntry customEntry)
            {
                // Set Tint Color to transparent
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                h.PlatformView.TextCursorDrawable.SetTint(customEntry.CursorColor.ToAndroid());
            }
        });
    }
}
