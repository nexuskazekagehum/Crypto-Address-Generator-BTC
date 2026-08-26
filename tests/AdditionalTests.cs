using CryptoAddressGeneratorBTC.Infrastructure.Persistence;
using CryptoAddressGeneratorBTC.Infrastructure.Validation;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class AdditionalTests
    {
        [Fact]
        public async Task JsonRepository_CanSaveAndLoad()
        {
            var repo = new JsonRepository<SampleEntity>(NullLogger<JsonRepository<SampleEntity>>.Instance);
            var entity = new SampleEntity { Id = "x", Value = 42 };
            await repo.SaveAsync(entity, "x");
            var loaded = await repo.LoadAsync("x");
            Assert.NotNull(loaded);
            Assert.Equal(42, loaded.Value);
        }

        [Fact]
        public async Task DefaultRequestValidator_AcceptsValidRequest()
        {
            var validator = new DefaultRequestValidator<SampleEntity>();
            var result = await validator.ValidateAsync(new SampleEntity { Id = "x", Value = 1 });
            Assert.True(result.IsValid);
        }

        public class SampleEntity
        {
            public string Id { get; set; } = string.Empty;
            public int Value { get; set; }
        }
    }
}
