using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace utils
{
    public class LogHelper
    {
        private object _lockerForLog = new object();
        private string _logPath = string.Empty;
        public LogHelper()
        { }
        public LogHelper(string logPath)
        {
            this._logPath = logPath;
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }
        }
        public void Log(string content)
        {
            try
            {
                lock (_lockerForLog)
                {
                    FileStream fs;
                    fs = new FileStream(Path.Combine(this._logPath, DateTime.Now.ToString("yyyyMMdd") + ".log"), FileMode.OpenOrCreate);
                    StreamWriter streamWriter = new StreamWriter(fs);
                    streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                    streamWriter.WriteLine("[" + DateTime.Now.ToString() + "]：" + content);
                    streamWriter.Flush();
                    streamWriter.Close();
                    fs.Close();
                }
            }
            catch
            {
            }
        }
    }
}
