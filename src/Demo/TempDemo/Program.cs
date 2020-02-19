using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TempDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //test();
            //CheckCRC();
            //var dt = DateTime.Now;
            //var s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //var dt0 = DateTime.Parse(s);
            Console.ReadLine();
        }
        public delegate string AsyncMethodCaller(int callDuration, out int threadId);
        private static void callBackMethod(IAsyncResult ar)
        {
            AsyncMethodCaller caller = ar.AsyncState as AsyncMethodCaller;
            string result = caller.EndInvoke(out int threadid, ar);
            Console.WriteLine("Completed!");
            Console.WriteLine(result);

        }
        static string TestMethodAsync(int callDuration, out int threadId)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("异步TestMethodAsync开始");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(callDuration);
                Console.WriteLine("TestMethodAsync:" + i.ToString());
            }
            sw.Stop();
            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("耗时{0}ms.", sw.ElapsedMilliseconds.ToString());
        }
        static void test()
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(TestMethodAsync);
            int threadid = 0;
            IAsyncResult result = caller.BeginInvoke(1000, out threadid, callBackMethod, caller);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("其它业务" + i.ToString());
            }
            Console.WriteLine("等待异步方法TestMethodAsync执行完成");
            Console.Read();
        }

        static void CheckCRC()
        {
            var crc = "01032A000F00000000000000000000000000000000000000000000000000000000000000000000000000000000";
            var res = CRC16.CheckCRC(crc);

        }
    }
}
