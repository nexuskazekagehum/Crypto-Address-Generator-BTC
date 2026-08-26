using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Scheduler
{
    public interface IScheduler
    {
        Task ScheduleAsync(SchedulerTask task, CancellationToken cancellationToken = default);
        Task<List<SchedulerTask>> GetDueTasksAsync(DateTime now, CancellationToken cancellationToken = default);
        Task MarkRunAsync(string taskId, CancellationToken cancellationToken = default);
    }

    public class SimpleScheduler : IScheduler
    {
        private readonly List<SchedulerTask> _tasks = new();
        private readonly ILogger<SimpleScheduler> _logger;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public SimpleScheduler(ILogger<SimpleScheduler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ScheduleAsync(SchedulerTask task, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { _tasks.Add(task); }
            finally { _lock.Release(); }
            _logger.LogInformation("Scheduled task {TaskId}", task.TaskId);
        }

        public async Task<List<SchedulerTask>> GetDueTasksAsync(DateTime now, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _tasks.Where(t => t.IsEnabled && (!t.NextRunAt.HasValue || t.NextRunAt.Value <= now)).ToList(); }
            finally { _lock.Release(); }
        }

        public async Task MarkRunAsync(string taskId, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                var task = _tasks.FirstOrDefault(t => t.TaskId == taskId);
                if (task is not null)
                {
                    task.LastRunAt = DateTime.UtcNow;
                    task.NextRunAt = DateTime.UtcNow.AddHours(1);
                    task.RunCount++;
                }
            }
            finally { _lock.Release(); }
        }
    }
}
