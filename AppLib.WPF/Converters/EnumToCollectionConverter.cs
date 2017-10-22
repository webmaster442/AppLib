using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Converts an enumeration to a List
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(IEnumerable<string>))]
    public class EnumToCollectionConverter : ConverterBase<EnumToCollectionConverter>, IValueConverter
    {
        private string GetDesciption(Enum enumItem)
        {
            var nAttributes = 
                enumItem.
                GetType().
                GetField(enumItem.ToString()).
                GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (nAttributes.Any())
                return (nAttributes.First() as DescriptionAttribute).Description;

            TextInfo oTI = CultureInfo.CurrentCulture.TextInfo;
            return oTI.ToTitleCase(oTI.ToLower(enumItem.ToString().Replace("_", " ")));
        }

        /// <summary>
        /// converts enumeration to a list of descriptions
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. can be words or lines</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>enumeration as a list of descriptions</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value.GetType();
            if (!t.IsEnum)
                return null;

            return Enum.GetValues(t).Cast<Enum>().Select((e) => GetDesciption(e)).ToList();
        }

        /// <summary>
        /// Returns null
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. can be words or lines</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
