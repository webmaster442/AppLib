using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Bool to Visibility converter with markup extension
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : ConverterBase<BoolToVisibilityConverter>, IValueConverter
    {
        /// <summary>
        /// Converts a bool to visibility
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Visibe, if value is true, otherwise Collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (bool)value;

            if (parameter != null)
            {
                if (!input)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
            {
                if (input)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }


        /// <summary>
        /// Converts a visibility to bool value
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>bool value. True if Visible, false if not</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility input = (Visibility)value;

            if (parameter != null)
            {
                switch (input)
                {
                    case Visibility.Visible:
                        return false;
                    case Visibility.Collapsed:
                    case Visibility.Hidden:
                    default:
                        return true;

                }
            }
            else
            {
                switch (input)
                {
                    case Visibility.Visible:
                        return true;
                    case Visibility.Collapsed:
                    case Visibility.Hidden:
                    default:
                        return false;

                }
            }
        }
    }
}
