using HouseExpenseTracker.ViewControls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace HouseExpenseTracker.ViewHandlers;

public partial class EditorHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(CustomEditor), (h, v) =>
        {
            if (h.VirtualView is CustomEditor customEntry)
            {
                // Set Tint Color to transparent
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());

                if(customEntry.CursorColor != default)
                {
                    h.PlatformView.TextCursorDrawable.SetTint(customEntry.CursorColor.ToAndroid());
                }
            }
        });
    }
}
