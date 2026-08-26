using CryptoAddressGeneratorBTC.Core.Models;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public interface ICryptoModule
    {
        Task<SimulationResult> SimulateAsync(string symbol, CancellationToken cancellationToken = default);
        Task<Snapshot> GetLatestSnapshotAsync(CancellationToken cancellationToken = default);
        Task<List<Metric>> GetMetricsAsync(CancellationToken cancellationToken = default);
    }
}
