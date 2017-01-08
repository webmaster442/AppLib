using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using AppLib.Common.Extensions;

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
    }
}
