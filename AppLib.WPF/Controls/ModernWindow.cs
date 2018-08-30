using AppLib.Common.PInvoke;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace AppLib.WPF.Controls
{

    /// <summary>
    /// A Window for Windows 10 &amp; 8
    /// </summary>
    public class ModernWindow : Window
    {
        private HwndSource _hwndSource;
        private Button _minimize;
        private Button _close;
        private ToggleButton _restore;
        private ToggleButton _transparency;
        private ToggleButton _allwaystop;

        /// <summary>
        /// Dependency property for WindowMinimizeIsVisible
        /// </summary>
        public static readonly DependencyProperty WindowMinimizeIsVisibleProperty
            = DependencyProperty.Register("WindowMinimizeIsVisible", typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property for WindowRestoreIsVisible
        /// </summary>
        public static readonly DependencyProperty WindowRestoreIsVisibleProperty
            = DependencyProperty.Register("WindowRestoreIsVisible", typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property for WindowOnTopIsVisible
        /// </summary>
        public static readonly DependencyProperty WindowOnTopIsVisibleProperty
            = DependencyProperty.Register("WindowOnTopIsVisible", typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property for WindowTransparentIsVisible
        /// </summary>
        public static readonly DependencyProperty WindowTransparentIsVisibleProperty
            = DependencyProperty.Register("WindowTransparentIsVisible", typeof(bool), typeof(ModernWindow), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property for WindowTransparencyLevel
        /// </summary>
        public static readonly DependencyProperty WindowTransparencyLevelProperty
            = DependencyProperty.Register("WindowTransparencyLevel", typeof(double), typeof(ModernWindow), new PropertyMetadata(0.85d));

        /// <summary>
        /// Dependency property for WindowIsTransparent
        /// </summary>
        public static readonly DependencyProperty WindowIsTransparentProperty
            = DependencyProperty.Register("WindowIsTransparent", typeof(bool), typeof(ModernWindow), new PropertyMetadata(false, WindowIsTransparentChanged));

        /// <summary>
        /// Window is transparent toggle
        /// </summary>
        public bool WindowIsTransparent
        {
            get { return (bool)GetValue(WindowIsTransparentProperty); }
            set { SetValue(WindowIsTransparentProperty, value); }
        }

        private static void WindowIsTransparentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernWindow sender = d as ModernWindow;

            if (sender.WindowIsTransparent)
                sender.Opacity = sender.WindowTransparencyLevel;
            else sender.Opacity = 1.0d;
        }

        /// <summary>
        /// Dependency property for DwmBorderBrush
        /// </summary>
        public static readonly DependencyProperty DwmBorderBrushProperty
            = DependencyProperty.Register("DwmBorderBrush", typeof(Brush), typeof(ModernWindow), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// Gets or sets the visibility of the  Minimize button
        /// </summary>
        public bool WindowMinimizeIsVisible
        {
            get { return (bool)GetValue(WindowMinimizeIsVisibleProperty); }
            set { SetValue(WindowMinimizeIsVisibleProperty, value); }
        }


        /// <summary>
        /// Gets or sets the visibility of the Restore button
        /// </summary>
        public bool WindowRestoreIsVisible
        {
            get { return (bool)GetValue(WindowRestoreIsVisibleProperty); }
            set { SetValue(WindowRestoreIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of the Allways on top togle button
        /// </summary>
        public bool WindowOnTopIsVisible
        {
            get { return (bool)GetValue(WindowOnTopIsVisibleProperty); }
            set { SetValue(WindowOnTopIsVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the visibility of the Window transparency togle button
        /// </summary>
        public bool WindowTransparentIsVisible
        {
            get { return (bool)GetValue(WindowTransparentIsVisibleProperty); }
            set { SetValue(WindowTransparentIsVisibleProperty, value); }
        }


        /// <summary>
        /// Window transparency level
        /// </summary>
        public double WindowTransparencyLevel
        {
            get { return (double)GetValue(WindowTransparencyLevelProperty); }
            set { SetValue(WindowTransparencyLevelProperty, value); }
        }

        /// <summary>
        /// DWM border brush
        /// </summary>
        public Brush DwmBorderBrush
        {
            get { return (Brush)GetValue(DwmBorderBrushProperty); }
            set { SetValue(DwmBorderBrushProperty, value); }
        }

        static ModernWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModernWindow),
                new FrameworkPropertyMetadata(typeof(ModernWindow)));
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
        /// OnSource Initialized hook
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
            _hwndSource.AddHook(WndProc);
            base.OnSourceInitialized(e);
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

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = User32.MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
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



        private void SetColor()
        {
            Dispatcher.Invoke(() =>
            {
                var color = GetWindowColorizationColor(true);
                DwmBorderBrush = new SolidColorBrush(color);
            });
        }


        /// <summary>
        /// Applies the template
        /// </summary>
        public override void OnApplyTemplate()
        {
            _minimize = GetTemplateChild("PART_MINIMIZE") as Button;
            _restore = GetTemplateChild("PART_RESTORE") as ToggleButton;
            _close = GetTemplateChild("PART_CLOSE") as Button;
            _transparency = GetTemplateChild("PART_TRANSPARENT") as ToggleButton;
            _allwaystop = GetTemplateChild("PART_ALWAYSTOP") as ToggleButton;

            if (_minimize != null)
                _minimize.Click += HandleMenuClicks;

            if (_restore != null)
                _restore.Click += HandleToggleClicks;

            if (_allwaystop != null)
                _allwaystop.Click += HandleToggleClicks;

            if (_transparency != null)
                _transparency.Click += HandleToggleClicks;

            if (_close != null)
                _close.Click += HandleMenuClicks;

            SetColor();
            base.OnApplyTemplate();
        }

        private void HandleToggleClicks(object sender, RoutedEventArgs e)
        {
            ToggleButton s = sender as ToggleButton;

            switch (s.Name)
            {
                case "PART_RESTORE":
                    WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
                    break;
                case "PART_ALWAYSTOP":
                    Topmost = !Topmost;
                    break;
                case "PART_TRANSPARENT":
                    WindowIsTransparent = !WindowIsTransparent;
                    break;
            }
        }

        private void HandleMenuClicks(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;

            switch (s.Name)
            {
                case "PART_CLOSE":
                    Close();
                    break;
                case "PART_MINIMIZE":
                    WindowState = WindowState.Minimized;
                    break;
            }

        }
    }
}
