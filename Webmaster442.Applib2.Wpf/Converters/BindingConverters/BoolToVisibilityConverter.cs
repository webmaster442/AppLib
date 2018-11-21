using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Bool to Visibility converter with markup extension
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : ConverterBase<BoolToVisibilityConverter>, IValueConverter
    {
        /// <inheritdoc/>
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


        /// <inheritdoc/>
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
