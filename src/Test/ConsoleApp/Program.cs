using Nkit.Core;
using Nkit.Core.Utils;
using Nkit.Data;
using Nkit.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            logger();
            //loger();
            //ConvertIp();
            //EL();
            regexMatch();

            //listtest();
            //log();
            //testCache();
            //testConvertor();
            Console.ReadLine();
        }
        static void logger()
        {
            for (int i = 0; i < 100; i++)
            {
                LogH.Debug($"{i} - 1111");
                LogH.Error($"{i} - eeee");
                LogH.Fatal($"{i} - ffffff");
                LogH.Info($"{i} - iiiiii");
                LogH.Warn($"{i} - wwwwww");
                Console.WriteLine(i.ToString()+" -------------------------------------");
                Thread.Sleep(1000);
            }
        }
        static void loger()
        {
            ILogger log = new LoggerFactory().GetLog();
            log.Debug("112");
            log.Error("112");
            log.Fatal("112");
            log.Info("112");
            log.Warn("112");


            ILogger log1 = new LoggerFactory().GetLog("test","APP");
            log1.Debug("112");
            log1.Error("112");
            log1.Fatal("112");
            log1.Info("112");
            log1.Warn("112");
        }
        static void ConvertIp()
        {
            var l = Convertor.Ip2Long("192.168.1.5");
            Console.WriteLine(l);
            Console.WriteLine(Convertor.Long2Ip(l));
        }
        static void EL()
        {
            //run as administrator
            EventLogger.Information("test", "Nkit.EL", "test");
        }
        static void regexMatch()
        {
            var stringMatch = @"^\d{9,13}$";
            while (true)
            {
                var input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    break;
                }
                if (input.ToLower() == "logger")
                {
                    logger();
                }
                //Console.WriteLine(Regex.IsMatch(input, stringMatch) ? "true" : "false");
            }
                
        }
        static void listtest()
        {
            var Keys = new List<string>
            {
                "111",
                "111222",
                "333",
                "333444",
                "444",
                "444555"
            };
            var key = "3331";
            var keys = Keys.Where(x => x.Contains(key)).ToList();

            foreach (var k in keys)
            {
                Keys.Remove(k);
            }

        }
        static void log()
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\log.txt", true))
                {
                    sw.WriteLine("【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】Start.");
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        static void testCache()
        {

            MemCache.Set<string>("noexpiration", "noexpiration-noexpiration-noexpiration-noexpiration");
            var a0 = MemCache.Get<string>("noexpiration");
            MemCache.Set<string>("testabsolutetime", "sdfaslfjasldfjadsfjsafsfsd", DateTime.Now.AddSeconds(3));
            var a1 = MemCache.Get<string>("testabsolutetime");
            Thread.Sleep(4000);
            var a2 = MemCache.Get<string>("testabsolutetime");
            a0 = MemCache.Get<string>("noexpiration");

            MemCache.Set<string>("test123", "sdfaslfjasldfjadsfjsafsfsd", new TimeSpan(0, 0, 5));
            var v1 = MemCache.Get<string>("test123");
            var v2 = MemCache.Get<string>("test123");
            var v3 = MemCache.Get<string>("test123");
            MemCache.Set<string>("test123", "123123123213213124324", new TimeSpan(0, 0, 5));
            var v0 = MemCache.Get<string>("test123");

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" - begin");
            getCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 1");
            getCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 2");
            getCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 3");
            getCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 4");
            getCache();
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 5");
            for (int i = 0; i < 12; i++)
            {
                var Name = getCache();
                Thread.Sleep(1000);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + i);
            }
        }
        static string getName(string id)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - getName");
            return id + ":test";
        }
        static string getCache()
        {
            var Name = MemCache.GetItem<string>("test_memcache",
                delegate () { return getName("213131"); },
                new TimeSpan(0, 0, 5));
            return Name;
        }

        static void testConvertor()
        {
            var ts = new TestStructure
            {
                S="tstsfsdfs",
                I=213123,
                DT=DateTime.Now
            };
            var bytTemp = Convertor.Object2Bytes(ts);
            Console.WriteLine("数组长度：" + bytTemp.Length);
            var tsB = (TestStructure)Convertor.Bytes2Object(bytTemp, typeof(TestStructure));
            Console.WriteLine(tsB.ToString());

            var b = new macbook
            {
                name = "mac10",
                price = 10000
            };
            var buff = Convertor.Object2Bytes(b);
            var o = (book)Convertor.Bytes2Object(buff, typeof(book));

            var buff2 = Convertor.ObjectToBytes(b);
            var obj = Convertor.BytesToObject(buff2);


            Console.ReadLine();
        }
    }
    static class data
    {
        static public string getName(string id)
        {
            return id + ":test";
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    class book
    {
        public string name { get; set; }
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    class macbook:book
    {
        public double price { get; set; }
    }


    struct TestStructure
    {
        public string S { get; set; }
        public int I { get; set; }
        public DateTime DT { get; set; }
        public override string ToString()
        {
            return string.Format("S:{0};I:{1};DT:{2}", S, I, DT);
        }
    }
}
