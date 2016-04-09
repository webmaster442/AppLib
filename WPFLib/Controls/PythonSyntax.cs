using System.Collections.Generic;

namespace WPFLib.Controls
{
    /// <summary>
    /// Python Syntax Provider
    /// </summary>
    public class PythonSyntax : SyntaxProvider
    {
        static readonly string[] _keywords;
        static readonly char[] _specials;

        static PythonSyntax()
        {
            _specials = new char[] { '.', ')', '(', '[', ']', '>', '<', ':', '\n', '\t' };
            _keywords = new string[]
            {
                "and", "del", "from", "not", "while", "as", " elif",
                "global", "or", "with", "assert", "else", "if", "pass",
                "yield", "break", "except", "import", "print", "class",
                "exec", "in", "raise", "continue",  "finally",  "is", "return",
                "def", "for", "lambda", "try"
            };
        }

        /// <summary>
        /// Python KeyWords
        /// </summary>
        public override IEnumerable<string> KeyWords
        {
            get { return _keywords; }
        }

        /// <summary>
        /// Python Special chars
        /// </summary>
        public override IEnumerable<char> SpecialChars
        {
            get { return _specials; }
        }
    }
}
