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
                });
            builder.Services.RegisterServices();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            Task.Run(() =>
            {

                var db = app.Services.GetService<AppDbContext>();
                db.Database.Migrate();
            });
            return app;
        }
    }
}
