namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class SchedulerTask
    {
        public string TaskId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string CronExpression { get; set; } = "0 0 * * *";
        public DateTime? LastRunAt { get; set; }
        public DateTime? NextRunAt { get; set; }
        public bool IsEnabled { get; set; } = true;
        public int RunCount { get; set; }
    }
}
