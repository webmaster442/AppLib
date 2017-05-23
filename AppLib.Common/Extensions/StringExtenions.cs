using System;
using System.Globalization;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// String Extension methoods
    /// </summary>
    public static class StringExtenions
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
        public static string TitleCase(this string s)
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
        public static string TitleCase(this string s, CultureInfo culture)
        {
            var textinfo = culture.TextInfo;
            return textinfo.ToTitleCase(s);
        }
    }
}
