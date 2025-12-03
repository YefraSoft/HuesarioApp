using HuesarioApp.Interfaces.AppServices;

namespace HuesarioApp.Services.AppServices;

public class CameraService : ICameraServices
{
    private readonly ILoggerService _logger;

    public CameraService(ILoggerService loggerService)
    {
        _logger = loggerService;
    }

    public async Task<Stream> TakePhoto()
    {
        try
        {
            if (!MediaPicker.Default.IsCaptureSupported)
                throw new NotSupportedException("No capture available");
            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                await Permissions.RequestAsync<Permissions.Camera>();
            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo is not null) return await photo.OpenReadAsync();
            _logger.LogInfo("Device: " + DeviceInfo.Current);
            throw new Exception("No photo available");
        }
        catch (Exception e)
        {
            _logger.LogError("Error in take photo process", e);
            throw;
        }
    }

    public Task<string?> SavePhoto(byte[] photoBytes)
    {
        try
        {
#if ANDROID
            var fileName = $"SaleDay_{DateTime.Now:yyyyMMdd_HHmmss_fff}_{Guid.NewGuid().ToString()[..8]}.png";
            var path = SavePictureService.SavePicture(photoBytes, fileName);
            return Task.FromResult(path);
#elif IOS
            var path = SavePictureService.SavePicture(photoBytes);
            return Task.FromResult(path);
#elif WINDOWS
            var fileName = $"SaleDay_{DateTime.Now:yyyyMMdd_HHmmss_fff}_{Guid.NewGuid().ToString()[..8]}.png";
            var path = SavePictureService.SavePicture(photoBytes, fileName);
            return Task.FromResult(path);
#else
            return Task.FromResult<string?>(null);
#endif
        }
        catch (Exception e)
        {
            _logger.LogError("Error saving photo", e);
            return Task.FromResult<string?>(null);
        }
    }

    public async Task<ImageSource?> TakeAndSavePhoto()
    {
        try
        {
            await using var photoStream = await TakePhoto();
            using var memoryStream = new MemoryStream();
            await photoStream.CopyToAsync(memoryStream);
            var photoBytes = memoryStream.ToArray();
            await SavePhoto(photoBytes);
            return ImageSource.FromStream(() => new MemoryStream(photoBytes));
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            throw;
        }
    }
}