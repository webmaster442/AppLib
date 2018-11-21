using System;
using System.Globalization;
using System.Windows.Data;
using Webmaster442.Applib.Controls;
using Webmaster442.Applib.Converters;

namespace Webmaster442.Applib.Internals
{
    internal class ProgressToAngleConverter : ConverterBase<ProgressToAngleConverter>, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double progress = (double)values[0];
            CircularProgressBar bar = values[1] as CircularProgressBar;

            return 359.999 * (progress / (bar.Maximum - bar.Minimum));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing };
        }
    }
}
