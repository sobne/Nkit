using Nkit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //log();
            testCache();

            Console.ReadLine();
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
    }
    static class data
    {
        static public string getName(string id)
        {
            return id + ":test";
        }
    }
    class book
    {
        public string name { get; set; }
    }
}
