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
        public static bool EqualsWithTolerance(this double d, double other, double diff = 0.00001)
        {
            double sub = 0;
            if (other > d) sub = other - d;
            else sub = d - other;
            return sub < diff;
        }

        /// <summary>
        /// Linear interpolation between two values
        /// </summary>
        /// <param name="d">double number</param>
        /// <param name="other">other number</param>
        /// <param name="amount">amount of interpolation. Must be berween 0 and 1</param>
        /// <returns>a value between the first parameter and the second parameter based on the amount</returns>
        public static double Lerp(this double d, double other, double amount)
        {
            if (amount < 0 || amount > 1)
                throw new ArgumentException("amount must be between 0 and 1");
            return (1 - amount) * d + amount * other;
        }

        /// <summary>
        /// Re-maps a number from one range to another. That is, a value of fromLow would get mapped to toLow, a value of fromHigh to toHigh, values in-between to values in-between, etc. 
        /// </summary>
        /// <param name="x">the number to map</param>
        /// <param name="in_min">the lower bound of the value's current range</param>
        /// <param name="in_max">the upper bound of the value's current range</param>
        /// <param name="out_min">the lower bound of the value's target range</param>
        /// <param name="out_max">the upper bound of the value's target range</param>
        /// <returns></returns>
        public static double Map(this double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        /// <summary>
        /// Get a certain percentage of the specified number.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentage">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static double GetPercentage(this double value, int percentage)
        {
            var percentAsDouble = (double)percentage / 100;
            return value * percentAsDouble;
        }
    }
}
