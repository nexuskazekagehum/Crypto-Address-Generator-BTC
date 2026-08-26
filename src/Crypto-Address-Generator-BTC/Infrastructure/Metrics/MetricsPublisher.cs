using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Metrics
{
    public interface IMetricsPublisher
    {
        Task IncrementAsync(string metricName, double value = 1, CancellationToken cancellationToken = default);
        Task RecordGaugeAsync(string metricName, double value, CancellationToken cancellationToken = default);
        Task RecordHistogramAsync(string metricName, double value, CancellationToken cancellationToken = default);
    }

    public class ConsoleMetricsPublisher : IMetricsPublisher
    {
        private readonly ILogger<ConsoleMetricsPublisher> _logger;

        public ConsoleMetricsPublisher(ILogger<ConsoleMetricsPublisher> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task IncrementAsync(string metricName, double value = 1, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[METRIC] counter {MetricName} += {Value}", metricName, value);
            return Task.CompletedTask;
        }

        public Task RecordGaugeAsync(string metricName, double value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[METRIC] gauge {MetricName} = {Value}", metricName, value);
            return Task.CompletedTask;
        }

        public Task RecordHistogramAsync(string metricName, double value, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[METRIC] histogram {MetricName} {Value}", metricName, value);
            return Task.CompletedTask;
        }
    }
}
