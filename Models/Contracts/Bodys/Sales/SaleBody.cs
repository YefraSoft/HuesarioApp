namespace HuesarioApp.Models.Contracts.Bodys.Sales;

public class SaleBody()
{
    public int? partId { get; set; }
    public string partName { get; set; }
    public int quantity { get; set; }
    public int price { get; set; }
    public bool hasWarranty { get; set; }
    public DateTime? warrantyExpirationDate { get; set; }
}