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
using System.Windows.Shapes;

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
        /// Dependency property for Dialog Content
        /// </summary>
        public static readonly DependencyProperty DialogContentProperty =
            DependencyProperty.Register("DialogContent", typeof(object), typeof(BaseWindow));

        /// <summary>
        /// Dialog Content
        /// </summary>
        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
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
