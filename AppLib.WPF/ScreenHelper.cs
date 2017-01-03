using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;

namespace AppLib.WPF
{
    /// <summary>
    /// A small, screen size helper class
    /// </summary>
    public static class ScreenHelper
    {
        private static readonly List<Size> _screens;
        private static readonly List<Point> _screenstartpoints;

        static ScreenHelper()
        {
            var screens = System.Windows.Forms.Screen.AllScreens;
            _screens = new List<Size>(screens.Length);
            _screenstartpoints = new List<Point>(screens.Length);
            foreach (var screen in screens)
            {
                var s = new Size(screen.Bounds.Width, screen.Bounds.Height);
                var p = new Point(screen.Bounds.Left, screen.Bounds.Top);
                _screens.Add(s);
                _screenstartpoints.Add(p);
            }
        }

        /// <summary>
        /// Gets the current monitor sizes
        /// </summary>
        public static Size[] ScreenSizes => _screens.ToArray();
        
        /// <summary>
        /// Gets the count of available screens
        /// </summary>
        public static int Count
        {
            get { return _screens.Count; }
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

        /// <summary>
        /// Moves a window to the default, primary screen
        /// </summary>
        /// <param name="w">Window to move</param>
        public static void MoveToDefaultScreen(Window w)
        {
            MoveToScreen(w, 0);
        }

        /// <summary>
        /// Moves the window to the specified screen
        /// </summary>
        /// <param name="index">screen index</param>
        public static void MoveToScreen(Window w, int index)
        {
            var s = _screens[index];
            var p = _screenstartpoints[index];

            var x = p.X + ((s.Width - w.Width) / 2);
            var y = p.Y + ((s.Height - w.Height) / 2);
            w.Left = x;
            w.Top = y;
            w.BringIntoView();
            w.Activate();

        }

        /// <summary>
        /// Maximizes window on selected screen
        /// </summary>
        /// <param name="w">Window to maximize</param>
        /// <param name="number">Screen number</param>
        public static void MaximizeOnScreen(Window w, int index)
        {
            var s = _screens[index];
            var p = _screenstartpoints[index];
            w.Left = p.X;
            w.Top = p.Y;
            w.Width = s.Width;
            w.Height = s.Height;
            w.BringIntoView();
            w.Activate();
        }
    }
}
