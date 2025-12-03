using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;
using HuesarioApp.Services.Messages;

namespace HuesarioApp.ViewModels.Inventory;

public sealed class ModelsInventoryVm : INotifyPropertyChanged
{
    public ModelsInventoryVm(IRepository<VehicleModels, int> modelsRepo, IRepository<Brands, int> brandRepo,
        IEntityValidator<VehicleModels> validator, ILoggerService logger)
    {
        WeakReferenceMessenger.Default.Register<ChangeInBrandMessage>(this,
            (_, message) => { BrandList = message.Value; });
        _logger = logger;
        _modelsRepo = modelsRepo;
        _brandRepo = brandRepo;
        _validator = validator;
        _ = LoadData();
        SaveCommand = new Command(async void () =>
        {
            try
            {
                await Save();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving vehicle model", ex);
            }
        });
    }

    /*
     * DEPENDENCIES
     */

    private readonly IRepository<VehicleModels, int> _modelsRepo;
    private readonly IRepository<Brands, int> _brandRepo;
    private readonly IEntityValidator<VehicleModels> _validator;
    private readonly ILoggerService _logger;

    /*
     * DATA BINDING
     */

    public DateTime SelectedYear
    {
        get => Model.Year >= 1 ? new DateTime(Model.Year, 1, 1) : DateTime.Now;
        set
        {
            if (Model.Year == value.Year) return;
            Model.Year = value.Year;
            OnPropertyChanged();
        }
    }

    private Brands _brand = new();

    public Brands Brand
    {
        get => _brand;
        set
        {
            if (_brand == value) return;
            _brand = value;
            Model.BrandId = value?.Id ?? 100;
            OnPropertyChanged();
        }
    }


    private ObservableCollection<VehicleModels> _modelsList = [];

    public ObservableCollection<VehicleModels> ModelsList
    {
        get => _modelsList;
        set
        {
            if (_modelsList == value) return;
            _modelsList = value;
            OnPropertyChanged();
        }
    }

    private VehicleModels _model = new();

    public VehicleModels Model
    {
        get => _model;
        set
        {
            if (_model == value) return;
            _model = value;
            OnPropertyChanged();
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
            OnPropertyChanged();
        }
    }

    public List<TransmissionType> TransmissionList { get; } =
        Enum.GetValues<TransmissionType>().ToList();


    /*
     * Commands, Actions Tasks
     */
    public ICommand SaveCommand { get; }

    private async Task LoadData()
    {
        var vehicles = await _modelsRepo.GetAll();
        var brands = await _brandRepo.GetAll();
        BrandList = new ObservableCollection<Brands>(brands);
        ModelsList = new ObservableCollection<VehicleModels>(vehicles);
    }

    private async Task Save()
    {
        if (!_validator.IsValid(Model))
        {
            var message = $"Por favor verifica los siguientes campos:\n" +
                          $"- Nombre: {Model.Name ?? "(vacío)"}\n" +
                          $"- Año: {Model.Year}\n" +
                          $"- Motor: {Model.Engine ?? "(vacío)"}\n" +
                          $"- Marca ID: {Model.BrandId}\n" +
                          $"- Transmisión: {Model.Transmission}";

            await Shell.Current.DisplayAlert("Error", message, "OK");
            return;
        }

        var newModel = new VehicleModels
        {
            Name = Model.Name,
            Year = Model.Year,
            Engine = Model.Engine,
            BrandId = Model.BrandId,
            Transmission = Model.Transmission
        };

        await _modelsRepo.Create(newModel);

        ModelsList.Add(newModel);

        Model = new VehicleModels();
    }

    // INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}