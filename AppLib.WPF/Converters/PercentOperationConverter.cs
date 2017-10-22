using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// A percent calculation converter
    /// </summary>
    public class PercentOperationConverter : ConverterBase<PercentOperationConverter>, IValueConverter
    {
        /// <summary>
        /// A converter that performs percent calculation on the value
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">Operation parameter like: 60</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The provided value * (parameter/100)</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = System.Convert.ToDouble(value, culture);
            double p = System.Convert.ToDouble(parameter, culture);
            p /= 100.0d;

            return v * p;


        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>unmodified input</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
