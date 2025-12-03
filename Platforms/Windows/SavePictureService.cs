namespace HuesarioApp;

public class SavePictureService
{
    public static string? SavePicture(byte[] arr, string imageName = "")
    {
        try
        {
            var picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var appFolderPath = Path.Combine(picturesPath, "HuesarioApp");

            if (!Directory.Exists(appFolderPath))
                Directory.CreateDirectory(appFolderPath);

            var fullPath = Path.Combine(appFolderPath, imageName);
            File.WriteAllBytes(fullPath, arr);

            return fullPath;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}