using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Queue
{
    public interface IJobQueue
    {
        Task EnqueueAsync(Job job, CancellationToken cancellationToken = default);
        Task<Job?> DequeueAsync(CancellationToken cancellationToken = default);
        Task<List<Job>> GetPendingAsync(CancellationToken cancellationToken = default);
    }

    public class InMemoryJobQueue : IJobQueue
    {
        private readonly Queue<Job> _queue = new();
        private readonly ILogger<InMemoryJobQueue> _logger;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public InMemoryJobQueue(ILogger<InMemoryJobQueue> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task EnqueueAsync(Job job, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { _queue.Enqueue(job); }
            finally { _lock.Release(); }
            _logger.LogInformation("Job enqueued: {JobId}", job.JobId);
        }

        public async Task<Job?> DequeueAsync(CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _queue.Count > 0 ? _queue.Dequeue() : null; }
            finally { _lock.Release(); }
        }

        public async Task<List<Job>> GetPendingAsync(CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _queue.ToList(); }
            finally { _lock.Release(); }
        }
    }
}
