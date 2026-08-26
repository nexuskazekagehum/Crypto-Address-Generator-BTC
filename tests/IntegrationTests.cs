using CryptoAddressGeneratorBTC.Infrastructure.Events;
using CryptoAddressGeneratorBTC.Infrastructure.Persistence;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public async Task EventBus_PublishAndRetrieve()
        {
            var bus = new InMemoryDomainEventBus(NullLogger<InMemoryDomainEventBus>.Instance);
            var evt = new SampleEvent { Payload = "hello" };
            await bus.PublishAsync(evt);
            var events = await bus.GetEventsAsync();
            Assert.Contains(events, e => e.EventId == evt.EventId);
        }

        [Fact]
        public async Task JsonRepository_ListSavedEntities()
        {
            var repo = new JsonRepository<SampleEntity>(NullLogger<JsonRepository<SampleEntity>>.Instance);
            await repo.SaveAsync(new SampleEntity { Id = "a", Value = 1 }, "a");
            await repo.SaveAsync(new SampleEntity { Id = "b", Value = 2 }, "b");
            var list = await repo.ListAsync();
            Assert.Equal(2, list.Count());
        }

        public class SampleEvent : Core.Events.DomainEvent
        {
            public string Payload { get; set; } = string.Empty;
        }

        public class SampleEntity
        {
            public string Id { get; set; } = string.Empty;
            public int Value { get; set; }
        }
    }
}
