using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Converts a visibility value to bool
    /// </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class VisibilityToBoolConverter : ConverterBase<VisibilityToBoolConverter>, IValueConverter
    {
        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;
            return b == true ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
