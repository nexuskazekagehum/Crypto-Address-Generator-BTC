namespace CryptoAddressGeneratorBTC.Infrastructure.RateLimit
{
    public interface IRateLimiter
    {
        Task<bool> AcquireAsync(CancellationToken cancellationToken = default);
        Task WaitAsync(CancellationToken cancellationToken = default);
    }

    public class TokenBucketRateLimiter : IRateLimiter
    {
        private readonly SemaphoreSlim _lock = new(1, 1);
        private readonly int _capacity;
        private readonly TimeSpan _refillInterval;
        private readonly int _refillAmount;
        private double _tokens;
        private DateTime _lastRefill;

        public TokenBucketRateLimiter(int capacity, TimeSpan refillInterval, int refillAmount)
        {
            _capacity = capacity;
            _refillInterval = refillInterval;
            _refillAmount = refillAmount;
            _tokens = capacity;
            _lastRefill = DateTime.UtcNow;
        }

        public async Task<bool> AcquireAsync(CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                Refill();
                if (_tokens >= 1)
                {
                    _tokens -= 1;
                    return true;
                }
                return false;
            }
            finally { _lock.Release(); }
        }

        public async Task WaitAsync(CancellationToken cancellationToken = default)
        {
            while (!await AcquireAsync(cancellationToken))
            {
                await Task.Delay(_refillInterval, cancellationToken);
            }
        }

        private void Refill()
        {
            var now = DateTime.UtcNow;
            var intervals = (int)((now - _lastRefill).TotalMilliseconds / _refillInterval.TotalMilliseconds);
            if (intervals > 0)
            {
                _tokens = Math.Min(_capacity, _tokens + intervals * _refillAmount);
                _lastRefill = now;
            }
        }
    }
}
