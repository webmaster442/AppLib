using AppLib.WPF.Controls.FontAwesome;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;

namespace AppLib.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fullpath = value.ToString();

            if (Directory.Exists(fullpath))
                return ImageAwesome.CreateImageSource(FaIcons.fa_folder_o, new SolidColorBrush(Colors.Black));

            var ext = Path.GetExtension(fullpath);

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
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_audio_o, new SolidColorBrush(Colors.Black));
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
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_video_o, new SolidColorBrush(Colors.Black));
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".psd":
                case ".bmp":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_image_o, new SolidColorBrush(Colors.Black));
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
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_archive_o, new SolidColorBrush(Colors.Black));
                case ".exe":
                    return ImageAwesome.CreateImageSource(FaIcons.fa_windows, new SolidColorBrush(Colors.Black));
                default:
                    return ImageAwesome.CreateImageSource(FaIcons.fa_file_o, new SolidColorBrush(Colors.Black));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
