using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;
using HuesarioApp.Models.DataSources;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.AppServices;
using HuesarioApp.Services.DataServices;
using HuesarioApp.Services.Validators;
using HuesarioApp.ViewModels.Inventory;
using HuesarioApp.ViewModels.Auth;
using HuesarioApp.ViewModels.Sales;
using HuesarioApp.ViewModels.Seller;

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
                    fonts.AddFont("AwesomeRegular.otf", "Awesome");
                });

            Task.Run(async () => await new LocalDbConfig().MakeTables()).Wait();
            // ViewModels
            builder.Services.AddTransient<SalesViewModel>();
            builder.Services.AddTransient<ModelsInventoryVm>();
            builder.Services.AddTransient<BranchInventoryVm>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<SellerViewModel>();
            builder.Services.AddTransient<IRepository<VehicleModels, int>, LocalRepository<VehicleModels, int>>();
            builder.Services.AddTransient<IRepository<Brands, int>, LocalRepository<Brands, int>>();
            builder.Services.AddTransient<IRepository<Sessions, int>, LocalRepository<Sessions, int>>();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://api.yefrasoft.com"),
                DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
            });
            builder.Services.AddSingleton<ICameraServices, CameraService>();
            builder.Services.AddSingleton<ILoggerService, LoggerService>();
            builder.Services.AddSingleton<IValidator, PrimitivesValidator>();
            builder.Services.AddSingleton<IEntityValidator<Brands>, BrandsValidator>();
            builder.Services.AddSingleton<IEntityValidator<VehicleModels>, ModelsValidator>();
            builder.Services.AddSingleton<IEntityValidator<Parts>, PartsValidator>();
            builder.Services.AddSingleton<IEntityValidator<SalesEntity>, SalesValidator>();
            builder.Services.AddSingleton<IEntityValidator<LoginBody>, CredentialsValidator>();
            builder.Services.AddSingleton<IEntityValidator<RegisterBody>, RegisterValidator>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}