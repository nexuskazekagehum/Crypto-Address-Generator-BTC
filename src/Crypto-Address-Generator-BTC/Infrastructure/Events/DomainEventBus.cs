using CryptoAddressGeneratorBTC.Core.Events;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Events
{
    public interface IDomainEventBus
    {
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IDomainEvent;
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(CancellationToken cancellationToken = default);
    }

    public class InMemoryDomainEventBus : IDomainEventBus
    {
        private readonly List<IDomainEvent> _events = new();
        private readonly ILogger<InMemoryDomainEventBus> _logger;

        public InMemoryDomainEventBus(ILogger<InMemoryDomainEventBus> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IDomainEvent
        {
            lock (_events) { _events.Add(@event); }
            _logger.LogInformation("Published event {EventType} {EventId}", typeof(T).Name, @event.EventId);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<IDomainEvent>> GetEventsAsync(CancellationToken cancellationToken = default)
        {
            lock (_events) { return Task.FromResult<IEnumerable<IDomainEvent>>(_events.ToList()); }
        }
    }
}
