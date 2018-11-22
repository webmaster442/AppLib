using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using Webmaster442.Applib.Internals;

namespace Webmaster442.Applib.Converters.BindingConverters
{
    public class FilenameToIconConverter: ConverterBase<FilenameToIconConverter>, IValueConverter
    {
        private Dictionary<string, ImageSource> _cache;
        private const long _cacheLimit = 1024L * 1024L * 8L; //8mb
        private long _cacheSize;

        public FilenameToIconConverter()
        {
            _cache = new Dictionary<string, ImageSource>();
            _cacheSize = 0;
        }

        private ImageSource Lookup(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension == ".exe" ||
                extension == ".lnk")
            {
                return FileconExtractor.GetIcon(path);
            }
            else if (Directory.Exists(path))
            {
                return FileconExtractor.GetIcon(path);
            }
            else
            {
                if (_cache.ContainsKey(extension))
                {
                    return _cache[extension];
                }
                else
                {
                    ImageSource img = FileconExtractor.GetIcon(path);

                    long size = (long)(img.Width * img.Height * 4);
                    if (_cacheSize + size > _cacheLimit)
                    {
                        _cache.Clear();
                        _cacheSize = 0;
                    }

                    _cacheSize += size;
                    _cache.Add(extension, img);
                    return _cache[extension];
                }
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (!string.IsNullOrEmpty(path))
            {
                return Lookup(path);
            }
            else return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

    }
}
