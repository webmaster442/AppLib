using System;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// Extension methods for float tpye
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Compare two floats with a maximum allowed difference.
        /// If differnece is not set, then by default 0.00001 is aplied
        /// </summary>
        /// <param name="a">float number</param>
        /// <param name="b">other float number</param>
        /// <param name="tolerance">maximum allowed difference.</param>
        /// <returns></returns>
        public static bool EqualsWithTolerance(this float a, float b, float tolerance = 1E-6f)
        {
            bool aIsNaN = float.IsNaN(a);
            bool bIsNaN = float.IsNaN(b);

            if (aIsNaN || bIsNaN)
            {
                return aIsNaN && bIsNaN ? true : false;
            }

            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a == b)
            {
                // shortcut, handles infinities and precise int
                return true;
            }
            else if (a == 0 || b == 0 || diff < float.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (tolerance * float.Epsilon);
            }
            else
            { // use relative error
                return diff / Math.Min((absA + absB), float.MaxValue) < tolerance;
            }
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
        public static float Map(this float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        /// <summary>
        /// Get a certain percentage of the specified number.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentage">The percentage of the specified number to get. If value > 100, then it's conscidered as 100 percent</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static float GetPercentage(this float value, uint percentage)
        {
            if (percentage > 100u) percentage = 100u;
            var percentAsfloat = (float)percentage / 100;
            return value * percentAsfloat;
        }
    }
}
