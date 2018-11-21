using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// Extension methods for IEnumerable tpye
    /// </summary>
    public static class IEnumerableExtensions
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
        /// Converts an IEnumerable collection to a HashSet
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="source">souce collection</param>
        /// <returns>source collection in an ObservableCollection</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var collection = new ObservableCollection<T>(source);
            return collection;
        }

        /// <summary>
        /// Converts an IEnumerable collection to a HashSet
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="source">souce collection</param>
        /// <returns>source collection in an BindingList</returns>
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var collection = new BindingList<T>(source.ToList());
            return collection;
        }

        /// <summary>
        /// Finds the first index of the element, that is matched by the rule.
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to search in</param>
        /// <param name="match">Match function</param>
        /// <returns>first index of the element, that is matched by the rule.</returns>
        public static int FirstIndexOf<T>(this IEnumerable<T> collection, Func<T, bool> match)
        {
            var index = 0;
            foreach (var item in collection)
            {
                if (match.Invoke(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the specified comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
        /// If null, the default equality comparer for <c>TSource</c> is used.</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return _(); IEnumerable<TSource> _()
            {
                var knownKeys = new HashSet<TKey>(comparer);
                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }

        /// <summary>
        /// Creates a System.Collections.Generic.Dictionary`2 from an System.Collections.Generic.IEnumerable`1
        /// according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector</typeparam>
        /// <typeparam name="TElement">The type of the value returned by elementSelector.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable`1 to create a System.Collections.Generic.Dictionary`2 from.</param>
        /// <param name="keySelector"> A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer`1 to compare keys</param>
        /// <returns>A System.Collections.Generic.Dictionary`2 that contains values of type TElement selected from the input sequence.</returns>
        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (keySelector == null)
                throw new ArgumentNullException("keySelector");

            if (elementSelector == null)
                throw new ArgumentNullException("elementSelector");

            ConcurrentDictionary<TKey, TElement> d = new ConcurrentDictionary<TKey, TElement>(comparer);

            foreach (TSource element in source)
                d.TryAdd(keySelector(element), elementSelector(element));

            return d;
        }
    }
}
