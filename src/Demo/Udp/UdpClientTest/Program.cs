using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace UdpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("127.0.0.1", 2000);
            Console.WriteLine("ping 127.0.0.1: " + reply.Status.ToString());

            sendata();

            Console.ReadLine();

        }
        static void sendata()
        {
            string sendStatusStr = string.Empty;
            sendStatusStr = ("0x530x060x34 0x300x00 0x00 0x00 0x000x020x43").Replace(" ", "").Replace("0x", "").Trim();
            int len = sendStatusStr.Length / 2;
            byte[] sendData = new byte[len];
            for (int j = 0; j < len; j++)
            {
                sendData[j] = Convert.ToByte(Convert.ToInt16(sendStatusStr.Substring(j * 2, 2), 16));
            }
            try
            {
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                IPEndPoint server = new IPEndPoint(serverIP, 50000);
                UdpClient udpClient = new UdpClient();
                udpClient.Send(sendData, sendData.Length, server);
                Console.WriteLine("发送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送失败: " + ex.ToString());
                return;
            }
        }
    }
}
