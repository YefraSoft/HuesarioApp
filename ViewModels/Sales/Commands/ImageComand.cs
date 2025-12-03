using System.Windows.Input;
using HuesarioApp.Interfaces.AppServices;

namespace HuesarioApp.ViewModels.Sales.Commands;

public class TakePictureCommand(ICameraServices cameraServices, ILoggerService logger, Action<ImageSource?> execute)
    : ICommand
{
    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        try
        {
            await using var stream = await cameraServices.TakePhoto();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var photoBytes = memoryStream.ToArray();
            var photo = ImageSource.FromStream(() => new MemoryStream(photoBytes));
            if (photo != null) execute?.Invoke(photo);
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to take photo", ex);
            await Shell.Current.DisplayAlert("Error", $"Failed to take photo: {ex.Message}", "OK");
        }
    }

    public event EventHandler? CanExecuteChanged
    {
        add { }
        remove { }
    }
}