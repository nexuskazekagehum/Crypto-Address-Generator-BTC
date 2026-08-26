using CryptoAddressGeneratorBTC.Core.Analyzers;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class AnalyzerTests
    {
        [Fact]
        public void GenericSummaryAnalyzer_CountsItems()
        {
            var analyzer = new GenericSummaryAnalyzer<int>();
            var result = analyzer.Analyze(new[] { 1, 2, 3 });
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result.First);
            Assert.Equal(3, result.Last);
        }
    }
}
