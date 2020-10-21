using NLog;
using System;

namespace LoggerLibrary
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogWarn(string message)
        {
            var dt = DateTime.Now;
            logger.Warn(message);
        }
    }
    public readonly struct LoggerMessage
    {
        public static string ClientObjectIsNull(object controller, object action) =>
            $"Object sent from client is null. Controller: {controller}, action: { action}";

        public static string ClientModelStateIsInvalid(object controller, object action) =>
            $"Invalid model state for the object from client. Controller:{ controller}, action: { action}";

        public static string DbTableItemDoesNotExist(string itemTableName, Guid itemId) =>
            $"Item with id: {itemId} does not exist in {itemTableName} table.";

        public static string ParameterIsNull(string parameterName) => $"{parameterName} is null.";
        public static string AlreadyExists(string target) => $"{target} already exists in the database.";
        public static string InvalidModelState(string target) => $"Invalid model state for the {target}.";
        public static string DoesNotExist(string target, Guid itemId) => $"{target} with id: {itemId} doesn't exist in the database.";
    }
}
