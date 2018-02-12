using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Net
{
    public class NetHelper
    {
        static public string getIPAddress()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] ip = ipe.AddressList;
            for (int i = 0; i < ip.Length; i++)
            {
                if (ip[i].AddressFamily.Equals(AddressFamily.InterNetwork))
                {

                    return ip[i].ToString();
                }
            }
            return null;
        }
        static public bool pingIp(string ip)
        {
            var pingSender = new Ping();
            var reply = pingSender.Send(ip, 2000);
            return reply.Status.Equals(IPStatus.Success);
        }
    }
}
