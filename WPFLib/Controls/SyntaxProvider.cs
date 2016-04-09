using System.Collections.Generic;
using System.Linq;

namespace WPFLib.Controls
{
    /// <summary>
    /// Syntax provider interface
    /// </summary>
    public abstract class SyntaxProvider
    {
        /// <summary>
        /// KeyWords
        /// </summary>
        public abstract IEnumerable<string> KeyWords { get; }
        /// <summary>
        /// Special chars
        /// </summary>
        public abstract IEnumerable<char> SpecialChars { get; }
        /// <summary>
        /// checks if the current word is a Known keyword or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsKnownKeyWord(string s)
        {
            return KeyWords.Contains(s);
        }
    }
}
