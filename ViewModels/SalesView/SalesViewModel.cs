using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HuesarioApp.Interfaces;
using HuesarioApp.ViewModels.SalesView.Commands;

namespace HuesarioApp.ViewModels.SalesView
{
    public class SalesViewModel : INotifyPropertyChanged
    {

        private ImageSource? _image;
        public ICommand TakePictureCommand { get; }
        public ImageSource? Image
        {
            get => _image;
            set
            {
                if (_image == value) return;
                _image = value;
                OnPropertyChanged();
            }
        }
        public SalesViewModel(ICameraServices  cameraServices)
        {
            Image = ImageSource.FromFile("cam_ico.png");
            TakePictureCommand = new TakePictureCommand(cameraServices, (photo)=>
                {
                    Image = photo;
                }
                );
        }
        
        // INotifyPropertyChanged Logic
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
