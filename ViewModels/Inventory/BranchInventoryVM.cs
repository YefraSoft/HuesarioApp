using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Entities;
using HuesarioApp.Services.Messages;

namespace HuesarioApp.ViewModels.Inventory;

public class BranchInventoryVm : INotifyPropertyChanged
{
    public BranchInventoryVm(IRepository<Brands, int> repo, IEntityValidator<Brands> validator, ILoggerService logger)
    {
        _repo = repo;
        _validator = validator;
        var logger1 = logger;
        _ = LoadData();
        SaveCommand = new Command(async void () =>
        {
            try
            {
                await Save();
            }
            catch (Exception ex)
            {
                logger1.LogError("Error saving brand", ex);
            }
        });
        
    }

    /*
     * DEPENDENCIES
     */

    private readonly IRepository<Brands, int> _repo;
    private readonly IEntityValidator<Brands> _validator;

    /*
     * DATA BINDING
     */

    private Brands _brand = new();

    public Brands Brand
    {
        get => _brand;
        set => SetField(ref _brand, value);
    }

    private ObservableCollection<Brands> _brandList = [];

    public ObservableCollection<Brands> BrandList
    {
        get => _brandList;
        set => SetField(ref _brandList, value);
    }

    /*
     * ACTIONS, COMMANDS AND TASKS
     */

    public ICommand SaveCommand { get; }

    private async Task LoadData()
    {
        var brands = await _repo.GetAll();
        BrandList = new ObservableCollection<Brands>(brands);
    }

    private async Task Save()
    {
        if (!_validator.IsValid(Brand))
        {
            await Shell.Current.DisplayAlert("Error", "Please Verify Fields", "OK");
        }

        var newBrand = new Brands
        {
            Name = Brand.Name
        };

        if (!(await _repo.Create(newBrand) > 0))
            return;
        BrandList.Add(newBrand);
        _ = WeakReferenceMessenger.Default.Send(new ChangeInBrandMessage(BrandList));
        Brand = new();
    }

    /*
     *  INOTIFY PROPERTY CHANGED METHODS
     */

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}