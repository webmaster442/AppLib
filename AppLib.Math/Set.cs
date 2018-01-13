using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.Maths
{
    /// <summary>
    /// A Set of numbers
    /// </summary>
    public class Set : List<double>
    {
        /// <summary>
        /// Create a set from a list of numbers
        /// </summary>
        /// <param name="numbers">list of numbers</param>
        public Set(params double[] numbers) : base(numbers.Length)
        {
            AddRange(numbers);
        }


        /// <summary>
        /// Create a set from a list of numbers
        /// </summary>
        /// <param name="itms">list of numbers</param>
        public Set(List<double> itms) : base(itms) { }

        /// <summary>
        /// Create a set
        /// </summary>
        /// <param name="count">Number of items to hold</param>
        public Set(int count) : base(count) { }

        /// <summary>
        /// Add a number to every item in the set
        /// </summary>
        /// <param name="input">Input set</param>
        /// <param name="number">number to add</param>
        /// <returns>set with added items</returns>
        public static Set operator +(Set input, double number)
        {
            var copy = new Set(input.Count);
            Parallel.For(0, input.Count, i =>
            {
                copy[i] = input[i] + number;
            });
            return copy;
        }

        /// <summary>
        /// Add a set's values to a nother set
        /// </summary>
        /// <param name="a">Input set 1</param>
        /// <param name="b">Input set 2</param>
        /// <returns>the sum of two sets</returns>
        public static Set operator +(Set a, Set b)
        {
            var min = System.Math.Min(a.Count, b.Count);
            var result = new Set(min);
            Parallel.For(0, min, i =>
            {
                result[i] = a[i] + b[i];
            });
            return result;
        }

        /// <summary>
        /// Subtract a number from a set's values
        /// </summary>
        /// <param name="input">input set</param>
        /// <param name="number">number to subtract</param>
        /// <returns>set after subtraction</returns>
        public static Set operator -(Set input, double number)
        {
            var copy = new Set(input.Count);
            Parallel.For(0, input.Count, i =>
            {
                copy[i] = input[i] + number;
            });
            return copy;
        }

        /// <summary>
        /// Subtract a set's values from a nother set's values
        /// </summary>
        /// <param name="a">Set to subtract from</param>
        /// <param name="b">Set to subtract</param>
        /// <returns>set after subtraction</returns>
        public static Set operator -(Set a, Set b)
        {
            var min = System.Math.Min(a.Count, b.Count);
            var result = new Set(min);
            Parallel.For(0, min, i =>
            {
                result[i] = a[i] - b[i];
            });
            return result;
        }

        /// <summary>
        /// Multiply a set's every item with a number
        /// </summary>
        /// <param name="input">input set</param>
        /// <param name="number">number to multiply with</param>
        /// <returns>set after multiplication</returns>
        public static Set operator *(Set input, double number)
        {
            var copy = new Set(input.Count);
            Parallel.For(0, input.Count, i =>
            {
                copy[i] = input[i] * number;
            });
            return copy;
        }

        /// <summary>
        /// Multiply a set's values from a nother set's values
        /// </summary>
        /// <param name="a">Set to multiply</param>
        /// <param name="b">Set to multiply with</param>
        /// <returns>set after multiplication</returns>
        public static Set operator *(Set a, Set b)
        {
            var min = System.Math.Min(a.Count, b.Count);
            var result = new Set(min);
            Parallel.For(0, min, i =>
            {
                result[i] = a[i] * b[i];
            });
            return result;
        }

        /// <summary>
        /// Divide a set's values with a number
        /// </summary>
        /// <param name="input">Set to divide</param>
        /// <param name="number">Divisor</param>
        /// <returns>Set after division</returns>
        public static Set operator /(Set input, double number)
        {
            var copy = new Set(input.Count);
            Parallel.For(0, input.Count, i =>
            {
                copy[i] = input[i] + number;
            });
            return copy;
        }

        /// <summary>
        /// Divide a set's value's with a nother set's values
        /// </summary>
        /// <param name="a">Set to divide</param>
        /// <param name="b">Divisior set</param>
        /// <returns>Set after division</returns>
        public static Set operator /(Set a, Set b)
        {
            var min = System.Math.Min(a.Count, b.Count);
            var result = new Set(min);
            Parallel.For(0, min, i =>
            {
                result[i] = a[i] / b[i];
            });
            return result;
        }

        /// <summary>
        /// MOD a set's value's with a number
        /// </summary>
        /// <param name="input">Set to mod</param>
        /// <param name="number">mod</param>
        /// <returns>set after mod</returns>
        public static Set operator %(Set input, double number)
        {
            var copy = new Set(input.Count);
            Parallel.For(0, input.Count, i =>
            {
                copy[i] = input[i] % number;
            });
            return copy;
        }

        /// <summary>
        ///  MOD a set's value's with a nother set's values
        /// </summary>
        /// <param name="a">Set to mod</param>
        /// <param name="b">MOD set</param>
        /// <returns>set after mod</returns>
        public static Set operator %(Set a, Set b)
        {
            var min = System.Math.Min(a.Count, b.Count);
            var result = new Set(min);
            Parallel.For(0, min, i =>
            {
                result[i] = a[i] % b[i];
            });
            return result;
        }

        /// <summary>
        /// Convert a set to string
        /// </summary>
        /// <returns>string representation of set</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int limit = Count;
            bool fulllist = true;
            if (Count > 12)
            {
                limit = 12;
                fulllist = false;
            }
            for (int i = 0; i < limit; i++)
            {
                sb.AppendFormat("{0}, ", this[i]);
            }
            if (!fulllist) sb.Append("...");
            return sb.ToString();

        }
    }
}
