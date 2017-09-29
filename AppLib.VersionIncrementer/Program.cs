using System;

namespace AppLib.VersionIncrementer
{
    class Program
    {
        /// <summary>
        /// Help
        /// </summary>
        internal static void Help()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("VersionIncrementer <VersioningTemplate.xml> <assembly info template> <assembly info to modify>");
            Console.WriteLine("VersionIncrementer /?");
            Console.WriteLine("VersionIncrementer /createtemplate <VersioningTemplate.xml>");
        }

        /// <summary>
        /// Display an exception
        /// </summary>
        /// <param name="ex">Exception to display</param>
        internal static void Error(Exception ex)
        {
            var def = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = def;
            Environment.Exit(-1);
        }


        public static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "/?")
            {
                Help();
                return;
            }
            else if (args.Length ==2 && args[0] == "/createtemplate")
            {
                IcrementerLogic.CreateTemplate(args[1]);
                return;
            }
            else if (args.Length == 3)
            {
                IcrementerLogic.Increment(args[0], args[1], args[2]);
                return;
            }
            else
            {
                Help();
                return;
            }
        }
    }
}
