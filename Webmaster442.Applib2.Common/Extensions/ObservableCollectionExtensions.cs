using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// ObservableCollection extension methods
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Adds a range of items to the observable collection
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to add to</param>
        /// <param name="items">items to add</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null || items == null || !items.Any())
                return;

            Type type = collection.GetType();

            var bindflags = BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic;

            type.InvokeMember("CheckReentrancy", bindflags, null, collection, null);

            var itemsProp = type.BaseType.GetProperty("Items", BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            var privateItems = itemsProp.GetValue(collection) as IList<T>;

            foreach (var item in items)
            {
                privateItems.Add(item);
            }

            type.InvokeMember("OnPropertyChanged", bindflags, null,
              collection, new object[] { new PropertyChangedEventArgs("Count") });

            type.InvokeMember("OnPropertyChanged", bindflags, null,
              collection, new object[] { new PropertyChangedEventArgs("Item[]") });

            type.InvokeMember("OnCollectionChanged", bindflags, null,
              collection, new object[] { new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset) });
        }


        /// <summary>
        /// Replace ObservableCollection items with items in enumerable
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to update</param>
        /// <param name="items">items to add</param>
        public static void ReplaceContents<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (items == null)
                throw new ArgumentException(nameof(items));

            collection.Clear();
            collection.AddRange(items);
        }

        /// <summary>
        /// Smart update, that replaces only changd items
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Collection to update</param>
        /// <param name="items">items to add</param>
        /// <param name="comparer">Equality comparer for items</param>
        public static void UpdateWith<T>(this ObservableCollection<T> collection, IList<T> items, IEqualityComparer<T> comparer = null)
        {
            if (collection == null || items == null || !items.Any())
                return;

            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            Type type = collection.GetType();

            var bindflags = BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic;

            type.InvokeMember("CheckReentrancy", bindflags, null, collection, null);

            var itemsProp = type.BaseType.GetProperty("Items", BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            var privateItems = itemsProp.GetValue(collection) as IList<T>;

            if (privateItems.Count > items.Count)
            {
                for (int i=items.Count-1; i<privateItems.Count; i++)
                {
                    privateItems.RemoveAt(i);
                }
            }

            for (int i=0; i< items.Count; i++)
            {
                if (i > privateItems.Count)
                {
                    privateItems.Add(items[i]);
                }
                else
                {
                    if (!comparer.Equals(privateItems[i], items[i]))
                    {
                        privateItems[i] = items[i];
                    }
                }
            }

            type.InvokeMember("OnPropertyChanged", bindflags, null,
                collection, new object[] { new PropertyChangedEventArgs("Count") });

            type.InvokeMember("OnPropertyChanged", bindflags, null,
                collection, new object[] { new PropertyChangedEventArgs("Item[]") });

            type.InvokeMember("OnCollectionChanged", bindflags, null,
                collection, new object[] { new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset) });
        }
    }
}
