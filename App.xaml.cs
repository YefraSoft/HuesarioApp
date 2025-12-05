using System.Text;
using System.Text.Json;
using HuesarioApp.Models.Enums;
using Microsoft.Extensions.Logging;

namespace HuesarioApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                var rol = await SecureStorage.GetAsync("rol");
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(rol) || IsTokenExpired(token))
                {
                    SecureStorage.Remove("token");
                    SecureStorage.Remove("rol");
                    await Shell.Current.GoToAsync("//Login");
                    return;
                }
                switch (rol)
                {
                    case nameof(RoleType.ADMIN):
                        await Shell.Current.GoToAsync("//AdminPanel");
                        break;

                    case nameof(RoleType.SELLER):
                        await Shell.Current.GoToAsync("//SellerPanel");
                        break;

                    case nameof(RoleType.INVENTORY_MANAGER):
                        await Shell.Current.GoToAsync("//InventoryManagerPanel");
                        break;

                    default:
                        SecureStorage.Remove("token");
                        SecureStorage.Remove("rol");
                        await Shell.Current.GoToAsync("//Login");
                        break;
                }
            }
            catch (Exception)
            {
                await Shell.Current.GoToAsync("//Login");
            }
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                    return true;

                var payload = parts[1];
                payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

                var payloadBytes = Convert.FromBase64String(payload);
                var jsonPayload = Encoding.UTF8.GetString(payloadBytes);

                var payloadData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonPayload);

                if (payloadData == null || !payloadData.TryGetValue("exp", out var expElement))
                    return true;

                var exp = expElement.GetInt64();
                var expDate = DateTimeOffset.FromUnixTimeSeconds(exp);

                return expDate <= DateTimeOffset.UtcNow;
            }
            catch
            {
                return true;
            }
        }
    }
}