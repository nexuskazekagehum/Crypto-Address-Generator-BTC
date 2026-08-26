using System.Text.Json.Serialization;

namespace CryptoAddressGeneratorBTC.Core.Events
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredAt { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        [JsonPropertyName("eventId")]
        public Guid EventId { get; } = Guid.NewGuid();

        [JsonPropertyName("occurredAt")]
        public DateTime OccurredAt { get; } = DateTime.UtcNow;
    }
}
