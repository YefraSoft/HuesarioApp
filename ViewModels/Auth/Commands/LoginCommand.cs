using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;
using HuesarioApp.Models.Contracts.Responses.Auth;

namespace HuesarioApp.ViewModels.Auth.Commands;

public class LoginCommand(ILoggerService logger) : ICommand
{
    private readonly HttpClient _client = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:8080")
    };

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (parameter is not LoginBody credentials)
            return;
        try
        {
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/auth/login", content);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {msg}");
            }

            var tokenRol = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync());
            if (tokenRol != null && !(tokenRol.token.Length > 0)) return;
            await SecureStorage.SetAsync("token", tokenRol.token);
            await SecureStorage.SetAsync("role", tokenRol.role);
            await Shell.Current.GoToAsync("//SellerWindow");
        }
        catch (Exception e)
        {
            logger.LogInfo(e.Message);
            throw;
        }
    }

    public event EventHandler? CanExecuteChanged;
}