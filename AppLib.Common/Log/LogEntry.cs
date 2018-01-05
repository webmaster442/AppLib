using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AppLib.Common.Log
{
    /// <summary>
    /// A Log entry
    /// </summary>
    [Serializable]
    public class LogEntry
    {
        /// <summary>
        /// Log event Time stamp
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Message level
        /// </summary>
        public MessageLevel Level { get; set; }

        /// <summary>
        /// Main log Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Additional related information
        /// </summary>
        public List<string> AdditionalInfo { get; set; }

        /// <summary>
        /// Creates a new instance of Log Entry
        /// </summary>
        public LogEntry()
        {
        }

        /// <summary>
        /// Creates a new instance of Log entry
        /// </summary>
        /// <param name="level">Message level</param>
        /// <param name="message">Log message</param>
        /// <param name="additionals">Additional information</param>
        public LogEntry(MessageLevel level, string message, params object[] additionals)
        {
            Date = DateTime.Now;
            Message = message;
            if (additionals != null && additionals.Length > 0)
            {
                AdditionalInfo = new List<string>(additionals.Length);
                foreach (var additional in additionals)
                {
                    AdditionalInfo.Add(additional.ToString());
                }
            }
        }

        /// <summary>
        /// Converts this instance to string
        /// </summary>
        /// <returns>string representation of log entry</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}: {2}", Date, Level, Message);
        }
    }
}
