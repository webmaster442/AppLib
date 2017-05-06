using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLib.Common.Console;

namespace Tests.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var format = Terminal.CreateFormat(FormatOptions.Underline, FormatOptions.BoldBright);
            Terminal.Init();
            for (byte i=1; i<255; i++)
            {
                Terminal.WriteWithColor(" ", 0xff, i);
                if (i % 80 == 0)
                    Console.WriteLine();
            }

            for (byte r=1; r<255; r++)
            {
                for (byte g = 1; g < 255; g++)
                {
                    for (byte b = 1; b < 255; b++)
                    {
                        Terminal.RGBColor c = new Terminal.RGBColor
                        {
                            R = r,
                            G = g,
                            B = b
                        };
                        Terminal.WriteWithColor(" ", c, c);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
