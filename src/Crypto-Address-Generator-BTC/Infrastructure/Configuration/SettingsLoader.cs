using CryptoAddressGeneratorBTC.Core.Models;
using Microsoft.Extensions.Configuration;

namespace CryptoAddressGeneratorBTC.Infrastructure.Configuration
{
    public interface ISettingsLoader
    {
        Task<IEnumerable<Setting>> LoadAsync(CancellationToken cancellationToken = default);
        Task<Setting?> GetAsync(string key, CancellationToken cancellationToken = default);
    }

    public class ConfigurationSettingsLoader : ISettingsLoader
    {
        private readonly IConfiguration _configuration;

        public ConfigurationSettingsLoader(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<IEnumerable<Setting>> LoadAsync(CancellationToken cancellationToken = default)
        {
            var settings = _configuration.GetChildren().Select(s => new Setting
            {
                Key = s.Key,
                Value = s.Value ?? string.Empty,
                Type = "string"
            }).ToList();
            return Task.FromResult<IEnumerable<Setting>>(settings);
        }

        public Task<Setting?> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            var value = _configuration[key];
            return Task.FromResult(value == null ? null : new Setting { Key = key, Value = value });
        }
    }
}
