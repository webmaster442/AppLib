using System;
using System.Collections.Generic;
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
        /// Creates a new instance of UID based on a value
        /// </summary>
        /// <param name="value">value it initialize with</param>
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
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as UId);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(UId other)
        {
            return other != null &&
                   _data == other._data;
        }

        /// <summary>
        /// Computes the hash of the current UID
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return -1945990370 + _data.GetHashCode();
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
        /// Explicitly converts an ulong to a uid
        /// </summary>
        /// <param name="value">value to convert</param>
        public static explicit operator UId(ulong value)
        {
            return new UId(value);
        }

        /// <summary>
        /// Compares two UID objects equality
        /// </summary>
        /// <param name="id1">First UID</param>
        /// <param name="id2">Second UID</param>
        /// <returns>true, if the two instances are identical, false otherwise</returns>
        public static bool operator ==(UId id1, UId id2)
        {
            return EqualityComparer<UId>.Default.Equals(id1, id2);
        }


        /// <summary>
        /// Compares two UID objects not equality
        /// </summary>
        /// <param name="id1">First UID</param>
        /// <param name="id2">Second UID</param>
        /// <returns>true, if the two instances are not identical, false otherwise</returns>
        public static bool operator !=(UId id1, UId id2)
        {
            return !(id1 == id2);
        }
    }
}
