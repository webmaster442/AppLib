using System;
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
    }
}
