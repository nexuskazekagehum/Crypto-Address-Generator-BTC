using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CryptoAddressGeneratorBTC.Infrastructure.Configuration
{
    public interface IConfigMerger
    {
        IConfiguration Merge(IConfiguration baseConfig, IConfiguration overrideConfig);
    }

    public class JsonConfigMerger : IConfigMerger
    {
        public IConfiguration Merge(IConfiguration baseConfig, IConfiguration overrideConfig)
        {
            var baseDict = Flatten(baseConfig);
            var overrideDict = Flatten(overrideConfig);
            foreach (var kvp in overrideDict)
                baseDict[kvp.Key] = kvp.Value;
            var json = JsonSerializer.Serialize(Unflatten(baseDict));
            return new ConfigurationBuilder()
                .AddJsonStream(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
                .Build();
        }

        private Dictionary<string, string?> Flatten(IConfiguration config)
        {
            var result = new Dictionary<string, string?>();
            FlattenRecursive(config, "", result);
            return result;
        }

        private void FlattenRecursive(IConfiguration config, string prefix, Dictionary<string, string?> result)
        {
            foreach (var child in config.GetChildren())
            {
                var key = string.IsNullOrEmpty(prefix) ? child.Key : $"{prefix}:{child.Key}";
                if (child.GetChildren().Any())
                    FlattenRecursive(child, key, result);
                else
                    result[key] = child.Value;
            }
        }

        private Dictionary<string, object> Unflatten(Dictionary<string, string?> dict)
        {
            var root = new Dictionary<string, object>();
            foreach (var kvp in dict)
            {
                var parts = kvp.Key.Split(':');
                var current = root as Dictionary<string, object>;
                for (int i = 0; i < parts.Length; i++)
                {
                    var key = parts[i];
                    if (i == parts.Length - 1)
                    {
                        current[key] = kvp.Value ?? "";
                    }
                    else
                    {
                        if (!current.ContainsKey(key))
                            current[key] = new Dictionary<string, object>();
                        current = (Dictionary<string, object>)current[key];
                    }
                }
            }
            return root;
        }
    }
}
