namespace BusinessLogic.Interface;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
    void LogError(Exception ex, string message);
}