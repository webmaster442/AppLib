using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace AppLib.WPF
{
    /// <summary>
    /// A class designed to download the bing photo of the day
    /// </summary>
    public static class BingPhotoOfDay
    {
        private static readonly string _feed;
        private static readonly string _tempfilename;
        private static readonly string _tempcoppyright;

        /// <summary>
        /// Creates a new instance of the Bing photo downloader
        /// </summary>
        static BingPhotoOfDay()
        {
            var tempdir = Environment.ExpandEnvironmentVariables("%temp%");
            _tempfilename = Path.Combine(tempdir, "bingphotooftheday.jpg");
            _tempcoppyright = Path.Combine(tempdir, "bingphotooftheday.txt");

            //photo of the day data in xml format
            _feed = "http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US";
            WasSuccesfull = true;

            if (NeedtoDownload())
            {
                WasSuccesfull = Download();
            }

            if (WasSuccesfull)
            {
                var cpy = File.ReadAllText(_tempcoppyright).Split(';');
                CoppyRightData = cpy[0];
                CoppyRightLink = cpy[1];
            }
        }

        /// <summary>
        /// Gets the Photo of the day in a WPF complaint ImageSource
        /// </summary>
        public static ImageSource PhotoOfDayImageSource
        {
            get
            {
                if (WasSuccesfull)
                    return new BitmapImage(new Uri(_tempfilename));
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the Photo of the day in a WPF complaint ImageBrush
        /// </summary>
        public static ImageBrush PhotoOfDayImageBrush
        {
            get
            {
                if (WasSuccesfull)
                    return new ImageBrush(new BitmapImage(new Uri(_tempfilename)));
                else
                    return null;
            }
        }

        /// <summary>
        /// CoppyRight data information
        /// </summary>
        public static string CoppyRightData
        {
            get;
            private set;
        }

        /// <summary>
        /// CoppyRightLink
        /// </summary>
        public static string CoppyRightLink
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates that downloading was succesfull or not
        /// </summary>
        public static bool WasSuccesfull
        {
            get;
            private set;
        }

        private static bool NeedtoDownload()
        {
            bool downloadneeded = true;
            if (File.Exists(_tempfilename) && File.Exists(_tempcoppyright))
            {
                var fi = new FileInfo(_tempfilename);
                if (DateTime.UtcNow.DayOfYear == fi.LastWriteTimeUtc.DayOfYear)
                {
                    downloadneeded = false;
                }
            }
            return downloadneeded;
        }

        private static bool TryRemoveMyTemp()
        {
            try
            {
                if (File.Exists(_tempcoppyright)) File.Delete(_tempcoppyright);
                if (File.Exists(_tempfilename)) File.Delete(_tempfilename);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        private static bool Download()
        {
            try
            {
                var document = XDocument.Load(_feed).Elements().Elements().FirstOrDefault();

                var url = (from i in document.Elements()
                           where i.Name == "url"
                           select i.Value).FirstOrDefault();

                var imgurl = "http://www.bing.com" + url;

                CoppyRightData = (from i in document.Elements()
                                  where i.Name == "copyright"
                                  select i.Value).FirstOrDefault();

                CoppyRightLink = (from i in document.Elements()
                                  where i.Name == "copyrightlink"
                                  select i.Value).FirstOrDefault();

                File.WriteAllText(_tempcoppyright, CoppyRightData + ";" + CoppyRightLink);

                using (var client = new WebClient())
                {
                    client.DownloadFile(imgurl, _tempfilename);
                }

                return true;

            }
            catch (Exception)
            {
                CoppyRightData = "Error on Download";
                CoppyRightLink = "Error on download";
                TryRemoveMyTemp();
                return false;
            }
        }
    }
}
