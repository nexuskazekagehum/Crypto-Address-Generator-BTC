namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class Job
    {
        public string JobId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string Payload { get; set; } = string.Empty;
        public int Attempts { get; set; }
        public int MaxAttempts { get; set; } = 3;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
