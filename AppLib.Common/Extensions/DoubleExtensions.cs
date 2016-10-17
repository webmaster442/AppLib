using System;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// Double number extension methods
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Compare two doubles with a maximum allowed difference.
        /// If differnece is not set, then by default 0.00001 is aplied
        /// </summary>
        /// <param name="d">double number</param>
        /// <param name="other">other double number</param>
        /// <param name="diff">maximum allowed difference.</param>
        /// <returns></returns>
        public static bool EqualsWithTolerance(this Double d, double other, double diff = 0.00001)
        {
            double sub = 0;
            if (other > d) sub = other - d;
            else sub = d - other;
            return sub < diff;
        }
    }
}
