using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nkit.Core.Utils
{
    public class EventLogger
    {
        static public void Error(string logName, string source, string message)
        {
            Log(logName, source, message, EventLogEntryType.Error);
        }
        static public void Warning(string logName, string source, string message)
        {
            Log(logName, source, message, EventLogEntryType.Warning);
        }
        static public void Information(string logName, string source, string message)
        {
            Log(logName, source, message, EventLogEntryType.Information);
        }
        static public void SuccessAudit(string logName, string source, string message)
        {
            Log(logName, source, message, EventLogEntryType.SuccessAudit);
        }
        static public void FailureAudit(string logName, string source, string message)
        {
            Log(logName, source, message, EventLogEntryType.FailureAudit);
        }
        static private void Log(string logName, string source, string message, EventLogEntryType type)
        {
            var el = new EventLog();
            try
            {
                if (!EventLog.SourceExists(source))
                {
                    if (EventLog.Exists(logName)) el.Log = logName;
                    else EventLog.CreateEventSource(source, logName);
                }
                el.Source = source;
                el.WriteEntry(message, type);
            }
            catch (Exception ex)
            {
                el.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
