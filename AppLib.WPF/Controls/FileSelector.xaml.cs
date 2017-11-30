using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A simple file selector, that shows the selected file name in a textbox &amp; offers a browse button.
    /// </summary>
    public partial class FileSelector : UserControl
    {
        /// <summary>
        /// Creates a new instance of FileSelector
        /// </summary>
        public FileSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Dependency property for SelectedFile
        /// </summary>
        public static readonly DependencyProperty SelectedFileProperty = 
            DependencyProperty.Register("SelectedFile", typeof(string), typeof(FileSelector), new PropertyMetadata("", SelectedFileChanged));

        private static void SelectedFileChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FileSelector fs = d as FileSelector;
            fs.FileTextBox.Text = e.NewValue?.ToString();
        }

        /// <summary>
        /// Dependency property for Filter
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(FileSelector), new PropertyMetadata(""));

        /// <summary>
        /// Gets or sets the selected file
        /// </summary>
        public string SelectedFile
        {
            get { return (string)GetValue(SelectedFileProperty); }
            set { SetValue(SelectedFileProperty, value); }
        }

        /// <summary>
        /// Gets or sets the file selector filter string
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (Filter.ToLower() == "folder")
            {
                var fs = new System.Windows.Forms.FolderBrowserDialog();

                if (System.IO.Directory.Exists(SelectedFile))
                    fs.SelectedPath = SelectedFile;

                fs.Description = "Select folder ...";
                if (fs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SelectedFile = fs.SelectedPath;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select file ...";
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = Filter;

                if (System.IO.File.Exists(SelectedFile))
                    openFileDialog.FileName = SelectedFile;

                if (openFileDialog.ShowDialog() == true)
                {
                    SelectedFile = openFileDialog.FileName;
                }
            }
        }

        private void FileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectedFile = FileTextBox.Text;
        }
    }
}
