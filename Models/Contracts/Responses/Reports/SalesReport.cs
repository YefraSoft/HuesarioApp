namespace HuesarioApp.Models.Contracts.Responses.Reports;

public class SalesReport
{
    public string Periodo { get; set; }
    public double TotalVendido { get; set; }
    public int CantidadDeVentas { get; set; }
}