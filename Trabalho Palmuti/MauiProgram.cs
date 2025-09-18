using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Trabalho_Palmuti
{
    public static class AppMetrics
    {
        public static readonly Stopwatch ColdStartStopwatch = new();
    }
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            AppMetrics.ColdStartStopwatch.Start();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
