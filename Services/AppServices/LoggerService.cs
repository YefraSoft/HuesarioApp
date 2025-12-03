using HuesarioApp.Interfaces.AppServices;
using Microsoft.Extensions.Logging;

namespace HuesarioApp.Services.AppServices;

public class LoggerService(ILogger<LoggerService> logger) : ILoggerService
{
    private readonly ILogger _logger = logger;

    public void LogError(string message, Exception ex)
    {
        _logger.LogError(ex, message);
        System.Diagnostics.Debug.WriteLine(
            $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message} | Exception: {ex.Message}");
    }

    public void LogInfo(string message)
    {
        _logger.LogInformation(message);
        System.Diagnostics.Debug.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
        System.Diagnostics.Debug.WriteLine($"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }
}