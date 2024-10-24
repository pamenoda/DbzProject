using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbzProject.ViewModel;

namespace DbzProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<MainMenuPage>();
            builder.Services.AddTransient<MainMenuViewModel>();

            builder.Services.AddTransient<FormCharacterPage>();
            builder.Services.AddTransient<FormCharacterViewModel>();

            builder.Services.AddTransient<ShowCharacterPage>();
            builder.Services.AddTransient<ShowCharacterViewModel>();

            builder.Services.AddTransient<ChartCharactersPage>();
            builder.Services.AddTransient<ChartCharactersViewModel>();

            builder.Services.AddTransient<CharactersCSVExportPage>();
            builder.Services.AddTransient<CharactersCSVViewModel>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

            builder.Services.AddTransient<JSONServices>();

            builder.Services.AddDbContext<DataAccessService>(e => e.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0"));

#endif

            return builder.Build();
        }
    }
}
