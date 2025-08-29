using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.ViewModels.Sales.Commands;

namespace HuesarioApp.ViewModels.Sales
{
    public class SalesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ImageSource?> _images;

        public ObservableCollection<ImageSource?> Images
        {
            get => _images;
            set
            {
                _images = value;
                OnPropertyChanged(nameof(_images));
            }
        }

        public ICommand TakePictureCommand { get; }

        public SalesViewModel(ICameraServices cameraServices)
        {
            _images = [];
            TakePictureCommand = new TakePictureCommand(cameraServices, (photo) =>
                {
                    if (photo != null) Images.Add(photo);
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