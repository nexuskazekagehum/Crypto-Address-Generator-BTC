using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Logging
{
    public interface IOperationLogger
    {
        IDisposable? BeginScope(string operationName);
        void LogStart(string operationName, object? input = null);
        void LogEnd(string operationName, TimeSpan duration, object? output = null);
        void LogError(string operationName, Exception exception, object? input = null);
    }

    public class DefaultOperationLogger : IOperationLogger
    {
        private readonly ILogger<DefaultOperationLogger> _logger;

        public DefaultOperationLogger(ILogger<DefaultOperationLogger> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IDisposable? BeginScope(string operationName) => _logger.BeginScope("{OperationName}: {operationName}");

        public void LogStart(string operationName, object? input = null)
        {
            _logger.LogInformation("Operation {OperationName} started", operationName);
        }

        public void LogEnd(string operationName, TimeSpan duration, object? output = null)
        {
            _logger.LogInformation("Operation {OperationName} completed in {DurationMs}ms", operationName, duration.TotalMilliseconds);
        }

        public void LogError(string operationName, Exception exception, object? input = null)
        {
            _logger.LogError(exception, "Operation {OperationName} failed", operationName);
        }
    }
}
