using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// Exception class extensions
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Writes an exception's details to the debugger, if it's attached
        /// </summary>
        /// <param name="ex">Exception to dump</param>
        public static void WriteToDebugger(this Exception ex)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Exception occured in: {0} of: {1}", ex.TargetSite, ex.Source);
                Debug.WriteLine("Message: {0}", ex.Message);
                Debug.WriteLine("Stack trace:\n {0}\n\n", ex.StackTrace);
            }
        }

        /// <summary>
        /// Creates a crash log based on the exception data and
        /// opens the crash log in notepad
        /// </summary>
        /// <param name="ex">The source of the crash dump</param>
        /// <param name="AdditionalText">Additonal text to add to the crash dump</param>
        public static void CreateCrashLog(this Exception ex, string AdditionalText = null)
        {
            var temp = Path.GetTempFileName();
            using (var text = File.CreateText(temp))
            {
                var appname = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                text.WriteLine("{0} has crashed.", appname);
                text.WriteLine("Crash time: {0}", DateTime.Now);
                if (!string.IsNullOrEmpty(AdditionalText))
                    text.WriteLine(AdditionalText);
                text.WriteLine("\n\nException message: {0}", ex.Message);
                text.WriteLine("Source: {0}", ex.Source);
                text.WriteLine("Target site: {0}", ex.TargetSite);
                text.WriteLine("Stack Trace: {0}", ex.StackTrace);
                text.WriteLine("Inner exceptions:");
                text.WriteLine(GetInnerExceptionDetails(ex));
            }
            Process.Start(temp);
        }

        private static string GetInnerExceptionDetails(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            Exception iex = null;
            int level = 0;
            while ((iex = ex.InnerException) != null)
            {
                sb.AppendFormat("Level : {0}\n", level);
                sb.AppendFormat("Message: {0}\nSouce: {1}", iex.Message, iex.Source);
                sb.AppendFormat("Target: {0}\nStack Trace: {1}\n", iex.TargetSite, iex.StackTrace);
            }
            return sb.ToString();
        }
    }
}
