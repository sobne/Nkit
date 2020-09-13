using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQDemo
{
    class Program
    {
        private static string _appID = string.Empty;
        private static string _url = string.Empty;
        static void Main(string[] args)
        {
            _appID = ConfigurationManager.AppSettings["AppID"];
            _url = ConfigurationManager.AppSettings["RabbitMQUri"];
            //string method = ConfigurationManager.AppSettings["method"];
            Console.WriteLine(">>>>please select the function<<<");
            Console.WriteLine("A    ttlpublisher         a    ttlconsumer");
            Console.WriteLine("B    basicpublisher       b    basicconsumer");
            Console.WriteLine("C    subcribeproducer     c    subcribeconsumer");
            Console.WriteLine("D    subcribepublish      d    subcribereceive");
            Console.WriteLine("D1    subcribepublish1     d1    subcribereceive1");
            Console.WriteLine("E    highpublish          e    highreceive");
            var input = Console.ReadLine();
            switch (input)
            {
                case "A":
                case "ttlpublisher":
                    ttlpublisher();
                    break;
                case "a":
                case "ttlconsumer":
                    ttlconsumer();
                    break;
                case "B":
                case "basicpublisher":
                    basicpublisher();
                    break;
                case "b":
                case "basicconsumer":
                    basicconsumer();
                    break;
                case "C":
                case "subcribeproducer":
                    subcribeproducer();
                    break;
                case "c":
                case "subcribeconsumer":
                    subcribeconsumer();
                    break;
                case "D":
                case "subcribepublish":
                    subcribepublish();
                    break;
                case "d":
                case "subcribereceive":
                    subcribereceive();
                    break;
                case "D1":
                case "subcribepublish1":
                    subcribepublish1();
                    break;
                case "d1":
                case "subcribereceive1":
                    subcribereceive1();
                    break;
                case "E":
                case "highpublish":
                    highpublish();
                    break;
                case "e":
                case "highreceive":
                    highreceive();
                    break;
                default:
                    break;
            }
            Console.ReadLine();
        }
        static void writeLine(string msg)
        {
            Console.WriteLine("{0}:{1}", DateTime.Now.ToString("MM-dd HH:mm:ss.fff"), msg);
        }
        #region ttl
        static void ttlpublisher()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.TTL", _appID);
                    var args = new Dictionary<string, object>();
                    args.Add("x-message-ttl", 10000);
                    channel.QueueDeclare(queue, false, false, false, args);

                    while (true)
                    {
                        Console.Write("请输入要发送的消息(消息10s后过期)：");
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", queue, null, body);

                        writeLine(string.Format("已发送的消息：{0}",message));
                    }
                }
            }
        }
        static void ttlconsumer()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.TTL", _appID);
                    var args = new Dictionary<string, object>();
                    args.Add("x-message-ttl", 10000);
                    channel.QueueDeclare(queue, false, false, false, args);

                    Console.WriteLine("准备接收消息(消息10s后过期).输入'get'开始获取");
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (input.ToLower() == "quit")
                        {
                            return;
                        }
                        else if (input.ToLower() == "get")
                        {
                            while (true)
                            {
                                var bgr = channel.BasicGet(queue, true);
                                if (bgr == null)
                                {
                                    writeLine("没有消息");
                                    break;
                                }
                                else
                                {
                                    var message = Encoding.UTF8.GetString(bgr.Body);
                                    writeLine(string.Format("接收消息-总数[{0}] - {1}", bgr.MessageCount, message));
                                }
                                Thread.Sleep(20);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        #endregion
        #region basic
        static void basicpublisher()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.BaseStudy", _appID);

                    channel.QueueDeclare(queue, false, false, false, null);  

                    while (true)
                    {
                        Console.Write("请输入要发送的消息：");
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", queue, null, body); 

                        Console.WriteLine("已发送的消息： {0}", message);
                    }
                }
            }
        }
        static void basicconsumer()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.BaseStudy", _appID);

                    channel.QueueDeclare(queue, false, false, false, null);

                    Console.WriteLine("准备接收消息：");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (s, e) =>
                    {
                        var body = e.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("接收到的消息： {0}", message);
                    };
                    channel.BasicConsume(queue, true, consumer);

                    Console.ReadLine();
                }
            }
        }
        #endregion
        #region subcribe
        static void subcribeproducer()
        {
            string exchange = string.Format("Ex{0}.sc", _appID);
            var routingKey = "*.rabbit";

            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange, "topic");

                    while (true)
                    {
                        Console.Write("请输入要发送的消息：");
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        IBasicProperties props = channel.CreateBasicProperties();
                        props.DeliveryMode = 2;
                        props.Expiration = "120000";


                        channel.BasicPublish(exchange, routingKey, props, body);

                        Console.WriteLine("已发送的消息： '{0}':'{1}'", routingKey, message);
                    }
                }
            }
        }
        static Dictionary<string, Tuple<string, IModel>> _dicCH = new Dictionary<string, Tuple<string, IModel>>();
        static void subcribeconsumer()
        {
            string exchange = string.Format("Ex{0}.sc", _appID);
            var routingKey = "*.rabbit";

            var factory = new ConnectionFactory { Uri = _url };
            var connection = factory.CreateConnection();
            
            for (int i = 0; i < 5; i++)
            {
                var channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange, "topic");
                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queueName, exchange, routingKey);

                _dicCH.Add("get" + i, new Tuple<string, IModel>(queueName, channel));
            }

            Console.WriteLine("get[0-4]已经订阅消息>>>");

            Console.WriteLine("输入get[0-4]获取消息");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    connection.Close();
                    return;
                }
                else if (input.ToLower().StartsWith("get"))
                {
                    while (true)
                    {
                        var bgr = _dicCH[input].Item2.BasicGet(_dicCH[input].Item1, true);
                        if (bgr == null)
                        {
                            writeLine("没有消息");
                            break;
                        }
                        else
                        {
                            var message = Encoding.UTF8.GetString(bgr.Body);
                            writeLine(string.Format("接收消息-总数[{0}] - {1}", bgr.MessageCount, message));
                        }
                        Thread.Sleep(20);
                    }

                }
            }
                //using (var channel = connection.CreateModel())
                //{

                //    channel.ExchangeDeclare(exchange, "topic");
                //    var queueName = channel.QueueDeclare().QueueName;

                //    channel.QueueBind(queueName, exchange, routingKey);

                //    Console.WriteLine("准备接收消息>>>");

                //    Console.WriteLine("输入get获取消息");
                //    while (true)
                //    {
                //        string input = Console.ReadLine();
                //        if (input.ToLower() == "quit")
                //        {
                //            return;
                //        }
                //        else if (input.ToLower() == "get")
                //        {
                //            while (true)
                //            {
                //                var bgr = channel.BasicGet(queueName, true);
                //                if (bgr == null)
                //                {
                //                    writeLine("没有消息");
                //                    break;
                //                }
                //                else
                //                {
                //                    var message = Encoding.UTF8.GetString(bgr.Body);
                //                    writeLine(string.Format("接收消息-总数[{0}] - {1}", bgr.MessageCount, message));
                //                }
                //                Thread.Sleep(20);
                //            }

                //        }
                //    }
                //}
            
        }
        #endregion
        #region subcribe
        static void subs(int i)
        {
            var routingKey = "*.rabbit";
            var message = "Hello World" + DateTime.Now.ToString("HH:mm:ss.fff");
            var body = Encoding.UTF8.GetBytes(message);

            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchange = string.Format("Ex{0}.Logs", _appID);

                    channel.ExchangeDeclare(exchange, "topic");

                    channel.BasicPublish(exchange, routingKey, null, body);

                    Console.WriteLine("已发送的消息：{2}  '{0}':'{1}'",i, routingKey, message);
                    Thread.Sleep(100*1000);
                }
            }
        }
        static void subcribepublish1()
        {
            while (true)
            {
                Console.Write("自动发送消息 数量：");
                var keyWithMsg = Console.ReadLine();

                var k = Convert.ToInt32(keyWithMsg);
                for (int i = 0; i < k; i++)
                {
                    Thread thread = new Thread(new ThreadStart(() => subs(i)));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }
            
        }
        static void subcribepublish()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchange = string.Format("Ex{0}.Logs",_appID);

                    channel.ExchangeDeclare(exchange, "topic"); 

                    while (true)
                    {
                        Console.Write("请输入要发送的消息，输入格式如'*.RoutingKey_Message'：");
                        var keyWithMsg = Console.ReadLine();

                        string[] args = keyWithMsg.Split('_');
                        var routingKey = args.Length > 1 ? args[0] : "*.rabbit";
                        var message = args.Length > 1 ? args[1] : "Hello World";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange, routingKey, null, body);

                        Console.WriteLine("已发送的消息： '{0}':'{1}'", routingKey, message);
                    }
                }
            }
        }
        static void subcribereceive()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchange = string.Format("Ex{0}.Logs", _appID);

                    channel.ExchangeDeclare(exchange, "topic");    
                    var queueName = channel.QueueDeclare().QueueName; 

                    Console.Write("请输入准备监听的消息主题格式，格式如'*.rabbit'或者'info.*'或者'info.warning.error'等：");

                    while (true)
                    {
                        var bindingKey = Console.ReadLine();
                        channel.QueueBind(queueName, exchange, bindingKey); 

                        Console.WriteLine("准备接收消息");

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (s, e) =>
                        {
                            var routingKey = e.RoutingKey;
                            var body = e.Body;
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine("接收到的消息： '{0}':'{1}'", routingKey, message);
                        };
                        channel.BasicConsume(queueName, true, consumer); 
                    }
                }
            }
        }
        static void subcribereceive1()
        {
            var factory = new ConnectionFactory { Uri = _url,AutomaticRecoveryEnabled=true };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string exchange = string.Format("Ex{0}.Logs", _appID);

                    channel.ExchangeDeclare(exchange, "topic");
                    var queueName = channel.QueueDeclare().QueueName;

                    Console.Write("请输入准备监听的消息主题格式，格式如'*.rabbit'或者'info.*'或者'info.warning.error'等：");



                    while (true)
                    {
                        var bindingKey = Console.ReadLine();
                        channel.QueueBind(queueName, exchange, bindingKey);

                        var consumer = new QueueingBasicConsumer(channel);
                        channel.BasicConsume(queueName, true, consumer);

                        Console.WriteLine(" [*] Waiting for messages." +
                                          "To exit press CTRL+C");
                        while (true)
                        {
                            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            Console.WriteLine(" [x] {0}", message);
                        }
                        
                    }
                }
            }
        }
        #endregion

        #region high
        static void highpublish()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.TaskQueue", _appID);

                    channel.QueueDeclare(queue, true, false, false, null);   //定义一个支持持久化的消息队列
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;    //1表示不持久，2表示持久化

                    Console.WriteLine("请注意：演示耗时较长的消息时，可通过发送带有‘.’的内容去模拟，每个‘.’加1秒！");

                    while (true)
                    {
                        Console.Write("请输入要发送的消息：");
                        var message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", queue, properties, body); 

                        Console.WriteLine("已发送的消息： {0}", message);
                    }
                }
            }
        }
        static void highreceive()
        {
            var factory = new ConnectionFactory { Uri = _url };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.TaskQueue", _appID);

                    channel.QueueDeclare(queue, true, false, false, null);

                    channel.BasicQos(0, 1, false);  //在一个消费者还在处理消息且没响应消息之前，不要给他分发新的消息，而是将这条新的消息发送给下一个不那么忙碌的消费者

                    Console.WriteLine("准备接收消息：");
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (s, e) =>
                    {
                        var message = Encoding.UTF8.GetString(e.Body);
                        SimulationTask(message);

                        channel.BasicAck(e.DeliveryTag, false); //手动Ack：用来确认消息已经被消费完成了
                    };
                    channel.BasicConsume(queue, false, consumer);    //开启消费者与通道、队列关联
                    //channel.BasicConsume(queue, true, consumer);    //开启消费者与通道、队列关联；自动Ack

                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// 模拟消息任务的处理过程
        /// </summary>
        /// <param name="message">消息</param>
        static void SimulationTask(string message)
        {
            Console.WriteLine("接收的消息： {0}", message);
            int dots = message.Split('.').Length - 1;
            Thread.Sleep(dots * 1000);
            Console.WriteLine("接收的消息处理完成，现在时间为{0}！", DateTime.Now);
        }
        #endregion
    }
}
