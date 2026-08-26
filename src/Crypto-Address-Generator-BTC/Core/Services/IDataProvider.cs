using CryptoAddressGeneratorBTC.Core.Models;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public interface IDataProvider
    {
        Task<SimulationResult> FetchAsync(string symbol, CancellationToken cancellationToken = default);
    }
}
