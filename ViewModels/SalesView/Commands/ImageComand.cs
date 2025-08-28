using System.Windows.Input;
using HuesarioApp.Interfaces;
using HuesarioApp.Interfaces.AppServices;

namespace HuesarioApp.ViewModels.SalesView.Commands;

public class TakePictureCommand(ICameraServices cameraServices, Action<ImageSource?> execute)
    : ICommand
{
    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        try
        {
            var photo = await cameraServices.TakeAndSavePhoto();
            if (photo != null) execute?.Invoke(photo);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to take photo: {ex.Message}", "OK");
        }
    }

    public event EventHandler? CanExecuteChanged;
}