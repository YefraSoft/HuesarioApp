namespace HuesarioApp.Models.Contracts.Responses.Warranty;

public class WarrantyResponse
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime ExpirationDate { get; set; }
}