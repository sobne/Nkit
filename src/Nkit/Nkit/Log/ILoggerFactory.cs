using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Log
{
    public interface ILoggerFactory
    {
        ILogger GetLog();
        ILogger GetLog(string name);
        ILogger GetLog(string name, string module);
    }
}
