/*
var Name = MemCache.GetItem<string>(key,
                delegate() { return BLLFactory<City>.Instance.GetNameByCode(code); },
                new TimeSpan(0, 10, 0));
*/
using System;
using System.Runtime.Caching;

namespace Nkit.Data
{
    static public class MemCache
    {
        static private readonly Object _locker = new object();

        static public T GetItem<T>(String key, Func<T> retrieveItem, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (retrieveItem == null) throw new ArgumentNullException("retrieveItem");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, retrieveItem());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }
            return (T)MemoryCache.Default[key];
        }

        static private CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }

        static public void Set<T>(string key, T obj, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            try
            {
                ObjectCache cache = MemoryCache.Default;
                var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                cache.Set(key, obj, policy);
            }
            catch (Exception)
            {
                throw;
            }
        }
        static public T Get<T>(string key) where T : class
        {
            try
            {
                ObjectCache cache = MemoryCache.Default;
                var obj = cache[key] as T;
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
