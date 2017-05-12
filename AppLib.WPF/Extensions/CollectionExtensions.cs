using System.Collections.Generic;
using System.Windows.Media;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// ObservableCollection extensions
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a range of items to the collection
        /// </summary>
        /// <param name="collection">Collection to add to</param>
        /// <param name="items">items to add</param>
        public static void AddRange(this DoubleCollection collection, IEnumerable<double> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}
