using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using Webmaster442.Applib.Internals;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Converts a file name to file size
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class FileNameToSizeConverter : ConverterBase<FileNameToSizeConverter>, IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = value as string;

            if (string.IsNullOrEmpty(name))
                return Binding.DoNothing;

            if (File.Exists(name))
            {
                var fi = new FileInfo(name);
                return FileSizeCalculator.Calculate(fi.Length);
            }
            else if (Directory.Exists(name))
            {
                return "Dir";
            }
            else if (Directory.GetLogicalDrives().Contains(name))
            {
                DriveInfo di = new DriveInfo(name);
                if (di.IsReady)
                    return FileSizeCalculator.Calculate(di.TotalSize);
                else
                    return FileSizeCalculator.Calculate(0);
            }
            else
                return Binding.DoNothing;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
