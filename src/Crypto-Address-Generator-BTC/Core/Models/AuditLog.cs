namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Action { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public string Resource { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string IpAddress { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
    }
}
