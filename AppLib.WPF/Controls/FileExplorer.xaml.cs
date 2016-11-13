using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Interaction logic for FileExplorer.xaml
    /// </summary>
    public partial class FileExplorer : UserControl
    {
        private object dummyNode = null;

        public FileExplorer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderDriveList();
            RenderFolderView();
        }

        #region Drive Bar
        private void RenderDriveList()
        {
            var drives = from drive in DriveInfo.GetDrives()
                         where drive.IsReady == true
                         orderby drive.Name ascending
                         select drive;

            Drives.Children.Clear();
            foreach (var drive in drives)
            {
                var ib = new ImageButton();
                ib.Content = drive.Name;
                ib.Click += DriveButton_Click;
                Drives.Children.Add(ib);
            }

            var refresh = new ImageButton();
            refresh.Click += Refresh_Click;
            Drives.Children.Add(refresh);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RenderDriveList();
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            var drive = (sender as ImageButton)?.Content?.ToString();
            RenderFolderView(drive);
        }

        #endregion

        #region Folder view

        private void RenderFolderView(string path=null)
        {
            if (string.IsNullOrEmpty(path)) return;
            Folders.Items.Clear();
            foreach (string s in Directory.GetDirectories(path))
            {
                var folder = new TreeViewItem();
                folder.Header = Path.GetFileName(s);
                folder.Tag = s;
                folder.FontWeight = FontWeights.Normal;
                folder.Items.Add(dummyNode);
                folder.Expanded += Folder_Expanded;
                Folders.Items.Add(folder);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += Folder_Expanded;
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void Folders_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        #endregion
    }
}
