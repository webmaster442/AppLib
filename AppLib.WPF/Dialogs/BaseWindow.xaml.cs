using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for BaseWindow.xaml
    /// </summary>
    internal partial class BaseWindow : Window
    {
        /// <summary>
        /// Creates a new instance of Base Window
        /// </summary>
        public BaseWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Dialog Content
        /// </summary>
        public object DialogContent
        {
            get { return ContentControl.Content; }
            set { ContentControl.Content = value; }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
