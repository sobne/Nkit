using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Log
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger GetLog()
        {
            return new Logger();
        }
        public ILogger GetLog(string name)
        {
            return new Logger(name);
        }
        public ILogger GetLog(string name, string module)
        {
            return new Logger(name, module);
        }
    }
}
