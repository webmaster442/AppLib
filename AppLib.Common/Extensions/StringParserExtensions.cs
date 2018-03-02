using System;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// String conversion extensions
    /// </summary>
    public static class StringParserExtensions
    {
        /// <summary>
        /// Convert string to decimal
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static decimal? ToDecimal(this string s)
        {
            decimal result;
            if (decimal.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to double
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static double? ToDouble(this string s)
        {
            double result;
            if (double.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to float
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static float? ToFloat(this string s)
        {
            float result;
            if (float.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to int
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static int? ToInt(this string s)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to long
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static long? ToLong(this string s)
        {
            long result;
            if (long.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to DateTime
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static DateTime? ToDate(this string s)
        {
            DateTime result;
            if (DateTime.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Convert string to TimeSpan
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>converted value</returns>
        public static TimeSpan? ToTimespan(this string s)
        {
            TimeSpan result;
            if (TimeSpan.TryParse(s, out result))
            {
                return result;
            }
            return null;
        }
    }
}
