using HuesarioApp.ViewModels.SalesView;
using Microsoft.Extensions.Logging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.DataSources;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.AppServices;
using HuesarioApp.Services.Validators;

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
            builder.Services.AddSingleton<IValidator, PrimitivesValidator>();
            builder.Services.AddSingleton<IEntityValidator<Brands>, BrandsValidator>();
            builder.Services.AddSingleton<IEntityValidator<VehicleModels>, ModelsValidator>();
            builder.Services.AddSingleton<IEntityValidator<Parts>, PartsValidator>();
            builder.Services.AddSingleton<IEntityValidator<Sales>, SalesValidator>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
