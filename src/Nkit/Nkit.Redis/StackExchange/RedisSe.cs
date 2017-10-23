using Nkit.Core.Utils;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Nkit.Redis.StackExchange
{
    public class RedisSe
    {
        private int _db { get; }
        private readonly ConnectionMultiplexer _conn;
        
        public RedisSe(string host,int db=0)
        {
            _db = db;
            _conn = string.IsNullOrWhiteSpace(host) ? RedisSeConnection.Instance : RedisSeConnection.GetConnection(host);
            //_conn = string.IsNullOrWhiteSpace(host) ? RedisSeConnection.Instance :RedisSeConnection.GetConnectionMultiplexer(host);
        }
        public IDatabase Db
        {
            get { return _conn.GetDatabase(_db); }
        }
        public IDatabase GetDb()
        {
            return _conn.GetDatabase(_db);
        }

        #region string

        public bool Set<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonHelper.Serialize<T>(obj);
            return Db.StringSet(key, json, expiry);
        }
        public T Get<T>(string key) where T : class
        {
            var str = Db.StringGet(key);
            return string.IsNullOrEmpty(str) ? null : JsonHelper.DeSerialize<T>(str);
        }
        public async Task<bool> SetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonHelper.Serialize<T>(obj);
            return await Db.StringSetAsync(key, json, expiry);
        }
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var str = await Db.StringGetAsync(key);
            return string.IsNullOrEmpty(str) ? null : JsonHelper.DeSerialize<T>(str);
        }
        #endregion

        #region list

        #endregion


        #region hashset

        #endregion


    }
}
