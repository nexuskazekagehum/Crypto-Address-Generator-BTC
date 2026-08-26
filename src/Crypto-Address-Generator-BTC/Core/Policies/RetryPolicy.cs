using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Core.Policies
{
    public interface IRetryPolicy
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> action, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default);
        Task ExecuteAsync(Func<Task> action, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default);
    }

    public class ExponentialBackoffRetryPolicy : IRetryPolicy
    {
        private readonly ILogger<ExponentialBackoffRetryPolicy> _logger;

        public ExponentialBackoffRetryPolicy(ILogger<ExponentialBackoffRetryPolicy> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
        {
            Exception? last = null;
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex)
                {
                    last = ex;
                    _logger.LogWarning(ex, "Attempt {Attempt} failed", i + 1);
                    if (i < maxRetries)
                        await Task.Delay(TimeSpan.FromMilliseconds(delay.TotalMilliseconds * Math.Pow(2, i)), cancellationToken);
                }
            }
            throw last ?? new InvalidOperationException("Retry policy failed");
        }

        public async Task ExecuteAsync(Func<Task> action, int maxRetries, TimeSpan delay, CancellationToken cancellationToken = default)
        {
            await ExecuteAsync(async () => { await action(); return true; }, maxRetries, delay, cancellationToken);
        }
    }
}
