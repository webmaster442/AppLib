using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AnalogClock.xaml
    /// </summary>
    public partial class AnalogClock : UserControl
    {
        private DispatcherTimer _timer;

        /// <summary>
        /// Creates a new instance of Analog Clock
        /// </summary>
        public AnalogClock()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += _timer_Tick;
            _timer.IsEnabled = true;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            MinuteAngle.Angle = now.Minute * 6;
            HourAngle.Angle = now.Hour * 30;
            SecondAngle.Angle = (now.Millisecond / 1000.0d) * 6;
            DateString.Text = string.Format("{0:0000} - {1:00} - {2:00}, {3}", now.Year, now.Month, now.Day, now.DayOfWeek);
            TimeString.Text = string.Format("{0:00}:{1:00}:{2:00}", now.Hour, now.Minute, now.Second);
        }

        /// <summary>
        /// Dependency property for HourHandBrush
        /// </summary>
        public static readonly DependencyProperty HourHandBrushProperty =
            DependencyProperty.Register("HourHandBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        /// <summary>
        /// Dependency property for MinuteHandBrush
        /// </summary>
        public static readonly DependencyProperty MinuteHandBrushProperty =
            DependencyProperty.Register("MinuteHandBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Orange)));

        /// <summary>
        /// Dependency property for SecondHandBrush
        /// </summary>
        public static readonly DependencyProperty SecondHandBrushProperty =
            DependencyProperty.Register("SecondHandBrush", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        /// <summary>
        /// Dependency property for the 1st color of the clock face
        /// </summary>
        public static readonly DependencyProperty ClockFaceBrush1Property =
            DependencyProperty.Register("ClockFaceBrush1", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Dependency property for the 2nd color of the clock face
        /// </summary>
        public static readonly DependencyProperty ClockFaceBrush2Property =
            DependencyProperty.Register("ClockFaceBrush2", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Dependency property for the 3rd color of the clock face
        /// </summary>
        public static readonly DependencyProperty ClockFaceBrush3Property =
            DependencyProperty.Register("ClockFaceBrush3", typeof(Brush), typeof(AnalogClock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        /// <summary>
        /// Gets or sets the hour hand brush
        /// </summary>
        public Brush HourHandBrush
        {
            get { return (Brush)GetValue(HourHandBrushProperty); }
            set { SetValue(HourHandBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minute hand brush
        /// </summary>
        public Brush MinuteHandBrush
        {
            get { return (Brush)GetValue(MinuteHandBrushProperty); }
            set { SetValue(MinuteHandBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the second hand brush
        /// </summary>
        public Brush SecondHandBrush
        {
            get { return (Brush)GetValue(SecondHandBrushProperty); }
            set { SetValue(SecondHandBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the 1st brush of the clock face
        /// </summary>
        public Brush ClockFaceBrush1
        {
            get { return (Brush)GetValue(ClockFaceBrush1Property); }
            set { SetValue(ClockFaceBrush1Property, value); }
        }

        /// <summary>
        /// Gets or sets the 2nd brush of the clock face
        /// </summary>
        public Brush ClockFaceBrush2
        {
            get { return (Brush)GetValue(ClockFaceBrush2Property); }
            set { SetValue(ClockFaceBrush2Property, value); }
        }

        /// <summary>
        /// Gets or sets the 3rd brush of the clock face
        /// </summary>
        public Brush ClockFaceBrush3
        {
            get { return (Brush)GetValue(ClockFaceBrush3Property); }
            set { SetValue(ClockFaceBrush3Property, value); }
        }
    }
}
