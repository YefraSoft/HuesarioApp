namespace HuesarioApp.Interfaces.AppServices;

public interface ICameraServices
{
    public Task<Stream> TakePhoto();
    public Task<bool> SavePhoto(byte[] imageStream);
    public Task<ImageSource?> TakeAndSavePhoto();
}