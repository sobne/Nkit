using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Redis.StackExchange
{
    /// <summary>
    /// ConnectionMultiplexer
    /// </summary>
    public static class RedisSeConnection
    {
        
        //"127.0.0.1:6379,allowadmin=true
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["RedisHost"].ConnectionString;

        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _instance;
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetConnection();
                        }
                    }
                }
                return _instance;
            }
        }
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetConnection(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        public static ConnectionMultiplexer GetConnection(string connectionString = null)
        {
            connectionString = connectionString ?? _connectionString;
            var connect = ConnectionMultiplexer.Connect(connectionString);

            connect.ConnectionFailed += MuxerConnectionFailed;

            return connect;
        }
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }
    }
    
}
