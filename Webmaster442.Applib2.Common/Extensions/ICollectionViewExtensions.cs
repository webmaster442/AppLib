using System.ComponentModel;

namespace Webmaster442.Applib.Extensions
{
    /// <summary>
    /// CollectionView Extensions
    /// </summary>
    public static class ICollectionViewExtensions
    {
        /// <summary>
        /// Activates Live sorting
        /// </summary>
        /// <param name="collectionView">Target collectionView</param>
        /// <param name="apend">Apend or overwrite properties</param>
        /// <param name="involvedProperties">Involved properties</param>
        public static void ActiveateLiveSorting(this ICollectionView collectionView, bool apend, params string[] involvedProperties)
        {
            if (!(collectionView is ICollectionViewLiveShaping collectionViewLiveShaping)) return;
            if (collectionViewLiveShaping.CanChangeLiveSorting)
            {
                if (!apend)
                    collectionViewLiveShaping.LiveSortingProperties.Clear();

                foreach (string propName in involvedProperties)
                {
                    collectionViewLiveShaping.LiveSortingProperties.Add(propName);
                }

                collectionViewLiveShaping.IsLiveSorting = true;
            }
        }

        /// <summary>
        /// Activates Live Grouping
        /// </summary>
        /// <param name="collectionView">Target collectionView</param>
        /// <param name="apend">Apend or overwrite properties</param>
        /// <param name="involvedProperties">Involved properties</param>
        public static void ActiveateLiveGrouping(this ICollectionView collectionView, bool apend, params string[] involvedProperties)
        {
            if (!(collectionView is ICollectionViewLiveShaping collectionViewLiveShaping)) return;
            if (collectionViewLiveShaping.CanChangeLiveGrouping)
            {
                if (!apend)
                    collectionViewLiveShaping.LiveGroupingProperties.Clear();

                foreach (string propName in involvedProperties)
                {
                    collectionViewLiveShaping.LiveGroupingProperties.Add(propName);
                }

                collectionViewLiveShaping.IsLiveGrouping = true;
            }
        }

        /// <summary>
        /// Activates live filtering
        /// </summary>
        /// <param name="collectionView">Target collectionView</param>
        /// <param name="apend">Apend or overwrite properties</param>
        /// <param name="involvedProperties">Involved properties</param>
        public static void ActiveateLiveFiltering(this ICollectionView collectionView, bool apend, params string[] involvedProperties)
        {
            if (!(collectionView is ICollectionViewLiveShaping collectionViewLiveShaping)) return;
            if (collectionViewLiveShaping.CanChangeLiveFiltering)
            {
                if (!apend)
                    collectionViewLiveShaping.LiveFilteringProperties.Clear();

                foreach (string propName in involvedProperties)
                {
                    collectionViewLiveShaping.LiveFilteringProperties.Add(propName);
                }

                collectionViewLiveShaping.IsLiveFiltering = true;
            }
        }
    }
}
