using System;
using System.IO;
using System.Text;

namespace AppLib.Common
{
    /// <summary>
    /// Identifies the logging level
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// Warning
        /// </summary>
        Warning,
        /// <summary>
        /// Degug
        /// </summary>
        Debug,
        /// <summary>
        /// Information
        /// </summary>
        Info
    }

    /// <summary>
    /// A simple and minimal logger that outputs CSV log files or to the console
    /// If log file is not writeable, the logger fails silently
    /// </summary>
    public static class CSVLogger
    {
        private static StreamWriter _writer;

        static CSVLogger()
        {
            ConsoleOutput = true;
            IsEnabled = true;
            ConsoleFilter = LogLevel.Info | LogLevel.Warning | LogLevel.Error;
            FileFilter = LogLevel.Info | LogLevel.Debug | LogLevel.Error | LogLevel.Warning;
        }

        /// <summary>
        /// Gets or sets console output option. Default value is true
        /// </summary>
        public static bool ConsoleOutput
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state of the logger. Default is true
        /// </summary>
        public static bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Set console filter level. Default is info, warning, error
        /// </summary>
        public static LogLevel ConsoleFilter
        {
            get;
            set;
        }

        /// <summary>
        /// File filter level. Default is everything
        /// </summary>
        public static LogLevel FileFilter
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the log file output.
        /// </summary>
        /// <param name="path">Path of the file to write to.</param>
        public static void SetLogFile(string path)
        {
            try
            {
                _writer = File.CreateText(path);
            }
            catch (IOException) { }
        }

        /// <summary>
        /// Closes the log file
        /// </summary>
        public static void CloseLog()
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }
        }

        #region Logger prototype functions
        private static bool DecideFilter(LogLevel big, LogLevel subset)
        {
            return (big & subset) == subset;
        }

        private static void LogToFile(LogLevel level, string message, params string[] additional)
        {
            if (!IsEnabled) return;
            try
            {
                if (!DecideFilter(FileFilter, level)) return;
                string line = String.Format("{0};{1};{2}", DateTime.Now, level, message);
                if (additional.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(';');
                    foreach (var ad in additional)
                    {
                        sb.Append(ad);
                        sb.Append(';');
                    }
                    line += additional;
                }
                _writer.WriteLine(line);
            }
            catch (IOException) { }
        }

        private static void LogToConsole(LogLevel level, string message)
        {
            if (!IsEnabled) return;
            if (!DecideFilter(FileFilter, level)) return;
            var dt = DateTime.Now.ToShortTimeString();
            var current = Console.ForegroundColor;

            if (level == LogLevel.Error)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0}\t{1}\t{2}", dt, level, message);

            if (level == LogLevel.Error)
                Console.ForegroundColor = current;
        }
        #endregion

        /// <summary>
        /// Add a log entry. If additional informations are given, then
        /// they are only logged to the output file
        /// </summary>
        /// <param name="level">Log entry level</param>
        /// <param name="message">Log message</param>
        /// <param name="additional">Addititonal informations.</param>
        public static void Log(LogLevel level, string message, params string[] additional)
        {
            LogToFile(level, message);
            if (ConsoleOutput) LogToConsole(level, message);
        }

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="ex">Exception data</param>
        public static void Exception(Exception ex)
        {
            Log(LogLevel.Error, "Exception", ex.Message, ex.StackTrace, ex.Source);
        }

        /// <summary>
        /// Logs an error event
        /// </summary>
        /// <param name="message">Log content message</param>
        public static void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        /// <summary>
        /// Logs a warning event
        /// </summary>
        /// <param name="message">Log content message</param>
        public static void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Log content message</param>
        public static void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <param name="message">Log content message</param>
        public static void Info(string message)
        {
            Log(LogLevel.Info, message);
        }
    }
}
