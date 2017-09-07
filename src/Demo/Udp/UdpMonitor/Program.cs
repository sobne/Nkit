using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace UdpMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient(50000);
            IPAddress serverIP = IPAddress.Parse("127.0.0.1");
            IPEndPoint server = new IPEndPoint(serverIP, 50000);
            Console.WriteLine("开始侦听端口:");
            while (true)
            {
                string receiveString = string.Empty;
                byte[] receiveArray = udpClient.Receive(ref server);
                foreach (byte b in receiveArray)
                {
                    if (b.ToString("X").Length < 2)
                    {
                        receiveString += "0" + b.ToString("X") + " ";
                    }
                    else
                    {
                        receiveString += b.ToString("X") + " ";
                    }
                }
                Console.WriteLine(receiveString);
                Console.WriteLine(Encoding.Default.GetString(receiveArray));

            }
            udpClient.Close();
        }
    }
}
