using System;
using System.IO;

namespace SentryUpdater
{
    public static class Log
    {
        static Log()
        {
            Prefix = "[Update] ";
        }

        public static bool Console { get; set; }

        public static bool Debug { get; set; }

        public static string Prefix { get; set; }

        public static event EventHandler<LogEventArgs> Event;

        private static void OnEvent(string message)
        {
            if (Event != null)
            {
                Event(null, new LogEventArgs(message));
            }
        }

        public static void Write(string format, params object[] args)
        {
            string message = string.Format(format, args);

            string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)).FullName + "\\SentryUpdateLog" + DateTime.Now.ToShortDateString().Replace("/", "").Replace(" ", "") + ".txt";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(DateTime.Now + ": " + message);
            }


            //OnEvent(message);

            //if (Console)
            //{
            //    System.Console.WriteLine(message);
            //}

            //if (Debug)
            //{
            //    System.Diagnostics.Debug.WriteLine(message);
            //}
        }
    }
}
