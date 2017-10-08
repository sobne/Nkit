using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Nkit.Core.Utils;
using Nkit.Core;

namespace Nkit.Net
{
    public class SocketUtils
    {
        private string _host { get; set; }
        private int _port { get; set; }
        private Socket _socket { get; set; }
        public SocketUtils(string host, int port)
        {
            _host = host;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(host, port);
        }
        public string SendString(string data)
        {
            return Send(Encoding.Default.GetBytes(data));
        }
        public string SendHex(string hex)
        {
            return Send(Convertor.Hex2Bytes(hex));
        }
        public string Send(byte[] buffer)
        {
            string result = string.Empty;
            _socket.Send(buffer);
            result = Receive(_socket, 5000 * 2);

            return result;
        }
        public void Shutdown()
        {
            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
            }
            _socket.Close();
        }
        private string Receive(Socket _socket,int timeout)
        {
            string result = string.Empty;
            _socket.ReceiveTimeout = timeout;
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[1024];
            int length = 0;
            try
            {
                while ((length = _socket.Receive(buffer)) > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        data.Add(buffer[j]);
                    }
                    if (length < buffer.Length)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                result=e.ToString();
            }
            if (data.Count > 0)
            {
                result = Encoding.Default.GetString(data.ToArray(), 0, data.Count);
            }

            return result;
        }

    }
}
