using System.Net.Http.Headers;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HuesarioApp.Interfaces.AppServices;
using HuesarioApp.Models.Contracts.Responses.Reports;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HuesarioApp.ViewModels.Dashboard;

public partial class DashboardViewModel(HttpClient client, ILoggerService logger) : ObservableObject
{
    [ObservableProperty] private ISeries[] _series = [];
    [ObservableProperty] private Axis[] _xAxes = [];
    [ObservableProperty] private bool _loading;
    [ObservableProperty] private string _title = string.Empty;

    private bool CanLoadData() => !Loading;

    [RelayCommand(CanExecute = nameof(CanLoadData))]
    private async Task LoadData()
    {
        try
        {
            Loading = true;
            var token = await SecureStorage.GetAsync("token");
            if (string.IsNullOrEmpty(token))
            {
                await Shell.Current.DisplayAlert("Error",
                    "No se pueden cargar los datos", "Ok");
                return;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("/api/sales-trend/monthly");
            if (!response.IsSuccessStatusCode) // CORREGIDO: era IsSuccessStatusCode sin negación
            {
                logger.LogWarning($"Request failed: {response.StatusCode} - {response.ReasonPhrase}");
                await Shell.Current.DisplayAlert("Información",
                    "No se obtuvo respuesta del servidor",
                    "Ok");
                return;
            }

            var data = JsonSerializer.Deserialize<List<SalesReport>>
            (await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (data is null || data.Count == 0)
            {
                await Shell.Current.DisplayAlert("Información",
                    "Sin datos que mostrar",
                    "Ok");
                return;
            }

            var values = data.Select(x => x.TotalVendido).ToArray();
            var labels = data.Select(x => x.Periodo).ToArray();

            Series =
            [
                new LineSeries<double>
                {
                    Name = "Ventas Mensuales",
                    Values = values,
                    Fill = null,
                    GeometrySize = 10,
                    Stroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 3 }
                }
            ];

            XAxes =
            [
                new Axis()
                {
                    Labels = labels,
                    LabelsRotation = 45
                }
            ];
        }
        catch (Exception ex)
        {
            logger.LogError("Error al cargar datos", ex);
            await Shell.Current.DisplayAlert("Error",
                "Ocurrió un error al cargar los datos",
                "Ok");
        }
        finally
        {
            Loading = false;
            LoadDataCommand.NotifyCanExecuteChanged();
        }
    }
}