using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts seconds to human readable time
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class SecondsToTimeStringConverter : ConverterBase<SecondsToTimeStringConverter>, IValueConverter
    {
        /// <summary>
        /// Converts seconds to human readable time format
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A human redable time format</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var input = System.Convert.ToDouble(value, culture);
                var timespan = TimeSpan.FromSeconds(input);

                if (timespan.TotalMinutes > 60)
                    return string.Format("{0:00}:{1:00}:{2:00}", timespan.Hours, timespan.Minutes, timespan.Seconds);
                else
                    return string.Format("{0:00}:{1:00}", timespan.Minutes, timespan.Seconds);

            }
            catch (Exception)
            {
                return value;
            }
        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>input</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
