using CommunityToolkit.Maui;
using HouseExpenseTracker.Infrastructure.Data;
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
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesome");
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

            return app;
        }
    }
}
