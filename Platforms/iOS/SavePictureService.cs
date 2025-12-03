using Foundation;
using UIKit;

namespace HuesarioApp;

public class SavePictureService
{
    public static string? SavePicture(byte[] arr)
    {
        var imageData = NSData.FromArray(arr);
        var image = UIImage.LoadFromData(imageData);
        string? success = null;
        image?.SaveToPhotosAlbum((img, error) =>
        {
            Console.WriteLine($"Error saving image: {error.LocalizedDescription}");
            success = null;
        });
        return success;
    }
}