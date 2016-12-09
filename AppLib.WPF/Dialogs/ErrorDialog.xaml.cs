using System;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// A generic, detailed Error dialog for exception handling
    /// </summary>
    /// <example>
    /// try { //something; }
    /// catch (Exception ex) { ErrorDialog.Show(ex); }
    /// </example>
    public partial class ErrorDialog : Window
    {
        private ErrorDialog()
        {
            InitializeComponent();
        }

        private static TreeViewItem RenderNode(Exception ex)
        {
            TreeViewItem node = new TreeViewItem();
            node.Header = string.Format("Message: {0}\nSource: {1}\nHelp: {2}", ex.Message, ex.Source, ex.HelpLink);
            if (ex.InnerException != null)
            {
                var child = RenderNode(ex.InnerException);
                node.Items.Add(child);
            }

            return node;
        }

        /// <summary>
        /// Show an error dialog based on an exception
        /// </summary>
        /// <param name="ex">Exception, that provides data for the dialog</param>
        public static bool? Show(Exception ex)
        {
            var dialog = new ErrorDialog();
            dialog.ErrorText.Text = ex.Message;
            dialog.StackTrace.Text = ex.StackTrace;
            var nodes = RenderNode(ex);
            dialog.InnerExceptions.Items.Add(nodes);
            System.Media.SystemSounds.Exclamation.Play();
            return dialog.ShowDialog();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
