using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using CleanApplication.Application.Common.Interfaces;

namespace CleanApplication.Infrastructure.Services
{
    namespace CleanApplication.Infrastructure.Services
    {
        public class CacheService : ICacheService
        {
            public IMemoryCache _cache { get; set; }
            public CacheService(IMemoryCache cache)
            {
                _cache=cache;
            }
            public object Get(object key)
            {
                _cache.TryGetValue(key, out object value);
                return value;
            }

            public TItem Get<TItem>(object key)
            {
                return (TItem)(_cache.Get(key) ?? default(TItem));
            }

            public bool TryGetValue<TItem>(object key, out TItem value)
            {
                if (_cache.TryGetValue(key, out object result))
                {
                    if (result is TItem item)
                    {
                        value = item;
                        return true;
                    }
                }

                value = default;
                return false;
            }

            public TItem Set<TItem>(object key, TItem value)
            {
                var entry = _cache.CreateEntry(key);
                entry.Value = value;
                entry.Dispose();

                return value;
            }

            public TItem Set<TItem>(object key, TItem value, DateTimeOffset absoluteExpiration)
            {
                var entry = _cache.CreateEntry(key);
                entry.AbsoluteExpiration = absoluteExpiration;
                entry.Value = value;
                entry.Dispose();

                return value;
            }

            public TItem Set<TItem>(object key, TItem value, TimeSpan absoluteExpirationRelativeToNow)
            {
                var entry = _cache.CreateEntry(key);
                entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
                entry.Value = value;
                entry.Dispose();

                return value;
            }

            public TItem Set<TItem>(object key, TItem value, IChangeToken expirationToken)
            {
                var entry = _cache.CreateEntry(key);
                entry.AddExpirationToken(expirationToken);
                entry.Value = value;
                entry.Dispose();

                return value;
            }

            public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options)
            {
                using (var entry = _cache.CreateEntry(key))
                {
                    if (options != null)
                    {
                        entry.SetOptions(options);
                    }

                    entry.Value = value;
                }

                return value;
            }

            public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
            {
                if (!_cache.TryGetValue(key, out object result))
                {
                    var entry = _cache.CreateEntry(key);
                    result = factory(entry);
                    entry.SetValue(result);
                    // need to manually call dispose instead of having a using
                    // in case the factory passed in throws, in which case we
                    // do not want to add the entry to the cache
                    entry.Dispose();
                }

                return (TItem)result;
            }

            public async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
            {
                if (!_cache.TryGetValue(key, out object result))
                {
                    var entry = _cache.CreateEntry(key);
                    result = await factory(entry);
                    entry.SetValue(result);
                    // need to manually call dispose instead of having a using
                    // in case the factory passed in throws, in which case we
                    // do not want to add the entry to the cache
                    entry.Dispose();
                }

                return (TItem)result;
            }
        }
    }
}
