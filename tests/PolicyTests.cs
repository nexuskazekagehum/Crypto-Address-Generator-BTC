using CryptoAddressGeneratorBTC.Core.Policies;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CryptoAddressGeneratorBTC.Tests
{
    public class PolicyTests
    {
        [Fact]
        public async Task RetryPolicy_SucceedsAfterFailure()
        {
            var policy = new ExponentialBackoffRetryPolicy(NullLogger<ExponentialBackoffRetryPolicy>.Instance);
            int attempts = 0;
            var result = await policy.ExecuteAsync(() =>
            {
                attempts++;
                if (attempts < 3) throw new InvalidOperationException("fail");
                return Task.FromResult(42);
            }, 3, TimeSpan.FromMilliseconds(10));
            Assert.Equal(42, result);
            Assert.Equal(3, attempts);
        }
    }
}
