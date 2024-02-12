using HouseExpenseTracker.ViewControls;

namespace HouseExpenseTracker.ViewHandlers;

public partial class EditorHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(CustomEditor), (h, v) =>
        {
            if (h.VirtualView is CustomEditor customEntry)
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            }
        });
    }
}
