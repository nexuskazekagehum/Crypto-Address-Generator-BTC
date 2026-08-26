using Microsoft.Extensions.Configuration;
using CryptoAddressGeneratorBTC.Core.Configuration;

namespace CryptoAddressGeneratorBTC.Infrastructure.Configuration
{
    public static class ConfigurationLoader
    {
        public static IConfiguration Build(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables("CRYPTO_")
                .Build();
        }

        public static CryptoOptions BindOptions(this IConfiguration configuration)
        {
            var options = new CryptoOptions();
            configuration.GetSection("Crypto").Bind(options);
            return options;
        }
    }
}
