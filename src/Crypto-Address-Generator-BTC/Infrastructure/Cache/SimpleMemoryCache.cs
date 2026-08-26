using System.Collections.Concurrent;

namespace CryptoAddressGeneratorBTC.Infrastructure.Cache
{
    public interface ICache<TKey, TValue> where TKey : notnull
    {
        TValue? Get(TKey key);
        void Set(TKey key, TValue value, TimeSpan? expiration = null);
        bool TryGet(TKey key, out TValue value);
        void Remove(TKey key);
    }

    public class SimpleMemoryCache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, CacheEntry> _store = new();
        private readonly TimeSpan _defaultExpiration;

        public SimpleMemoryCache(TimeSpan? defaultExpiration = null)
        {
            _defaultExpiration = defaultExpiration ?? TimeSpan.FromMinutes(5);
        }

        public TValue? Get(TKey key)
        {
            if (_store.TryGetValue(key, out var entry) && entry.IsValid)
                return entry.Value;
            _store.TryRemove(key, out _);
            return default;
        }

        public void Set(TKey key, TValue value, TimeSpan? expiration = null)
        {
            _store[key] = new CacheEntry(value, DateTime.UtcNow.Add(expiration ?? _defaultExpiration));
        }

        public bool TryGet(TKey key, out TValue value)
        {
            var found = Get(key);
            if (found is not null)
            {
                value = found;
                return true;
            }
            value = default!;
            return false;
        }

        public void Remove(TKey key) => _store.TryRemove(key, out _);

        private class CacheEntry
        {
            public TValue Value { get; }
            public DateTime ExpiresAt { get; }
            public bool IsValid => DateTime.UtcNow < ExpiresAt;

            public CacheEntry(TValue value, DateTime expiresAt)
            {
                Value = value;
                ExpiresAt = expiresAt;
            }
        }
    }
}
