using Nkit.Redis.StackExchange;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = ConfigurationManager.ConnectionStrings["RedisHost"].ConnectionString ?? "127.0.0.1:6379";
            RedisSe redis = new RedisSe(host,2);

            for (int i = 0; i < 2; i++)
            {
                redis.Db.StringSet("123:121", i.ToString());
                Console.WriteLine(redis.Db.StringGet("123:121"));
                redis.Db.StringSet("123:122", i.ToString());
                Console.WriteLine(redis.Db.StringGet("123:122"));
            }

            
            //redis.Set("1212:bca", data);
            //var v=redis.Get<string>("1212:bca");
            //Console.WriteLine(v);
            Console.ReadLine();
        }
    }
}
