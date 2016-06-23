using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WPFLib.Dialogs
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

        private string _details;

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
            StringBuilder details = new StringBuilder();
            var dialog = new ErrorDialog();
            dialog.ErrorText.Text = ex.Message;
            details.AppendFormat("Message: {0}\nStack trace: {1}\n", ex.Message, ex.StackTrace);
            dialog.StackTrace.Text = ex.StackTrace;
            var nodes = RenderNode(ex);
            dialog.InnerExceptions.Items.Add(nodes);
            dialog._details = details.ToString();
            return dialog.ShowDialog();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void BtnClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_details);
        }
    }
}
