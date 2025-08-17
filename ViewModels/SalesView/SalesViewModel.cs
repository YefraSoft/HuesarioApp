using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HuesarioApp.ViewModels.SalesView
{
    public class SalesViewModel : INotifyPropertyChanged
    {

        private ImageSource? _image;
        public ICommand TakePictureCom { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public ImageSource? Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        public SalesViewModel()
        {
            TakePictureCom = new Command(
                execute: async () =>
                {
                    try
                    {
                        if (MediaPicker.Default.IsCaptureSupported)
                        {
                            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                            if (status != PermissionStatus.Granted)
                            {
                                status = await Permissions.RequestAsync<Permissions.Camera>();
                            }

                            FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
                            if (photo != null)
                            {
                                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                                var sourceStream = await photo.OpenReadAsync();
                                Image = ImageSource.FromStream(() => sourceStream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al tomar foto: {ex.Message}");
                    }
                }
                );
        }

        protected void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
