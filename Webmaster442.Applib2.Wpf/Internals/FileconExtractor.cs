using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Webmaster442.Applib.PInvoke;

namespace Webmaster442.Applib.Internals
{
    internal static class FileconExtractor
    {
        public static ImageSource GetIcon(string path, bool smallIcon = false, bool isDirectory = false)
        {
            // SHGFI_USEFILEATTRIBUTES takes the file name and attributes into account if it doesn't exist
            uint flags = SHGetFileInfoConstants.SHGFI_ICON | SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES;
            if (smallIcon)
                flags |= SHGetFileInfoConstants.SHGFI_SMALLICON;

            uint attributes = SHGetFileInfoConstants.FILE_ATTRIBUTE_NORMAL;
            if (isDirectory)
                attributes |= SHGetFileInfoConstants.FILE_ATTRIBUTE_DIRECTORY;

            SHFILEINFO shfi;
            if (0 != Shell32.SHGetFileInfo(
                        path,
                        attributes,
                        out shfi,
                        (uint)Marshal.SizeOf(typeof(SHFILEINFO)),
                        flags))
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            shfi.hIcon,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
            }
            return null;
        }
    }
}
