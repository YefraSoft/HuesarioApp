using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Auth;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.Messages;
using HuesarioApp.ViewModels.Auth.Commands;

namespace HuesarioApp.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    /*
     * DEPENDENCIES
     */
    private readonly LoginCommand _loginCommand;
    private readonly IEntityValidator<LoginBody> _validator;
    private readonly IRepository<Sessions, int> _sessionsRepository;

    public LoginViewModel(ILoggerService loggerService,
        IEntityValidator<LoginBody> validator,
        IRepository<Sessions, int> sessionsRepository)
    {
        _validator = validator;
        _username = "";
        _password = "";
        _selectedSession = new Sessions();
        _sessionsRepository = sessionsRepository;
        _loginCommand = new LoginCommand(loggerService);

        SessionsList = new ObservableCollection<Sessions>();
        LoadSessions();
        WeakReferenceMessenger.Default.Register<sessionMessage>(this, (r, m) => { SessionsList.Add(m.Value); });
    }

    [ObservableProperty] private string _username;
    [ObservableProperty] private string _password;
    [ObservableProperty] private ObservableCollection<Sessions> _sessionsList;
    [ObservableProperty] private Sessions _selectedSession;

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
            await Shell.Current.DisplayAlert("Error",
                "Error en precarga",
                "Ok");
        }
    }

    public ICommand LoginCommand => new RelayCommand(() =>
    {
        var body = new LoginBody
        {
            username = this.Username,
            password = this.Password
        };
        if (!_validator.IsValid(body))
        {
            Shell.Current.DisplayAlert("Error",
                "Credenciales Incorrectas",
                "Ok");
            return;
        }

        _loginCommand.Execute(body);
        WeakReferenceMessenger.Default.Send(new sessionMessage(SelectedSession));
    });
}