using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// Extension methods for the color type
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Computes the negative color corresponding to the color. Ignores the alpha channel
        /// </summary>
        /// <param name="c">a color</param>
        /// <returns>the color's negative color</returns>
        public static Color Negative(this Color c)
        {
            return Color.FromRgb((byte)(255 - c.R), (byte)(255 - c.G), (byte)(255 - c.B));
        }

        /// <summary>
        /// Computes the inverse color
        /// </summary>
        /// <param name="c">a color</param>
        /// <returns>The color's inverse color</returns>
        public static Color Inverse(this Color c)
        {
            var hsl = HSLColor.RGBtoHSL(c);
            hsl.Hue = 360 - hsl.Hue;
            hsl.Saturation = 1 - hsl.Saturation;
            return HSLColor.HSLtoRGB(hsl);
        }

        /// <summary>
        /// Creates a solid color brush from the parameter color
        /// </summary>
        /// <param name="c">color to convert to brush</param>
        /// <returns>A solid color brush with the specified color</returns>
        public static SolidColorBrush ToColorBrush(this Color c)
        {
            return new SolidColorBrush(c);
        }

        /// <summary>
        /// Converts the current color to a HSL color value
        /// </summary>
        /// <param name="c">Color to convert</param>
        /// <returns>Color in HSL color space</returns>
        public static HSLColor ToHSLColor(this Color c)
        {
            return HSLColor.RGBtoHSL(c);
        }

        /// <summary>
        /// Converts a HSL color to a Color
        /// </summary>
        /// <param name="hsl">HSL color to convert</param>
        /// <returns>An RGB color instance</returns>
        public static Color ToColor(this HSLColor hsl)
        {
            return HSLColor.HSLtoRGB(hsl);
        }

        /// <summary>
        /// Get the dominant color of an image
        /// </summary>
        /// <param name="img">image to get color from</param>
        /// <returns>the dominant color of an image</returns>
        public static Color GetDominantColor(this ImageSource img)
        {
            var rect = new Rect(0, 0, 1, 1);
            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(img, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                1, 1,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);
            byte[] pixels = new byte[4];
            resizedImage.CopyPixels(pixels, 4, 0);

            return Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);
        }
    }
}
