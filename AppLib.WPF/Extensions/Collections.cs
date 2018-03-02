using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Collection extensions
    /// </summary>
    public static class Collections
    {
        /// <summary>
        /// Create a collection view source from an observableCollection
        /// </summary>
        /// <typeparam name="T">Type of items</typeparam>
        /// <param name="collection">Observable collection</param>
        /// <param name="liveShapingEnabled">Enable or disable collection live shaping</param>
        /// <returns>a CollectionViewSource instance</returns>
        public static CollectionViewSource ToCollectionViewSource<T>(this ObservableCollection<T> collection, bool liveShapingEnabled = true)
        {
            CollectionViewSource ret = new CollectionViewSource();
            ret.IsLiveFilteringRequested = liveShapingEnabled;
            ret.IsLiveGroupingRequested = liveShapingEnabled;
            ret.IsLiveSortingRequested = liveShapingEnabled;
            ret.Source = collection;

            return ret;
        }
    }
}
