using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;

namespace Webmaster442.Applib
{

    /// <summary>
    /// A class representing a montitor display
    /// </summary>
    public class Display
    {
        /// <summary>
        /// Start point
        /// </summary>
        public Point StartPoint { get; private set; }
        /// <summary>
        /// End point
        /// </summary>
        public Point EndPoint { get; private set; }
        /// <summary>
        /// Resoulution
        /// </summary>
        public Size Size { get; private set; }

        private Display(Point start, Point end, Size size)
        {
            StartPoint = start;
            EndPoint = end;
            Size = size;
        }

        /// <summary>
        /// Creates a display instance from a System.Windows.Forms.Screen instance
        /// </summary>
        /// <param name="screen">a System.Windows.Forms.Screen instance</param>
        /// <returns>a display instance from the parameter</returns>
        public static Display CreateFrom(System.Windows.Forms.Screen screen)
        {
            var size = new Size(screen.Bounds.Width, screen.Bounds.Height);
            var start = new Point(screen.Bounds.Left, screen.Bounds.Top);
            var end = new Point(start.X + size.Width, start.Y + size.Height);
            return new Display(start, end, size);
        }

        /// <summary>
        /// Converts this instance to text
        /// </summary>
        /// <returns>A string representation of the instance</returns>
        public override string ToString()
        {
            return string.Format("Start pos: {0} | End pos: {1} | Size: {2}", StartPoint, EndPoint, Size);
        }
    }

    /// <summary>
    /// A small, screen size helper class
    /// </summary>
    public static class ScreenHelper
    {
        private static List<Display> _displays;

        /// <summary>
        /// Gets an array of displays
        /// </summary>
        public static Display[] Displays
        {
            get { return _displays.ToArray(); }
        }

        static ScreenHelper()
        {
            _displays = new List<Display>();
            var screens = System.Windows.Forms.Screen.AllScreens;
            foreach (var screen in screens)
            {
                _displays.Add(Display.CreateFrom(screen));
            }
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
        /// Returns the screen index by the mouse pointer position
        /// </summary>
        /// <returns>The index of the screen, otherwise -1</returns>
        public static int GetScreenIndexByMouse()
        {
            var currentpos = System.Windows.Forms.Cursor.Position;
            for (int i=0; i<_displays.Count; i++)
            {

                if (currentpos.X >= Displays[i].StartPoint.X &&
                    currentpos.X <= Displays[i].EndPoint.X &&
                    currentpos.Y >= Displays[i].StartPoint.Y &&
                    currentpos.Y <= Displays[i].EndPoint.Y)
                {
                    return i;
                }
            }
            return -1;
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
        /// <param name="w">Window to move</param>
        /// <param name="index">screen index</param>
        public static void MoveToScreen(Window w, int index)
        {
            var display = Displays[index];
            w.Left = display.StartPoint.X + ((display.Size.Width- w.Width) / 2);
            w.Top = display.StartPoint.Y + ((display.Size.Height - w.Height) / 2);
            w.BringIntoView();
            w.Activate();

        }

        /// <summary>
        /// Maximizes window on selected screen
        /// </summary>
        /// <param name="w">Window to maximize</param>
        /// <param name="index">Screen number</param>
        public static void MaximizeOnScreen(Window w, int index)
        {
            var display = Displays[index];
            w.Left = display.StartPoint.X;
            w.Top = display.StartPoint.Y;
            w.Width = display.EndPoint.X - display.StartPoint.X;
            w.Height = display.EndPoint.Y - display.StartPoint.Y;
            w.BringIntoView();
            w.Activate();
        }
    }
}
