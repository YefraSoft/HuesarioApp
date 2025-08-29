using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;

namespace HuesarioApp.ViewModels.Inventory;

public class ModelsInventoryVm : INotifyPropertyChanged
{
    private VehicleModels _model = new VehicleModels();

    public VehicleModels Model
    {
        get => _model;
        set
        {
            if (_model == value) return;
            _model = value;
            OnPropertyChanged(nameof(_model));
        }
    }

    private ObservableCollection<Brands> _brandList = [];

    public ObservableCollection<Brands> BrandList
    {
        get => _brandList;
        set
        {
            if (_brandList == value) return;
            _brandList = value;
            OnPropertyChanged(nameof(_brandList));
        }
    }

    public List<TransmissionType> TransmissionList { get; } =
        Enum.GetValues<TransmissionType>().Cast<TransmissionType>().ToList();
    
    public ICommand SaveCommand { get; }

    public ModelsInventoryVm()
    {
        SaveCommand = new Command((() => 
                Shell.Current.DisplayAlert(Model.Name + Model.Year + Model.Engine + Model.CreatedAt + Model.Transmission,"AllGood","OK")));
    }

    // INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}