using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Enum to bool value converter. Usage:
    /// &lt;RadioButton IsChecked="{Binding Path=Type, Converter={applib:EnumToBoolConverter}, ConverterParameter={x:Static local:CompanyTypes.Type1Comp}}" Content=""/&gt;
    /// </summary>
    public class EnumToBoolConverter: ConverterBase<EnumToBoolConverter>, IValueConverter
    {
        /// <summary>
        /// Converts an enum value to bool
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>enum value to bool</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.Equals(value))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Converts back
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>parameter name</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
