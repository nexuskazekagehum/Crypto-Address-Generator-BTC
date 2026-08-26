using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Clients
{
    public interface IExternalDataClient
    {
        Task<string> GetAsync(string endpoint, CancellationToken cancellationToken = default);
        Task<T?> DeserializeAsync<T>(string json, CancellationToken cancellationToken = default);
    }

    public class SimulatedExternalDataClient : IExternalDataClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<SimulatedExternalDataClient> _logger;

        public SimulatedExternalDataClient(HttpClient client, ILogger<SimulatedExternalDataClient> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GetAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Simulated GET {Endpoint}", endpoint);
            await Task.Delay(50, cancellationToken);
            return "{\"status\":\"ok\",\"data\":[]}";
        }

        public Task<T?> DeserializeAsync<T>(string json, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
        }
    }
}
