using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// converts a string to word count or line count 
    /// </summary>
    [ValueConversion(typeof(string), typeof(int))]
    public class StringInfoConverter : ConverterBase<StringInfoConverter>, IValueConverter
    {
        /// <summary>
        /// converts a string to word count or line count 
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. can be words or lines</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>word or line count based on parameter</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var str = value?.ToString();
            var par = parameter.ToString().ToLower();

            switch (par)
            {
                case "words":
                    return Count(str, (c) =>
                    {
                        return char.IsWhiteSpace(c);
                    });
                case "lines":
                    return Count(str, (c) =>
                    {
                        return c == '\n';
                    });
                default:
                    return "No converter parameter given. Valid converter parameters are: words, lines";
            }

        }

        private int Count(string s, Predicate<char> process)
        {
            var count = 0;
            for (int i=0; i<s.Length; i++)
            {
                if (process(s[i]))
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>string, file size</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
