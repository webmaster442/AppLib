using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Converts multiple bool values to a single bool value. Output will be true, if one of the inputs is true
    /// </summary>
    public class OrConverter : MultiConverterBase<OrConverter>, IMultiValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var inputs = CastInput<bool>(values);
            return inputs.Any(i => i == true);
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
