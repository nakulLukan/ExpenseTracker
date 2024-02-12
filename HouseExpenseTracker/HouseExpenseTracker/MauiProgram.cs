using CommunityToolkit.Maui;
using HouseExpenseTracker.Infrastructure.Data;
using HouseExpenseTracker.ViewHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace HouseExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.RegisterServices();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            Task.Run(async () =>
            {
                using var db = app.Services.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();
            });

            EntryHandler.UseHandler();
            EditorHandler.UseHandler();
            PickerHandler.UseHandler();
            DatePickerHandler.UseHandler();
            return app;
        }
    }
}
