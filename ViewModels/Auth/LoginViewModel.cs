using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;
using HuesarioApp.Models.Contracts.Responses.Auth;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.Messages;
using HuesarioApp.ViewModels.Auth.Commands;

namespace HuesarioApp.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    /*
     * DEPENDENCIES
     */
    private readonly IEntityValidator<LoginBody> _validator;
    private readonly IRepository<Sessions, int> _sessionsRepository;
    private readonly HttpClient _client;
    private readonly ILoggerService _logger;

    public LoginViewModel(ILoggerService loggerService,
        IEntityValidator<LoginBody> validator, HttpClient client,
        IRepository<Sessions, int> sessionsRepository)
    {
        _validator = validator;
        _username = string.Empty;
        _password = string.Empty;
        _logger = loggerService;
        _client = client;
        _selectedSession = new Sessions();
        _sessionsRepository = sessionsRepository;
        SessionsList = [];
        LoadSessions();
        WeakReferenceMessenger.Default.Register<sessionMessage>(this, (r, m) => { SessionsList.Add(m.Value); });
    }

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(LoggingCommand))]
    private string _username;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(LoggingCommand))]
    private string _password;

    [ObservableProperty] private ObservableCollection<Sessions> _sessionsList;
    [ObservableProperty] private Sessions _selectedSession;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(LoggingCommand))]
    private bool _isBusy;

    partial void OnSelectedSessionChanged(Sessions oldValue, Sessions newValue)
    {
        Username = newValue.Username;
    }

    private async void LoadSessions()
    {
        try
        {
            var sessions = await _sessionsRepository.GetAll();
            SessionsList = new ObservableCollection<Sessions>(sessions);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Error loading sessions: {e.Message}");
            await Shell.Current.DisplayAlert("Aviso",
                "No se pudieron cargar las sesiones guardadas, continue iniciando sesión manualmente.",
                "Ok");
        }
    }

    [RelayCommand(CanExecute = nameof(CanLogging))]
    private async Task Logging()
    {
        IsBusy = true;
        var body = new LoginBody
        {
            username = this.Username,
            password = this.Password
        };
        if (!_validator.IsValid(body))
        {
            await Shell.Current.DisplayAlert("Información",
                "Por favor ingrese usuario y contraseña válidos",
                "Ok");
            return;
        }

        try
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/auth/login", content);
            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Login failed: {response.StatusCode} - {errorMsg}");
                await Shell.Current.DisplayAlert("Información",
                    "Error al iniciar sesión. Intente nuevamente",
                    "Ok");
                return;
            }

            var tokenRol = JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync());
            if (tokenRol == null || string.IsNullOrWhiteSpace(tokenRol.token))
            {
                _logger.LogWarning("Login response was invalid");
                await Shell.Current.DisplayAlert("Información",
                    "Error al iniciar sesión. Intente nuevamente",
                    "Ok");
                return;
            }

            await SecureStorage.SetAsync("token", tokenRol.token);
            await SecureStorage.SetAsync("role", tokenRol.role);
            await Shell.Current.GoToAsync("//SellerWindow");
        }
        catch (Exception e)
        {
            _logger.LogError($"Unexpected error during login : ", e);
            await Shell.Current.DisplayAlert("Información",
                "Error al iniciar sesión. Intente nuevamente",
                "Ok");
        }
        finally
        {
            IsBusy = false;
            LoggingCommand.NotifyCanExecuteChanged();
        }
    }

    private bool CanLogging()
    {
        return !string.IsNullOrEmpty(Username) &&
               !string.IsNullOrEmpty(Password) &&
               !IsBusy;
    }
}