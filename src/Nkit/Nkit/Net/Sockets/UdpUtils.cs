using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nkit.Net.Sockets
{
    public class UdpUtils
    {
        private UdpClient _udpClient = null;
        private IPEndPoint _remoteEP = null;
        public delegate void DataReceiveEventArgs(IPEndPoint ep, byte[] buffer);
        public event DataReceiveEventArgs OnDataReceived;
        public UdpUtils(int port)
        {
            _udpClient = new UdpClient(port);

        }
        public void SetRemote(string hostname, int port)
        {
            _remoteEP= new IPEndPoint(IPAddress.Parse(hostname), port);
        }
        
        public void Send(byte[] buffer)
        {
            try
            {
                _udpClient.Send(buffer, buffer.Length, _remoteEP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BeginReceive()
        {
            var thread = new Thread(Receive)
            {
                IsBackground = true
            };
            thread.Start();

        }
        public void Receive()
        {
            while (true)
            {
                var remoteEP = _remoteEP == null ? new IPEndPoint(IPAddress.Any, 0) : _remoteEP;
                try
                {
                    var receiveString = string.Empty;
                    byte[] buffer = _udpClient.Receive(ref remoteEP);
                    if (buffer != null)
                    {
                        OnDataReceived?.Invoke(remoteEP, buffer);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
