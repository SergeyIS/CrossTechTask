using System;
using System.Diagnostics;
using CrossTechTask.DataContracts;

namespace CrossTechTask.EventLogger
{
    public class ELogger : ILogger
    {
        private EventLog _eventLog = null;
        private string _loggername = null;
        public ELogger(string loggerName)
        {
            _eventLog = new EventLog();
            _loggername = loggerName;
        }

        public void WriteLog(string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException("value has NULL value");

            try
            {
                if (!EventLog.SourceExists(_loggername))
                {
                    EventLog.CreateEventSource(_loggername, _loggername);
                }
                _eventLog.Source = _loggername;
                _eventLog.WriteEntry(value);
            }
            catch { }
        }
        public void WriteLog(string value, Exception e)
        {
            if (String.IsNullOrEmpty(value) || e == null)
                throw new ArgumentNullException("value or e has NULL value");

            try
            {
                if (!EventLog.SourceExists(_loggername))
                {
                    EventLog.CreateEventSource(_loggername, _loggername);
                }
                _eventLog.Source = _loggername;
                _eventLog.WriteEntry($"{value}:{e.Message}");
            }
            catch { }
        }
    }
}
