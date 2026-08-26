using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Background
{
    public class DomainHostedService : IHostedService
    {
        private readonly ILogger<DomainHostedService> _logger;
        private Timer? _timer;

        public DomainHostedService(ILogger<DomainHostedService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain background service started");
            _timer = new Timer(OnTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        private void OnTick(object? state)
        {
            _logger.LogInformation("Background tick: {DateTime.UtcNow}", DateTime.UtcNow);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain background service stopped");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
