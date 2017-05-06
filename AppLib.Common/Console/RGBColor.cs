using System;

namespace AppLib.Common.Console
{
    /// <summary>
    /// An RGB color class
    /// </summary>
    public class RGBColor : IComparable<RGBColor>, IEquatable<RGBColor>
    {
        /// <summary>
        /// Red component value
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// Green component value
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// Blue component value
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Creates a new instace of RGB color
        /// </summary>
        public RGBColor() { }

        /// <summary>
        /// Creates a new instace of RGB color
        /// </summary>
        /// <param name="r">red value</param>
        /// <param name="g">green value</param>
        /// <param name="b">blue value</param>
        public RGBColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Creates a new instace of RGB color
        /// </summary>
        /// <param name="value">a 32 bit integer value</param>
        public RGBColor(int value)
        {
            R = (byte)((value & 0xFF0000) >> 16);
            G = (byte)((value & 0x00FF00) >> 8);
            B = (byte)((value & 0x0000FF));
        }

        private int Pack()
        {
            int val = R << 16;
            val |= G << 8;
            val |= B;
            return val;
        }

        /// <summary>
        /// Converts the current instance to a string representation
        /// </summary>
        /// <returns>string representation of current instace</returns>
        public override string ToString()
        {
            return string.Format("R: {0}, G: {1}, B: {2}", R, G, B);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as RGBColor;
            if (other == null) return false;
            else return Equals(other);
        }

        /// <summary>
        /// Computes the hash of the current UID
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Pack().GetHashCode();
        }


        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(RGBColor other)
        {
            var p = Pack();
            var op = Pack();
            return p.CompareTo(op);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(RGBColor other)
        {
            return Pack() == other.Pack();
        }
    }
}
