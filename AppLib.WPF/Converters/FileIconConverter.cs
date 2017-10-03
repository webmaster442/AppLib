using AppLib.WPF.Controls.ImageControls;
using AppLib.WPF.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts a file name to a FontAwesome icon &amp; returns the icon
    /// </summary>
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class FileIconConverter : ConverterBase<FileIconConverter>, IValueConverter
    {

        private static Color[] _colors;
        private static Dictionary<string, ImageSource> _cache;
        private const int RenderSize = 128;

        static FileIconConverter()
        {
            _colors = new Color[]
            {
                Color.FromRgb(26, 188, 156),
                Color.FromRgb(22, 160, 133),
                Color.FromRgb(46, 204, 113),
                Color.FromRgb(39, 174, 96),
                Color.FromRgb(52, 152, 219),
                Color.FromRgb(41, 128, 185),
                Color.FromRgb(155, 89, 182),
                Color.FromRgb(142, 68, 173),
                Color.FromRgb(52, 73, 94),
                Color.FromRgb(44, 62, 80),
                Color.FromRgb(241, 196, 15),
                Color.FromRgb(243, 156, 18),
                Color.FromRgb(230, 126, 34),
                Color.FromRgb(211, 84, 0),
                Color.FromRgb(231, 76, 60),
                Color.FromRgb(192, 57, 43),
                Color.FromRgb(189, 195, 199),
                Color.FromRgb(149, 165, 166),
                Color.FromRgb(127, 140, 141)
            };
            _colors.OrderBy(i => i.R * i.G * i.B);
            _cache = new Dictionary<string, ImageSource>();
        }

        private ImageSource GetIcon(string ext)
        {
            if (_cache.ContainsKey(ext))
                return _cache[ext];
            else
            {
                var index = Math.Abs(ext.GetHashCode()) % _colors.Length;
                Border b = new Border();
                b.Width = RenderSize;
                b.Height = RenderSize;
                b.Background = new ImageBrush(ImageAwesome.CreateImageSource(FaIcons.fa_file_o, new SolidColorBrush(_colors[index])));
                TextBlock t = new TextBlock();
                Viewbox strecher = new Viewbox();
                strecher.Child = t;
                strecher.Margin = new System.Windows.Thickness(5);
                b.Child = strecher;
                t.Text = ext;
                t.Foreground = new SolidColorBrush(Colors.Black);
                var img = b.Render();
                _cache.Add(ext, img);
                return img;

            }
        }

        private ImageSource RenderDriveIcon(string path)
        {
            var di = new DriveInfo(path);

            switch (di.DriveType)
            {
                case DriveType.CDRom:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_circle_o, new SolidColorBrush(Colors.CadetBlue));
                case DriveType.Fixed:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_hdd_o, new SolidColorBrush(Colors.CadetBlue));
                case DriveType.Network:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_globe, new SolidColorBrush(Colors.CadetBlue));
                case DriveType.Removable:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_usb, new SolidColorBrush(Colors.CadetBlue));
                case DriveType.Ram:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_rocket, new SolidColorBrush(Colors.CadetBlue));
                case DriveType.Unknown:
                case DriveType.NoRootDirectory:
                default:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_question, new SolidColorBrush(Colors.CadetBlue));
            }

        }


        private SolidColorBrush GetBrush(string ext)
        {
            var index = Math.Abs(ext.GetHashCode()) % _colors.Length;
            return new SolidColorBrush(_colors[index]);
        }

        /// <summary>
        /// Converts a file name to a FontAwesome icon &amp; returns the icon
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>an ImageSource</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fullpath = value.ToString();

            if (Directory.GetLogicalDrives().Contains(fullpath))
                return RenderDriveIcon(fullpath);

            if (Directory.Exists(fullpath))
                return ImageAwesome.CreateImageSource(FaIcons.fa_folder_o, new SolidColorBrush(Colors.Gold));

            var ext = Path.GetExtension(fullpath).ToLower();

            switch (ext)
            {
                case ".wav":
                case ".mp3":
                case ".wma":
                case ".m4a":
                case ".m4b":
                case ".flac":
                case ".wv":
                case ".ogg":
                case ".ac3":
                case ".dts":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_audio_o, GetBrush(ext));
                case ".mp4":
                case ".avi":
                case ".mkv":
                case ".wmv":
                case ".mpeg":
                case ".mpg":
                case ".webm":
                case ".asf":
                case ".3gp":
                case ".flv":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_video_o, GetBrush(ext));
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".psd":
                case ".bmp":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_image_o, GetBrush(ext));
                case ".zip":
                case ".rar":
                case ".7z":
                case ".ace":
                case ".arj":
                case ".tar":
                case ".gz":
                case ".bz2":
                case ".rpm":
                case ".deb":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_archive_o, GetBrush(ext));
                case ".c":
                case ".h":
                case ".cpp":
                case ".xml":
                case ".xaml":
                case ".cs":
                case ".vb":
                case ".js":
                case ".css":
                case ".py":
                case ".lua":
                case ".less":
                case ".java":
                case ".php":
                case ".r":
                case ".perl":
                case ".tcl":
                case ".matlab":
                case ".pde":
                case ".ino":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_code_o, GetBrush(ext));
                case ".txt":
                case ".md":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_text_o, GetBrush(ext));
                case ".exe":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_windows, GetBrush(ext));
                default:
                    return GetIcon(ext);
            }

        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>an ImageSource</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
