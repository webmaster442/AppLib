using System;
using System.Globalization;

namespace AppLib.Maths
{
    /// <summary>
    /// A Fraction number type
    /// </summary>
    [Serializable]
    public struct Fraction : IComparable,
                            IEquatable<Fraction>,
                            IEquatable<double>,
                            IEquatable<float>,
                            IEquatable<decimal>,
                            IComparable<Fraction>,
                            IComparable<double>,
                            IComparable<float>,
                            IComparable<decimal>,
                            IFormattable,
                            IConvertible
    {
        private long _numerator;
        private long _denominator;

        /// <summary>
        /// Numerator
        /// </summary>
        public long Numerator
        {
            get { return _numerator; }
        }

        /// <summary>
        /// Denominator
        /// </summary>
        public long Denominator
        {
            get { return _denominator; }
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="numerator">Numerator value</param>
        /// <param name="denominator">Denominator value. Can't be null</param>
        public Fraction(long numerator, long denominator)
        {
            _numerator = numerator;
            if (denominator == 0)
                throw new ArgumentException("denominator can't be 0");
            _denominator = denominator;
            Reduce();
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="value">integer value</param>
        public Fraction(int value)
        {
            _numerator = value;
            _denominator = 1;
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="value">long value</param>
        public Fraction(long value)
        {
            _numerator = value;
            _denominator = 1;
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="value">Double value</param>
        public Fraction(double value)
        {
            if (value % 1 == 0)
            {
                //whole number
                _numerator = (long)value;
                _denominator = 0;
            }
            else
            {
                double dTemp = value;
                long iMultiple = 1;
                string strTemp = value.ToString(CultureInfo.InvariantCulture);
                while (strTemp.IndexOf("E") > 0)    // if in the form like 12E-9
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    strTemp = dTemp.ToString(CultureInfo.InvariantCulture);
                }
                int i = 0;
                while (strTemp[i] != '.')
                    i++;
                int iDigitsAfterDecimal = strTemp.Length - i - 1;
                while (iDigitsAfterDecimal > 0)
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    iDigitsAfterDecimal--;
                }
                _numerator = (long)Math.Round(dTemp);
                _denominator = iMultiple;
            }
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="value">decimal value</param>
        public Fraction(decimal value)
        {
            if (value % 1 == 0)
            {
                //whole number
                _numerator = (long)value;
                _denominator = 0;
            }
            else
            {
                decimal dTemp = value;
                long iMultiple = 1;
                string strTemp = value.ToString(CultureInfo.InvariantCulture);
                while (strTemp.IndexOf("E") > 0)    // if in the form like 12E-9
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    strTemp = dTemp.ToString(CultureInfo.InvariantCulture);
                }
                int i = 0;
                while (strTemp[i] != '.')
                    i++;
                int iDigitsAfterDecimal = strTemp.Length - i - 1;
                while (iDigitsAfterDecimal > 0)
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    iDigitsAfterDecimal--;
                }
                _numerator = (long)Math.Round(dTemp);
                _denominator = iMultiple;
            }
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="strValue">String to parse</param>
        public Fraction(string strValue)
        {
            int i;
            for (i = 0; i < strValue.Length; i++)
            {
                if (strValue[i] == '/')
                    break;
            }

            if (i == strValue.Length)
            {
                // if string is not in the form of a fraction
                // then it is double or integer
                var f = new Fraction(Convert.ToDouble(strValue));
                _numerator = f.Numerator;
                _denominator = f.Denominator;
            }

            // else string is in the form of Numerator/Denominator
            long iNumerator = Convert.ToInt64(strValue.Substring(0, i));
            long iDenominator = Convert.ToInt64(strValue.Substring(i + 1));

            _numerator = iNumerator;
            _denominator = iDenominator;
        }

        /// <summary>
        /// Creates a new instance of Fraction
        /// </summary>
        /// <param name="value">float value</param>
        public Fraction(float value) : this((double)value)
        {
        }

        private void Reduce()
        {
            try
            {
                if (_numerator == 0)
                {
                    _denominator = 1;
                    return;
                }

                long iGCD = GCD(_numerator, _denominator);
                _numerator /= iGCD;
                _denominator /= iGCD;

                if (_denominator < 0)	// if -ve sign in denominator
                {
                    //pass -ve sign to numerator
                    _numerator *= -1;
                    _denominator *= -1;
                }
            } // end try
            catch (Exception exp)
            {
                throw new InvalidOperationException("Cannot reduce Fraction: " + exp.Message);
            }
        }

        private static long GCD(long iNo1, long iNo2)
        {
            // take absolute values
            if (iNo1 < 0) iNo1 = -iNo1;
            if (iNo2 < 0) iNo2 = -iNo2;

            do
            {
                if (iNo1 < iNo2)
                {
                    long tmp = iNo1;  // swap the two operands
                    iNo1 = iNo2;
                    iNo2 = tmp;
                }
                iNo1 = iNo1 % iNo2;
            } while (iNo1 != 0);
            return iNo2;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(Fraction other)
        {
            return (other.Numerator == this.Numerator && other.Denominator == this.Denominator);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(double other)
        {
            return Equals(new Fraction(other));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(float other)
        {
            return Equals(new Fraction(other));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(decimal other)
        {
            return Equals(new Fraction(other));
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="obj"> An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings: Value Meaning Less than zero This object
        /// is less than the other parameter.Zero This object is equal to other. Greater
        /// than zero This object is greater than other.</returns>
        public int CompareTo(object obj)
        {
            var other = (Fraction)obj;
            return CompareTo(other);

        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="obj"> An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings: Value Meaning Less than zero This object
        /// is less than the other parameter.Zero This object is equal to other. Greater
        /// than zero This object is greater than other.</returns>
        public int CompareTo(Fraction other)
        {
            if (other < this)
                return -1;
            else if (other > this)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="obj"> An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings: Value Meaning Less than zero This object
        /// is less than the other parameter.Zero This object is equal to other. Greater
        /// than zero This object is greater than other.</returns>
        public int CompareTo(double number)
        {
            var other = new Fraction(number);

            if (other < this)
                return -1;
            else if (other > this)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="obj"> An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings: Value Meaning Less than zero This object
        /// is less than the other parameter.Zero This object is equal to other. Greater
        /// than zero This object is greater than other.</returns>
        public int CompareTo(float number)
        {
            var other = new Fraction(number);

            if (other < this)
                return -1;
            else if (other > this)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="obj"> An object to compare with this object.</param>
        /// <returns> A value that indicates the relative order of the objects being compared. The
        /// return value has the following meanings: Value Meaning Less than zero This object
        /// is less than the other parameter.Zero This object is equal to other. Greater
        /// than zero This object is greater than other.</returns>
        public int CompareTo(decimal number)
        {
            var other = new Fraction(number);

            if (other < this)
                return -1;
            else if (other > this)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the System.IFormattable implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual Basic)
        /// to obtain the numeric format information from the current locale setting of the operating system.
        /// </param>
        /// <returns> The value of the current instance in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            string str;
            if (this.Denominator == 1)
                str = Numerator.ToString(format, formatProvider);
            else
                str = Numerator.ToString(format, formatProvider) + "/" + Denominator.ToString(format, formatProvider);
            return str;
        }

        /// <summary>
        /// Returns the System.TypeCode for this instance.
        /// </summary>
        /// <returns> The enumerated constant that is the System.TypeCode of the class or value typethat implements this interface.</returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A Boolean value equivalent to the value of this instance.</returns>
        public bool ToBoolean(IFormatProvider provider)
        {
            return (Numerator != 0 && Denominator != 0);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A Unicode character equivalent to the value of this instance.</returns>
        public char ToChar(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToChar(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
        public sbyte ToSByte(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToSByte(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
        public byte ToByte(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToByte(val);
        }

        /// <summary>
        ///  Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
        public short ToInt16(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToInt16(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
        public ushort ToUInt16(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToUInt16(val);
        }

        /// <summary>
        ///  Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public int ToInt32(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToInt32(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public uint ToUInt32(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToUInt32(val);
        }

        /// <summary>
        ///  Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public long ToInt64(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToInt64(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
        public ulong ToUInt64(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToUInt64(val);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public float ToSingle(IFormatProvider provider)
        {
            return (float)this;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public double ToDouble(IFormatProvider provider)
        {
            return (double)this;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent System.Decimal number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A System.Decimal number equivalent to the value of this instance.</returns>
        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)this;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent System.DateTime using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A System.DateTime instance equivalent to the value of this instance.</returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ToDateTime(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>A System.String instance equivalent to the value of this instance.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            double val = (double)this;
            return Convert.ChangeType(val, conversionType);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"> The object to compare with the current object.</param>
        /// <returns> true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Fraction)
            {
                var fraction = (Fraction)obj;
                return (fraction.Numerator == Numerator && fraction.Denominator == Denominator);
            }
            else return false;
        }

        /// <summary>
        /// Computes the hash of the current instance
        /// </summary>
        /// <returns> A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash += 11 * hash + _numerator.GetHashCode();
                hash += 11 * hash + _denominator.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string str;
            if (Denominator == 1)
                str = Numerator.ToString();
            else
                str = this.Numerator + "/" + Denominator;
            return str;
        }

        /// <summary>
        /// Returns the inverse of the input faction
        /// </summary>
        /// <param name="input">fracton to invert</param>
        /// <returns>the inverse of the input fraction (numerator & denominator swapped)</returns>
        public static Fraction Inverse(Fraction input)
        {
            if (input.Numerator == 0)
                throw new ArgumentException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

            long iNumerator = input.Denominator;
            long iDenominator = input.Numerator;
            return new Fraction(iNumerator, iDenominator);
        }

        /// <summary>
        /// Negates an input fraction
        /// </summary>
        /// <param name="frac1">Fraction to negate</param>
        /// <returns>Negated fraction</returns>
        public static Fraction Negate(Fraction frac1)
        {
            long iNumerator = -frac1.Numerator;
            long iDenominator = frac1.Denominator;
            return new Fraction(iNumerator, iDenominator);
        }

        /// <summary>
        /// Adds two fractions together
        /// </summary>
        /// <param name="frac1">Input Fraction 1</param>
        /// <param name="frac2">Input Fraction 2</param>
        /// <returns>the sum of the two factions</returns>
        public static Fraction Add(Fraction frac1, Fraction frac2)
        {
            checked
            {
                long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
                long iDenominator = frac1.Denominator * frac2.Denominator;
                return (new Fraction(iNumerator, iDenominator));
            }

        }

        /// <summary>
        /// Multiplies two factions
        /// </summary>
        /// <param name="frac1">Input Fraction 1</param>
        /// <param name="frac2">Input Fraction 2</param>
        /// <returns>The two factions multiplied together</returns>
        public static Fraction Multiply(Fraction frac1, Fraction frac2)
        {
            checked
            {
                long iNumerator = frac1.Numerator * frac2.Numerator;
                long iDenominator = frac1.Denominator * frac2.Denominator;
                return (new Fraction(iNumerator, iDenominator));
            }
        }

        /// <summary>
        /// Minimum value
        /// </summary>
        public static Fraction MinValue
        {
            get { return new Fraction(-1, long.MaxValue); }
        }

        /// <summary>
        /// Maximum value
        /// </summary>
        public static Fraction MaxValue
        {
            get { return new Fraction(long.MaxValue, 1); }
        }

        /// <summary>
        /// Negates the input fraction
        /// </summary>
        /// <param name="value">Fraction to negate</param>
        /// <returns>Negated faction</returns>
        public static Fraction operator -(Fraction value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Adds two fractions
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>The sum of two frtactions</returns>
        public static Fraction operator +(Fraction frac1, Fraction frac2)
        {
            return Add(frac1, frac2);
        }

        /// <summary>
        /// Adds a fraction to an integer
        /// </summary>
        /// <param name="iNo">integer to add to</param>
        /// <param name="frac1">Fraction to add</param>
        /// <returns>The sum of the integer and the faction</returns>
        public static Fraction operator +(int iNo, Fraction frac1)
        {
            return Add(frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Adds an integer to a fraction
        /// </summary>
        /// <param name="frac1">Fraction to add to</param>
        /// <param name="iNo">integer to add</param>
        /// <returns>The sum of the integer and the faction</returns>
        public static Fraction operator +(Fraction frac1, int iNo)
        {
            return Add(frac1, new Fraction(iNo));
        }


        /// <summary>
        /// Adds a fraction to a long
        /// </summary>
        /// <param name="iNo">long to add to</param>
        /// <param name="frac1">Fraction to add</param>
        /// <returns>The sum of the long and the faction</returns>
        public static Fraction operator +(long iNo, Fraction frac1)
        {
            return Add(frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Adds a long to a Fraction
        /// </summary>
        /// <param name="frac1">Fraction to add to</param>
        /// <param name="iNo">the long to add</param>
        /// <returns>The sum of the Fraction and long</returns>
        public static Fraction operator +(Fraction frac1, long iNo)
        {
            return Add(frac1, new Fraction(iNo));
        }


        /// <summary>
        /// Adds a fraction to a double
        /// </summary>
        /// <param name="iNo">double to add to</param>
        /// <param name="frac1">Fraction to add</param>
        /// <returns>The sum of the double and the faction</returns>
        public static Fraction operator +(double dbl, Fraction frac1)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Adds a double to a fraction
        /// </summary>
        /// <param name="frac1">Fraction to add to</param>
        /// <param name="iNo">double to add</param>
        /// <returns>The sum of the double and the faction</returns>
        public static Fraction operator +(Fraction frac1, double dbl)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Adds a fraction to a float
        /// </summary>
        /// <param name="iNo">float to add to</param>
        /// <param name="frac1">Fraction to add</param>
        /// <returns>The sum of the float and the faction</returns>
        public static Fraction operator +(float dbl, Fraction frac1)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Adds a float to a fraction
        /// </summary>
        /// <param name="frac1">Fraction to add to</param>
        /// <param name="iNo">float to add</param>
        /// <returns>The sum of the float and the faction</returns>
        public static Fraction operator +(Fraction frac1, float dbl)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Adds a fraction to a decimal
        /// </summary>
        /// <param name="iNo">decimal to add to</param>
        /// <param name="frac1">Fraction to add</param>
        /// <returns>The sum of the decimal and the faction</returns>
        public static Fraction operator +(decimal dbl, Fraction frac1)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Adds a decimal to a fraction
        /// </summary>
        /// <param name="frac1">Fraction to add to</param>
        /// <param name="iNo">decimal to add</param>
        /// <returns>The sum of the decimal and the faction</returns>
        public static Fraction operator +(Fraction frac1, decimal dbl)
        {
            return Add(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Subtracts two fractions
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>The difference of the fractions</returns>
        public static Fraction operator -(Fraction frac1, Fraction frac2)
        {
            return Add(frac1, -frac2);
        }

        /// <summary>
        /// Subtracts a fraction from an integer
        /// </summary>
        /// <param name="iNo">Integer to subtract from</param>
        /// <param name="frac1">Fraction to subtract</param>
        /// <returns>The difference of the int and the fraction</returns>
        public static Fraction operator -(int iNo, Fraction frac1)
        {
            return Add(-frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Subtracts an integer from a faction
        /// </summary>
        /// <param name="frac1">Fraction to subtract from</param>
        /// <param name="iNo">integer to subtract</param>
        /// <returns>the difference of the fraction and the int</returns>
        public static Fraction operator -(Fraction frac1, int iNo)
        {
            return Add(frac1, -(new Fraction(iNo)));
        }

        /// <summary>
        /// Subtracts a fraction from an integer
        /// </summary>
        /// <param name="iNo">Integer to subtract from</param>
        /// <param name="frac1">Fraction to subtract</param>
        /// <returns>The difference of the int and the fraction</returns>
        public static Fraction operator -(long iNo, Fraction frac1)
        {
            return Add(-frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Subtracts a long from a faction
        /// </summary>
        /// <param name="frac1">Fraction to subtract from</param>
        /// <param name="iNo">long to subtract</param>
        /// <returns>the difference of the fraction and the long</returns>
        public static Fraction operator -(Fraction frac1, long iNo)
        {
            return Add(frac1, -(new Fraction(iNo)));
        }

        /// <summary>
        /// Subtracts a fraction from an double
        /// </summary>
        /// <param name="dbl">double to subtract from</param>
        /// <param name="frac1">Fraction to subtract</param>
        /// <returns>The difference of the double and the fraction</returns>
        public static Fraction operator -(double dbl, Fraction frac1)
        {
            return Add(-frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Subtracts a double from a faction
        /// </summary>
        /// <param name="frac1">Fraction to subtract from</param>
        /// <param name="iNo">double to subtract</param>
        /// <returns>the difference of the fraction and the double</returns>
        public static Fraction operator -(Fraction frac1, double dbl)
        {
            return Add(frac1, -new Fraction(dbl));
        }

        /// <summary>
        /// Subtracts a fraction from a float
        /// </summary>
        /// <param name="dbl">float to subtract from</param>
        /// <param name="frac1">Fraction to subtract</param>
        /// <returns>The difference of the float and the fraction</returns>
        public static Fraction operator -(float dbl, Fraction frac1)
        {
            return Add(-frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Subtracts a float from a faction
        /// </summary>
        /// <param name="frac1">Fraction to subtract from</param>
        /// <param name="iNo">float to subtract</param>
        /// <returns>the difference of the fraction and the float</returns>
        public static Fraction operator -(Fraction frac1, float dbl)
        {
            return Add(frac1, -new Fraction(dbl));
        }


        /// <summary>
        /// Subtracts a fraction from a decimal
        /// </summary>
        /// <param name="dbl">decimal to subtract from</param>
        /// <param name="frac1">Fraction to subtract</param>
        /// <returns>The difference of the decimal and the fraction</returns>
        public static Fraction operator -(decimal dbl, Fraction frac1)
        {
            return Add(-frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Subtracts a decimal from a faction
        /// </summary>
        /// <param name="frac1">Fraction to subtract from</param>
        /// <param name="iNo">decimal to subtract</param>
        /// <returns>the difference of the fraction and the decimal</returns>
        public static Fraction operator -(Fraction frac1, decimal dbl)
        {
            return Add(frac1, -new Fraction(dbl));
        }

        /// <summary>
        /// Multiplies two fractions
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, Fraction frac2)
        {
            return Multiply(frac1, frac2);
        }

        /// <summary>
        /// Multiplies an integer and a fraction
        /// </summary>
        /// <param name="iNo">integer parameter</param>
        /// <param name="frac1">Fraction</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(int iNo, Fraction frac1)
        {
            return Multiply(frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Multiplies a Fraction and an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">integer</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, int iNo)
        {
            return Multiply(frac1, new Fraction(iNo));
        }


        /// <summary>
        /// Multiplies a long and a fraction
        /// </summary>
        /// <param name="iNo">long parameter</param>
        /// <param name="frac1">Fraction</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(long iNo, Fraction frac1)
        {
            return Multiply(frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Multiplies a Fraction and a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">long</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, long iNo)
        {
            return Multiply(frac1, new Fraction(iNo));
        }

        /// <summary>
        /// Multiplies a double and a fraction
        /// </summary>
        /// <param name="iNo">double parameter</param>
        /// <param name="frac1">Fraction</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(double dbl, Fraction frac1)
        {
            return Multiply(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Multiplies a Fraction and a double
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, double dbl)
        {
            return Multiply(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Multiplies a float and a fraction
        /// </summary>
        /// <param name="iNo">float parameter</param>
        /// <param name="frac1">Fraction</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(float dbl, Fraction frac1)
        {
            return Multiply(frac1, new Fraction(dbl));
        }


        /// <summary>
        /// Multiplies a Fraction and a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, decimal dbl)
        {
            return Multiply(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Multiplies a decimal and a fraction
        /// </summary>
        /// <param name="iNo">decimal parameter</param>
        /// <param name="frac1">Fraction</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(decimal dbl, Fraction frac1)
        {
            return Multiply(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Multiplies a Fraction and a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>The result of the multiplication</returns>
        public static Fraction operator *(Fraction frac1, float dbl)
        {
            return Multiply(frac1, new Fraction(dbl));
        }

        /// <summary>
        /// Divides a fraction with a fraction
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, Fraction frac2)
        {
            return Multiply(frac1, Inverse(frac2));
        }

        /// <summary>
        /// Divides an integer with a fraction
        /// </summary>
        /// <param name="iNo">Integer to divide</param>
        /// <param name="frac1">Fraction to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(int iNo, Fraction frac1)
        {
            return Multiply(Inverse(frac1), new Fraction(iNo));
        }

        /// <summary>
        /// Divides a Fraction with an integer
        /// </summary>
        /// <param name="frac1">Fraction to divide</param>
        /// <param name="iNo">integer to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, int iNo)
        {
            return Multiply(frac1, Inverse(new Fraction(iNo)));
        }

        /// <summary>
        /// Divides a long with a fraction
        /// </summary>
        /// <param name="iNo">long to divide</param>
        /// <param name="frac1">Fraction to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(long iNo, Fraction frac1)
        {
            return Multiply(Inverse(frac1), new Fraction(iNo));
        }

        /// <summary>
        /// Divides a Fraction with a long
        /// </summary>
        /// <param name="frac1">Fraction to divide</param>
        /// <param name="iNo">long to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, long iNo)
        {
            return Multiply(frac1, Inverse(new Fraction(iNo)));
        }

        /// <summary>
        /// Divides a double with a fraction
        /// </summary>
        /// <param name="iNo">double to divide</param>
        /// <param name="frac1">Fraction to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(double dbl, Fraction frac1)
        {
            return Multiply(Inverse(frac1), new Fraction(dbl));
        }

        /// <summary>
        /// Divides a Fraction with a double
        /// </summary>
        /// <param name="frac1">Fraction to divide</param>
        /// <param name="iNo">double to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, double dbl)
        {
            return Multiply(frac1, Fraction.Inverse(new Fraction(dbl)));
        }

        /// <summary>
        /// Divides a float with a fraction
        /// </summary>
        /// <param name="iNo">float to divide</param>
        /// <param name="frac1">Fraction to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(float dbl, Fraction frac1)
        {
            return Multiply(Inverse(frac1), new Fraction(dbl));
        }

        /// <summary>
        /// Divides a Fraction with an decimal
        /// </summary>
        /// <param name="frac1">Fraction to divide</param>
        /// <param name="iNo">decimal to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, decimal dbl)
        {
            return Multiply(frac1, Fraction.Inverse(new Fraction(dbl)));
        }

        /// <summary>
        /// Divides an decimal with a fraction
        /// </summary>
        /// <param name="iNo">decimal to divide</param>
        /// <param name="frac1">Fraction to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(decimal dbl, Fraction frac1)
        {
            return Multiply(Inverse(frac1), new Fraction(dbl));
        }

        /// <summary>
        /// Divides a Fraction with a float
        /// </summary>
        /// <param name="frac1">Fraction to divide</param>
        /// <param name="iNo">float to divide with</param>
        /// <returns>result of division</returns>
        public static Fraction operator /(Fraction frac1, float dbl)
        {
            return Multiply(frac1, Fraction.Inverse(new Fraction(dbl)));
        }

        /// <summary>
        /// Cheks the equality of two fractions
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, Fraction frac2)
        {
            return frac1.Equals(frac2);
        }

        /// <summary>
        /// Cheks the unquality of two fractions
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, Fraction frac2)
        {
            return !frac1.Equals(frac2);
        }

        /// <summary>
        /// Checks the equality of a fraction and an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Integer</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, int iNo)
        {
            return frac1.Equals(new Fraction(iNo));
        }


        /// <summary>
        /// Cheks the unquality of a fraction and an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="frac2">integer</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, int iNo)
        {
            return !frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Checks the equality of a fraction and a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">long</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, long iNo)
        {
            return frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Cheks the unquality of a fraction and a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="frac2">long</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, long iNo)
        {
            return !frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Checks the equality of a fraction and a double
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, double iNo)
        {
            return frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Cheks the unquality of a fraction and an double
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="frac2">double</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, double iNo)
        {
            return !frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Checks the equality of a fraction and a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, float iNo)
        {
            return frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Cheks the unquality of a fraction and a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="frac2">float</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, float iNo)
        {
            return !frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Checks the equality of a fraction and a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>true, if they are equal, false if not</returns>
        public static bool operator ==(Fraction frac1, decimal iNo)
        {
            return frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Cheks the unquality of a fraction and a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="frac2">decimal</param>
        /// <returns>true, if they are unequal, false if not</returns>
        public static bool operator !=(Fraction frac1, decimal iNo)
        {
            return !frac1.Equals(new Fraction(iNo));
        }

        /// <summary>
        /// Cheks if a fraction is less then another fraction
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if fraction 1 is less than fraction2</returns>
        public static bool operator <(Fraction frac1, Fraction frac2)
        {
            return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;
        }

        /// <summary>
        ///  Cheks if a fraction is greater then another fraction
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if fraction 1 is greater than fraction2</returns>
        public static bool operator >(Fraction frac1, Fraction frac2)
        {
            return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;
        }

        /// <summary>
        /// Cheks if a fraction is less then or equal to another fraction
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if fraction 1 is less than or equal to fraction2</returns>
        public static bool operator <=(Fraction frac1, Fraction frac2)
        {
            return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;
        }

        /// <summary>
        /// Cheks if a fraction is greater then or equal to another fraction
        /// </summary>
        /// <param name="frac1">Fraction 1</param>
        /// <param name="frac2">Fraction 2</param>
        /// <returns>true, if fraction 1 is greater than or equal to fraction2</returns>
        public static bool operator >=(Fraction frac1, Fraction frac2)
        {
            return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;
        }

        /// <summary>
        /// Cheks if a fraction is less than an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Integer</param>
        /// <returns>true, if the fraction is less than the integer</returns>
        public static bool operator <(Fraction frac1, int iNo)
        {
            return frac1 < new Fraction(iNo);
        }

        /// <summary>
        /// Cheks if a fraction is greater than an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Integer</param>
        /// <returns>true, if the fraction is greater than the integer</returns>
        public static bool operator >(Fraction frac1, int iNo)
        {
            return frac1 > new Fraction(iNo);
        }

        /// <summary>
        /// Cheks if a fraction is less than or equal to an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Integer</param>
        /// <returns>true, if the fraction is less then or equal to the integer</returns>
        public static bool operator <=(Fraction frac1, int iNo)
        {
            return frac1 <= new Fraction(iNo);
        }

        /// <summary>
        /// Cheks if a fraction is greater than or equal to an integer
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Integer</param>
        /// <returns>true, if the fraction is greater than or equal to the integer</returns>
        public static bool operator >=(Fraction frac1, int iNo)
        {
            return frac1 >= new Fraction(iNo);
        }

        /// <summary>
        /// Cheks if an integer is less than a fracton
        /// </summary>
        /// <param name="iNo">integer</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the integer is less than the fraction</returns>
        public static bool operator <(int iNo, Fraction frac2)
        {
            return new Fraction(iNo) < frac2;
        }

        /// <summary>
        /// Cheks if an integer is greater than a fracton
        /// </summary>
        /// <param name="iNo">integer</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the integer is greater than the fraction</returns>
        public static bool operator >(int iNo, Fraction frac2)
        {
            return new Fraction(iNo) > frac2;
        }

        /// <summary>
        /// Cheks if an integer is less than or equal to a fracton
        /// </summary>
        /// <param name="iNo">integer</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the integer is less than or equal the fraction</returns>
        public static bool operator <=(int iNo, Fraction frac2)
        {
            return new Fraction(iNo) <= frac2;
        }

        /// <summary>
        ///  Cheks if an integer is greater than or equal to a fracton
        /// </summary>
        /// <param name="iNo">integer</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the integer is greater than or equal to the fraction</returns>
        public static bool operator >=(int iNo, Fraction frac2)
        {
            return new Fraction(iNo) >= frac2;
        }

        /// <summary>
        /// Cheks that a fraction is less than a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Long</param>
        /// <returns>true, if the fraction is less than the long</returns>
        public static bool operator <(Fraction frac1, long iNo)
        {
            return frac1 < new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is greater than a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Long</param>
        /// <returns>true, if the fraction is greater than the long</returns>
        public static bool operator >(Fraction frac1, long iNo)
        {
            return frac1 > new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is less than or equal to a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Long</param>
        /// <returns>true, if the fraction is less than or equal to the long</returns>
        public static bool operator <=(Fraction frac1, long iNo)
        {
            return frac1 <= new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is greater than or equal to a long
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">Long</param>
        /// <returns>true, if the fraction is greater than or equal to the long</returns>
        public static bool operator >=(Fraction frac1, long iNo)
        {
            return frac1 >= new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a long is less than a Fraction 
        /// </summary>
        /// <param name="iNo">long</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the long is less than the fraction</returns>
        public static bool operator <(long iNo, Fraction frac2)
        {
            return new Fraction(iNo) < frac2;
        }

        /// <summary>
        /// Checks that a long is greater than a Fraction 
        /// </summary>
        /// <param name="iNo">long</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the long is greater than the fraction</returns>
        public static bool operator >(long iNo, Fraction frac2)
        {
            return new Fraction(iNo) > frac2;
        }

        /// <summary>
        /// Checks that a long is less than or equals to a Fraction 
        /// </summary>
        /// <param name="iNo">long</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the long is less than or equals to the fraction</returns>
        public static bool operator <=(long iNo, Fraction frac2)
        {
            return new Fraction(iNo) <= frac2;
        }

        /// <summary>
        /// Checks that a long is greater than or equal to a Fraction 
        /// </summary>
        /// <param name="iNo">long</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the long is greater than the fraction</returns>
        public static bool operator >=(long iNo, Fraction frac2)
        {
            return new Fraction(iNo) >= frac2;
        }

        /// <summary>
        /// Checks that a fraction is less than a double
        /// </summary>
        /// <param name="frac1">fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>true, if the fraction is less than the double</returns>
        public static bool operator <(Fraction frac1, double iNo)
        {
            return frac1 < new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a fraction is greater than a double
        /// </summary>
        /// <param name="frac1">fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>true, if the fraction is greater than the double</returns>
        public static bool operator >(Fraction frac1, double iNo)
        {
            return frac1 > new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a fraction is less than or equal to a double
        /// </summary>
        /// <param name="frac1">fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>true, if the fraction is less than or equal to the double</returns>
        public static bool operator <=(Fraction frac1, double iNo)
        {
            return frac1 <= new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a fraction is greater than or equal to a double
        /// </summary>
        /// <param name="frac1">fraction</param>
        /// <param name="iNo">double</param>
        /// <returns>true, if the fraction is greater than or equal to the double</returns>
        public static bool operator >=(Fraction frac1, double iNo)
        {
            return frac1 >= new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a double is less than a fraction
        /// </summary>
        /// <param name="iNo">double</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the double is less than the fraction</returns>
        public static bool operator <(double iNo, Fraction frac2)
        {
            return new Fraction(iNo) < frac2;
        }

        /// <summary>
        /// Checks that a double is greater than a fraction
        /// </summary>
        /// <param name="iNo">double</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the double is greater than the fraction</returns>
        public static bool operator >(double iNo, Fraction frac2)
        {
            return new Fraction(iNo) > frac2;
        }

        /// <summary>
        /// Checks that a double is less than or equal to a fraction
        /// </summary>
        /// <param name="iNo">double</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the double is less than or equal to the fraction</returns>
        public static bool operator <=(double iNo, Fraction frac2)
        {
            return new Fraction(iNo) <= frac2;
        }

        /// <summary>
        /// Checks that a double is greater than or equal to a fraction
        /// </summary>
        /// <param name="iNo">double</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the double is greater than or equal to the fraction</returns>
        public static bool operator >=(double iNo, Fraction frac2)
        {
            return new Fraction(iNo) >= frac2;
        }

        /// <summary>
        /// Cheks that a fraction is less than a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>true, if the fraction is less than the float</returns>
        public static bool operator <(Fraction frac1, float iNo)
        {
            return frac1 < new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is greater than a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>true, if the fraction is greater than the float</returns>
        public static bool operator >(Fraction frac1, float iNo)
        {
            return frac1 > new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is less than or equal to a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>true, if the fraction is less than or equal to the float</returns>
        public static bool operator <=(Fraction frac1, float iNo)
        {
            return frac1 <= new Fraction(iNo);
        }


        /// <summary>
        /// Cheks that a fraction is greater than or equal to a float
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">float</param>
        /// <returns>true, if the fraction is greater than or equal to the float</returns>
        public static bool operator >=(Fraction frac1, float iNo)
        {
            return frac1 >= new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a float is less than a fraction
        /// </summary>
        /// <param name="iNo">float</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the float is less than the Faction</returns>
        public static bool operator <(float iNo, Fraction frac2)
        {
            return new Fraction(iNo) < frac2;
        }

        /// <summary>
        /// Cheks that a float is greater than a fraction
        /// </summary>
        /// <param name="iNo">float</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the float is greater than the Faction</returns>
        public static bool operator >(float iNo, Fraction frac2)
        {
            return new Fraction(iNo) > frac2;
        }

        /// <summary>
        /// Cheks that a float is less than or equal to a fraction
        /// </summary>
        /// <param name="iNo">float</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the float is less than or equal to the Faction</returns>
        public static bool operator <=(float iNo, Fraction frac2)
        {
            return new Fraction(iNo) <= frac2;
        }

        /// <summary>
        /// Cheks that a float is greater than or equal to a fraction
        /// </summary>
        /// <param name="iNo">float</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the float is greater than or equal to the Faction</returns>
        public static bool operator >=(float iNo, Fraction frac2)
        {
            return new Fraction(iNo) >= frac2;
        }

        /// <summary>
        /// Cheks that a fraction is less than a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>true, if the fraction is less than the decimal</returns>
        public static bool operator <(Fraction frac1, decimal iNo)
        {
            return frac1 < new Fraction(iNo);
        }

        /// <summary>
        /// Cheks that a fraction is greater than a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>true, if the fraction is greater than the decimal</returns>
        public static bool operator >(Fraction frac1, decimal iNo)
        {
            return frac1 > new Fraction(iNo);
        }


        /// <summary>
        /// Cheks that a fraction is less than or equal to a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>true, if the fraction is less than or equal to the decimal</returns>
        public static bool operator <=(Fraction frac1, decimal iNo)
        {
            return frac1 <= new Fraction(iNo);
        }


        /// <summary>
        /// Cheks that a fraction is greater than or equal to a decimal
        /// </summary>
        /// <param name="frac1">Fraction</param>
        /// <param name="iNo">decimal</param>
        /// <returns>true, if the fraction is greater than or equal to the decimal</returns>
        public static bool operator >=(Fraction frac1, decimal iNo)
        {
            return frac1 >= new Fraction(iNo);
        }

        /// <summary>
        /// Checks that a decimal is less than a faction
        /// </summary>
        /// <param name="iNo">decimal</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the decimal is less than the fraction</returns>
        public static bool operator <(decimal iNo, Fraction frac2)
        {
            return new Fraction(iNo) < frac2;
        }

        /// <summary>
        /// Checks that a decimal is greater than a faction
        /// </summary>
        /// <param name="iNo">decimal</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the decimal is greater than the fraction</returns>
        public static bool operator >(decimal iNo, Fraction frac2)
        {
            return new Fraction(iNo) > frac2;
        }

        /// <summary>
        /// Checks that a decimal is less than or equal to a faction
        /// </summary>
        /// <param name="iNo">decimal</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the decimal is less than or equal to the fraction</returns>
        public static bool operator <=(decimal iNo, Fraction frac2)
        {
            return new Fraction(iNo) <= frac2;
        }

        /// <summary>
        /// Checks that a decimal is greater than or equal to a faction
        /// </summary>
        /// <param name="iNo">decimal</param>
        /// <param name="frac2">Fraction</param>
        /// <returns>true, if the decimal is greater or equal to than the fraction</returns>
        public static bool operator >=(decimal iNo, Fraction frac2)
        {
            return new Fraction(iNo) >= frac2;
        }

        /// <summary>
        /// Implicit operator for converting an int to Fraction
        /// </summary>
        /// <param name="ino">integer to convert</param>
        public static implicit operator Fraction(int ino)
        {
            return new Fraction(ino);
        }

        /// <summary>
        /// Implicit operator for converting a long to Fraction
        /// </summary>
        /// <param name="ino">long to convert</param>
        public static implicit operator Fraction(long lNo)
        {
            return new Fraction(lNo);
        }

        /// <summary>
        /// Implicit operator for converting a double to Fraction
        /// </summary>
        /// <param name="dNo">double to convert</param>
        public static implicit operator Fraction(double dNo)
        {
            return new Fraction(dNo);
        }

        /// <summary>
        /// Implicit operator for converting a float to Fraction
        /// </summary>
        /// <param name="ino">float to convert</param>
        public static implicit operator Fraction(float dNo)
        {
            return new Fraction(dNo);
        }

        /// <summary>
        /// Implicit operator for converting a decimal to Fraction
        /// </summary>
        /// <param name="ino">decimal to convert</param>
        public static implicit operator Fraction(decimal dNo)
        {
            return new Fraction(dNo);
        }

        /// <summary>
        /// Implicit operator for converting a string to Fraction
        /// </summary>
        /// <param name="ino">string to convert</param>
        public static implicit operator Fraction(string strNo)
        {
            return new Fraction(strNo);
        }

        public static implicit operator string(Fraction frac)
        {
            return frac.ToString();
        }

        /// <summary>
        /// Explicit operator for conversion to double
        /// </summary>
        /// <param name="frac">Fraction to convert</param>
        public static explicit operator double(Fraction frac)
        {
            return (double)frac.Numerator / frac.Denominator;
        }

        /// <summary>
        /// Explicit operator for conversion to float
        /// </summary>
        /// <param name="frac">Fraction to convert</param>
        public static explicit operator float(Fraction frac)
        {
            return (float)frac.Numerator / frac.Denominator;
        }

        /// <summary>
        /// Explicit operator for conversion to decimal
        /// </summary>
        /// <param name="frac">Fraction to convert</param>
        public static explicit operator decimal(Fraction frac)
        {
            return (decimal)frac.Numerator / frac.Denominator;
        }
    }
}
