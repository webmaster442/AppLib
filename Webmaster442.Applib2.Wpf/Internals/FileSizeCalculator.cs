namespace Webmaster442.Applib.Internals
{
    internal static class FileSizeCalculator
    {
        /// <summary>
        /// Calculate a file size in bytes to a human readable size
        /// </summary>
        /// <param name="value">long input value</param>
        /// <returns>Human readable file size</returns>
        public static string Calculate(long value)
        {
            double val = System.Convert.ToDouble(value);
            string unit = "Byte";
            if (val > 1125899906842624)
            {
                val /= 1125899906842624;
                unit = "EiB";
            }
            else if (val > 1099511627776D)
            {
                val /= 1099511627776D;
                unit = "TiB";
            }
            else if (val > 1073741824D)
            {
                val /= 1073741824D;
                unit = "GiB";
            }
            else if (val > 1048576D)
            {
                val /= 1048576D;
                unit = "MiB";
            }
            else if (val > 1024D)
            {
                val /= 1024D;
                unit = "kiB";
            }
            return string.Format("{0:0.###} {1}", val, unit);
        }
    }
}
