using CryptoAddressGeneratorBTC.Core.Models;
using CryptoAddressGeneratorBTC.Infrastructure.Queue;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class QueueTests
    {
        [Fact]
        public async Task JobQueue_EnqueueAndDequeue()
        {
            var queue = new InMemoryJobQueue(NullLogger<InMemoryJobQueue>.Instance);
            await queue.EnqueueAsync(new Job { Name = "test" });
            var job = await queue.DequeueAsync();
            Assert.NotNull(job);
            Assert.Equal("test", job.Name);
        }
    }
}
