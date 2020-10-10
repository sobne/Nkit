using System;
using System.IO;

namespace Nkit.Log
{
    public class LoggerUtil
    {
        private static string _rootPath = string.Empty;
        private static DateTime _lastClearTime= DateTime.MinValue;
        private static string GetLogWithTime(string module, string message, string logType)
        {
            module = string.IsNullOrEmpty(module) ? "" : module.PadRight(5,' ');
            return string.Format("[{0}] {1} {2}   {3}", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), module, logType.Substring(0, 1), message);
            //return $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] {module} {logType.Substring(0,1)}   {message}";
        }
        private static string GetName(string name, string logType="")
        {
            if(string.IsNullOrEmpty(logType))
                return string.Format("{0}_{1}.log", string.IsNullOrEmpty(name) ? "log" : name, DateTime.Now.ToString("yyyy-MM-dd"));
            else
                return string.Format("{0}_{1}_{2}.log", string.IsNullOrEmpty(name) ? "log" : name, logType, DateTime.Now.ToString("yyyy-MM-dd"));
        }
        private static void DelLog()
        {
            if (_lastClearTime==DateTime.MinValue)
            {
                ClearFolder();//excute when start
            }
            TimeSpan ts = DateTime.Now - _lastClearTime;
            if(ts.TotalDays>1)//excute 1time/day
            {
                ClearFolder();
            }
        }
        private static void ClearFolder()
        {
            try
            {
                _lastClearTime = DateTime.Now;
                var dtLine = DateTime.Today.AddMonths(-6);
                var folder = new DirectoryInfo(_rootPath);
                foreach (var dir in folder.GetDirectories())
                {
                    var dtDir = DateTime.ParseExact(dir.Name, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                    if (dtLine > dtDir)
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        public static void Append(string message, string name="", string module = "",  LoggerType logType= LoggerType.Debug, bool logTypeDist =false)
        {
            if (string.IsNullOrEmpty(_rootPath))
            {
                _rootPath = AppDomain.CurrentDomain.BaseDirectory + "Log";
                FileHelper.CreateFloder(_rootPath);
            }
            var path = _rootPath + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            FileHelper.CreateFloder(path);

            DelLog();

            var fileName = logTypeDist ? GetName(name, logType.ToString()) : GetName(name);
            var logFile =System.IO.Path.Combine(path, fileName);

            var content = GetLogWithTime(module, message,logType.ToString());
            Console.WriteLine(content);
            FileHelper.Append(logFile, content);
        }
        public static void Append(LoggerModel logger, bool logTypeDist = false)
        {
            Append(logger.Message, logger.Name, logger.Module, logger.LogType, logTypeDist);
        }
    }
    
}
