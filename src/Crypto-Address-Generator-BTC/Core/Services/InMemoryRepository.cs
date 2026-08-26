using CryptoAddressGeneratorBTC.Core.Models;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<SimulationResult> _results = new();
        private readonly SemaphoreSlim _lock = new(1, 1);

        public async Task SaveResultAsync(SimulationResult result, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                _results.Add(result);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<List<SimulationResult>> GetResultsAsync(CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                return _results.ToList();
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
