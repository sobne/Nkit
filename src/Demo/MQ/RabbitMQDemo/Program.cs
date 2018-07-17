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
        private static readonly string _appID = ConfigurationManager.AppSettings["AppID"];
        static void Main(string[] args)
        {
            string method = ConfigurationManager.AppSettings["method"];
            switch (method)
            {
                case "ttlpublisher":
                    ttlpublisher();
                    break;
                case "ttlconsumer":
                    ttlconsumer();
                    break;
                case "basicpublisher":
                    basicpublisher();
                    break;
                case "basicconsumer":
                    basicconsumer();
                    break;
                case "subcribepublish":
                    subcribepublish();
                    break;
                case "subcribereceive":
                    subcribereceive();
                    break;
                case "highpublish":
                    highpublish();
                    break;
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
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queue = string.Format("MQ{0}.TTL", _appID);
                    var args = new Dictionary<string, object>();
                    args.Add("x-message-ttl", 10000);
                    channel.QueueDeclare(queue, false, false, false, args);

                    Console.WriteLine("准备接收消息(消息10s后过期)：");
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
                    }
                }
            }
        }
        #endregion
        #region basic
        static void basicpublisher()
        {
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
        static void subcribepublish()
        {
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
        #endregion

        #region high
        static void highpublish()
        {
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
            var factory = new ConnectionFactory { Uri = ConfigurationManager.AppSettings["RabbitMQUri"] };
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
