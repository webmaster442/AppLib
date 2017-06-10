using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Calculates the difference between the parameter time and the current time
    /// </summary>
    public class TimePassedSinceConverter : ConverterBase<TimePassedSinceConverter>, IValueConverter
    {
        /// <summary>
        /// Calculates the difference between the parameter time and the current time
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. can be words or lines</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>difference between the parameter time and the current time as string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime == false)
                return "Input a date parameter";

            var dt = (DateTime)value;
            var diff = DateTime.Now - dt;
            if (diff.TotalSeconds < 60)
                return string.Format("{0:0.0} sec ago", diff.TotalSeconds);
            else if (diff.TotalMinutes < 60)
                return string.Format("{0:0.0} minutes ago", diff.TotalMinutes);
            else if (diff.TotalHours < 60)
                return string.Format("{0:0.0} hrs ago", diff.TotalHours);
            else if (diff.TotalDays < 365)
                return string.Format("{0:0.0} days ago", diff.TotalDays);
            else
                return string.Format("{0:0.0} years ago", diff.TotalDays / 365.0);
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
