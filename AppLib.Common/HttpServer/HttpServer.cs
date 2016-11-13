using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace AppLib.Common.HttpServer
{
    /// <summary>
    /// A HTTP server based on the HTTP listener class
    /// </summary>
    public class HttpServer
    {
        private readonly HttpListener _listener;

        /// <summary>
        /// Creates a new instance of HTTP server
        /// </summary>
        /// <param name="port">startup port. 80 is default for http, but it requires root privilages</param>
        public HttpServer(uint port)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");

            _listener = new HttpListener();
            MimeTypes = new Dictionary<string, string>(5);
            Cache = new HttpCache();
            MimeTypes.Add(".svg", "image/svg+xml");
            MimeTypes.Add(".ttf", "application/x-font-truetype");
            MimeTypes.Add(".eot", "application / vnd.ms - fontobject");
            MimeTypes.Add(".woff", "application/font-woff");
            MimeTypes.Add(".otf", "application/x-font-opentype");
        }

        /// <summary>
        /// Server startup directory
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Custom mime type database. Key is extension. Value is mime type
        /// </summary>
        public Dictionary<string, string> MimeTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Cache
        /// </summary>
        public HttpCache Cache
        {
            get;
            private set;
        }

        private string GetMime(string file)
        {
            var extension = System.IO.Path.GetExtension(file);
            if (MimeTypes.Keys.Contains(extension))
                return MimeTypes[extension];
            else
                return MimeMapping.GetMimeMapping(file);
        }

        private static string FormatPath(string path)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Xbox:
                case PlatformID.WinCE:
                    return path.Replace(@"/", @"\");
                default:
                    return path.Replace(@"\", @"/");
            }
        }

        private static string GetFileName(string rawURL)
        {
            var noquery = Regex.Replace(rawURL, @"\?.+", "");
            if (noquery.StartsWith("/"))
                return noquery.Substring(1, noquery.Length - 1);
            else
                return noquery;
        }

        private void HandleError(HttpListenerContext context, int ErrorCode, Exception ex)
        {
            var content = Properties.Resources.ErrorPage.Replace("{{number}}", ErrorCode.ToString());
            content = content.Replace("{{message}}", ex.Message);
            content = content.Replace("{{trace}}", ex.StackTrace);
            byte[] buff = Encoding.UTF8.GetBytes(content);
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.ContentLength64 = buff.Length;
            context.Response.OutputStream.Write(buff, 0, buff.Length);
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        public void Start()
        {
            _listener.Start();
            //threaded serve function
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                var file = GetFileName(ctx.Request.RawUrl);
                                if (string.IsNullOrEmpty(file)) file = "index.html";

                                if (Cache.Contains(file))
                                {
                                    byte[] data = Cache[file];
                                    ctx.Response.StatusCode = 200;
                                    ctx.Response.ContentLength64 = data.Length;
                                    ctx.Response.ContentType = GetMime(file);
                                    ctx.Response.OutputStream.Write(data, 0, data.Length);
                                }
                                else
                                {
                                    var f = Path + "\\" + FormatPath(file);
                                    if (Directory.Exists(f)) f += "\\index.html";

                                    if (File.Exists(f))
                                    {
                                        using (var fs = File.OpenRead(f))
                                        {
                                            ctx.Response.StatusCode = 200;
                                            ctx.Response.ContentType = MimeMapping.GetMimeMapping(f);
                                            ctx.Response.ContentLength64 = fs.Length;
                                            fs.CopyTo(ctx.Response.OutputStream);
                                        }
                                    }
                                    else
                                    {
                                        HandleError(ctx, 404, new Exception(string.Format("File doesn't exist: {0}", ctx.Request.RawUrl)));
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                HandleError(ctx, 500, ex);
                            }
                            finally
                            {
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Server error", ex);
                }
            });
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }


    }
}
