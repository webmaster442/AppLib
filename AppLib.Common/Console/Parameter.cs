using System.Collections.Generic;
using System.Linq;

namespace AppLib.Common.Console
{
    /// <summary>
    /// Parameter class used with ParametersParser
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Gets the index of the parameter
        /// </summary>
        public int Index { get; private set; }

        private readonly IEnumerable<CharContext> _charContexts;

        internal Parameter(IEnumerable<CharContext> charContexts, int index)
        {
            Index = index;
            _charContexts = charContexts;
        }

        /// <summary>
        /// Converts the parameter to string
        /// </summary>
        /// <returns>A string representation of this object</returns>
        public override string ToString()
        {
            return NameRaw;
        }

        // Including quotes
        /// <summary>
        /// Gets the parameter name with quotes
        /// </summary>
        public string NameRaw
        {
            get
            {
                var charInfos = _charContexts.Select(c => c.Value);
                return new string(charInfos.ToArray());
            }
        }

        // Excluding quotes
        /// <summary>
        /// Gets the parameter name without quotes
        /// </summary>
        public string Name
        {
            get
            {
                var charInfos = _charContexts.Where(c => c.IsNetto).Select(c => c.Value);
                return new string(charInfos.ToArray());
            }
        }

        /// <summary>
        /// Gets the parameter key
        /// </summary>
        public string Key
        {
            get
            {
                if (!HasValue)
                {
                    return Name;
                }
                var valueChars = _charContexts.Take(IndexOfKeyValueSplitter)
                    .Where(c => c.IsNetto)
                    .Select(v => v.Value);
                var result = new string(valueChars.ToArray());
                return result;
            }
        }

        /// <summary>
        /// True, if the parameter has a value
        /// </summary>
        public bool HasValue
        {
            get
            {
                return IndexOfKeyValueSplitter > -1;
            }
        }

        /// <summary>
        /// Gets the value
        /// </summary>
        public string Value
        {
            get
            {
                if (!HasValue)
                {
                    return null;
                }
                var valueChars = _charContexts.Skip(IndexOfKeyValueSplitter + 1)
                    .Where(c => c.IsNetto)
                    .Select(v => v.Value);
                var result = new string(valueChars.ToArray());
                return result;
            }
        }

        private int IndexOfKeyValueSplitter
        {
            get
            {
                for (var index = 0; index < _charContexts.Count(); index++)
                {
                    var charContext = _charContexts.ElementAt(index);
                    if (charContext.IsKeyValueSplitter)
                    {
                        return index;
                    }
                }
                return -1;
            }
        }

    }
}
