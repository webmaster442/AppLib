using AppLib.Common.Extensions;
using System.Collections.Generic;

namespace AppLib.Common
{
    public class CommandLineData
    {
        public HashSet<string> Switches
        {
            get;
            private set;
        }

        public Dictionary<string, string> SwitchArguments
        {
            get;
            private set;
        }

        public List<string> Arguments
        {
            get;
            private set;
        }

        public CommandLineData()
        {
            Switches = new HashSet<string>();
            SwitchArguments = new Dictionary<string, string>();
            Arguments = new List<string>();
        }

        public bool IsPresent(string swith, bool hasvalue)
        {
            if (hasvalue) return SwitchArguments.ContainsKey(swith);
            else return Switches.Contains(swith);
        }
    }

    public static class CommandLineParser
    {
        public static CommandLineData Parse(string[] arguments)
        {
            CommandLineData ret = new CommandLineData();

            int i = 0;
            int step = 0;
            while (i < arguments.Length)
            {
                var current = arguments[i];
                var next = (i + 1) < arguments.Length ? arguments[i + 1] : null;

                if (current.StartsWith("-"))
                {
                    var name = current.Substring(1);
                    if (current.StartsWith("--")) name = current.Substring(2);

                    if (!string.IsNullOrEmpty(next))
                    {
                        //next item is a switch
                        if (next.StartsWith("-"))
                        {
                            step = 1;
                            ret.Switches.Add(name);
                        }
                        else
                        {
                            step = 2;
                            ret.SwitchArguments.AddOrUpdate(name, next);
                        }
                    }
                    i += step;
                }
                else
                {
                    ret.Arguments.Add(current);
                    i += 1;
                }

            }
            return ret;
        }

    }
}
