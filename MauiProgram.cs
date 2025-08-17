using HuesarioApp.ViewModels.SalesView;
using Microsoft.Extensions.Logging;
using HuesarioApp.Interfaces;
using HuesarioApp.Services;

namespace HuesarioApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("AwesomeRegular.otf","Awesome");
                });

            builder.Services.AddTransient<SalesViewModel>();
            builder.Services.AddSingleton<ICameraServices, CameraService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
