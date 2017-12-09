using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Linq;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// A FileInfo converter that converts between various informations of a file
    /// </summary>
    public class FileInfoConverter : ConverterBase<FileInfoConverter>, IValueConverter
    {
        private enum ItemType
        {
            File,
            Drive,
            Directory,
            NotExists
        }

        private string Render(ItemType t, string fileContent, string driveContent, string dirContent, string notexistscontent = null)
        {
            switch (t)
            {
                case ItemType.Directory:
                    return dirContent;
                case ItemType.Drive:
                    return driveContent;
                case ItemType.File:
                    return fileContent;
                case ItemType.NotExists:
                    return notexistscontent;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns various informations of a file based on its name and the given converter parameter
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. Can be one of the following:
        /// name, size, extension, date, namenoextension
        /// </param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>requested data specified by the parameter</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = parameter.ToString().ToLower();
            var filename = value.ToString();

            ItemType type = ItemType.File;

            FileInfo fi = null;
            DirectoryInfo di = null;
            DriveInfo dri = null;

            if (Directory.GetLogicalDrives().Contains(filename))
            {
                dri = new DriveInfo(filename);
                type = ItemType.Drive;
            }
            if (File.Exists(filename))
            {
                fi = new FileInfo(filename);
                type = ItemType.File;
            }
            else if (Directory.Exists(filename))
            {
                di = new DirectoryInfo(filename);
                type = ItemType.Directory;
            }
            else
            {
                type = ItemType.NotExists;
            }

            switch (par)
            {
                case "name":
                case "filename":
                    return Render(type, fi?.Name, dri?.Name, di?.Name, filename);
                case "namenoextension":
                    return Render(type, fi?.Name.Replace(fi?.Extension, ""), dri?.Name, di?.Name, filename);
                case "size":
                case "filesize":
                    return Render(type, FileSizeConverter.Calculate(fi == null ? 0 : fi.Length), FileSizeConverter.Calculate(dri == null ? 0 : dri.TotalSize), " - ", "N/A");
                case "extension":
                case "fileextension":
                    return Render(type, fi?.Extension, dri?.DriveType.ToString(), "Directory", "N/A");
                case "date":
                case "filedate":
                    return Render(type, fi?.LastWriteTime.ToString(culture), di?.LastWriteTime.ToString(culture), di?.LastWriteTime.ToString(culture), "N/A");
                default:
                    return "No converter parameter given. Valid converter parameters are: name, namenoextension, size, extension, date";
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
