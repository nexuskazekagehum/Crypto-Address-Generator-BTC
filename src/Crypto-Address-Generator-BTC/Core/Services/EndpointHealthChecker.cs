using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Core.Services
{
    public class EndpointHealthChecker : IHealthChecker
    {
        private readonly ILogger<EndpointHealthChecker> _logger;

        public EndpointHealthChecker(ILogger<EndpointHealthChecker> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<bool> CheckAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Checking endpoint health");
            return Task.FromResult(true);
        }
    }
}
