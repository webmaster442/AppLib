using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Converts multiple bool values to a single bool value. Output will be true, if all the inputs are true.
    /// </summary>
    public class AndConverter : MultiConverterBase<AndConverter>, IMultiValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var inputs = CastInput<bool>(values);
            return inputs.All(i => i == true);
        }

        /// <inheritdoc/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
