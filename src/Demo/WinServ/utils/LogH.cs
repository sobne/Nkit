using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nkit.IO
{
    public class LogH
    {
        private object _locker = new object();
        private string _dir = string.Empty;
        private string _name = string.Empty;
        public LogH():this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"),DateTime.Now.ToString("yyyyMMdd") + ".log")
        {
        }
        public LogH(string logPath, string logName)
        {
            this._dir = logPath;
            this._name = logName;
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }
        }
        public void Log(string content)
        {
            try
            {
                lock (_locker)
                {
                    using (FileStream fs= new FileStream(Path.Combine(this._dir, this._name), FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.BaseStream.Seek(0, SeekOrigin.End);
                            sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "]  " + content);
                            sw.Flush();
                            sw.Close();
                        }
                        fs.Close();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
