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
        public SalesViewModel(ICameraServices cameraServices, ILoggerService logger)
        {
            _images = [];
            _logger = logger;
            _cameraServices = cameraServices;
            // _partsRepo = partsRepo;
            //_salesRepo = salesRepo;
            /*
            TakePictureCommand = new TakePictureCommand(cameraServices, (photo) =>
            {
                if (photo != null) Images.Add(photo);
            });
            SaveImagesCommand = new Command(async () => await SaveCapturedImages());
            SaveSaleCommand = new Command(async () => await SaveSale());
            SaveSaleWithImagesCommand = new Command(async () => await SaveSaleWithImages());
            _ = LoadData();*/
        }

        /*
         * DEPENDENCIES
         */
        private readonly ILoggerService _logger;
        private readonly ICameraServices _cameraServices;
        //private readonly IRepository<Parts, int> _partsRepo;

        //private readonly IRepository<SalesEntity, int> _salesRepo;
        /*
         * DATA BINDING
         */

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

        /*
         * ACTIONS, COMMANDS AND TASKS
         */
        public ICommand TakePictureCommand { get; }

        // CONTINUAR AQUI 
        private Task LoadData()
        {
            return null;
        }

        /*
         * INOTIFY PROPERTY CHANGED
         */
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}