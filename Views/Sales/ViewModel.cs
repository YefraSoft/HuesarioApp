using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HuesarioApp.Views.Sales;

public partial class ViewModel(HttpClient client) : ObservableObject
{
    public class ReporteVentas
    {
        public string Periodo { get; set; }
        public double TotalVendido { get; set; }
        public int CantidadDeVentas { get; set; }
    }

    // ---- Propiedades LiveCharts ----
    [ObservableProperty] private ISeries[] series;

    [ObservableProperty] private Axis[] xAxes;

    public async Task CargarTrendMensualAsync()
    {
        try
        {
            // --- Obtener token del login ---
            var token = await SecureStorage.GetAsync("token");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("No hay token guardado");
                return;
            }

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // --- Llamar API ---
            var response = await client.GetAsync("/api/sales-trend/monthly");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var datos = JsonSerializer.Deserialize<List<ReporteVentas>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (datos is null) return;

            // ---- Convertir datos a LiveCharts ----
            var valores = datos.Select(x => x.TotalVendido).ToArray();
            var labels = datos.Select(x => x.Periodo).ToArray();

            Series =
            [
                new ColumnSeries<double>
                {
                    Values = valores,
                    Fill = new SolidColorPaint(SKColors.DeepSkyBlue),
                    Stroke = null
                }
            ];

            XAxes =
            [
                new Axis
                {
                    Labels = labels,
                    LabelsRotation = 15
                }
            ];
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error cargando gráfico: " + ex.Message);
        }
    }
}