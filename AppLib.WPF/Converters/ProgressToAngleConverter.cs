using AppLib.WPF.Controls;
using System;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    internal class ProgressToAngleConverter: ConverterBase<ProgressToAngleConverter>, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double progress = (double)values[0];
            CircularProgressBar bar = values[1] as CircularProgressBar;

            return 359.999 * (progress / (bar.Maximum - bar.Minimum));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
