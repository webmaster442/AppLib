using System;
using System.Windows.Media;
using AppLib.Common.Extensions;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Represents a color in the Hue, Saturation, Lightness modell
    /// </summary>
    public sealed class HSLColor: IFormattable, IEquatable<HSLColor>, IEquatable<Color>
    {
        private float _hue;
        private float _saturation;
        private float _lightness;

        /// <summary>
        /// Creates a new instace of HSL color
        /// </summary>
        public HSLColor()
        {
            var c = RGBtoHSL(0, 0, 0);
            _hue = c.Hue;
            _lightness = c.Lightness;
            _saturation = c.Saturation;
        }

        /// <summary>
        /// Creates a new instance of HSL color
        /// </summary>
        /// <param name="h">Initial Hue</param>
        /// <param name="s">Initial Saturation</param>
        /// <param name="l">Initial Light</param>
        public HSLColor(float h, float s, float l)
        {
            Hue = h;
            Saturation = s;
            Lightness = l;
        }

        /// <summary>
        /// Creates a new instance of HSL color
        /// </summary>
        /// <param name="color">Initial color in RGB model</param>
        public HSLColor(Color color)
        {
            var c = RGBtoHSL(color.R, color.G, color.B);
            _hue = c.Hue;
            _lightness = c.Lightness;
            _saturation = c.Saturation;
        }

        /// <summary>
        /// Gets or sets the Hue. Value can be between 0 and 360
        /// </summary>
        /// <remarks>the degree to which a stimulus can be described as similar to or different from stimuli that are described as red, green, blue, and yellow</remarks>
        public float Hue
        {
            get { return _hue; }
            set
            {
                if (_hue > 360) _hue = 360;
                else if (_hue < 0) _hue = 0;
                else _hue = value;
            }
        }

        /// <summary>
        /// Gets or sets the saturation. Value can be between 0 and 1
        /// </summary>
        /// <remarks>The saturation of a color is determined by a combination of light intensity and how much it is distributed across the spectrum of different wavelengths</remarks>
        public float Saturation
        {
            get { return _saturation; }
            set
            {
                if (_saturation < 0) _saturation = 0;
                else if (_saturation > 1) _saturation = 1;
                else  _saturation = value;
            }
        }

        /// <summary>
        /// Gets or sets the Lightness. Value can be between 0 and 1
        /// </summary>
        public float Lightness
        {
            get { return _lightness; }
            set
            {
                if (_lightness < 0) _lightness = 0;
                else if (_lightness > 1) _lightness = 1;
                _lightness = value;
            }
        }

        /// <summary>
        /// Converts an RGB color to the HSL model
        /// </summary>
        /// <param name="red">Red chanel value</param>
        /// <param name="green">Green chanel value</param>
        /// <param name="blue">Blue chanel value</param>
        /// <returns>A color represented in th HSL model</returns>
        public static HSLColor RGBtoHSL(int red, int green, int blue)
        {
            float h = 0, s = 0, l = 0;

            red = (red > 255) ? 255 : ((red < 0) ? 0 : red);
            green = (green > 255) ? 255 : ((green < 0) ? 0 : green);
            blue = (blue > 255) ? 255 : ((blue < 0) ? 0 : blue);

            // normalize red, green, blue values
            float r = red / 255.0f;
            float g = green / 255.0f;
            float b = blue / 255.0f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            // hue
            if (max == min) h = 0f;
            else if (max == r && g >= b) h = 60.0f * (g - b) / (max - min);
            else if (max == r && g < b) h = 60.0f * (g - b) / (max - min) + 360.0f;
            else if (max == g) h = 60.0f * (b - r) / (max - min) + 120.0f;
            else if (max == b) h = 60.0f * (r - g) / (max - min) + 240.0f;

            // luminance
            l = (max + min) / 2.0f;

            // saturation
            if (l == 0 || max == min) s = 0f;
            else if (0 < l && l <= 0.5) s = (max - min) / (max + min);
            else if (l > 0.5) s = (max - min) / (2 - (max + min));

            return new HSLColor(h, s, l);
        }

        /// <summary>
        /// Converts an RGB color to the HSL model
        /// </summary>
        /// <param name="c">An RGB color to convert from</param>
        /// <returns>A color represented in th HSL model</returns>
        public static HSLColor RGBtoHSL(Color c)
        {
            return RGBtoHSL(c.R, c.G, c.B);
        }

        /// <summary>
        /// Converts a color represented in the HSL model to the RGB model equivalent
        /// </summary>
        /// <param name="h">Hue value</param>
        /// <param name="s">Saturation value</param>
        /// <param name="l">Light value</param>
        /// <returns>A color in RGB</returns>
        public static Color HSLtoRGB(float h, float s, float l)
        {
            h = (h > 360) ? 360 : ((h < 0) ? 0 : h);
            s = (s > 1) ? 1 : ((s < 0) ? 0 : s);
            l = (l > 1) ? 1 : ((l < 0) ? 0 : l);

            if (s == 0)
            {
                // achromatic color (gray scale)
                var val = Convert.ToByte((l * 255.0f));
                return Color.FromRgb(val, val, val);
            }
            else
            {
                float q = (l < 0.5f) ? (l * (1.0f + s)) : (l + s - (l * s));
                float p = (2.0f * l) - q;

                float Hk = h / 360.0f;
                float[] T = new float[3];
                T[0] = Hk + (1.0f / 3.0f);    // Tr
                T[1] = Hk;                // Tb
                T[2] = Hk - (1.0f / 3.0f);    // Tg

                for (int i = 0; i < 3; i++)
                {
                    if (T[i] < 0) T[i] += 1.0f;
                    if (T[i] > 1) T[i] -= 1.0f;

                    if ((T[i] * 6) < 1) T[i] = p + ((q - p) * 6.0f * T[i]);
                    else if ((T[i] * 2.0) < 1) T[i] = q;
                    else if ((T[i] * 3.0) < 2) T[i] = p + (q - p) * ((2.0f / 3.0f) - T[i]) * 6.0f;
                    else T[i] = p;
                }

                var r = Convert.ToByte(T[0] * 255.0f);
                var g = Convert.ToByte(T[1] * 255.0f);
                var b = Convert.ToByte(T[2] * 255.0f);

                return Color.FromRgb(r, g, b);
            }
        }

        public override string ToString()
        {
            return string.Format("H: {0}, S: {1},  L: {2}", _hue, _saturation, _lightness);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool Equals(HSLColor other)
        {
            var eq = Hue.EqualsWithTolerance(other.Hue);
            if (eq) eq = Saturation.EqualsWithTolerance(other.Saturation);
            if (eq) eq = Lightness.EqualsWithTolerance(other.Lightness);
            return eq;
        }

        public bool Equals(Color other)
        {
            var hsl = RGBtoHSL(other);
            var eq = Hue.EqualsWithTolerance(hsl.Hue);
            if (eq) eq = Saturation.EqualsWithTolerance(hsl.Saturation);
            if (eq) eq = Lightness.EqualsWithTolerance(hsl.Lightness);
            return eq;
        }
    }
}