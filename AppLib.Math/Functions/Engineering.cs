using System;
using System.Numerics;

namespace AppLib.Maths
{
    /// <summary>
    /// Engineering Math functions
    /// </summary>
    public static class Engineering
    {
        /// <summary>
        /// Replus calculation
        /// </summary>
        /// <param name="x1">Parameter 1</param>
        /// <param name="x2">Parameter 2</param>
        public static double Replus(double x1, double x2)
        {
            return (x1 * x2) / (x1 + x2);
        }

        /// <summary>
        /// Complex replus calculation
        /// </summary>
        /// <param name="c1">Parameter 1</param>
        /// <param name="c2">Parameter 2</param>
        public static Complex Replus(Complex c1, Complex c2)
        {
            return (c1 * c2) / (c1 + c2);
        }

        /// <summary>
        /// Calculates the angular frequency
        /// </summary>
        /// <param name="freq">regular frequency</param>
        /// <returns>angular frequency</returns>
        public static double AngularFreq(double freq)
        {
            return Math.PI * 2 * freq;
        }

        /// <summary>
        /// Calculates the wavelength of a frequency
        /// </summary>
        /// <param name="freq">frequency</param>
        /// <returns>wavelength of frequency</returns>
        public static double Wavelength(double freq)
        {
            return 299792.458 / freq;
        }

        /// <summary>
        /// Calculate the complex impedance of a capacitor
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="capacity">Capacity in farads</param>
        /// <returns>Complex impedance</returns>
        public static Complex Xc(double frequency, double capacity)
        {
            double imaginary = 1 / (2 * Math.PI * frequency * capacity);
            return new Complex(0, -imaginary);
        }

        /// <summary>
        /// Calculate the complex impedance of an inductor
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="inductivity">Inductivity, in henry</param>
        /// <returns>Complex impedance</returns>
        public static Complex Xl(double frequency, double inductivity)
        {
            double imaginary = 2 * Math.PI * frequency * inductivity;
            return new Complex(0, imaginary);
        }

        /// <summary>
        /// Re-maps a number from one range to another. That is, a value of fromLow would get mapped to toLow, a value of fromHigh to toHigh, values in-between to values in-between, etc.
        /// </summary>
        /// <param name="x">the number to map</param>
        /// <param name="in_min">the lower bound of the value’s current range</param>
        /// <param name="in_max">the upper bound of the value’s current range</param>
        /// <param name="out_min">the lower bound of the value’s target range</param>
        /// <param name="out_max">the upper bound of the value’s target range</param>
        /// <returns>The mapped value.</returns>
        public static double Map(double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        /// <summary>
        /// Gausian Error function
        /// </summary>
        /// <param name="x">input number</param>
        /// <returns>The error function value for the input x</returns>
        public static double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
    }
}
