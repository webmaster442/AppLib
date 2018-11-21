using System.Windows;
using Webmaster442.Applib.Shaders.Transition;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// Abstract class that provides transition selecton
    /// </summary>
    public abstract class TransitionSelector
    {
        /// <summary>
        /// Provides Transition function for the TransitionControl
        /// </summary>
        /// <param name="oldContent">old content stored in the control</param>
        /// <param name="newContent">new content to be presented</param>
        /// <param name="container">container of the items</param>
        /// <returns>A transitionEffect</returns>
        public abstract Transition GetTransition(object oldContent, object newContent, DependencyObject container);
    }
}
