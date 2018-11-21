using System;
using System.Globalization;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{

    /// <summary>
    /// Converts a bool value to negated bool value
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegateConverter : ConverterBase<NegateConverter>, IValueConverter
    {

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return !(bool)value;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return !(bool)value;
        }
    }
}
