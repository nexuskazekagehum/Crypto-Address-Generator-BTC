using CryptoAddressGeneratorBTC.Core.Specifications;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class SpecificationTests
    {
        private class IsPositive : Specification<int>
        {
            public override bool IsSatisfiedBy(int candidate) => candidate > 0;
        }

        private class IsEven : Specification<int>
        {
            public override bool IsSatisfiedBy(int candidate) => candidate % 2 == 0;
        }

        [Fact]
        public void AndSpecification_MatchesBoth()
        {
            var spec = new IsPositive().And(new IsEven());
            Assert.True(spec.IsSatisfiedBy(2));
            Assert.False(spec.IsSatisfiedBy(3));
            Assert.False(spec.IsSatisfiedBy(-2));
        }
    }
}
