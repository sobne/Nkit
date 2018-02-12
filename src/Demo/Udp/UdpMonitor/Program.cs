using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace UdpMonitor
{
    class Program
    {
        static private UdpClient _udpClient=null;
        static private IPEndPoint _remoteEP = null;
        static void Main(string[] args)
        {
            var localIP = IPAddress.Parse("127.0.0.1");
            var localEP = new IPEndPoint(localIP, 50000);
            _udpClient = new UdpClient(localEP);

            Console.WriteLine("开始侦听端口:");

            //var thread = new Thread(Receive)
            //{
            //    IsBackground = true
            //};
            //thread.Start();
            Receive();
            Console.ReadLine();
        }
        static void Receive()
        {
            _remoteEP = new IPEndPoint(IPAddress.Any,0);
            while (true)
            {
                try
                {
                    var receiveString = string.Empty;
                    byte[] receiveBuffer = _udpClient.Receive(ref _remoteEP);
                    if (receiveBuffer != null)
                    {
                        foreach (byte b in receiveBuffer)
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
                    }
                    Console.WriteLine(receiveString);
                    Console.WriteLine(Encoding.Default.GetString(receiveBuffer));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
