using AppLib.WPF.Controls.ImageControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppLib.WPF.Extensions;
using System.Text;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A File explorer user control
    /// </summary>
    public partial class FileExplorer : UserControl
    {
        private object _dummyNode;
        private string _currentpath;
        private bool _loaded;

        /// <summary>
        /// Creates a new instance of file explorer
        /// </summary>
        public FileExplorer()
        {
            InitializeComponent();
            _dummyNode = null;
            _currentpath = null;
            _loaded = false;
        }

        /// <summary>
        /// Event handler for double click
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">File event arguments</param>
        public delegate void FileDoubleClickHandler(object sender, FileEventArgs e);
        
        /// <summary>
        /// Extensions to filter
        /// </summary>
        public IEnumerable<string> FilteredExtensions
        {
            get;
            set;
        }

        /// <summary>
        /// Event, when a file is double clicked.
        /// </summary>
        public event FileDoubleClickHandler FileDoubleClick;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RenderDriveList();
            RenderFolderView();
            RenderFileList();
            RenderPath(null);
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
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_circle_o, new SolidColorBrush(Colors.CadetBlue));
                        break;
                    case DriveType.Fixed:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_hdd_o, new SolidColorBrush(Colors.CadetBlue));
                        break;
                    case DriveType.Network:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_globe, new SolidColorBrush(Colors.CadetBlue));
                        break;
                    case DriveType.Removable:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_usb, new SolidColorBrush(Colors.CadetBlue));
                        break;
                    case DriveType.Ram:
                    case DriveType.Unknown:
                        ib.Image = ImageAwesome.CreateImageSource(FaIcons.fa_question, new SolidColorBrush(Colors.CadetBlue));
                        break;
                    default:
                        continue;
                }

                ib.Content = drive.Name;
                ib.MinWidth = 60;
                ib.Margin = new Thickness(3, 2, 3, 2);
                ib.Click += DriveButton_Click;
                Drives.Children.Add(ib);
            }

            var refresh = new ImageButton();
            refresh.Image = ImageAwesome.CreateImageSource(FaIcons.fa_refresh, new SolidColorBrush(Colors.CadetBlue));
            refresh.ImageWidth = 24;
            refresh.MinWidth = 60;
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
                RenderPath(item.Tag.ToString());
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

        private void RenderFileList(string path = null)
        {
            var items = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                _currentpath = path;
                SelectNodePath(path);
                RenderPath(path);

                if (CBHidden.IsChecked == true)
                {
                    items.AddRange(Directory.GetDirectories(path));
                    if (FilteredExtensions != null && FilteredExtensions.Any())
                        return;
                    else
                    {
                        var dir = new DirectoryInfo(path);
                        var files = from i in dir.GetFiles()
                                    where FilteredExtensions.Contains(i.Extension)
                                    select i.FullName;
                    }
                }
                else
                {
                    var dir = new DirectoryInfo(path);
                    var folders = from i in dir.GetDirectories()
                                  where !i.Attributes.HasFlag(FileAttributes.Hidden)
                                  select i.FullName;
                    items.AddRange(folders);

                    if (FilteredExtensions != null && FilteredExtensions.Any())
                    {
                        var files = from i in dir.GetFiles()
                                    where !i.Attributes.HasFlag(FileAttributes.Hidden)
                                    && FilteredExtensions.Contains(i.Extension)
                                    select i.FullName;
                        items.AddRange(files);
                    }
                    else
                    {
                        var files = from i in dir.GetFiles()
                                    where !i.Attributes.HasFlag(FileAttributes.Hidden)
                                    select i.FullName;
                        items.AddRange(files);
                    }
                }
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
            else FileDoubleClick?.Invoke(this, new FileEventArgs { Filename = selected });
        }

        #endregion

        #region Path Selector

        private void RenderPath(string path)
        {
            if (PathSelector.Children.Count > 0)
            {
                foreach (var button in PathSelector.FindChildren<Button>())
                {
                    button.Click -= PathSelectorButtonClick;
                }
                PathSelector.Children.Clear();
            }

            if (string.IsNullOrEmpty(path))
                return;

            var parts = path.Split('\\');

            var pathbuilder = new StringBuilder();

            foreach(var part in parts)
            {
                if (string.IsNullOrEmpty(part))
                    continue;

                pathbuilder.AppendFormat("{0}\\", part);

                var navbutton = new Button();
                navbutton.Content = part;
                navbutton.Click += PathSelectorButtonClick;
                navbutton.MaxWidth = 150;
                navbutton.MinWidth = 25;
                navbutton.ToolTip = pathbuilder.ToString();
                navbutton.VerticalAlignment = VerticalAlignment.Center;
                navbutton.Margin = new Thickness(5, 0, 5, 0);
                PathSelector.Children.Add(navbutton);

                PathSelector.Children.Add(new TextBlock
                {
                    Text = "\\",
                    VerticalAlignment = VerticalAlignment.Center

                });
            }

        }

        private void PathSelectorButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            RenderFileList(btn.ToolTip.ToString());
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

    /// <summary>
    /// File event arguments
    /// </summary>
    public class FileEventArgs: RoutedEventArgs
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        public string Filename { get; set; }
    }
}
