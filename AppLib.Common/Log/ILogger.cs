using System;
using System.Collections.Generic;
using System.IO;

namespace AppLib.Common.Log
{
    /// <summary>
    /// Logger Interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        void Error(string message, params object[] additionalData);
        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="ex">Exception data</param>
        void Error(Exception ex);
        /// <summary>
        /// Log a Warning
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        void Warning(string message, params object[] additionalData);
        /// <summary>
        /// Log an information
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="additionalData">Additional data objects</param>
        void Info(string message, params object[] additionalData);
        /// <summary>
        /// Save log to a stream as serialized XML
        /// </summary>
        /// <param name="target">Stream to write to</param>
        void SaveToStream(Stream target);
        /// <summary>
        /// Save log to a file as serialized XML
        /// </summary>
        /// <param name="Filename"></param>
        void SaveToFile(string Filename);
        /// <summary>
        /// Get all Error Log entries
        /// </summary>
        IEnumerable<LogEntry> Errors { get; }
        /// <summary>
        /// Get all Warning Log entries
        /// </summary>
        IEnumerable<LogEntry> Warnings { get; }
        /// <summary>
        /// Get all Info Log entries
        /// </summary>
        IEnumerable<LogEntry> Infos { get; }
        /// <summary>
        /// Get All log entries
        /// </summary>
        IEnumerable<LogEntry> All { get; }
        /// <summary>
        /// Get the Last 10 entry
        /// </summary>
        IEnumerable<LogEntry> Last10 { get; }
        /// <summary>
        /// Clear Log entries
        /// </summary>
        void Clear();
        /// <summary>
        /// Automaticaly clear the log after this number of entries. To disable set it to -1
        /// </summary>
        int AutoClearCounter { get; set; }
    }
}
