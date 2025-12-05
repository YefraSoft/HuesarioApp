using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Enums;
using HuesarioApp.Models.Contracts.Bodys.Auth;
using HuesarioApp.Models.Contracts.Responses.Auth;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.Messages;

namespace HuesarioApp.ViewModels.Auth;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private RegisterBody _registerbody;

    [ObservableProperty] private KeyValuePair<int, string> _seletedRole;
    [ObservableProperty] private ObservableCollection<KeyValuePair<int, string>> _rolesList;
    [ObservableProperty] private ObservableCollection<string> _avatarsList;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string _selectedAvatar;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private bool _isBusy;

    public RegisterViewModel(HttpClient client, ILoggerService loggerService,
        IEntityValidator<RegisterBody> validator, IRepository<Sessions, int> repo)
    {
        _client = client;
        _loggerService = loggerService;
        _registerbody = new RegisterBody();
        _validator = validator;
        _repo = repo;
        _selectedAvatar = string.Empty;
        RolesList = new ObservableCollection<KeyValuePair<int, string>>(
            Enum.GetValues<RolId>()
                .Select(r => new KeyValuePair<int, string>((int)r, r.ToString()))
        );
        _avatarsList = new ObservableCollection<string>
        {
            "chevrolet.png",
            "crysler.png",
            "dodge.png",
            "honda.png",
            "nissan.png",
            "volkswagen.png"
        };
    }
    /*
     * DEPENDENCIES
     */

    private readonly HttpClient _client;
    private readonly ILoggerService _loggerService;
    private readonly IEntityValidator<RegisterBody> _validator;
    private readonly IRepository<Sessions, int> _repo;

    partial void OnSeletedRoleChanged(KeyValuePair<int, string> oldValue, KeyValuePair<int, string> newValue)
    {
        Registerbody.roleId = newValue.Key;
    }

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task Register()
    {
        IsBusy = true;
        var register = new RegisterBody
        {
            name = Registerbody.name,
            username = Registerbody.username,
            password = Registerbody.password,
            roleId = Registerbody.roleId
        };
        try
        {
            var objet = JsonSerializer.Serialize(Registerbody);
            var content = new StringContent(objet, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/auth/register", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error {response.StatusCode}: {response.ReasonPhrase}");
            }

            var credentials = JsonSerializer.Deserialize<RegisterResponse>(await response.Content.ReadAsStringAsync());
            if (credentials is null)
            {
                await Shell.Current.DisplayAlert("Error",
                    "Error de registro",
                    "Ok");
                return;
            }

            var newSession = new Sessions
            {
                Username = register.username,
                Name = register.name,
                RoleId = credentials.id,
                RoleName = credentials.role,
                Image = SelectedAvatar
            };
            if (!(await _repo.Create(newSession) > 0))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Error de en sessiones",
                    "Ok");
                return;
            }

            Registerbody = new();
            WeakReferenceMessenger.Default.Send(new sessionMessage(newSession));
            await Shell.Current.GoToAsync("//Login");
        }
        catch (Exception e)
        {
            _loggerService.LogWarning(e.Message);
            await Shell.Current.DisplayAlert("Error",
                "Error de en de regsitro",
                "Ok");
        }
        finally
        {
            IsBusy = false;
            RegisterCommand.NotifyCanExecuteChanged();
        }
    }

    private bool CanRegister()
    {
        return _validator.IsValid(Registerbody) &&
               !string.IsNullOrEmpty(SelectedAvatar) &&
               !IsBusy;
    }
}