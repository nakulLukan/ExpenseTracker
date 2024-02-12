using HouseExpenseTracker.ViewControls;

namespace HouseExpenseTracker.ViewHandlers;

public partial class PickerHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(CustomPicker), (h, v) =>
        {
            if (h.VirtualView is CustomPicker customPicker)
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            }
        });
    }
}
