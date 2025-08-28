using Android.Content;
using Android.OS;
using static Android.Content.Intent;
using static Android.Provider.MediaStore;
using static Android.Provider.MediaStore.IMediaColumns;

namespace HuesarioApp;

public static class SavePictureService
{
    public static bool SavePicture(byte[] arr, string imageName)
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
        {
            var contentValues = new ContentValues();
            contentValues.Put(DisplayName, imageName);
            contentValues.Put(Files.IFileColumns.MimeType, "image/png");
            contentValues.Put(RelativePath, "Pictures/Sales");
            try
            {
                if (Images.Media.ExternalContentUri != null)
                {
                    var uri = MainActivity.Instance?.ContentResolver?.Insert(Images.Media.ExternalContentUri,
                        contentValues);
                    if (uri != null)
                    {
                        var output = MainActivity.Instance?.ContentResolver?.OpenOutputStream(uri);
                        output?.Write(arr, 0, arr.Length);
                        output?.Flush();
                        output?.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }

            contentValues.Put(IsPending, 1);
        }
        else
        {
            var picturesDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures)?.AbsolutePath;
            if (picturesDir != null)
            {
                var targetDir = System.IO.Path.Combine(picturesDir, "Sales");
                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);

                var filePath = System.IO.Path.Combine(targetDir, imageName);
                System.IO.File.WriteAllBytes(filePath, arr);
                var mediaScanIntent = new Android.Content.Intent(ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));
                MainActivity.Instance?.SendBroadcast(mediaScanIntent);
            }
        }

        return true;
    }
}