using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace Webmaster442.Applib
{
    //based on https://weblog.west-wind.com/posts/2016/May/13/Creating-Single-Instance-WPF-Applications-that-open-multiple-Files
    /// <summary>
    /// A Signle Instance application helper
    /// </summary>
    public sealed class SingleInstanceApp: IDisposable
    {
        private static Mutex _mutex;
        private readonly bool _isfirst;
        private readonly string _UID;
        private const string EXIT_STRING = "__EXIT__";
        private Thread _server;
        private bool _isRunning = false;

        /// <summary>
        /// Event that occures when command line parameter are available for processing
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event EventHandler<string> CommandLineArgumentsRecieved;

        /// <summary>
        /// Creates a new single instance app
        /// </summary>
        /// <param name="AppName">Application Name</param>
        public SingleInstanceApp(string AppName)
        {
            _UID = GetUnique(AppName);
            _mutex = new Mutex(true, _UID, out _isfirst);
            _server = new Thread(ServerThread);
            _isRunning = true;
            _server.Start();
        }

        /// <summary>
        /// Returns true, if the current app is the first started instance
        /// </summary>
        public bool IsFirstInstance
        {
            get { return _isfirst; }
        }

        /// <summary>
        /// Close the App
        /// </summary>
        public void Close()
        {
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
                _mutex.Dispose();
                _mutex = null;
            }
            _isRunning = false;
            Write(EXIT_STRING);
            Thread.Sleep(3); // give time for thread shutdown
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private bool Write(string text, int connectTimeout = 300)
        {
            using (var client = new NamedPipeClientStream(_UID))
            {
                try { client.Connect(connectTimeout); }
                catch { return false; }

                if (!client.IsConnected) return false;

                using (StreamWriter writer = new StreamWriter(client))
                {
                    writer.Write(text);
                    writer.Flush();
                }
            }
            return true;
        }

        /// <summary>
        /// Submints recieved command line parameters to an allready running instance of the app
        /// </summary>
        public void SubmitParameters()
        {
            var pars = Environment.GetCommandLineArgs();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < pars.Length; i++)
            {
                sb.AppendFormat("{0}\n", pars[i]);
            }
            Write(sb.ToString());
        }

        private static string GetUnique(string AppName)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(AppName);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private void ServerThread()
        {
            while (true)
            {
                string text;
                using (var server = new NamedPipeServerStream(_UID))
                {
                    server.WaitForConnection();

                    using (StreamReader reader = new StreamReader(server))
                    {
                        text = reader.ReadToEnd();
                    }
                }

                if (text == EXIT_STRING) break;

                CommandLineArgumentsRecieved?.Invoke(this, text);
                if (_isRunning == false) break;
            }
        }

        /// <summary>
        /// Dispose fields
        /// </summary>
        public void Dispose()
        {
            Close();
        }
    }

}
