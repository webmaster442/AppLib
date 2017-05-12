namespace AppLib.Common.Extensions
{
    /// <summary>
    /// String conversion extensions
    /// </summary>
    public static class StringConversionExtensions
    {
        /// <summary>
        /// Converts a string to Byte
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static byte ToByte(this string s, byte defaultvalue)
        {
            byte parsed;
            if (byte.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Byte
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static byte? ToByte(this string s)
        {
            byte parsed;
            if (byte.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to short
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static short ToShort(this string s, short defaultvalue)
        {
            short parsed;
            if (short.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to short
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static short? ToShort(this string s)
        {
            short parsed;
            if (short.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to Int
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static int ToInt(this string s, int defaultvalue)
        {
            int parsed;
            if (int.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Int
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static int? ToInt(this string s)
        {
            int parsed;
            if (int.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to Long
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static long ToLong(this string s, long defaultvalue)
        {
            long parsed;
            if (long.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Long
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static long? ToLong(this string s)
        {
            long parsed;
            if (long.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to Float
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static float ToFloat(this string s, float defaultvalue)
        {
            float parsed;
            if (float.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Float
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static float? ToFloat(this string s)
        {
            float parsed;
            if (float.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to Double
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static double ToDouble(this string s, double defaultvalue)
        {
            double parsed;
            if (double.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Double
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static double? ToDouble(this string s)
        {
            double parsed;
            if (double.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to Decimal
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static decimal ToDecimal(this string s, decimal defaultvalue)
        {
            decimal parsed;
            if (decimal.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to Decimal
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static decimal? ToDecimal(this string s)
        {
            decimal parsed;
            if (decimal.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to SByte
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static sbyte ToSByte(this string s, sbyte defaultvalue)
        {
            sbyte parsed;
            if (sbyte.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to SByte
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static sbyte? ToSByte(this string s)
        {
            sbyte parsed;
            if (sbyte.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to ushort
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static ushort ToUShort(this string s, ushort defaultvalue)
        {
            ushort parsed;
            if (ushort.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to ushort
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static ushort? ToUShort(this string s)
        {
            ushort parsed;
            if (ushort.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to UInt
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static uint ToUInt(this string s, uint defaultvalue)
        {
            uint parsed;
            if (uint.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to UInt
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static uint? ToUInt(this string s)
        {
            uint parsed;
            if (uint.TryParse(s, out parsed)) return parsed;
            else return null;
        }

        /// <summary>
        /// Converts a string to ULong
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="defaultvalue">Fallback vallue</param>
        /// <returns>Returns the converted string, if conversion was not possible, the default value is returned</returns>
        public static ulong ToULong(this string s, ulong defaultvalue)
        {
            ulong parsed;
            if (ulong.TryParse(s, out parsed)) return parsed;
            else return defaultvalue;
        }

        /// <summary>
        /// Converts a string to ULong
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>Returns the converted string, if conversion was not possible, null is returned</returns>
        public static ulong? ToULong(this string s)
        {
            ulong parsed;
            if (ulong.TryParse(s, out parsed)) return parsed;
            else return null;
        }
    }
}
