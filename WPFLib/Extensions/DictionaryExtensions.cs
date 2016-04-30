using System.Collections.Generic;

namespace WPFLib.Extensions
{
    /// <summary>
    /// Dictionary Tkey, TValue extension methoods 
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds or updates a value into the dictionary with the associated key
        /// </summary>
        /// <typeparam name="Tkey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="dictionary">dictionary to add to or update</param>
        /// <param name="key">key parameter</param>
        /// <param name="value">value parameter</param>
        public static void AddOrUpdate<Tkey, TValue>(this Dictionary<Tkey, TValue> dictionary, Tkey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) dictionary[key] = value;
            else dictionary.Add(key, value);
        }
    }
}
