namespace HuesarioApp.Interfaces.AppServices;

public interface ILoggerService
{
    void LogError(string message, Exception ex);
    void LogInfo(string message);
    void LogWarning(string message);
}