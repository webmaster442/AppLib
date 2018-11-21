using System;
using System.Collections.Generic;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// Extension methods for Dictionary tpye
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

        /// <summary>
        /// Get a value from a dictionary. If value is not present, then the fallback value is returned
        /// </summary>
        /// <typeparam name="Tkey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="dictionary">dictionary to add to or update</param>
        /// <param name="key">key parameter</param>
        /// <param name="fallback">fallback value</param>
        /// <returns></returns>
        public static TValue GetValueOrFallback<Tkey, TValue>(this Dictionary<Tkey, TValue> dictionary, Tkey key, TValue fallback)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key];
            else return fallback;
        }

        /// <summary>
        /// Flatten Dictionary and return all values indepentent of key
        /// </summary>
        /// <typeparam name="Tkey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to flatten</param>
        /// <returns>all values indepentent of key</returns>
        public static IEnumerable<TValue> AllValues<Tkey, TValue>(this Dictionary<Tkey, TValue> dictionary)
        {
            foreach (var keyvaluePair in dictionary)
            {
                yield return keyvaluePair.Value;
            }
        }

        /// <summary>
        /// Converts dictionary to an Ienumerable collection of tuples
        /// </summary>
        /// <typeparam name="Tkey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">input dictionary</param>
        /// <returns>Dictionary to convert to KeyValueTuples</returns>
        public static IEnumerable<Tuple<Tkey, TValue>> ToKeyValueTuples<Tkey, TValue>(this Dictionary<Tkey, TValue> dictionary)
        {
            foreach (var keyvaluePair in dictionary)
            {
                yield return new Tuple<Tkey, TValue>(keyvaluePair.Key, keyvaluePair.Value);
            }
        }
    }
}
