using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace MyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            socketconnect();
            return;

            Console.WriteLine("客户端启动……");
            TcpClient client = new TcpClient();
            try
            {
                //client.Connect("localhost", 250);
                client.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 2500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            
            Console.WriteLine("已经成功与客户端建立连接！{0} --> {1}", client.Client.LocalEndPoint, client.Client.RemoteEndPoint);
            Console.WriteLine("\n输入\"Q\"键退出。");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            }
            while (key != ConsoleKey.Q);
        }
        static void socketconnect()
        {
            byte[] data = new byte[1024];
            Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newclient.Bind(new IPEndPoint(IPAddress.Any, 905));

            IPEndPoint ie = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            try
            {
                newclient.Connect(ie);
            }
            catch (SocketException e)
            {
                Console.WriteLine("unable to connect to server");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }

            int receivedDataLebgth = newclient.Receive(data);
            string stringdata = Encoding.ASCII.GetString(data, 0, receivedDataLebgth);
            Console.WriteLine(stringdata);

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit") break;

                newclient.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];
                receivedDataLebgth = newclient.Receive(data);
                stringdata = Encoding.ASCII.GetString(data, 0, receivedDataLebgth);
                Console.WriteLine(stringdata);
            }

            Console.WriteLine("disconnect from server");
            newclient.Shutdown(SocketShutdown.Both);

            newclient.Close();
            Console.ReadLine();
        }
    }
}
