using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public class SimulatedDataProvider : IDataProvider
    {
        private readonly ILogger<SimulatedDataProvider> _logger;
        private readonly Random _random = new();

        public SimulatedDataProvider(ILogger<SimulatedDataProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<SimulationResult> FetchAsync(string symbol, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching simulated data for {Symbol}", symbol);
            return Task.FromResult(new SimulationResult
            {
                Symbol = symbol,
                Currency = "USD",
                Value = Math.Round(_random.NextDouble() * 100000, 2)
            });
        }
    }
}
