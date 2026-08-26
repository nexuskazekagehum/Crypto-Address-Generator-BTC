using CryptoAddressGeneratorBTC.Core.Models;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public interface IRepository
    {
        Task SaveResultAsync(SimulationResult result, CancellationToken cancellationToken = default);
        Task<List<SimulationResult>> GetResultsAsync(CancellationToken cancellationToken = default);
    }
}
