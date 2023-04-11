using AventStack.ExtentReports;
using Microsoft.Extensions.Logging;

namespace BNZ.TestAutomation.Sample.Model
{
    public static class LoggerExtensions
    {
        public static void LogInformation(this ILogger logger, ExtentTest test, string? message, MediaEntityModelProvider? provider = null)
        {
            test.Info(message, provider);
            logger.Log(LogLevel.Information, message); ;
        }

        public static void LogPass(this ILogger logger, ExtentTest test, string? message, MediaEntityModelProvider? provider = null)
        {
            test.Pass(message, provider);
            logger.Log(LogLevel.Information, message); ;
        }

        public static void LogFail(this ILogger logger, ExtentTest test, string? message, MediaEntityModelProvider? provider = null)
        {
            test.Fail(message, provider);
            logger.Log(LogLevel.Error, message); ;
        }

        public static void LogFail(this ILogger logger, ExtentTest test, Exception? exception, string? message, MediaEntityModelProvider? provider = null)
        {
            test.Fail(exception, provider);
            logger.Log(LogLevel.Error, exception, message); ;
        }

        public static void LogError(this ILogger logger, ExtentTest test, Exception? exception, string? message, MediaEntityModelProvider? provider = null)
        {
            test.Error(exception, provider);
            logger.Log(LogLevel.Error, exception, message); ;
        }
    }
}
