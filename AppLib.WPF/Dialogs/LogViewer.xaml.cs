using AppLib.Common.Log;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer : UserControl
    {
        /// <summary>
        /// Creates a new instance of log viewer
        /// </summary>
        public LogViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance of log viewer
        /// </summary>
        /// <param name="log">Log to display</param>
        public LogViewer(ILogger log)
        {
            InitializeComponent();
            Log = log;
        }

        /// <summary>
        /// Dependency property for Log
        /// </summary>
        public static readonly DependencyProperty LogProperty =
            DependencyProperty.Register("Log", typeof(ILogger), typeof(LogViewer), new PropertyMetadata(null));

        /// <summary>
        /// Log
        /// </summary>
        public ILogger Log
        {
            get { return (ILogger)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        } 
    }
}
