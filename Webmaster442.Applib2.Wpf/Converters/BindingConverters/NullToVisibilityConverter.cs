using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Convert a value to visibility.
    /// </summary>
    public class NullToVisibilityConverter : ConverterBase<NullToVisibilityConverter>, IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            if (value == null)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
