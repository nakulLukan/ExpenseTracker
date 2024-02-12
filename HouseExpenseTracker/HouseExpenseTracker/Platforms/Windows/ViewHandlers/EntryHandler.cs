using HouseExpenseTracker.ViewControls;

namespace HouseExpenseTracker.ViewHandlers;

public partial class EntryHandler
{
    static partial void Handle()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntry), (h, v) =>
        {
            if (h.VirtualView is CustomEntry customEntry)
            {
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            }
        });
    }
}
