using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Metrics
{
    public interface IMetricCollector
    {
        Task RecordAsync(MetricSnapshot snapshot, CancellationToken cancellationToken = default);
        Task<List<MetricSnapshot>> GetMetricsAsync(string name, CancellationToken cancellationToken = default);
    }

    public class InMemoryMetricCollector : IMetricCollector
    {
        private readonly List<MetricSnapshot> _snapshots = new();
        private readonly ILogger<InMemoryMetricCollector> _logger;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public InMemoryMetricCollector(ILogger<InMemoryMetricCollector> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task RecordAsync(MetricSnapshot snapshot, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { _snapshots.Add(snapshot); }
            finally { _lock.Release(); }
            _logger.LogInformation("[METRIC] {Name} = {Value} {Unit}", snapshot.Name, snapshot.Value, snapshot.Unit);
        }

        public async Task<List<MetricSnapshot>> GetMetricsAsync(string name, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _snapshots.Where(m => m.Name == name).ToList(); }
            finally { _lock.Release(); }
        }
    }
}
