namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class Report
    {
        public string ReportId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Format { get; set; } = "json";
        public string Content { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
