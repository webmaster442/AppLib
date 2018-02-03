using System;
using System.Security.Cryptography;

namespace AppLib.Common
{
    /// <summary>
    /// A Variable length Unique identifier, similar to GUID
    /// </summary>
    public sealed class UId : IEquatable<UId>, IComparable<UId>
    {
        private ulong _data;

        /// <summary>
        /// Creates a new instance of UID
        /// </summary>
        public UId()
        {
            var buffer = new byte[8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);

            _data = BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Creates a new instance of UID
        /// </summary>
        /// <param name="value">intialization value</param>
        public UId(int value)
        {
            _data = Convert.ToUInt64(value);
        }

        /// <summary>
        /// Creates a new instance of UID
        /// </summary>
        /// <param name="value">intialization value</param>
        public UId(uint value)
        {
            _data = Convert.ToUInt64(value);
        }


        /// <summary>
        /// Creates a new instance of UID
        /// </summary>
        /// <param name="value">intialization value</param>
        public UId(long value)
        {
            _data = Convert.ToUInt64(value);
        }


        /// <summary>
        /// Creates a new instance of UID
        /// </summary>
        /// <param name="value">intialization value</param>
        public UId(ulong value)
        {
            _data = value;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return _data.ToString("X16");
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as UId;
            if (other == null) return false;
            else return Equals(other);
        }

        /// <summary>
        /// Computes the hash of the current UID
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
                return _data.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(UId other)
        {
            return other._data == _data;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(UId other)
        {
            return _data.CompareTo(other._data);
        }

        /// <summary>
        /// Checks wheather the specified Uid is null or zero
        /// </summary>
        /// <param name="id">Uid to check</param>
        /// <returns>True, if the Uid is null or contains the value 0</returns>
        public static bool IsNullOrZero(UId id)
        {
            if (id == null) return true;
            return id._data == 0;
        }

        /// <summary>
        /// Compares two UID objects equality
        /// </summary>
        /// <param name="One">First UID</param>
        /// <param name="other">Second UID</param>
        /// <returns>true, if the two instances are identical, false otherwise</returns>
        public static bool operator == (UId One, UId other)
        {
            return One.Equals(other);
        }

        /// <summary>
        /// Compares two UID objects not equality
        /// </summary>
        /// <param name="One">First UID</param>
        /// <param name="other">Second UID</param>
        /// <returns>true, if the two instances are not identical, false otherwise</returns>
        public static bool operator !=(UId One, UId other)
        {
            return !One.Equals(other);
        }

        /// <summary>
        /// returns true if the first operand is less than the second, false otherwise.
        /// </summary>
        /// <param name="one">First UID</param>
        /// <param name="other">Second UID</param>
        /// <returns>true if the first operand is less than the second, false otherwise.</returns>
        public static bool operator < (UId one, UId other)
        {
            return one.CompareTo(other) == -1;
        }

        /// <summary>
        /// returns true if the first operand is greater than the second, false otherwise
        /// </summary>
        /// <param name="one">First UID</param>
        /// <param name="other">Second UID</param>
        /// <returns>true if the first operand is greater than the second, false otherwise</returns>
        public static bool operator >(UId one, UId other)
        {
            return one.CompareTo(other) == 1;
        }
    }
}
