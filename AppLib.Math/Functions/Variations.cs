using System;
using System.Linq;

namespace AppLib.Maths
{
    /// <summary>
    /// Variation calculation functions
    /// </summary>
    public static class Variations
    {
        /// <summary>
        /// Variation without repetition. Take a set A of n different elements. Choose k elements in a specific order.
        /// </summary>
        /// <param name="n">Number of different elements</param>
        /// <param name="k">number of elements choosen in specific order</param>
        /// <returns></returns>
        public static double VariationNoRepeat(double n, double k)
        {
            return GeneralFunctions.Fact(n) / GeneralFunctions.Fact(n - k);
        }

        /// <summary>
        /// Variation with repetition. n different elements and k a whole number, In how many ways can we choose k elements?
        /// </summary>
        /// <param name="n">Number of different elements</param>
        /// <param name="k">number of elements to choose</param>
        /// <returns></returns>
        public static double VariationRepeat(double n, double k)
        {
            return Math.Pow(n, k);
        }

        /// <summary>
        /// Combination without repetition
        /// </summary>
        /// <param name="n">number of elements</param>
        /// <param name="k">number of elements not to take into account</param>
        /// <returns>the number of combinations</returns>
        public static double CombinationNoRepeat(double n, double k)
        {
            return GeneralFunctions.Fact(n) / GeneralFunctions.Fact(n - k) * GeneralFunctions.Fact(k);
        }

        /// <summary>
        /// Combination with repetition.
        /// </summary>
        /// <param name="n">number of elements</param>
        /// <param name="k">number of repeating elements</param>
        /// <returns>the number of possible combinations/returns>
        public static double CombinationRepeat(double n, double k)
        {
            double n2 = (n + k) - 1;
            return GeneralFunctions.Fact(n2) / GeneralFunctions.Fact(n2 - k) * GeneralFunctions.Fact(k);
        }

        /// <summary>
        /// Permutation. How many different ways are there to arrange n items, with a list of them repeating
        /// </summary>
        /// <param name="n">Number of different element</param>
        /// <param name="repeatk">list of repeating elements</param>
        /// <returns>Permutation of elements</returns>
        public static double Permutation(double n, params double[] repeatk)
        {
            double ktest = repeatk.Sum();
            if (ktest > n)
                throw new ArgumentException("Sum of repeating elements is bigger than the first parameter!", nameof(repeatk));

            double factn = GeneralFunctions.Fact(n);
            double sk = 1;
            foreach (var k in repeatk)
            {
                sk *= GeneralFunctions.Fact(k);
            }
            return factn / sk;
        }
    }
}
