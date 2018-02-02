using AppLib.Common.Extensions;
using System.Collections.Generic;

namespace AppLib.Common.Console
{
    /// <summary>
    /// A parameter parser class
    /// </summary>
    public class ParameterParser
    {
        private readonly string[] _CommandLineParts;
        private readonly List<string> _Files;
        private readonly List<string> _Switches;
        private readonly Dictionary<string, string> _SwithcesWithValues;

        private static string[] ParseArguments(string commandLine)
        {
            char[] parmChars = commandLine.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }
            return (new string(parmChars)).Split('\n');
        }

        private void Process()
        {
            int i = 0;
            int increment = 0;
            do
            {
                var current = _CommandLineParts[i];
                var next = GetNext(_CommandLineParts, i);
                if (current.StartsWith("/"))
                {
                    if (!next.StartsWith("/"))
                    {
                        //switch with value
                        _SwithcesWithValues.AddOrUpdate(current, next);
                        increment = 2;
                    }
                    else
                    {
                        //single switch
                        _Switches.Add(current);
                        increment = 1;
                    }
                }
                else
                {
                    //file
                    _Files.Add(current);
                    increment = 1;
                }

                i += increment;
            }
            while (i < _CommandLineParts.Length - 1);
        }

        private string GetNext(string[] commandLineParts, int i)
        {
            int nextindex = i + 1;
            if (nextindex < commandLineParts.Length) return commandLineParts[nextindex];
            else return "";
        }

        private ParameterParser()
        {
            _Files = new List<string>();
            _Switches = new List<string>();
            _SwithcesWithValues = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates a new command line parameter parser
        /// </summary>
        /// <param name="commandLine">command line string</param>
        /// <param name="lowercase">force lowecasing arguments</param>
        public ParameterParser(string commandLine, bool lowercase = true): this()
        {
            if (lowercase)
                _CommandLineParts = ParseArguments(commandLine.ToLower());
            else
                _CommandLineParts = ParseArguments(commandLine);

            Process();
        }

        /// <summary>
        /// Create a parameter parser from an allready tokenized input
        /// </summary>
        /// <param name="arguments">tokenized input</param>
        public ParameterParser(string[] arguments) : this()
        {
            _CommandLineParts = arguments;

            Process();
        }

        /// <summary>
        /// Standalone switches
        /// </summary>
        public IList<string> StandaloneSwitches
        {
            get { return _Switches; }
        }

        /// <summary>
        /// Files
        /// </summary>
        public IList<string> Files
        {
            get { return _Files; }
        }

        /// <summary>
        /// Switches with values
        /// </summary>
        public IDictionary<string, string> SwitchesWithValue
        {
            get { return _SwithcesWithValues; }
        }
    }
}
