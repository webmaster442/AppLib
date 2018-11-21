using System;
using System.Globalization;

namespace Webmaster442.Applib.Extensions
{ 
    /// <summary>
    /// Extension methods for string type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <param name="s">The string to search in</param>
        /// <param name="search">The string to seek.</param>
        /// <param name="comp">String comparision type</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool Contains(this string s, string search, StringComparison comp)
        {
            return s.IndexOf(search, comp) >= 0;
        }

        /// <summary>
        /// Converts a string to Title case
        /// </summary>
        /// <param name="s">string to convert to title case</param>
        /// <returns>a title cased string</returns>
        public static string ToTitleCase(this string s)
        {
            var textinfo = CultureInfo.CurrentCulture.TextInfo;
            return textinfo.ToTitleCase(s);
        }

        /// <summary>
        /// Converts a string to Title case
        /// </summary>
        /// <param name="s">string to convert to title case</param>
        /// <param name="culture">culture info</param>
        /// <returns>a title cased string</returns>
        public static string ToTitleCase(this string s, CultureInfo culture)
        {
            var textinfo = culture.TextInfo;
            return textinfo.ToTitleCase(s);
        }

        /// <summary>
        /// Returns the number of words in a string
        /// </summary>
        /// <param name="str">parameter string</param>
        /// <returns>number of words</returns>
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Reverses the input string
        /// </summary>
        /// <param name="input">parameter string</param>
        /// <returns>string in reversed order</returns>
        public static string Reverse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
