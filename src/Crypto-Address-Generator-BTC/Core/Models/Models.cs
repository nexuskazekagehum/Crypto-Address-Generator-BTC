namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class SimulationResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Symbol { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime ComputedAt { get; set; } = DateTime.UtcNow;
    }

    public class Snapshot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<SimulationResult> Results { get; set; } = new();
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }

    public class Metric
    {
        public string Name { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}
