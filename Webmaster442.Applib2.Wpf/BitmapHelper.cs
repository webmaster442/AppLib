using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Webmaster442.Applib
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
        public static BitmapImage CreateFrozenBitmap(Uri source)
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
        public static BitmapImage CreateFrozenBitmap(string source)
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
        public static BitmapImage CreateFrozenBitmap(Stream source)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = source;
            image.EndInit();
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Create a frozen bitmap from a base64 encoded image file
        /// </summary>
        /// <param name="base64">base64 encoded image file</param>
        /// <returns>BitmapImage that's frozen</returns>
        public static BitmapImage CreateBitmapFromBase64(string base64)
        {
            byte[] binaryData = Convert.FromBase64String(base64);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(binaryData);
            image.EndInit();
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Encode a bitmapimage to base64 string
        /// </summary>
        /// <typeparam name="Encoder">Image Encoder</typeparam>
        /// <param name="img">Image to encode</param>
        /// <returns>Base64 encoded string</returns>
        public static string Base64JpegEncode<Encoder>(BitmapImage img) where Encoder: BitmapEncoder, new()
        {
            var encoder = new Encoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
