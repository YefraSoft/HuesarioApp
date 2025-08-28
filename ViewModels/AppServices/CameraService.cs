using HuesarioApp.Interfaces.AppServices;

namespace HuesarioApp.ViewModels.AppServices;

public class CameraService : ICameraServices
{
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
            if (photo == null)
                throw new Exception("No photo available");
            return await photo.OpenReadAsync();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            throw;
        }
    }

    public async Task<bool> SavePhoto(byte[] photoBytes)
    {
        try
        {
#if ANDROID
    return SavePictureService.SavePicture(photoBytes, $"SaleDay_{DateTime.Now:yyyyMMdd_HHmm}.png");
#elif IOS
            return SavePictureService.SavePicture(photoBytes);
#elif WINDOWS
            return SavePictureService.SavePicture(photoBytes,$"SaleDay_{DateTime.Now:yyyyMMdd_HHmm}.png");
#else
            return false;
#endif
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            throw;
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