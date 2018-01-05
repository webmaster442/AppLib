using System;
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
    public sealed partial class ErrorControl : UserControl
    {
        /// <summary>
        /// Creates a new instance of ErrorControl
        /// </summary>
        public ErrorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new ErrorControl
        /// </summary>
        /// <param name="ex">Exception to show</param>
        public ErrorControl(Exception ex): this()
        {
            ErrorText.Text = ex.Message;
            StackTrace.Text = ex.StackTrace;
            var nodes = RenderNode(ex);
            InnerExceptions.Items.Add(nodes);
            System.Media.SystemSounds.Exclamation.Play();
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
    }
}
