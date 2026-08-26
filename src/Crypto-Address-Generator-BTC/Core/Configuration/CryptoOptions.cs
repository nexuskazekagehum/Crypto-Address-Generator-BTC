namespace CryptoAddressGeneratorBTC.Core.Configuration
{
    public class CryptoOptions
    {
        public int RefreshIntervalMs { get; set; } = 30000;
        public string DefaultCurrency { get; set; } = "USD";
        public string DataEndpoint { get; set; } = "https://api.example.com/crypto";
    }
}
