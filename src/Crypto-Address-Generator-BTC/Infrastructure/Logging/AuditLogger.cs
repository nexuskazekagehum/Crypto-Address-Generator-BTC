using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Logging
{
    public interface IAuditLogger
    {
        Task LogAsync(AuditLog log, CancellationToken cancellationToken = default);
        Task<IEnumerable<AuditLog>> GetLogsAsync(string actor, CancellationToken cancellationToken = default);
    }

    public class InMemoryAuditLogger : IAuditLogger
    {
        private readonly List<AuditLog> _logs = new();
        private readonly ILogger<InMemoryAuditLogger> _logger;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public InMemoryAuditLogger(ILogger<InMemoryAuditLogger> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task LogAsync(AuditLog log, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { _logs.Add(log); }
            finally { _lock.Release(); }
            _logger.LogInformation("Audit: {Action} by {Actor}", log.Action, log.Actor);
        }

        public async Task<IEnumerable<AuditLog>> GetLogsAsync(string actor, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try { return _logs.Where(l => l.Actor == actor).ToList(); }
            finally { _lock.Release(); }
        }
    }
}
