/*
var Name = MemCache.GetItem<string>(key,
                delegate() { return BLLFactory<City>.Instance.GetNameByCode(code); },
                new TimeSpan(0, 10, 0));
*/
using System;
using System.Data.SqlClient;
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

        static private CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration=null, DateTime? absoluteExpiration=null)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration != null && absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration != null && slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
        static private void Set<T>(string key, T obj, CacheItemPolicy policy)
        {
            MemoryCache.Default.Set(key, obj, policy);
        }
        static public void Set<T>(string key, T obj)
        {
            try
            {
                var policy = CreatePolicy();
                Set(key, obj, policy);
            }
            catch (Exception)
            {
                throw;
            }
        }
        static public void Set<T>(string key, T obj, TimeSpan slidingExpiration)
        {
            try
            {
                var policy = CreatePolicy(slidingExpiration, null);
                Set(key, obj, policy);
            }
            catch (Exception)
            {
                throw;
            }
        }
        static public void Set<T>(string key, T obj, DateTime absoluteExpiration)
        {
            try
            {
                var policy = CreatePolicy(null, absoluteExpiration);
                Set(key, obj, policy);
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
        static public void Remove(string key)
        {
            try
            {
                MemoryCache.Default.Remove(key);
            }
            catch (Exception)
            {
                throw;
            }
        }
        static public bool IsInMaintenanceMode()
        {
            bool inMaintenanceMode;

            if (MemoryCache.Default["MaintenanceMode"] == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();

                string connStr = "DB CONNECTION STRING";

                SqlDependency.Start(connStr);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand("Select MaintenanceMode From dbo.MaintenanceMode", conn))
                    {
                        command.Notification = null;

                        SqlDependency dep = new SqlDependency();

                        dep.AddCommandDependency(command);

                        conn.Open();

                        inMaintenanceMode = (bool)command.ExecuteScalar();

                        SqlChangeMonitor monitor = new SqlChangeMonitor(dep);

                        policy.ChangeMonitors.Add(monitor);
                    }
                }

                MemoryCache.Default.Add("MaintenanceMode", inMaintenanceMode, policy);
            }
            else
            {
                inMaintenanceMode = (bool)MemoryCache.Default.Get("MaintenanceMode");
            }

            return inMaintenanceMode;
        }
    }
}
