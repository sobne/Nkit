using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            socketlisten();
            Console.ReadLine();
            return;

            Console.WriteLine("Server服务已启动……");
            //IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 2500);
            listener.Start(); 
            Console.WriteLine("开始监听……");
            Console.WriteLine("\n输入\"Q\"键退出。");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            }
            while (key != ConsoleKey.Q);
        }
        static void socketlisten()
        {
            int recv; 
            byte[] data; 
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);  
            newsock.Bind(ipep);
            newsock.Listen(10);

            Console.WriteLine("waiting for a client......");

            Socket client = newsock.Accept();
            IPEndPoint clientip = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("connect with client " + clientip.Address + " at port " + clientip.Port);

            string welcom = "welcom here!";
            data = Encoding.ASCII.GetBytes(welcom);

            client.Send(data, data.Length, SocketFlags.None);

            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                Console.WriteLine("recv = " + recv);

                if (recv == 0) break; 

                Console.WriteLine( Encoding.ASCII.GetString(data, 0, recv));
                client.Send(data, recv, SocketFlags.None);

                //在服务器端输入，发送到客户端
                //string input = Console.ReadLine();
                //if (input == "exit") break;
                //client.Send(Encoding.ASCII.GetBytes(input));
                //data = new byte[1024];
                //recv = client.Receive(data);
                //input = Encoding.ASCII.GetString(data, 0, recv);
                //textBox1.Text=input;
            }

            Console.WriteLine("Disconnect from " + clientip.Address);
            client.Close();
            newsock.Close();
        }
    }
}
