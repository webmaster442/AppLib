using AppLib.WPF.Converters;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for Updater.xaml
    /// </summary>
    public sealed partial class Updater : Window, IDisposable
    {
        private WebClient _client;
        private string _updatefile;

        private Updater()
        {
            InitializeComponent();
            _client = new WebClient();
            _client.DownloadProgressChanged += _client_DownloadProgressChanged;
        }

        /// <summary>
        /// Dispses the internal web client
        /// </summary>
        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }

        private void _client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Progressbar.Value = e.ProgressPercentage;
                TbProgress.Text = string.Format("{0} / {1}",
                    FileSizeConverter.Calculate(e.BytesReceived),
                    FileSizeConverter.Calculate(e.TotalBytesToReceive));
            });
        }

        private void SetButtonLayout(bool cancel, bool ok, bool dload)
        {
            BtnOk.Visibility = dload ? Visibility.Visible : Visibility.Collapsed;
            BtnCancel.Visibility = cancel ? Visibility.Visible : Visibility.Collapsed;
            BtnDownload.Visibility = dload ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Configure(bool download)
        {
            TbProgress.Text = "";
            TbProgress.Visibility = download ? Visibility.Visible : Visibility.Collapsed;
            Progressbar.Value = 0;
            Progressbar.Visibility = download ? Visibility.Visible : Visibility.Collapsed;
        }

        private async Task<string> DownloadVersionInfo(string file)
        {
            TbStatus.Text = "Downloading version info ...";
            Configure(true);
            var recieved = await _client.DownloadStringTaskAsync(file);
            Configure(false);
            return recieved;
        }

        private void Parse(string xml, Version current)
        {
            TbStatus.Text = "Working ...";
            using (var s = new StringReader(xml))
            {
                var xs = new XmlSerializer(typeof(UpdateInfo[]));
                var updates = (UpdateInfo[])xs.Deserialize(s);
                var latestupdate = (from u in updates
                                    where u.PubDate >= DateTime.Now &&
                                    u.Version > current
                                    select u).FirstOrDefault();

                Progressbar.Visibility = Visibility.Collapsed;
                DialogText.Visibility = Visibility.Visible;

                if (latestupdate != null)
                {
                    //update found
                    TbStatus.Text = "An update found";
                    _updatefile = latestupdate.File;
                    DialogText.Text = string.Format("New version found: {0}", latestupdate);
                    SetButtonLayout(true, false, true);
                }
                else
                {
                    //no updates
                    TbStatus.Text = "Up to date";
                    DialogText.Text = string.Format("Application is up to date");
                    SetButtonLayout(false, true, false);
                }
            }
        }

        /// <summary>
        /// Download an executable updater from the internet.
        /// The path must be an xml contining a serialized UpdateUnfo[] array
        /// </summary>
        /// <seealso cref="UpdateInfo"/>
        /// <param name="url">Url of th eserialized xml</param>
        public static async void SearchForUpdate(string url)
        {
            using (var dialog = new Updater())
            {
                var version = Assembly.GetCallingAssembly().GetName().Version;
                dialog.ShowDialog();

                try
                {
                    dialog.SetButtonLayout(true, false, false);
                    var xml = await dialog.DownloadVersionInfo(url);
                    dialog.Parse(xml, version);
                }
                catch (Exception ex)
                {
                    dialog.SetButtonLayout(false, true, false);
                    dialog.Progressbar.Visibility = Visibility.Collapsed;
                    dialog.DialogText.Visibility = Visibility.Visible;
                    dialog.DialogText.Text = ex.Message;
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_client.IsBusy)
            {
                var q = MessageBox.Show("Abort current download?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (q == MessageBoxResult.Yes)
                {
                    _client.CancelAsync();
                    Close();
                }
            }
            else
                Close();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            TbStatus.Text = "Downloading update. When finished, the update process will be started";
            if (!string.IsNullOrEmpty(_updatefile))
            {
                var tempfile = Path.GetTempFileName();
                Path.ChangeExtension(tempfile, ".exe");
                await _client.DownloadFileTaskAsync(_updatefile, tempfile);
                Process p = new Process();
                p.StartInfo.FileName = tempfile;
                p.Start();
                Close();
            }
        }
    }
}
