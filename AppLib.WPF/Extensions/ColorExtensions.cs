using AppLib.Common.Extensions;
using System.Windows.Media;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Extension methods for the color type
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Fade a color to another color
        /// </summary>
        /// <param name="source">Color Source</param>
        /// <param name="target">Target color</param>
        /// <param name="amount">Amount of fading. Must be between 0 and 1</param>
        /// <returns>A color faded to another color</returns>
        public static Color FadeTo(this Color source, Color target, double amount)
        {
            double r = source.R;
            double g = source.G;
            double b = source.B;
            r = r.Lerp(target.R, amount);
            g = g.Lerp(target.G, amount);
            b = b.Lerp(target.B, amount);
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

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
    }
}
