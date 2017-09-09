using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// An Async Progress indicator
    /// </summary>
    public partial class AsyncProgressIndicator : UserControl
    {
        /// <summary>
        /// Creates a new instance of Async progress indicator
        /// </summary>
        public AsyncProgressIndicator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Dependency property for wait text
        /// </summary>
        public static readonly DependencyProperty WaitTextProperty =
            DependencyProperty.Register("WaitText", typeof(string), typeof(AsyncProgressIndicator), new PropertyMetadata("Working..."));

        /// <summary>
        /// WaitText
        /// </summary>
        public string WaitText
        {
            get { return (string)GetValue(WaitTextProperty); }
            set { SetValue(WaitTextProperty, value); }
        }
    }
}
