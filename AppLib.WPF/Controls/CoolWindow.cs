using AppLib.Common.PInvoke;
using AppLib.WPF.Extensions;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Coolwindows is a custom themed window with an allways on top button-
    /// CoolWindow border is affected by the theme accent.
    /// </summary>
    public class CoolWindow: Window
    {
        private Button _minimizeButton;
        private ToggleButton _restoreButton;
        private ToggleButton _allwaystopButton;
        private Button _closeButton;

        static CoolWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CoolWindow),
                new FrameworkPropertyMetadata(typeof(CoolWindow)));
        }

        private static Color GetWindowColorizationColor(bool opaque)
        {
            var par = new DWMCOLORIZATIONPARAMS();
            DwmApi.DwmGetColorizationParameters(ref par);

            return Color.FromArgb(
                (byte)(opaque ? 255 : par.ColorizationColor >> 24),
                (byte)(par.ColorizationColor >> 16),
                (byte)(par.ColorizationColor >> 8),
                (byte)par.ColorizationColor);
        }

        /// <summary>
        /// Creates a new instance of CoolWindow
        /// </summary>
        public CoolWindow() : base()
        {
            PreviewMouseMove += OnPreviewMouseMove;
        }

        private HwndSource _hwndSource;

        /// <summary>
        /// Ovverride the OnInitialized event
        /// </summary>
        /// <param name="e">Event parameters</param>
        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                //WM_DWMCOLORIZATIONCOLORCHANGED
                case 0x320:
                    SetColor();
                    handled = true;
                    return IntPtr.Zero;
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    return IntPtr.Zero;
                default:
                    return IntPtr.Zero;
            }
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = User32.MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != System.IntPtr.Zero)
            {

                MONITORINFO monitorInfo = new MONITORINFO();
                User32.GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }



        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
            _hwndSource.AddHook(WndProc);
        }

        private void SetColor()
        {
            Color c = GetWindowColorizationColor(true);
            Grid BorderGrid = GetTemplateChild("BorderGrid") as Grid;
            if (BorderGrid != null)
                BorderGrid.Background = new SolidColorBrush(c);

            var inverse = new SolidColorBrush(c.Inverse());
            _minimizeButton.Foreground = inverse;
            _restoreButton.Foreground = inverse;
            _allwaystopButton.Foreground = inverse;

        }

        /// <summary>
        /// Applies the template
        /// </summary>
        public override void OnApplyTemplate()
        {
            _minimizeButton = GetTemplateChild("minimizeButton") as Button;
            if (_minimizeButton != null)
                _minimizeButton.Click += MinimizeClick;

            _restoreButton = GetTemplateChild("restoreButton") as ToggleButton;
            if (_restoreButton != null)
                _restoreButton.Click += RestoreClick;

            _allwaystopButton = GetTemplateChild("allwaystopButton") as ToggleButton;
            if (_allwaystopButton != null)
                _allwaystopButton.Click += AllwaystopButton_Click;

            _closeButton = GetTemplateChild("closeButton") as Button;
            if (_closeButton != null)
                _closeButton.Click += CloseClick;

            Border moove = GetTemplateChild("moveRectangle") as Border;
            if (moove != null)
                moove.PreviewMouseDown += moveRectangle_PreviewMouseDown;

            Grid resizeGrid = GetTemplateChild("resizeGrid") as Grid;
            if (resizeGrid != null)
            {
                foreach (UIElement element in resizeGrid.Children)
                {
                    Rectangle resizeRectangle = element as Rectangle;
                    if (resizeRectangle != null)
                    {
                        resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                        resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
                    }
                }
            }

            SetColor();
            base.OnApplyTemplate();
        }

        private void AllwaystopButton_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void RestoreClick(object sender, RoutedEventArgs e)
        {
            WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void moveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ResizeRectangle_MouseMove(Object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        private void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name)
            {
                case "top":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            User32.SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        /// <summary>
        /// Dependency property for TitleContent
        /// </summary>
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(FrameworkElement), typeof(CoolWindow));


        /// <summary>
        /// Gets or sets the Titlebar Content
        /// </summary>
        public FrameworkElement TitleContent
        {
            get { return (FrameworkElement)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        /// <summary>
        /// Dependency property for SysButtonAlwaystopVisible
        /// </summary>
        public static readonly DependencyProperty SysButtonAlwaystopVisibleProperty =
            DependencyProperty.Register("SysButtonAlwaystopVisible", typeof(bool), typeof(CoolWindow), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Dependency property for SysButtonMinimizeVisible
        /// </summary>
        public static readonly DependencyProperty SysButtonMinimizeVisibleProperty =
            DependencyProperty.Register("SysButtonMinimizeVisible", typeof(bool), typeof(CoolWindow), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Dependency property for SysButtonMaximizeVisible
        /// </summary>
        public static readonly DependencyProperty SysButtonMaximizeVisibleProperty =
            DependencyProperty.Register("SysButtonMaximizeVisible", typeof(bool), typeof(CoolWindow), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets the visibility of the Always top button
        /// </summary>
        public bool SysButtonAlwaystopVisible
        {
            get { return (bool)GetValue(SysButtonAlwaystopVisibleProperty); }
            set { SetValue(SysButtonAlwaystopVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of the mimimize button
        /// </summary>
        public bool SysButtonMinimizeVisible
        {
            get { return (bool)GetValue(SysButtonMinimizeVisibleProperty); }
            set { SetValue(SysButtonMinimizeVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of the maximize button
        /// </summary>
        public bool SysButtonMaximizeVisible
        {
            get { return (bool)GetValue(SysButtonMaximizeVisibleProperty); }
            set { SetValue(SysButtonMaximizeVisibleProperty, value); }
        }

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
    }
}
