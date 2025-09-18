
using HuesarioApp.ViewModels.Inventory;

namespace HuesarioApp.Views.Inventory.ModelsView;

public partial class ModelsInventoryView
{
    public ModelsInventoryView(ModelsInventoryVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}