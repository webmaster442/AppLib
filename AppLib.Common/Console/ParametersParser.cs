using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppLib.Common.Console
{
    /// <summary>
    /// Parameter Parser class
    /// based on: http://www.siepman.nl/blog/post/2014/03/26/command-line-arguments-parsing-query-with-Linq.aspx
    /// </summary>
    public class ParametersParser : IEnumerable<Parameter>
    {
        private readonly bool _caseSensitive;
        private readonly List<Parameter> _parameters;

        /// <summary>
        /// Parameters to parse
        /// </summary>
        public string ParametersText { get; private set; }

        /// <summary>
        /// Creates a new instance of parameter parser
        /// </summary>
        /// <param name="parametersText">
        /// Command line to parse. If not set, or null then
        /// the content will be read from the Environment
        /// </param>
        /// <param name="caseSensitive">set case sensitivity. Default is false.</param>
        /// <param name="keyValuesplitter">Key and value splitter char. The default value is =</param>
        public ParametersParser(
            string parametersText = null,
            bool caseSensitive = false,
            char keyValuesplitter = '=')
        {
            _caseSensitive = caseSensitive;
            ParametersText = parametersText != null ? parametersText : GetAllParametersText();
            _parameters = new BareParametersParser(ParametersText, keyValuesplitter)
                                 .Parameters.ToList();
        }

        /// <summary>
        /// Creates a new instance of parameter parser
        /// </summary>
        /// <param name="caseSensitive">set case sensitivity. Default is false.</param>
        public ParametersParser(bool caseSensitive)
            : this(null, caseSensitive)
        {
        }

        /// <summary>
        /// Returns the parameters data with the given keys
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>parameter strings with the given key</returns>
        public IEnumerable<Parameter> GetParameters(string key)
        {
            return _parameters.Where(p => p.Key.Equals(key, ThisStringComparison));
        }

        /// <summary>
        /// Returns the values data with the given keys
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>value datas with the given keys</returns>
        public IEnumerable<string> GetValues(string key)
        {
            return GetParameters(key).Where(p => p.HasValue).Select(p => p.Value);
        }

        /// <summary>
        /// Returns the first value string that matches a key
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>A string that is the first value, which matches the given key</returns>
        public string GetFirstValue(string key)
        {
            return GetFirstParameter(key).Value;
        }

        /// <summary>
        /// Returns a first parameter or null
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>The first parameter or if no parameters are present then null</returns>
        public Parameter GetFirstParameterOrDefault(string key)
        {
            return ParametersWithDistinctKeys.FirstOrDefault(KeyEqualsPredicate(key));
        }

        /// <summary>
        /// Returns a first parameter
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>The first parameter</returns>
        public Parameter GetFirstParameter(string key)
        {
            return ParametersWithDistinctKeys.First(KeyEqualsPredicate(key));
        }

        private Func<Parameter, bool> KeyEqualsPredicate(string key)
        {
            return p => p.Key.Equals(key, ThisStringComparison);
        }

        /// <summary>
        /// Gets an enumerable list of keys
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                return _parameters.Select(p => p.Key);
            }
        }

        /// <summary>
        /// Get an enumerable list of unique keys
        /// </summary>
        public IEnumerable<string> DistinctKeys
        {
            get
            {
                return ParametersWithDistinctKeys.Select(p => p.Key);
            }
        }

        /// <summary>
        /// Gets an enumerable list of parameters with unique name
        /// </summary>
        public IEnumerable<Parameter> ParametersWithDistinctKeys
        {
            get
            {
                return _parameters.GroupBy(k => k.Key, ThisEqualityComparer).Select(k => k.First());
            }
        }

        /// <summary>
        /// Checks, that a given key is present or not in the command line 
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>true, if the key is present, false if not</returns>
        public bool HasKey(string key)
        {
            return GetParameters(key).Any();
        }

        /// <summary>
        /// Checks, that a given key is present or not in the command line and that key has a value.
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>true, if the key is present and has a value, false if not present or hasn't got a value</returns>
        public bool HasKeyAndValue(string key)
        {
            var parameter = GetFirstParameterOrDefault(key);
            return parameter != null && parameter.HasValue;
        }

        /// <summary>
        /// Checks, that a given key is present and has no value.
        /// </summary>
        /// <param name="key">key to search for</param>
        /// <returns>true, if the key is present and hasn't got a value, false if not present or has a value</returns>
        public bool HasKeyAndNoValue(string key)
        {
            var parameter = GetFirstParameterOrDefault(key);
            return parameter != null && !parameter.HasValue;
        }

        private IEqualityComparer<string> ThisEqualityComparer
        {
            get
            {
                return _caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            }
        }

        private StringComparison ThisStringComparison
        {
            get
            {
                return _caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            }
        }

        /// <summary>
        /// Help key is present in the command line or not?
        /// </summary>
        public bool HasHelpKey
        {
            get
            {
                return HelpParameters.Any(h =>
                    _parameters.Any(p => p.Key.Equals(h, StringComparison.OrdinalIgnoreCase)));
            }
        }

        /// <summary>
        /// List of valid help keys
        /// </summary>
        public static IEnumerable<string> HelpParameters
        {
            get
            {
                return new[] { "?", "help", "-?", "/?", "-help", "/help" };
            }
        }

        private static string GetAllParametersText()
        {
            var everything = Environment.CommandLine;
            var executablePath = Environment.GetCommandLineArgs()[0];

            var result = everything.StartsWith("\"") ?
                everything.Substring(executablePath.Length + 2) :
                everything.Substring(executablePath.Length);
            result = result.TrimStart(' ');
            return result;
        }

        /// <summary>
        /// IEnumerable Implementation to support LINQ and foreach.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<Parameter> GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets a given parameter at the specified index
        /// </summary>
        /// <param name="index">index of parameter to be retrieved</param>
        /// <returns>the parameter at the given index</returns>
        public Parameter this[int index]
        {
            get
            {
                return _parameters[index];
            }
        }

        /// <summary>
        /// Returns the number of parsed parameters
        /// </summary>
        public int Count
        {
            get
            {
                return _parameters.Count;
            }
        }
    }
}
