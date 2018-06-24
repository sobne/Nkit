using Nkit.Redis.StackExchange;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class RedisClient
    {
        static string host = ConfigurationManager.AppSettings["RedisHost"];
        static int index = 1;
        static RedisSe redis = new RedisSe(host, 2);
        //static RedisSe redis;
        static public T Get<T>(string key) where T : class
        {
            try
            {
                //if (redis ==null) redis = new RedisSe(host, index);
                if (!redis.Connected) return null;
                else return redis.Get<T>(key);
            }
            catch
            {
                //log
                return null;
            }
        }
        static public bool Set<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            try
            {
                //if (redis == null) redis = new RedisSe(host, index);
                if (!redis.Connected) return false;
                else return redis.Set<T>(key, obj, expiry);
            }
            catch
            {
                //log
                return false;
            }
        }
        static public bool Delete(string key)
        {
            return false;
            //RedisSe redis = new RedisSe(host, index);
            //return redis.Db.KeyDelete()
        }
    }
}
