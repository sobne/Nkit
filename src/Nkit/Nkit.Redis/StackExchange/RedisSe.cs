using Newtonsoft.Json;
using Nkit.Core.Utils;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nkit.Redis.StackExchange
{
    public class RedisSe
    {
        private string _host { get; }
        private int _db { get; }
        private ConnectionMultiplexer _conn;

        public bool Connected = false;
        public RedisSe(string host,int db=0)
        {
            _host = host;
            _db = db;
            
            try
            {
                _conn = string.IsNullOrWhiteSpace(host) ? RedisSeConnection.Instance : RedisSeConnection.GetConnection(host);
                Connected = _conn.IsConnected;
                _conn.ConnectionFailed += _conn_ConnectionFailed;
            }
            catch (Exception)
            {
                new Thread(Connect){IsBackground = true}.Start();
                //throw;
            }
            
            //_conn = string.IsNullOrWhiteSpace(host) ? RedisSeConnection.Instance :RedisSeConnection.GetConnectionMultiplexer(host);
        }
        private void Connect()
        {
            Console.WriteLine("reconnect");
            while (true)
            {
                try
                {
                    _conn = RedisSeConnection.GetConnection(_host);
                    if (_conn.IsConnected)
                    {
                        Connected = _conn.IsConnected;
                        //_conn.ConnectionFailed += _conn_ConnectionFailed;
                        break;
                    }
                    Thread.Sleep(100);
                }
                catch
                {
                }
            }
        }
        private void _conn_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("_conn_ConnectionFailed");
            Connected = false;
            //_conn.Close();
            new Thread(Connect)
            {
                IsBackground = true
            }.Start();
            //throw new NotImplementedException();
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
            //string json = JsonHelper.Serialize<T>(obj);
            string json = JsonConvert.SerializeObject(obj);
            return Db.StringSet(key, json, expiry);
        }
        public T Get<T>(string key) where T : class
        {
            var str = Db.StringGet(key);
            return string.IsNullOrEmpty(str) ? null : JsonConvert.DeserializeObject<T>(str);
            //return string.IsNullOrEmpty(str) ? null : JsonHelper.DeSerialize<T>(str);
        }
        public async Task<bool> SetAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            //string json = JsonHelper.Serialize<T>(obj);
            string json = JsonConvert.SerializeObject(obj);
            return await Db.StringSetAsync(key, json, expiry);
        }
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var str = await Db.StringGetAsync(key);
            return string.IsNullOrEmpty(str) ? null : JsonConvert.DeserializeObject<T>(str);
            //return string.IsNullOrEmpty(str) ? null : JsonHelper.DeSerialize<T>(str);
        }
        #endregion

        #region list

        #endregion


        #region hashset

        #endregion


    }
}
