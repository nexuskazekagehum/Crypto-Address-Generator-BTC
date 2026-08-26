using CryptoAddressGeneratorBTC.Core.Models;
using CryptoAddressGeneratorBTC.Core.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class CryptoModuleTests
    {
        private readonly CryptoModule _module;

        public CryptoModuleTests()
        {
            var provider = new SimulatedDataProvider(NullLogger<SimulatedDataProvider>.Instance);
            var repository = new InMemoryRepository();
            _module = new CryptoModule(provider, repository, NullLogger<CryptoModule>.Instance);
        }

        [Fact]
        public async Task SimulateAsync_SavesResult()
        {
            var result = await _module.SimulateAsync("BTC");
            Assert.NotNull(result);
            Assert.Equal("BTC", result.Symbol);
            var snapshot = await _module.GetLatestSnapshotAsync();
            Assert.Single(snapshot.Results);
        }

        [Fact]
        public async Task GetMetricsAsync_ThrowsNotImplementedException()
        {
            await Assert.ThrowsAsync<NotImplementedException>(() => _module.GetMetricsAsync());
        }
    }
}
