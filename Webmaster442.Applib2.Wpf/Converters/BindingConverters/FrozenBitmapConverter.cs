using System;
using System.Globalization;
using System.Windows.Data;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Converts an URI or string to a BitmapImage that is frozen.
    /// </summary>
    public class FrozenBitmapConverter : ConverterBase<FrozenBitmapConverter>, IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Uri uri)
                return BitmapHelper.CreateFrozenBitmap(uri);
            else if (value is string str)
                return BitmapHelper.CreateFrozenBitmap(str);
            else
                return Binding.DoNothing;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
