namespace CryptoAddressGeneratorBTC.Core.Constants
{
    public static class DomainConstants
    {
        public const int DefaultPageSize = 50;
        public const int MaxPageSize = 1000;
        public const int DefaultRetryCount = 3;
        public const int DefaultTimeoutMs = 30000;
        public const string DefaultLogLevel = "Information";
        public const string ConfigurationSectionName = "CryptoAddressGeneratorBTC";
        public const string HealthCheckEndpoint = "/health";
        public const string MetricsEndpoint = "/metrics";
        public const int CacheExpirationMinutes = 5;
    }
}
