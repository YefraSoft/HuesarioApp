using HuesarioApp.ViewModels.Inventory;

namespace HuesarioApp.Views.Inventory;

public partial class ModelsInventory : ContentPage
{
    public ModelsInventory(ModelsInventoryVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}