using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Interfaces.DataServices;
using HuesarioApp.Models.Contracts.Bodys.Sales;
using HuesarioApp.Models.Contracts.Bodys.Ticket;
using HuesarioApp.Models.Entities;
using HuesarioApp.Models.Enums;
using HuesarioApp.Services.Messages;

namespace HuesarioApp.ViewModels.Seller;

public partial class SellerViewModel : ObservableObject
{
    /*
     * DEPENDENCIES
     */
    private readonly HttpClient _client;
    private readonly ILoggerService _logger;

    /*
     * PROPERTIES
     */
    [ObservableProperty] private Sessions _currentSession;

    [ObservableProperty] private TicketBody _ticketBody;

    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private ObservableCollection<SaleBody> _saleItems;
    [ObservableProperty] private SaleBody _selectedSale;

    public ObservableCollection<PaymentMethods> PaymentMethodsList { get; }
    [ObservableProperty] private PaymentMethods _selectedPaymentMethod;

    public SellerViewModel(HttpClient client,
        ILoggerService logger)
    {
        _client = client;
        _logger = logger;
        _selectedSale = new SaleBody();
        _saleItems = new ObservableCollection<SaleBody>();
        PaymentMethodsList = new ObservableCollection<PaymentMethods>(
            Enum.GetValues<PaymentMethods>().Cast<PaymentMethods>()
        );
        SelectedPaymentMethod = PaymentMethods.CASH;
        _ticketBody = new TicketBody();
        _currentSession = new Sessions();
        WeakReferenceMessenger.Default.Register<sessionMessage>(this, (r, m) => { CurrentSession = m.Value; });
        LoadToken();
    }

    private void RecalculateTotal()
    {
        TicketBody.total = (float)SaleItems.Sum(x => x.price * x.quantity);
    }

    partial void OnSelectedPaymentMethodChanged(PaymentMethods oldValue, PaymentMethods newValue)
    {
        TicketBody.paymentMethod = newValue.ToString();
    }

    private async void LoadToken()
    {
        try
        {
            var token = await SecureStorage.GetAsync("token");
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al cargar token: " + ex.Message, ex);
        }
    }

    /*
     * COMMANDS
     */

    [RelayCommand]
    private void AddItem()
    {
        if (string.IsNullOrWhiteSpace(SelectedSale?.partName))
        {
            Shell.Current.DisplayAlert("Error", "El nombre de la pieza no puede estar vacío", "OK");
            return;
        }

        if (SelectedSale.price <= 0)
        {
            Shell.Current.DisplayAlert("Error", "El precio debe ser mayor a 0", "OK");
            return;
        }

        if (SelectedSale.quantity <= 0)
        {
            Shell.Current.DisplayAlert("Error", "La cantidad debe ser mayor a 0", "OK");
            return;
        }

        if (SelectedSale.hasWarranty && SelectedSale.warrantyExpirationDate == null)
        {
            Shell.Current.DisplayAlert("Error", "Debes seleccionar la fecha de vencimiento de la garantía", "OK");
            return;
        }

        var newItem = new SaleBody
        {
            partId = SelectedSale.partId,
            partName = SelectedSale.partName,
            quantity = SelectedSale.quantity,
            price = SelectedSale.price,
            hasWarranty = SelectedSale.hasWarranty,
            warrantyExpirationDate = SelectedSale.warrantyExpirationDate
        };
        SaleItems.Add(newItem);
        TicketBody.items = SaleItems.ToList();
        RecalculateTotal();
    }


    [RelayCommand]
    private void RemoveItem()
    {
        TicketBody.items = SaleItems.ToList();
        RecalculateTotal();
    }

    [RelayCommand]
    private async Task CreateTicket()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var json = JsonSerializer.Serialize(TicketBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/tickets", content);
            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                _logger.LogWarning($"Error creando ticket: {msg}");

                await Shell.Current.DisplayAlert("Error",
                    "No se pudo crear el ticket",
                    "OK");
                return;
            }

            await Shell.Current.DisplayAlert("Éxito",
                "Ticket creado correctamente",
                "OK");

            SaleItems.Clear();
            TicketBody = new TicketBody();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            await Shell.Current.DisplayAlert("Error",
                "Error inesperado al crear el ticket",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}