

namespace HuesarioApp.Views.Sales;

public partial class SalesRecordView : ContentPage
{
    private readonly ViewModel vm;
    public SalesRecordView(ViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
    }
    
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await vm.CargarTrendMensualAsync();
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
}