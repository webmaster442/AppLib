using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the number of words in a string
        /// </summary>
        /// <param name="str">parameter string</param>
        /// <returns>number of words</returns>
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' },StringSplitOptions.RemoveEmptyEntries).Length;
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
            return new String(chars);
        }

        /// <summary>
        /// Checks if the input is a valid e-mail or not.
        /// </summary>
        /// <param name="input">parameter string</param>
        /// <returns>true, if the input is an e-mail adress</returns>
        public static bool IsEmail(this string input)
        {
            var regex = new Regex(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$");
            return regex.IsMatch(input);
        }
    }
}
