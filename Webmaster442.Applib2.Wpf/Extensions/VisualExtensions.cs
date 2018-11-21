using System;
using System.Windows;
using System.Windows.Media;

namespace AppLib.WPF.Extensions
{
    /// <summary>
    /// Visual class extensions
    /// </summary>
    public static class VisualExtensions
    {
        /// <summary>
        /// Get Visual Descendant casted to type
        /// </summary>
        /// <param name="element">Element that Descendant needs to be found</param>
        /// <param name="Descendant">Descendant to find</param>
        /// <returns>Descendant visual</returns>
        public static T GetDescendantByType<T>(this Visual element, Type Descendant = null) where T: Visual
        {
            if (element == null) return null;

            if (Descendant == null) Descendant = typeof(T);

            if (element.GetType() == Descendant) return (T)element;
            T foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType<T>(visual, Descendant);
                if (foundElement != null)
                    break;
            }
            return (T)foundElement;
        }
    }
}
