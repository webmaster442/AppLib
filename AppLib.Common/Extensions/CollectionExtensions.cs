﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// Extension methoods for IEnumerable
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts an IEnumerable collection to a stack
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="source">souce collection</param>
        /// <returns>source collection in a stack</returns>
        public static Stack<T> ToStack<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var stack = new Stack<T>(source);
            return stack;
            
        }

        /// <summary>
        /// Converts an IEnumerable collection to a Queue
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="source">souce collection</param>
        /// <returns>source collection in a Queue</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var que = new Queue<T>(source);
            return que;
        }

        /// <summary>
        /// Converts an IEnumerable collection to a HashSet
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="source">souce collection</param>
        /// <returns>source collection in a HashSet</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var hash = new HashSet<T>(source);
            return hash;
        }

        /// <summary>
        /// Adds a range of items to the observable collection
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to add to</param>
        /// <param name="items">items to add</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (items == null)
                throw new ArgumentException(nameof(items));

            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        /// Update an observable collection with new items
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to update</param>
        /// <param name="items">items to add</param>
        public static void UpdateWith<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (items == null)
                throw new ArgumentException(nameof(items));

            collection.Clear();

            foreach (var item in items)
                collection.Add(item);
        }

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