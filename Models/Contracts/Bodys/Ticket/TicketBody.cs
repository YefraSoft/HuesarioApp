using HuesarioApp.Models.Contracts.Bodys.Sales;

namespace HuesarioApp.Models.Contracts.Bodys.Ticket;

public class TicketBody
{
    public int sellerId { get; set; }
    public string paymentMethod { get; set; }
    public float total { get; set; }
    public List<SaleBody> items { get; set; }
}