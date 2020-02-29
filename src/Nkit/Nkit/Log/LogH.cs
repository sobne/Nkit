using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace Nkit.Log
{
    public class QueueLogModel
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public LoggerType LogType { get; set; }
    }
    public static class LogH
    {
        private static ConcurrentQueue<QueueLogModel> queueLog { get; set; }
        static LogH()
        {
            queueLog = new ConcurrentQueue<QueueLogModel>();
            Start();
        }
        private static void Start()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    ConsumeMessage();
                    Thread.Sleep(500);
                }
            });
        }
        private static void ConsumeMessage()
        {
            while (queueLog.Count > 0)
            {
                queueLog.TryDequeue(out QueueLogModel logModel);
                LoggerUtil.Append(logModel.Message, logModel.Name, logModel.Module, logModel.LogType);
                Thread.Sleep(1);
            }
        }
        private static void ProduceMessage(string message, string name = "", string module = "", LoggerType loggerType = LoggerType.Debug, bool logTypeDist = false)
        {
            queueLog.Enqueue(new QueueLogModel
            {
                Message=message,
                Name=name,
                Module=module,
                LogType=loggerType
            });
        }
        public static void Debug(string message, string name = "", string module = "")
        {
            ProduceMessage(message, name, module, LoggerType.Debug);
        }

        public static void Debug(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Debug(text);
        }

        public static void Error(string message, string name = "", string module = "")
        {
            ProduceMessage(message, name, module, LoggerType.Error);
        }

        public static void Error(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Error(text);
        }

        public static void Fatal(string message, string name = "", string module = "")
        {
            ProduceMessage(message, name, module, LoggerType.Fatal);
        }

        public static void Fatal(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Fatal(text);
        }

        public static void Info(string message, string name = "", string module = "")
        {
            ProduceMessage(message, name, module, LoggerType.Info);
        }

        public static void Info(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Info(text);
        }

        public static void Warn(string message, string name = "", string module = "")
        {
            ProduceMessage(message, name, module, LoggerType.Warn);
        }

        public static void Warn(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Warn(text);
        }
    }
}
