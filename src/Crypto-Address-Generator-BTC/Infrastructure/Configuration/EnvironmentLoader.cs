using Microsoft.Extensions.Configuration;

namespace CryptoAddressGeneratorBTC.Infrastructure.Configuration
{
    public static class EnvironmentLoader
    {
        public static IConfigurationRoot Load(string[]? args = null)
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables("ADDRESSGENERATOR_")
                .AddCommandLine(args ?? Array.Empty<string>())
                .Build();
        }
    }
}
