namespace CryptoAddressGeneratorBTC.Infrastructure.Resilience
{
    public interface ICircuitBreaker
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
        Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default);
    }

    public class SimpleCircuitBreaker : ICircuitBreaker
    {
        private int _failureCount;
        private readonly int _threshold;
        private readonly TimeSpan _openDuration;
        private DateTime? _openedAt;
        private State _state = State.Closed;

        public SimpleCircuitBreaker(int threshold = 5, TimeSpan? openDuration = null)
        {
            _threshold = threshold;
            _openDuration = openDuration ?? TimeSpan.FromSeconds(30);
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
        {
            if (_state == State.Open && DateTime.UtcNow - _openedAt < _openDuration)
                throw new InvalidOperationException("Circuit breaker is open");

            try
            {
                var result = await action();
                _failureCount = 0;
                _state = State.Closed;
                return result;
            }
            catch
            {
                _failureCount++;
                if (_failureCount >= _threshold)
                {
                    _state = State.Open;
                    _openedAt = DateTime.UtcNow;
                }
                throw;
            }
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(async () => { await action(); return true; }, cancellationToken);
        }

        private enum State { Closed, Open, HalfOpen }
    }
}
