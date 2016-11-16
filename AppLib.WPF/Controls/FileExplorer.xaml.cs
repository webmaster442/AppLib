using AppLib.WPF.Controls.FontAwesome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public RoutedEventHandler MouseDoubleClick;

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
                ib.ImageHeight = 24;
                ib.ImageWidth = 24;
                
                switch (drive.DriveType)
                {
                    case DriveType.CDRom:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_circle_o, new SolidColorBrush(Colors.Black));
                        break;
                    case DriveType.Fixed:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_hdd_o, new SolidColorBrush(Colors.Black));
                        break;
                    case DriveType.Network:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_globe, new SolidColorBrush(Colors.Black));
                        break;
                    case DriveType.Removable:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_usb, new SolidColorBrush(Colors.Black));
                        break;
                    case DriveType.Ram:
                    case DriveType.Unknown:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_question, new SolidColorBrush(Colors.Black));
                        break;
                }

                ib.Content = drive.Name;
                ib.Margin = new Thickness(2);
                ib.Click += DriveButton_Click;
                Drives.Children.Add(ib);
            }

            var refresh = new ImageButton();
            refresh.Image = ImageAwesome.CreateImageSource(FaIcons.fa_refresh, new SolidColorBrush(Colors.Black));
            refresh.ImageWidth = 24;
            refresh.ToolTip = "Rescan drives";
            refresh.ImageHeight = 24;
            refresh.Margin = new Thickness(2);
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
            RenderFileList(drive);
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
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)  return;
            RenderFileList(temp.Tag.ToString());
        }
        #endregion

        private void RenderFileList(string path)
        {
            var items = new List<string>();
            try
            {
                items.AddRange(Directory.GetDirectories(path));
                items.AddRange(Directory.GetFiles(path));
                Files.ItemsSource = null;
                Files.ItemsSource = items;
            }
            catch (Exception)
            {
                Files.ItemsSource = null;
            }

        }

        private void Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Files.SelectedItem == null) return;
            var selected = Files.SelectedItem as string;
            if (Directory.Exists(selected)) RenderFileList(selected);
            else if (this.MouseDoubleClick != null) MouseDoubleClick(this, e);
        }
    }
}
