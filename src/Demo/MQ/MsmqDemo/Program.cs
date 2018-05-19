using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading;
using System.Configuration;

namespace MsmqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(new ThreadStart(() => SendQueue()));
            thread.Start();
            Thread.Sleep(3000);

            //Thread thread1 = new Thread(new ThreadStart(() => ReceiveQueue()));
            //thread1.Start();
            Console.ReadLine();
        }
        static void SendQueue()
        {
            string snapQueue = ".\\Private$\\snapQueue";
            if (!MessageQueue.Exists(snapQueue))
            {
                MessageQueue.Create(snapQueue);
            }
            using (var queue = new MessageQueue(snapQueue))
            {
                string msgs = ConfigurationManager.AppSettings["msg"];
                foreach (string msg in msgs.Split(';'))
                {
                    queue.Send(msg);
                    Console.WriteLine("Message sent: {0}", msg);
                }
                //for (int i = 0; i < 20; i++)
                //{
                //    Thread.Sleep(500);
                //    string msg = DateTime.Now.ToString("MMdd HH:mm:ss");
                //    queue.Send(msg);
                //    Console.WriteLine("Message sent: {0}", msg);
                //}
            }
        }
        static void ReceiveQueue()
        {
            string snapQueue = ".\\Private$\\snapQueue";
            using (var queue = new MessageQueue(snapQueue))
            {
                if (!MessageQueue.Exists(snapQueue))
                {
                    Console.WriteLine("No existing queue");
                }
                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                while (true)
                {
                    var m = queue.Receive();
                    string msg = (string)m.Body;
                    Console.WriteLine("Message received: {0}", msg);
                }
            }
        }
    }
}
