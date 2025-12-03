namespace HuesarioApp.Models.Contracts.Responses.Sales;

public class SaleResponse
{
    public int Id { get; set; }
    public int? PartId { get; set; }
    public string? PartName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}