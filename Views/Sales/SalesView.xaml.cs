using HuesarioApp.ViewModels.SalesView;

namespace HuesarioApp.Views.Sales;

public partial class SalesView : ContentPage
{
    public SalesView(SalesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}