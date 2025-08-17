using Foundation;
using UIKit;

namespace HuesarioApp;

public class SavePictureService
{
    public static bool SavePicture(byte[] arr)
    {
        var imageData = NSData.FromArray(arr);
        var image = UIImage.LoadFromData(imageData);
        var success = true;
        image?.SaveToPhotosAlbum((img, error) =>
        {
            Console.WriteLine($"Error saving image: {error.LocalizedDescription}");
            success = false;
        });
        return success;
    }
}