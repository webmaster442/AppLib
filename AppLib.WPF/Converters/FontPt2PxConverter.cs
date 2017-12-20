using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts a value given in points to pixels
    /// </summary>
    public class FontPt2PxConverter : ConverterBase<FontPt2PxConverter>, IValueConverter
    {
        /// <summary>
        /// Converts a points value to pixel size
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>pixels</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var points = (double)value;
                return points * (4 / 3);
            }
            else
                return null;
        }

        /// <summary>
        /// Converts pixel size to points
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>points</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var pixels = (double)value;
                return pixels * 0.75;
            }
            else
                return null;
        }
    }
}
