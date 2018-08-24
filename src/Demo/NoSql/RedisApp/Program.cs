using Nkit.Redis.StackExchange;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace RedisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string host = ConfigurationManager.ConnectionStrings["RedisHost"].ConnectionString ?? "127.0.0.1:6379";
            //RedisSe redis = new RedisSe(host,2);

            //for (int i = 0; i < 2; i++)
            //{
            //    redis.Db.StringSet("123:121", i.ToString());
            //    Console.WriteLine(redis.Db.StringGet("123:121"));
            //    redis.Db.StringSet("123:122", i.ToString());
            //    Console.WriteLine(redis.Db.StringGet("123:122"));
            //}


            //redis.Set("1212:bca", data);
            //var v=redis.Get<string>("1212:bca");
            //Console.WriteLine(v);

            RedisClient.Set("clienttest", "clienttest,clienttest-clienttest-clienttest");
            var clienttest = RedisClient.Get<string>("clienttest");

            Console.ReadLine();
            RedisClient.Del("clienttest");

            var ts = new TimeSpan(0, 5, 0);
            RedisClient.Set("clienttest", "clienttest,sdfsfsfdsf", ts);
            for (int j = 0; j < 20; j++)
            {
                RedisClient.Set("test"+j, "test value "+j, ts);
                Thread.Sleep(5000);
                var v = RedisClient.Get<string>("test" + j);
                Console.WriteLine(j+" - "+v);
            }
            var key = "absolutetime";
            RedisClient.Set(key, "clienttest,absolutetime", DateTime.Now.AddSeconds(3));
            var a0 = RedisClient.Get<string>(key);
            Console.WriteLine("a0:" + a0);
            Thread.Sleep(4000);
            a0 = RedisClient.Get<string>(key);
            Console.WriteLine("a0:" + a0);

            Console.ReadLine();
        }
    }
}
