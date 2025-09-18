using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuesarioApp.ViewModels.Inventory;

namespace HuesarioApp.Views.Inventory.BranchView;

public partial class BranchInventoryView
{
    public BranchInventoryView(BranchInventoryVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}