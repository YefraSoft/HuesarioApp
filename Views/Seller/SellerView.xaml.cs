using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HuesarioApp.ViewModels.Seller;

namespace HuesarioApp.Views.Seller;

public partial class SellerView : ContentPage
{
    public SellerView(SellerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}