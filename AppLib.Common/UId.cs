using System;
using System.Security.Cryptography;
using System.Text;

namespace AppLib.Common
{
    /// <summary>
    /// A Variable length Unique identifier, similar to GUID
    /// </summary>
    public sealed class UId : IEquatable<UId>, IComparable<UId>
    {
        private byte[] _data;

        private UId(byte[] data)
        {
            _data = data;
        }

        /// <summary>
        /// Creates a new UID from a long value.
        /// The Resulting UID will be 8 bytes
        /// </summary>
        /// <param name="value">A value</param>
        /// <returns>A Uid containing the value</returns>
        public static UId CreateFrom(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return new UId(bytes);
        }

        /// <summary>
        /// Creates a new UID from an unsigned long value.
        /// The Resulting UID will be 8 bytes
        /// </summary>
        /// <param name="value">A value</param>
        /// <returns>A Uid containing the value</returns>
        public static UId CreateFrom(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return new UId(bytes);
        }

        /// <summary>
        /// Creates a new random UID 
        /// </summary>
        /// <param name="size">Size of UID in bytes. By default 8 is used</param>
        /// <returns>A new random UID</returns>
        public static UId Create(int size = 8)
        {
            var buffer = new byte[size];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return new UId(buffer);
        }

        /// <summary>
        /// Gets the UID length
        /// </summary>
        public int Length
        {
            get { return _data.Length; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i=0; i<_data.Length; i++)
            {
                sb.AppendFormat("{0:X2}", _data[i]);
                if (i != _data.Length - 1)
                    sb.Append("-");

            }
            return sb.ToString();
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
            else return Equals(this, other);
        }

        /// <summary>
        /// Computes the hash of the current UID
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < _data.Length; i++)
                {
                    hash = hash * 23 + _data[i].GetHashCode();
                }
                return hash;
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(UId other)
        {
            if (this.Length != other.Length) return false;
            for (int i = 0; i < other._data.Length; i++)
            {
                if (this._data[i] != other._data[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(UId other)
        {
            if (Length > other.Length) return 1;
            for (int i = Length - 1; i >= 0; i--)
            {
                if (_data[i] > other._data[i]) return 1;
                else if (_data[i] < other._data[i]) return -1;
            }
            return 0;
        }

        /// <summary>
        /// Checks wheather the specified Uid is null or zero
        /// </summary>
        /// <param name="id">Uid to check</param>
        /// <returns>True, if the Uid is null or contains the value 0</returns>
        public static bool IsNullOrZero(UId id)
        {
            if (id == null) return true;
            foreach (var b in id._data)
            {
                if (b != 0) return false;
            }
            return true;
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
