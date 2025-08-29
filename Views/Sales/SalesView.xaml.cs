using HuesarioApp.ViewModels.Sales;

namespace HuesarioApp.Views.Sales;

public partial class SalesView : ContentPage
{
    public SalesView(SalesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}