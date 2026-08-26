using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CryptoAddressGeneratorBTC.Infrastructure.Persistence
{
    public interface IJsonRepository<T> where T : class
    {
        Task SaveAsync(T entity, string id, CancellationToken cancellationToken = default);
        Task<T?> LoadAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default);
    }

    public class JsonRepository<T> : IJsonRepository<T> where T : class
    {
        private readonly string _basePath;
        private readonly ILogger<JsonRepository<T>> _logger;
        private readonly JsonSerializerOptions _options;

        public JsonRepository(ILogger<JsonRepository<T>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _basePath = Path.Combine(AppContext.BaseDirectory, "data");
            Directory.CreateDirectory(_basePath);
            _options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public Task SaveAsync(T entity, string id, CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(_basePath, $"{typeof(T).Name}-{id}.json");
            var json = JsonSerializer.Serialize(entity, _options);
            File.WriteAllText(path, json);
            _logger.LogInformation("Persisted {Type} to {Path}", typeof(T).Name, path);
            return Task.CompletedTask;
        }

        public Task<T?> LoadAsync(string id, CancellationToken cancellationToken = default)
        {
            var path = Path.Combine(_basePath, $"{typeof(T).Name}-{id}.json");
            if (!File.Exists(path)) return Task.FromResult<T?>(null);
            var json = File.ReadAllText(path);
            return Task.FromResult(JsonSerializer.Deserialize<T>(json, _options));
        }

        public Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            var files = Directory.GetFiles(_basePath, "{typeof(T).Name}-*.json");
            var results = new List<T>();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var entity = JsonSerializer.Deserialize<T>(json, _options);
                if (entity is not null) results.Add(entity);
            }
            return Task.FromResult<IEnumerable<T>>(results);
        }
    }
}
