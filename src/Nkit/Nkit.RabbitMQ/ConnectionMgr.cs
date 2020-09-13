using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nkit.RabbitMq
{
    public class ConnectionMgr
    {
        public delegate void ReconnectedEventArgs();
        public event ReconnectedEventArgs OnReconnected;
        private static ConnectionMgr _instance;
        private static readonly object Locker = new object();
        private string _uri = "";
        public bool IsConnected = false;
        public IConnection Connection=null;
        private ConnectionMgr()
        {
        }
        public static ConnectionMgr GetInstance()
        {
            if(_instance ==null)
            {
                lock(Locker)
                {
                    if(_instance==null)
                    {
                        _instance = new ConnectionMgr();
                    }
                }
            }
            return _instance;
        }
        public void Close()
        {
            //if (null != _consumer && _consumer.IsRunning)
            //{
            //    if (null != _channel) _channel.Close();
            //    if (null != Connection) Connection.Close();
            //}
        }
        public void Connect(string uri,bool autoReconnect=false)
        {
            _uri = uri;
            try
            {
                InitConnection();
                log("connected");
            }
            catch (Exception)
            {
                if(autoReconnect)
                {
                    ReConnect();
                }
            }
        }
        private void InitConnection()
        {
            var factory = new ConnectionFactory { Uri = _uri };
            Connection = factory.CreateConnection();
            IsConnected = true;
            Connection.ConnectionBlocked += Connection_ConnectionBlocked;
            Connection.ConnectionShutdown += Connection_ConnectionShutdown;
            Connection.ConnectionUnblocked += Connection_ConnectionUnblocked;
            Connection.CallbackException += Connection_CallbackException;
        }

        private void ReConnect()
        {
            new Thread(ReConnecting)
            {
                IsBackground = true
            }.Start();
        }

        private void ReConnecting()
        {
            while (true)
            {
                log("ReConnecting");
                try
                {
                    InitConnection();
                    OnReconnected?.Invoke();
                    log("Reconnected");
                    break;
                }
                catch
                {
                }
                Thread.Sleep(60000);
            }
        }

        private void Connection_CallbackException(object sender, RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Connection_ConnectionUnblocked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            log("Connection_ConnectionShutdown");
            //Instance.Abort();
            //Instance.Close();
            IsConnected = false;
            ReConnect();
            //throw new NotImplementedException();
        }

        private void Connection_ConnectionBlocked(object sender, RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void log(string str)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log\\rabbitmqconnection.txt", true))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + " " + str);
                }
            }
            catch
            {   
            }
            
        }
    }
}
