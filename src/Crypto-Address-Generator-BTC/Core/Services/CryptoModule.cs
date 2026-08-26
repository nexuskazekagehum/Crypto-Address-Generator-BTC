using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public class CryptoModule : ICryptoModule
    {
        private readonly IDataProvider _dataProvider;
        private readonly IRepository _repository;
        private readonly ILogger<CryptoModule> _logger;

        public CryptoModule(IDataProvider dataProvider, IRepository repository, ILogger<CryptoModule> logger)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SimulationResult> SimulateAsync(string symbol, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Running simulation for {Symbol}", symbol);
            var result = await _dataProvider.FetchAsync(symbol, cancellationToken);
            await _repository.SaveResultAsync(result, cancellationToken);
            return result;
        }

        public async Task<Snapshot> GetLatestSnapshotAsync(CancellationToken cancellationToken = default)
        {
            var results = await _repository.GetResultsAsync(cancellationToken);
            return new Snapshot { Results = results };
        }

        public async Task<List<Metric>> GetMetricsAsync(CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;
            throw new NotImplementedException("Metrics aggregation is not implemented in this demo");
        }
    }
}
