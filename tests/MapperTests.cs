using CryptoAddressGeneratorBTC.Core.Transformers;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class MapperTests
    {
        [Fact]
        public void DefaultMapper_MapsMatchingProperties()
        {
            var mapper = new DefaultMapper<Source, Destination>();
            var result = mapper.Map(new Source { Id = 1, Name = "x" });
            Assert.Equal(1, result.Id);
            Assert.Equal("x", result.Name);
        }

        public class Source { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
        public class Destination { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
    }
}
