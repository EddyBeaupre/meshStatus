using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meshUtils
{
    public class EventLogger
    {
        private EventLog eventLog;
        private EventLogEntryType logLevel;

        public EventLogger(EventLog eventlog, String source, String logtype, EventLogEntryType loglevel)
        {
            eventLog = eventlog;
            eventLog.Source = source;
            eventLog.Log = logtype;
            logLevel = loglevel;
        }

        public EventLogger(EventLog eventlog, String source, String logtype)
        {
            eventLog = eventlog;
            eventLog.Source = source;
            eventLog.Log = logtype;
            logLevel = EventLogEntryType.Information;
        }

        public void WriteEntry(String message, EventLogEntryType type, Exception ex)
        {
            if (eventLog != null)
            {
                if (type < EventLogEntryType.Error)
                {
                    type = EventLogEntryType.Error;
                }

                if (type > EventLogEntryType.FailureAudit)
                {
                    type = EventLogEntryType.FailureAudit;
                }

                if (message != String.Empty)
                {
                    if (type <= logLevel)
                    {
                        if (ex != null)
                        {
                            eventLog.WriteEntry( message + Environment.NewLine + ex.ToString(), type);
                        }
                        else
                        {
                            eventLog.WriteEntry(message, type);
                        }
                    }
                }
            }
        }

        public void WriteEntry(String message, EventLogEntryType type)
        {
            WriteEntry(message, type, null);
        }
    }
}
