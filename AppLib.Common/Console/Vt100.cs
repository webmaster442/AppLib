using AppLib.Common.PInvoke;
using System;
using System.Text;

namespace AppLib.Common.Console
{
    /// <summary>
    /// A wrapper for the Windows 10 VT100 emulation
    /// </summary>
    public static class Terminal
    {
        private const int STD_INPUT_HANDLE = -10;
        private const int STD_OUTPUT_HANDLE = -11;

        /// <summary>
        /// Enables the VT100 support in the cmd.exe
        /// </summary>
        public static void Init()
        {
            var iStdIn = Kernel32.GetStdHandle(STD_INPUT_HANDLE);
            var iStdOut = Kernel32.GetStdHandle(STD_OUTPUT_HANDLE);

            if (!Kernel32.GetConsoleMode(iStdIn, out uint inConsoleMode))
                throw new InvalidOperationException("Failed to get input console mode");

            if (!Kernel32.GetConsoleMode(iStdOut, out uint outConsoleMode))
                throw new InvalidOperationException("Failed to get output console mode");

            inConsoleMode |= ConsoleModes.ENABLE_VIRTUAL_TERMINAL_INPUT;
            outConsoleMode |= ConsoleModes.ENABLE_VIRTUAL_TERMINAL_PROCESSING | ConsoleModes.DISABLE_NEWLINE_AUTO_RETURN;

            if (!Kernel32.SetConsoleMode(iStdIn, inConsoleMode))
                throw new InvalidOperationException("Failed to set input console mode");

            if (!Kernel32.SetConsoleMode(iStdOut, outConsoleMode))
                throw new InvalidOperationException("Failed to set output console mode");
        }

        /// <summary>
        /// Creates a VT100 terminal format string
        /// </summary>
        /// <param name="formats">formats to use</param>
        /// <returns>format string</returns>
        public static string CreateFormat(params FormatOptions[] formats)
        {
            StringBuilder sb = new StringBuilder();
            if (formats.Length > 0)
                sb.Append("\x1b[");

            for (int i=0; i<formats.Length; i++)
            {
                if (i + 1 < formats.Length)
                    sb.AppendFormat("{0};", (int)formats[i]);
                else
                    sb.AppendFormat("{0}m", (int)formats[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Write a string to the console using the specified format
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="format">Format to use</param>
        /// <param name="pars">parameters</param>
        public static void WriteFormated(string text, string format, params object[] pars)
        {
            System.Console.Write(format+text, pars);
        }

        /// <summary>
        /// Write a line to the console using the specified format
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="format">Format to use</param>
        /// <param name="pars">parameters</param>
        public static void WriteLineFormated(string text, string format, params object[] pars)
        {
            System.Console.WriteLine(format + text, pars);
        }

        /// <summary>
        /// Write a text with a custom color
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="foreground">Foreground color</param>
        /// <param name="background">Background color, can be null</param>
        /// <param name="pars">paramters</param>
        public static void WriteWithColor(string text, RGBColor foreground, RGBColor background, params object[] pars)
        {
            var format = string.Format("\x1b[38;2;{0};{1};{2}", foreground.R, foreground.G, foreground.B);
            if (background != null)
                format += string.Format(";48;2;{0};{1};{2}", background.R, background.G, background.B);
            WriteFormated(text, format+"m", pars);
        }

        /// <summary>
        /// Write a line with a custom color
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="foreground">Foreground color</param>
        /// <param name="background">Background color, can be null</param>
        /// <param name="pars">paramters</param>
        public static void WriteLineWithColor(string text, RGBColor foreground, RGBColor background, params object[] pars)
        {
            var format = string.Format("\x1b[38;2;{0};{1};{2}", foreground.R, foreground.G, foreground.B);
            if (background != null)
                format += string.Format(";48;2;{0};{1};{2}", background.R, background.G, background.B);
            WriteLineFormated(text, format + "m", pars);
        }

        /// <summary>
        /// Write a text with a palete color
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="foreground">Foreground palate index</param>
        /// <param name="background">Background palete index, can be null.</param>
        /// <param name="pars">parameters</param>
        public static void WriteWithColor(string text, byte foreground, byte? background, params object[] pars)
        {
            var format = string.Format("\x1b[38;5;{0}", foreground);
            if (background != null)
                format += string.Format(";48;5;{0}", background.Value);
            WriteFormated(text, format+"m", pars);
        }

        /// <summary>
        /// Write a line with a palete color
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="foreground">Foreground palate index</param>
        /// <param name="background">Background palete index, can be null.</param>
        /// <param name="pars">parameters</param>
        public static void WriteLineWithColor(string text, byte foreground, byte? background, params object[] pars)
        {
            var format = string.Format("\x1b[38;5;{0}", foreground);
            if (background != null)
                format += string.Format(";48;5;{0}", background.Value);
            WriteLineFormated(text, format + "m", pars);
        }

        /// <summary>
        /// Pause current execution. Continue on any key press
        /// </summary>
        /// <param name="message">Pause message</param>
        public static void Pause(string message)
        {
            System.Console.WriteLine(message);
            System.Console.ReadKey();
        }
    }

    /// <summary>
    /// Format options
    /// </summary>
    public enum FormatOptions: int
    {
        /// <summary>
        /// Returns all attributes to the default state prior to modification
        /// </summary>
        Default = 0,
        /// <summary>
        /// Applies brightness/intensity flag to foreground color
        /// </summary>
        BoldBright = 1,
        /// <summary>
        /// Adds underline
        /// </summary>
        Underline = 4,
        /// <summary>
        /// Removes underline
        /// </summary>
        NoUnderline = 24,
        /// <summary>
        /// Swaps foreground and background colors
        /// </summary>
        Negative = 7,
        /// <summary>
        /// Returns foreground/background to normal 
        /// </summary>
        Positive = 27,
        /// <summary>
        /// Applies non-bold/bright black to foreground
        /// </summary>
        ForegroundBlack = 30,
        /// <summary>
        /// Applies non-bold/bright red to foreground
        /// </summary>
        ForegroundRed = 31,
        /// <summary>
        /// Applies non-bold/bright green to foreground
        /// </summary>
        ForegroundGreen = 32,
        /// <summary>
        /// Applies non-bold/bright yellow to foreground
        /// </summary>
        ForegroundYellow = 33,
        /// <summary>
        /// Applies non-bold/bright blue to foreground
        /// </summary>
        ForegroundBlue = 34,
        /// <summary>
        /// pplies non-bold/bright magenta to foreground
        /// </summary>
        ForegroundMagenta = 35,
        /// <summary>
        /// Applies non-bold/bright cyan to foreground
        /// </summary>
        ForegroundCyan = 36,
        /// <summary>
        /// Applies non-bold/bright white to foreground
        /// </summary>
        ForegroundWhite = 37,
        /// <summary>
        /// Applies only the foreground portion of the defaults 
        /// </summary>
        ForegroundDefault = 39,
        /// <summary>
        /// Applies non-bold/bright black to background
        /// </summary>
        BackgroundBlack = 40,
        /// <summary>
        /// Applies non-bold/bright red to background
        /// </summary>
        BackgroundRed = 41,
        /// <summary>
        /// Applies non-bold/bright green to background
        /// </summary>
        BackgroundGreen = 42,
        /// <summary>
        /// Applies non-bold/bright yellow to background
        /// </summary>
        BackgroundYellow = 43,
        /// <summary>
        /// Applies non-bold/bright blue to background
        /// </summary>
        BackgroundBlue = 44,
        /// <summary>
        /// Applies non-bold/bright magenta to background
        /// </summary>
        BackgroundMagenta = 45,
        /// <summary>
        /// Applies non-bold/bright cyan to background
        /// </summary>
        BackgroundCyan = 46,
        /// <summary>
        /// Applies non-bold/bright white to background
        /// </summary>
        BackgroundWhite = 47,
        /// <summary>
        /// Applies only the background portion of the defaults
        /// </summary>
        BackgroundDefault = 49,
        /// <summary>
        /// Applies bold/bright black to foreground
        /// </summary>
        BrightForegroundBlack = 90,
        /// <summary>
        /// Applies bold/bright red to foreground
        /// </summary>
        BrightForegroundRed = 91,
        /// <summary>
        /// Applies bold/bright green to foreground
        /// </summary>
        BrightForegroundGreen = 92,
        /// <summary>
        /// Applies bold/bright yellow to foreground
        /// </summary>
        BrightForegroundYellow = 93,
        /// <summary>
        /// Applies bold/bright blue to foreground
        /// </summary>
        BrightForegroundBlue = 94,
        /// <summary>
        /// Applies bold/bright magenta to foreground
        /// </summary>
        BrightForegroundMagenta = 95,
        /// <summary>
        /// Applies bold/bright cyan to foreground
        /// </summary>
        BrightForegroundCyan = 96,
        /// <summary>
        /// Applies bold/bright white to foreground
        /// </summary>
        BrightForegroundWhite = 97,
        /// <summary>
        /// Applies bold/bright black to background
        /// </summary>
        BrightBackgroundBlack = 100,
        /// <summary>
        /// Applies bold/bright red to background
        /// </summary>
        BrightBackgroundRed = 101,
        /// <summary>
        /// Applies bold/bright green to background
        /// </summary>
        BrightBackgroundGreen = 102,
        /// <summary>
        /// Applies bold/bright yellow to background
        /// </summary>
        BrightBackgroundYellow = 103,
        /// <summary>
        /// Applies bold/bright blue to background
        /// </summary>
        BrightBackgroundBlue = 104,
        /// <summary>
        /// Applies bold/bright magenta to background
        /// </summary>
        BrightBackgroundMagenta = 105,
        /// <summary>
        /// Applies bold/bright cyan to background
        /// </summary>
        BrightBackgroundCyan = 106,
        /// <summary>
        /// Applies bold/bright white to background
        /// </summary>
        BrightBackgroundWhite = 107

    }
}
