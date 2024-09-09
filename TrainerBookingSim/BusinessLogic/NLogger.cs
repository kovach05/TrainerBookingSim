using NLog;
using ILogger = BusinessLogic.Interface.ILogger;

namespace BusinessLogic;

public class NLogger : ILogger
{
    private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

    public void LogInfo(string message)
    {
        logger.Info(message);
    }

    public void LogError(string message)
    {
        logger.Error(message);
    }

    public void LogError(Exception ex, string message)
    {
        logger.Error(ex, message);
    }
}