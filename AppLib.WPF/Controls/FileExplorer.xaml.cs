using AppLib.WPF.Controls.ImageControls;
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
        private object _dummyNode;
        private string _currentpath;
        private bool _loaded;

        public FileExplorer()
        {
            InitializeComponent();
            _dummyNode = null;
            _currentpath = null;
            _loaded = false;
        }

        public delegate void FileDoubleClickHandler(object sender, FileEventArgs e);

        public event FileDoubleClickHandler FileDoubleClick;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderDriveList();
            RenderFolderView();
            _loaded = true;
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
                    default:
                        continue;
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

            string[] directories = null;

            if (CBHidden.IsChecked == false)
            {
                var dir = new DirectoryInfo(path);
                var folders = from i in dir.GetDirectories()
                              where !i.Attributes.HasFlag(FileAttributes.Hidden)
                              select i.FullName;
                directories = folders.ToArray();
            }
            else
            {
                directories = Directory.GetDirectories(path);
            }
            foreach (string s in directories)
            {
                var folder = new TreeViewItem();
                folder.Header = Path.GetFileName(s);
                folder.Tag = s;
                folder.FontWeight = FontWeights.Normal;
                folder.Items.Add(_dummyNode);
                folder.Expanded += Folder_Expanded;
                Folders.Items.Add(folder);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == _dummyNode)
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
                        subitem.Items.Add(_dummyNode);
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

        #region File view

        private TreeViewItem FromID(string itemId, TreeViewItem rootNode)
        {
            if (rootNode == null)
            {
                var q = Folders.Items.OfType<TreeViewItem>().FirstOrDefault(node => node.Tag.Equals(itemId));
                if (q != null) return q;
                else
                {
                    var q2 = from i in Folders.Items.OfType<TreeViewItem>()
                             where i.IsExpanded == true
                             select i;
                    foreach (var node in q2)
                    {
                        var result = FromID(itemId, node);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    return null;
                }
            }
            else
            {
                foreach (TreeViewItem node in rootNode.Items)
                {
                    if (node == null) continue;
                    if (node.Tag.Equals(itemId)) return node;
                    var next = FromID(itemId, node);
                    if (next != null) return next;
                }
                return null;
            }
        }

        private void SelectNodePath(string path)
        {
            var node = FromID(path, null);
            if (node != null) node.IsExpanded = true;
        }

        private void RenderFileList(string path)
        {
            var items = new List<string>();
            try
            {
                _currentpath = path;
                SelectNodePath(path);

                if (CBHidden.IsChecked == true)
                {
                    items.AddRange(Directory.GetDirectories(path));
                    items.AddRange(Directory.GetFiles(path));
                }
                else
                {
                    var dir = new DirectoryInfo(path);
                    var folders = from i in dir.GetDirectories()
                                  where !i.Attributes.HasFlag(FileAttributes.Hidden)
                                  select i.FullName;
                    items.AddRange(folders);

                    var files = from i in dir.GetFiles()
                                where !i.Attributes.HasFlag(FileAttributes.Hidden)
                                select i.FullName;
                    items.AddRange(files);
                }
                Files.ItemsSource = null;
                Files.ItemsSource = items;
            }
            catch (Exception ex)
            {
                Files.ItemsSource = null;
            }

        }

        private void Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Files.SelectedItem == null) return;
            var selected = Files.SelectedItem as string;
            if (Directory.Exists(selected)) RenderFileList(selected);
            else FileDoubleClick?.Invoke(this, new FileEventArgs { Filename = selected });
        }

        #endregion

        #region View
        private void CBHidden_Checked(object sender, RoutedEventArgs e)
        {
            var drive = Path.GetPathRoot(_currentpath);
            RenderFolderView(drive);
            RenderFileList(_currentpath);

        }

        private void View_Checked(object sender, RoutedEventArgs e)
        {
            if (!_loaded) return;
            if (ViewGrid.IsChecked == true)
                Files.Style = this.FindResource("Grid") as Style;
            else if (ViewList.IsChecked == true)
                Files.Style = this.FindResource("List") as Style;
        }
        #endregion


    }

    public class FileEventArgs: RoutedEventArgs
    {
        public string Filename { get; set; }
    }
}
