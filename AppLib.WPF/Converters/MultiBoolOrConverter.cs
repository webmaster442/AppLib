using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts multiple bool values to a single bool value. Output will be true, if one of the outputs is true
    /// </summary>
    public class MultiBoolOrConverter : IMultiValueConverter
    {
        /// <summary>
        ///  Converts multiple bool values to a single bool value.
        ///  Output is true, if one of the values is true
        /// </summary>
        /// <param name="values">an array of values</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">input culture</param>
        /// <returns>Output is true, if one of the values is true</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var q = from i in values.Cast<bool>()
                    where i == true
                    select i;

            return q.Count() > 0 ? true : false;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetTypes">target type</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
