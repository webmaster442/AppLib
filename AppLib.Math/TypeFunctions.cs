using System;
using System.Linq;
using System.Numerics;

namespace AppLib.Maths
{
    /// <summary>
    /// Type creating functions
    /// </summary>
    public static class TypeFunctions
    {
        /// <summary>
        /// Return the complex conjugate of parameter
        /// </summary>
        /// <param name="param">Input parameter</param>
        /// <returns>Complex conjugate</returns>
        public static Complex CplxConjugate(Complex param)
        {
            return Complex.Conjugate(param);
        }

        /// <summary>
        /// create a Complex number from polar coordinate form
        /// </summary>
        /// <param name="abs">absolute value</param>
        /// <param name="angle">anlge</param>
        /// <returns>Complex value</returns>
        public static Complex CplxPolar(double abs, double angle)
        {
            double rad = 0;
            switch (Trigonometry.Mode)
            {
                case TrigonometryMode.DEG:
                    rad = Trigonometry.Deg2Rad(angle);
                    break;
                case TrigonometryMode.GRAD:
                    rad = Trigonometry.Grad2Rad(angle);
                    break;
                case TrigonometryMode.RAD:
                    rad = angle;
                    break;
            }
            return Complex.FromPolarCoordinates(abs, rad);
        }

        /// <summary>
        /// Crete a complex number
        /// </summary>
        /// <param name="r">Real part</param>
        /// <param name="i">Imaginary part</param>
        /// <returns>Complex value</returns>
        public static Complex CplxRi(double r, double i)
        {
            return new Complex(r, i);
        }

        /// <summary>
        /// Create a Fraction
        /// </summary>
        /// <param name="numerator">Numerator</param>
        /// <param name="denominator">denumerator</param>
        /// <returns>Fraction</returns>
        public static Fraction Fraction(double numerator, double denominator)
        {
            return new Fraction(Convert.ToInt64(numerator), Convert.ToInt64(denominator));
        }

        /// <summary>
        /// Create a set from a list of numbers
        /// </summary>
        /// <param name="d">set of numbers</param>
        /// <returns>Number set</returns>
        public static Set Set(params double[] d)
        {
            return new Set(d);
        }

        /// <summary>
        /// Return the distint values from a set
        /// </summary>
        /// <param name="arg">Input set</param>
        /// <returns>Set with distint values</returns>
        public static Set Distinct(Set arg)
        {
            var res = arg.Distinct().ToList();
            return new Set(res);
        }

        /// <summary>
        /// Return the intersection of two sets
        /// </summary>
        /// <param name="arg1">set 1</param>
        /// <param name="arg2">set 2</param>
        /// <returns>Intersection of sets</returns>
        public static Set Intersect(Set arg1, Set arg2)
        {
            var res = arg1.Intersect(arg2).ToList();
            return new Set(res);
        }

        /// <summary>
        /// Union of sets
        /// </summary>
        /// <param name="arg1">set 1</param>
        /// <param name="arg2">set 2</param>
        /// <returns>Union of inputs</returns>
        public static Set Union(Set arg1, Set arg2)
        {
            var res = arg1.Union(arg2).ToList();
            return new Set(res);
        }

        /// <summary>
        /// Except a set from a nother set
        /// </summary>
        /// <param name="arg1">set 1</param>
        /// <param name="arg2">set 2</param>
        /// <returns>Excepted value</returns>
        public static Set Except(Set arg1, Set arg2)
        {
            var res = arg1.Except(arg2).ToList();
            return new Set(res);
        }

        /// <summary>
        /// Create a set based on a series
        /// </summary>
        /// <param name="start">starting value</param>
        /// <param name="d">difference</param>
        /// <param name="items">number of items</param>
        /// <returns>A set containing the series</returns>
        public static Set Series(double start, double d, double items)
        {
            var set = new Set();
            set.Add(start);
            var current = start;
            for (int i = 0; i < items; i++)
            {
                current += d;
                set.Add(current);
            }
            return set;
        }

        /// <summary>
        /// Create a set based on a geometric series
        /// </summary>
        /// <param name="start">start value</param>
        /// <param name="q">difference</param>
        /// <param name="items">number of items to create</param>
        /// <returns>A set containing the series</returns>
        public static Set GeometricSeries(double start, double q, double items)
        {
            var set = new Set(items);
            set.Add(start);
            var current = start;
            for (int i = 0; i < items; i++)
            {
                current *= q;
                set.Add(current);
            }
            return set;
        }

        /// <summary>
        /// Create a set with random values
        /// </summary>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <param name="items">number of items</param>
        /// <returns>set of random items</returns>
        public static Set RandomSet(double min, double max, double items)
        {
            var set = new Set(items);
            Random r = new Random();
            var imin = Convert.ToInt32(min);
            var imax = Convert.ToInt32(max);
            for (int i = 0; i < items; i++)
            {

                var item = r.Next(imin, imax);
                set.Add(item);
            }
            return set;
        }

        /// <summary>
        /// Sort a set ascending
        /// </summary>
        /// <param name="set">set to sort</param>
        /// <returns>set in ascending order</returns>
        public static Set SortA(Set set)
        {
            var q = from i in set orderby i ascending select i;
            return new Set(q.ToList());
        }

        /// <summary>
        /// Sort a set descending
        /// </summary>
        /// <param name="set">set to sort</param>
        /// <returns>set in descending order</returns>
        public static Set SortD(Set set)
        {
            var q = from i in set orderby i descending select i;
            return new Set(q.ToList());
        }
    }
}
