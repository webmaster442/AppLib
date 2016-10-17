﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Dependency Object Extension methoods
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Find a Child of a specified type in a container
        /// </summary>
        /// <typeparam name="T">Type to search for</typeparam>
        /// <param name="parent">Parent container</param>
        /// <param name="childName">Child name</param>
        /// <returns>null, if child not found, otherwise the child</returns>
        public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        /// <summary>
        /// Finds the nearest child of the specified type, or null if one wasn't found.
        /// </summary>
        /// <typeparam name="T">Type to search for</typeparam>
        /// <param name="reference">Parent container</param>
        /// <returns>nearest child of the specified type, or null if one wasn't found.</returns>
        public static T FindChild<T>(this DependencyObject reference) where T : class
        {
            // Do a breadth first search.
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(reference);
            while (queue.Count > 0)
            {
                DependencyObject child = queue.Dequeue();
                T obj = child as T;
                if (obj != null)
                {
                    return obj;
                }

                // Add the children to the queue to search through later.
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(child); i++)
                {
                    queue.Enqueue(VisualTreeHelper.GetChild(child, i));
                }
            }
            return null; // Not found.
        }

        /// <summary>
        /// Finds all children of a specified type in a container
        /// </summary>
        /// <typeparam name="T">Type of children to search</typeparam>
        /// <param name="source">The container</param>
        /// <returns>An enumerable collecton of the children</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source != null)
            {
                var childs = GetChildObjects(source);
                foreach (DependencyObject child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    //recurse tree
                    foreach (T descendant in FindChildren<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        /// <summary>
        /// Get all children objects of a container
        /// </summary>
        /// <param name="parent">The container</param>
        /// <returns>An enumerable collecton of the children</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent)
        {
            if (parent == null) yield break;

            if (parent is ContentElement || parent is FrameworkElement)
            {
                //use the logical tree for content / framework elements
                foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                {
                    var depObj = obj as DependencyObject;
                    if (depObj != null) yield return (DependencyObject)obj;
                }
            }
            else
            {
                //use the visual tree per default
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }
    }
}
