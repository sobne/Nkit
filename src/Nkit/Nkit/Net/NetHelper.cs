using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

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

        public static bool PingIpOrDomainName(string strIpOrDName, out string msg)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 2000;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                msg = objPinReply.Status.ToString();
                if (msg == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        public static bool TestConnection(string host, int port, int millisecondsTimeout, out string msg)
        {
            TcpClient client = new TcpClient();
            try
            {
                var ar = client.BeginConnect(host, port, null, null);
                ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                msg = client.Connected.ToString();
                return client.Connected;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;
                //throw e;
            }
            finally
            {
                client.Close();
            }

        }
    }
}
