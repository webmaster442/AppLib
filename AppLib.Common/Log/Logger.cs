using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AppLib.Common.Log
{
    /// <summary>
    /// An XML logger
    /// </summary>
    public sealed class Logger : ILogger, INotifyCollectionChanged
    {
        private readonly List<LogEntry> _log;

        /// <summary>
        /// Collection changed event
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void Add(LogEntry e)
        {
            if (AutoClearCounter > 0 && _log.Count > AutoClearCounter)
            {
                _log.Clear();
            }
            Debug.WriteLine(e);
            _log.Add(e);
            CallCollectionChanged(NotifyCollectionChangedAction.Add);

        }

        private void CallCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }

        /// <summary>
        /// Creates a new instance of XML logger
        /// </summary>
        public Logger()
        {
            AutoClearCounter = 100;
            _log = new List<LogEntry>(100);
        }

        /// <summary>
        /// Get All log entries
        /// </summary>
        public IEnumerable<LogEntry> All
        {
            get { return _log; }
        }

        /// <summary>
        /// Get all Error Log entries
        /// </summary>
        public IEnumerable<LogEntry> Errors
        {
            get { return _log.Where(entry => entry.Level == MessageLevel.Error); }
        }

        /// <summary>
        /// Get all Info Log entries
        /// </summary>
        public IEnumerable<LogEntry> Infos
        {
            get { return _log.Where(entry => entry.Level == MessageLevel.Info); }
        }

        /// <summary>
        /// Get all Warning Log entries
        /// </summary>
        public IEnumerable<LogEntry> Warnings
        {
            get { return _log.Where(entry => entry.Level == MessageLevel.Warning); }
        }

        /// <summary>
        /// Get the Last 10 entry
        /// </summary>
        public IEnumerable<LogEntry> Last10
        {
            get { return _log.Skip(Math.Max(0, _log.Count - 10)); }
        }

        /// <summary>
        /// Automaticaly clear the log after this number of entries. To disable set it to -1
        /// </summary>
        public int AutoClearCounter
        {
            get;
            set;
        }

        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="ex">Exception data</param>
        public void Error(Exception ex)
        {
            Add(new LogEntry(MessageLevel.Error, ex.Message, ex.Source, ex.StackTrace));
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        public void Error(string message, params object[] additionalData)
        {
            Add(new LogEntry(MessageLevel.Error, message, additionalData));
        }

        /// <summary>
        /// Log an information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        public void Info(string message, params object[] additionalData)
        {
            Add(new LogEntry(MessageLevel.Info, message, additionalData));
        }

        /// <summary>
        /// Log a Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        public void Warning(string message, params object[] additionalData)
        {
            Add(new LogEntry(MessageLevel.Warning, message, additionalData));
        }

        /// <summary>
        /// Save log to a file
        /// </summary>
        /// <param name="Filename">File to write</param>
        public void SaveToFile(string Filename)
        {
            using (var fs = File.Create(Filename))
            {
                SaveToStream(fs);
            }
        }

        /// <summary>
        /// Save log to a stream as serialized XML
        /// </summary>
        /// <param name="target">Stream to write to</param>
        public void SaveToStream(Stream target)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<LogEntry>));
            xs.Serialize(target, _log);
        }

        /// <summary>
        /// Clear Log entries
        /// </summary>
        public void Clear()
        {
            int count = _log.Count;
            _log.Clear();
            CallCollectionChanged(NotifyCollectionChangedAction.Reset);
            _log.Add(new LogEntry(MessageLevel.Info, $"Log cleared. Deleted {count} entries."));
            CallCollectionChanged(NotifyCollectionChangedAction.Add);
        }
    }

}
