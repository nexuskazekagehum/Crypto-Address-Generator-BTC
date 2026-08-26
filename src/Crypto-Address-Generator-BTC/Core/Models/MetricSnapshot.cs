namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class MetricSnapshot
    {
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
        public Dictionary<string, string> Tags { get; set; } = new();
    }
}
