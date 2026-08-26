using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Notifications
{
    public interface INotificationDispatcher
    {
        Task DispatchAsync(NotificationMessage message, CancellationToken cancellationToken = default);
        Task<List<NotificationMessage>> GetPendingAsync(CancellationToken cancellationToken = default);
    }

    public class ConsoleNotificationDispatcher : INotificationDispatcher
    {
        private readonly List<NotificationMessage> _pending = new();
        private readonly ILogger<ConsoleNotificationDispatcher> _logger;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public ConsoleNotificationDispatcher(ILogger<ConsoleNotificationDispatcher> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task DispatchAsync(NotificationMessage message, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { _pending.Add(message); }
            finally { _lock.Release(); }
            _logger.LogInformation("[NOTIFY] {Subject} to {Recipient}", message.Subject, message.Recipient);
        }

        public async Task<List<NotificationMessage>> GetPendingAsync(CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _pending.ToList(); }
            finally { _lock.Release(); }
        }
    }
}
