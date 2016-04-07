using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;

namespace WPFLib
{
    /// <summary>
    /// A small, screen size helper class
    /// </summary>
    public static class ScreenHelper
    {
        private static readonly List<Size> _screens;

        static ScreenHelper()
        {
            var screens = System.Windows.Forms.Screen.AllScreens;
            _screens = new List<Size>(screens.Length);
            foreach (var screen in screens)
            {
                var s = new Size(screen.Bounds.Width, screen.Bounds.Height);
                _screens.Add(s);
            }
        }

        /// <summary>
        /// Gets the current monitor sizes
        /// </summary>
        public static Size[] ScreenSizes
        {
            get { return _screens.ToArray(); }
        }

        /// <summary>
        /// Returns the current screen size
        /// </summary>
        /// <param name="w">A window</param>
        /// <returns>The current screen size which displays the window</returns>
        public static Size GetCurrentScreenSize(Window w)
        {
            var helper = new WindowInteropHelper(w); //this being the wpf form 
            var current = System.Windows.Forms.Screen.FromHandle(helper.Handle);
            return new Size(current.Bounds.Width, current.Bounds.Height);
        }
    }
}
