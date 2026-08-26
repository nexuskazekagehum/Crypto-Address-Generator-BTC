using CryptoAddressGeneratorBTC.Core.Builders;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void EntityBuilder_BuildsConfiguredEntity()
        {
            var entity = EntityBuilder.For<SampleEntity>()
                .With(e => e.Name = "test")
                .With(e => e.Value = 42)
                .Build();
            Assert.Equal("test", entity.Name);
            Assert.Equal(42, entity.Value);
        }

        [Fact]
        public void EntityBuilder_BuildMany_CreatesMany()
        {
            var items = EntityBuilder.For<SampleEntity>().BuildMany(5);
            Assert.Equal(5, items.Count());
        }

        public class SampleEntity
        {
            public string Name { get; set; } = string.Empty;
            public int Value { get; set; }
        }
    }
}
