namespace CryptoAddressGeneratorBTC.Core.Models
{
    public class Setting
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Type { get; set; } = "string";
        public string Description { get; set; } = string.Empty;
        public bool IsEncrypted { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
