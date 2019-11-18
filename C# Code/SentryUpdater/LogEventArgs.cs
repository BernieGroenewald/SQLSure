using System;

namespace SentryUpdater
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; private set; }
        public string Message { get; private set; }
    }
}
