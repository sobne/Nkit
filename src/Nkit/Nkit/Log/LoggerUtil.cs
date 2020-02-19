using System;

namespace Nkit.Log
{
    public class LoggerUtil
    {
        private static string GetLogWithTime(string module, string message, string logType)
        {
            module = string.IsNullOrEmpty(module) ? "" : module.PadRight(5,' ');
            return string.Format("[{0}] {1} {2}   {3}", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), module, logType.Substring(0, 1), message);
            //return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] {module} {logType.Substring(0,1)}   {message}";
        }
        private static string GetName(string name, string logType)
        {
            return string.Format("{0}_{1}_{2}.log", string.IsNullOrEmpty(name) ? "log" : name, logType, DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private static string GetName(string name)
        {
            return string.Format("{0}_{1}.log", string.IsNullOrEmpty(name) ? "log" : name, DateTime.Now.ToString("yyyy-MM-dd"));
        }

        public static void Append(string message, string name="", string module = "",  LoggerType logType= LoggerType.Debug, bool logTypeDist =false)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            FileHelper.CreateFloder(path);
            var fileName = logTypeDist ? GetName(name, logType.ToString()) : GetName(name);
            var logFile =System.IO.Path.Combine(path, fileName);

            var content = GetLogWithTime(module, message,logType.ToString());
            Console.WriteLine(content);
            FileHelper.Append(logFile, content);
        }
    }
    
}
