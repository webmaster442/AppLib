using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace AppLib.WPF
{
    /// <summary>
    /// Bitmap Helpers
    /// </summary>
    public class BitmapHelper
    {
        /// <summary>
        /// Create a frozen bitmap
        /// </summary>
        /// <param name="source">Image source</param>
        /// <returns>BitmapImage that's frozen</returns>
        public static BitmapImage FrozenBitmap(Uri source)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = source;
            image.EndInit();
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Create a frozen bitmap
        /// </summary>
        /// <param name="source">Image source</param>
        /// <returns>BitmapImage that's frozen</returns>
        public static BitmapImage FrozenBitmap(string source)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(source);
            image.EndInit();
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Create a frozen bitmap
        /// </summary>
        /// <param name="source">Image source</param>
        /// <returns>BitmapImage that's frozen</returns>
        public static BitmapImage FrozenBitmap(Stream source)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = source;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
