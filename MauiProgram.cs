using HuesarioApp.ViewModels.SalesView;
using Microsoft.Extensions.Logging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Models.DataServices;
using HuesarioApp.ViewModels.AppServices;

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
            
            Task.Run(async () => await new LocalDbConfig().MakeTables()).Wait();
            builder.Services.AddTransient<SalesViewModel>();
            builder.Services.AddSingleton<ICameraServices, CameraService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
