using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// A FileInfo converter that converts between various informations of a file
    /// </summary>
    class FileInfoConverter : IValueConverter
    {
        /// <summary>
        /// Returns various informations of a file based on its name and the given converter parameter
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. Can be one of the following:
        /// name, size, extension, date
        /// </param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>requested data specified by the parameter</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = parameter.ToString().ToLower();
            var filename = value.ToString();

            bool isdir = false;
            FileInfo fi = null;
            DirectoryInfo di = null;

            if (File.Exists(filename)) fi = new FileInfo(filename);
            else if (Directory.Exists(filename))
            {
                di = new DirectoryInfo(filename);
                isdir = true;
            }
            else return "File doesn't exist: " + filename;

            switch (par)
            {
                case "name":
                case "filename":
                    return isdir ? di.Name : fi.Name;
                case "size":
                case "filesize":
                    return isdir ? " - " : FileSizeConverter.Calculate(fi.Length);
                case "extension":
                case "fileextension":
                    return isdir ? "Directory" : fi.Extension;
                case "date":
                case "filedate":
                    return isdir ? di.LastWriteTime.ToString(culture) : fi.LastWriteTime.ToString(culture);
                default:
                    return "No converter parameter given. Valid converter parameters are: name, size, extension, date";
            }
        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>the unmodified input object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
