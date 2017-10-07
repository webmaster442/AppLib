﻿using System;

namespace ECalc.Maths
{

    /// <summary>
    /// Trigonometry modes
    /// </summary>
    public enum TrigMode
    {
        /// <summary>
        /// Degrees
        /// </summary>
        DEG,
        /// <summary>
        /// Radians
        /// </summary>
        RAD,
        /// <summary>
        /// Gradians
        /// </summary>
        GRAD
    }

    /// <summary>
    /// Trigonometrical functions
    /// </summary>
    public static class Trigonometry
    {
        static Trigonometry()
        {
            Mode = TrigMode.RAD;
        }


        /// <summary>
        /// Gets or sets the Trigonometry Mode
        /// </summary>
        public static TrigMode Mode
        {
            get;
            set;
        }

        /// <summary>
        /// Converts radians to degrees. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="rad">input radians</param>
        
        public static double Rad2Deg(double rad)
        {
            return rad * (180 / Math.PI);
        }

        /// <summary>
        /// Converts degrees to radians. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="deg">input degrees</param>
        
        public static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        /// <summary>
        /// Converts degress to gradians. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="deg">input degrees</param>
        
        public static double Deg2Grad(double deg)
        {
            return (400.0 / 360.0) * deg;
        }

        /// <summary>
        /// Converts gradians to degrees. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="grad">input gradians</param>
        /// <returns></returns>
        
        public static double Grad2Deg(double grad)
        {
            return (360.0 / 400.0) * grad;
        }

        /// <summary>
        /// Converts gradians to radians. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="grad">input gradians</param>
        
        public static double Grad2Rad(double grad)
        {
            double fok = (360.0 / 400.0) * grad;
            return (Math.PI / 180) * fok;
        }

        /// <summary>
        /// Converts radians to gradians. Current trig mode does not affect this function.
        /// </summary>
        /// <param name="rad">input radians</param>
        
        public static double Rad2Grad(double rad)
        {
            double fok = (rad * 180) / Math.PI;
            return (400.0 / 360.0) * fok;
        }

        /// <summary>
        /// Returns the sine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Sin(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    if ((Deg2Rad(value1) >= Math.PI) && ((Deg2Rad(value1) % Math.PI) == 0)) return 0;
                    else return Math.Sin(Deg2Rad(value1));
                case TrigMode.GRAD:
                    if ((Grad2Rad(value1) >= Math.PI) && ((Grad2Rad(value1) % Math.PI) == 0)) return 0;
                    else return Math.Sin(Grad2Rad(value1));
                case TrigMode.RAD:
                    if ((value1 >= Math.PI) && ((value1 % Math.PI) == 0)) return 0;
                    else return Math.Sin(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the cosine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Cos(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    if ((((Deg2Rad(value1) - (Math.PI / 2)) % Math.PI) == 0) || Deg2Rad(value1) == (Math.PI / 2)) return 0;
                    else return Math.Cos(Deg2Rad(value1));
                case TrigMode.GRAD:
                    if ((((Grad2Rad(value1) - (Math.PI / 2)) % Math.PI) == 0) || Grad2Rad(value1) == (Math.PI / 2)) return 0;
                    else return Math.Cos(Grad2Rad(value1));
                case TrigMode.RAD:
                    if ((((value1 - (Math.PI / 2)) % Math.PI) == 0) || value1 == (Math.PI / 2)) return 0;
                    else return Math.Cos(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the tangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Tan(double value1)
        {
            return Sin(value1) / Cos(value1);
        }

        /// <summary>
        /// Returns the secant of a number, depending on the mode set.  
        /// </summary>
        /// <param name="x">input number</param>
        
        public static double Sec(double x)
        {
            return 1 / Cos(x);
        }

        /// <summary>
        /// Returns the cosecant of a number, depending on the mode set.  
        /// </summary>
        /// <param name="x">input number</param>
        
        public static double Cosec(double x)
        {
            return 1 / Sin(x);
        }


        /// <summary>
        /// Returns the cotangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Ctg(double value1)
        {
            return Cos(value1) / Sin(value1);
        }

        /// <summary>
        /// Returns the hyperbolic sine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Sinh(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Math.Sinh(Deg2Rad(value1));
                case TrigMode.GRAD:
                    return Math.Sinh(Grad2Rad(value1));
                case TrigMode.RAD:
                    return Math.Sinh(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the hyperbolic cosine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Cosh(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Math.Cosh(Deg2Rad(value1));
                case TrigMode.RAD:
                    return Math.Cosh(Grad2Rad(value1));
                case TrigMode.GRAD:
                    return Math.Cosh(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the hyperbolic tangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double Tanh(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Math.Tanh(Deg2Rad(value1));
                case TrigMode.GRAD:
                    return Math.Tanh(Grad2Rad(value1));
                case TrigMode.RAD:
                    return Math.Tanh(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the hyperbolic secant of a number, depending on the mode set.  
        /// </summary>
        /// <param name="value">input number</param>
        
        public static double Sech(double value)
        {
            double rad = value;
            switch (Mode)
            {
                case TrigMode.DEG:
                    rad = Deg2Rad(value);
                    break;
                case TrigMode.GRAD:
                    rad = Grad2Rad(value);
                    break;
            }
            return 2 / (Math.Exp(rad) + Math.Exp(-rad));
        }

        /// <summary>
        /// Returns the arcus sine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcSin(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(Math.Asin(value1));
                case TrigMode.GRAD:
                    return Rad2Grad(Math.Asin(value1));
                case TrigMode.RAD:
                    return Math.Asin(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the arcus cosine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcCos(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(Math.Acos(value1));
                case TrigMode.GRAD:
                    return Rad2Grad(Math.Acos(value1));
                case TrigMode.RAD:
                    return Math.Acos(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the arcus tangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcTan(double value1)
        {
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(Math.Atan(value1));
                case TrigMode.GRAD:
                    return Rad2Grad(Math.Atan(value1));
                case TrigMode.RAD:
                    return Math.Atan(value1);
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// returns the arcus cotangent of a number, depending on the mode set.  
        /// </summary>
        /// <param name="value">input number</param>
        
        public static double ArcCtg(double value)
        {
            return ArcTan(1 / value);
        }


        /// <summary>
        /// Returns the arcus Secant of a number, depending on the mode set.
        /// </summary>
        /// <param name="x">input number</param>
        /// <returns>arcus Secant of a number, depending on the mode set.</returns>
        public static double ArcSec(double x)
        {
            return 2 * ArcTan(1) - ArcTan(Math.Sign(x) / Math.Sqrt(x * x - 1));
        }


        /// <summary>
        /// Returns the arcus hyperboc sine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcSinh(double value1)
        {
            double inrads = Math.Log(Math.Pow(Math.Pow(value1, 2) + 1, 0.5), Math.E);
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(inrads);
                case TrigMode.GRAD:
                    return Rad2Grad(inrads);
                case TrigMode.RAD:
                    return inrads;
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the arcus hyperbolic cosine of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcCosh(double value1)
        {
            double inrads = Math.Log(Math.Pow(Math.Pow(value1, 2) - 1, 0.5), Math.E);
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(inrads);
                case TrigMode.GRAD:
                    return Rad2Grad(inrads);
                case TrigMode.RAD:
                    return inrads;
                default:
                    return double.NaN;
            }
        }


        /// <summary>
        /// Returns the arcus hyperbolic tangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value1">input number</param>
        
        public static double ArcTanh(double value1)
        {
            double inrads = 0.5 * Math.Log((1 + value1 / 1 - value1), Math.E);
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(inrads);
                case TrigMode.GRAD:
                    return Rad2Grad(inrads);
                case TrigMode.RAD:
                    return inrads;
                default:
                    return double.NaN;
            }
        }


        /// <summary>
        /// Returns the arcus hyperbolic secant of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>hyperbolic secant of a number,depending on the mode set.</returns>
        public static double ArcSech(double value)
        {
            double inrad = Math.Log((Math.Sqrt(-value * value + 1) + 1) / value);
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(inrad);
                case TrigMode.GRAD:
                    return Rad2Grad(inrad);
                case TrigMode.RAD:
                    return inrad;
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the arcus cosecant of a number, depending on the mode set. 
        /// </summary>
        /// <param name="x">input number</param>
        /// <returns>arcus cosecant of a number, depending on the mode set</returns>
        public static double ArcCosec(double x)
        {
            return ArcTan(Math.Sign(x) / Math.Sqrt(x * x - 1));
        }


        /// <summary>
        /// Returns the hyperbolic arcus cosecant of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value">input number</param>
        /// <returns>hyperbolic arcus cosecant of a number, depending on the mode set.</returns>
        public static double ArcCosech(double value)
        {
            double inrad = Math.Log((Math.Sign(value) * Math.Sqrt(value * value + 1) + 1) / value);
            switch (Mode)
            {
                case TrigMode.DEG:
                    return Rad2Deg(inrad);
                case TrigMode.GRAD:
                    return Rad2Grad(inrad);
                case TrigMode.RAD:
                    return inrad;
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of a number, depending on the mode set.  
        /// </summary>
        /// <param name="value">input number</param>
        
        public static double Cosech(double value)
        {
            double rad = value;
            switch (Mode)
            {
                case TrigMode.DEG:
                    rad = Deg2Rad(value);
                    break;
                case TrigMode.GRAD:
                    rad = Grad2Rad(value);
                    break;
            }
            return 2 / (Math.Exp(rad) - Math.Exp(-rad));
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of a number, depending on the mode set. 
        /// </summary>
        /// <param name="value">input number</param>
        public static double Ctgh(double value)
        {
            double rad = value;
            switch (Mode)
            {
                case TrigMode.DEG:
                    rad = Deg2Rad(value);
                    break;
                case TrigMode.GRAD:
                    rad = Grad2Rad(value);
                    break;
            }
            return (Math.Exp(rad) + Math.Exp(-rad)) / (Math.Exp(rad) - Math.Exp(-rad));
        }
    }
}
