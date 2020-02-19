using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Log
{
    public interface ILogger
    {
        void Debug(string text);
        void Debug(string format, params object[] args);
        void Error(string text);
        void Error(string format, params object[] args);
        void Fatal(string text);
        void Fatal(string format, params object[] args);
        void Info(string text);
        void Info(string format, params object[] args);
        void Warn(string text);
        void Warn(string format, params object[] args);
    }
}
