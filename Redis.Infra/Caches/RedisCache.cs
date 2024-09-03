using Microsoft.Extensions.Caching.Distributed;
using Redis.Model.Interfaces;
using System.Text.Json;

namespace Redis.Infra.Caches
{
    public class RedisCache<T> : ICache<T>
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetSingle(string key, T entity, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            var data = JsonSerializer.SerializeToUtf8Bytes(entity);
            await Set(key, data, absoluteExpiration, slidingExpiration);
        }

        public async Task<T> GetSingle(string key)
        {
            var bytes = await Get(key);

            if (bytes == null)
                return default;

            return JsonSerializer.Deserialize<T>(bytes);
        }

        public async Task SetAll(string key, List<T> entities, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            var data = JsonSerializer.SerializeToUtf8Bytes(entities);
            await Set(key, data, absoluteExpiration, slidingExpiration);
        }

        public async Task<List<T>> GetAll(string key)
        {
            var bytes = await Get(key);

            if (bytes == null)
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(bytes);            
        }

        private async Task Set(string key, byte[] data, TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration)
        {
            await _distributedCache.SetAsync(key, data, options: new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            });
        }

        private async Task<byte[]> Get(string key)
        {
            return await _distributedCache.GetAsync(key);
        }
    }
}
