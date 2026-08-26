using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoAddressGeneratorBTC.Infrastructure.Serialization
{
    public static class JsonOptions
    {
        public static JsonSerializerOptions Default => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public static JsonSerializerOptions Compact => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true
        };
    }
}
