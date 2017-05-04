using System;

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
            return s.IndexOf(search, comp) > 0;
        }
    }
}
