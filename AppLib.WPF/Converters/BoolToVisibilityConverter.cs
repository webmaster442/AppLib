using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts a visibility value to bool
    /// </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class VisibilityToBoolConverter : ConverterBase<VisibilityToBoolConverter>, IValueConverter
    {
        /// <summary>
        /// Converts a visibility to bool value
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>bool value. True if Visible, false if not</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vs = (Visibility)value;
            switch (vs)
            {
                case Visibility.Collapsed:
                case Visibility.Hidden:
                    return false;
                case Visibility.Visible:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Converts a bool to visibility
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Visibe, if value is true, otherwise Collapsed</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;
            return b == true ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
