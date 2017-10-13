using System;
using System.Globalization;
using System.Windows.Data;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Performs basic math operation on a binding
    /// </summary>
    public class OperationConverter : ConverterBase<OperationConverter>, IValueConverter
    {
        /// <summary>
        /// A converter that performs basic operations on the binded value
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">
        /// Operation parameter like: + 20
        /// Operation symbol and value must be sepparated by space
        /// The following operation parameters are supported:
        /// +, +=, -, -=, *, *=, /, /=, %, %=, ++, -- 
        /// </param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>difference between the parameter time and the current time as string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = System.Convert.ToDouble(value, culture);
            string p = System.Convert.ToString(parameter);

            var ops = p.Split(' ');
            if (ops.Length < 2)
            {
                return v;
            }

            double p2 = 0;

            if (!double.TryParse(ops[1], NumberStyles.Float, culture, out p2))
                return v;


            switch (ops[0])
            {
                case "*":
                case "*=":
                    return v * p2;
                case "-":
                case "-=":
                    return v - p2;
                case "+":
                case "+=":
                    return v + p2;
                case "/":
                case "/=":
                    return v / p2;
                case "%":
                case "%=":
                    return v % p2;
                case "++":
                    return v++;
                case "--":
                    return v--;
                default:
                    return v;
            }

        }

        /// <summary>
        /// Returns the unmodified input
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>unmodified input</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
