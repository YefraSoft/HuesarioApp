using HuesarioApp.ViewModels.SalesView;

namespace HuesarioApp.Views;

public partial class SalesView : ContentPage
{
    public SalesView(SalesViewModel VM)
    {
        InitializeComponent();
        BindingContext = VM;
    }
}