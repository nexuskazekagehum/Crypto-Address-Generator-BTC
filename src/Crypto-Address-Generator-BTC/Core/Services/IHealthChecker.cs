namespace CryptoAddressGeneratorBTC.Core.Services
{
    public interface IHealthChecker
    {
        Task<bool> CheckAsync(CancellationToken cancellationToken = default);
    }
}
