using HuesarioApp.Models.Contracts.Responses.Sales;
using HuesarioApp.Models.Contracts.Responses.Warranty;

namespace HuesarioApp.Models.Contracts.Responses.Ticket;

public class TicketResponse
{
    public string Folio { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public double Total { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int Items { get; set; }
    public DateTime Date { get; set; }
    public List<SaleResponse> Sales { get; set; } = [];
    public List<WarrantyResponse> Warranties { get; set; } = [];
}