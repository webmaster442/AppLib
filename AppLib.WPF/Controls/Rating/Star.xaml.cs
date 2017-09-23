using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Interaction logic for Star.xaml
    /// </summary>
    public partial class Star : UserControl
    {
        /// <summary>
        /// Star on color dependency property.
        /// </summary>
        public static readonly DependencyProperty OnColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(Star), new FrameworkPropertyMetadata(Brushes.Yellow, new PropertyChangedCallback(OnStarOnColorChanged)));

        /// <summary>
        /// Star off color dependency property.
        /// </summary>
        public static readonly DependencyProperty OffColorProperty =
            DependencyProperty.Register("OffColor", typeof(Brush), typeof(Star), new FrameworkPropertyMetadata(Brushes.White, new PropertyChangedCallback(OnStarOffColorChanged), new CoerceValueCallback(CoerceOnStarOffColor)));

        /// <summary>
        /// Star state dependency property.
        /// </summary>
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(StarState), typeof(Star), new FrameworkPropertyMetadata(StarState.Off, new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// Creates a new instance of Star
        /// </summary>
        public Star()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Notifies when <see cref="State"/> has been changed.
        /// </summary>
        public event EventHandler<StarStateChangedEventArgs> StateChanged;

        /// <summary>
        /// Gets or sets the color of star fill when <see cref="State"/> is <see cref="StarState.On"/>.
        /// </summary>
        public Brush OnColor
        {
            get { return (Brush)GetValue(OnColorProperty); }
            set { SetValue(OnColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of star fill when <see cref="State"/> is <see cref="StarState.Off"/>.
        /// </summary>
        public Brush OffColor
        {
            get { return (Brush)GetValue(OffColorProperty); }
            set { SetValue(OffColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the state of the star.
        /// </summary>
        public StarState State
        {
            get { return (StarState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        /// <summary>
        /// Gets whether or not <see cref="State"/> is <see cref="StarState.On"/>.
        /// </summary>
        public bool IsOn
        {
            get { return (State == StarState.On); }
        }

        /// <summary>
        /// Gets or sets the star fill brush.
        /// </summary>
        private Brush StarFill
        {
            get { return pathFill.Fill; }
            set { pathFill.Fill = value; }
        }

        private void OnGridMouseEnter(object sender, MouseEventArgs e)
        {
            //// if star is not on, set fill to on color
            if (!IsOn)
                StarFill = OnColor;
        }

        private void OnGridMouseLeave(object sender, MouseEventArgs e)
        {
            //// if star is not on, set fill to off color
            if (!IsOn)
                StarFill = OffColor;
        }

        private void OnGridMouseUp(object sender, MouseButtonEventArgs e)
        {
            //// change state if left mouse button was released
            if (e.ChangedButton == MouseButton.Left)
                State = (State == StarState.On) ? StarState.Off : StarState.On;
        }

        private void OnStateChanged(StarStateChangedEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;
            if (star == null) return;

            StarState newState = (StarState)e.NewValue;
            // set the fill based on the state
            star.StarFill = (newState == StarState.On) ? star.OnColor : star.OffColor;
            // raise state change event
            star.OnStateChanged(new StarStateChangedEventArgs(star.State));
        }

        private static void OnStarOnColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;
            // if star is on, set fill color to on color
            if (star != null && star.IsOn)
                star.StarFill = star.OnColor;
        }

        private static void OnStarOffColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;
            // if star is off, set fill color to off color
            if (star != null && !star.IsOn)
                star.StarFill = star.OffColor;
        }

        private static object CoerceOnStarOffColor(DependencyObject obj, object value)
        {
            Star star = obj as Star;
            if (star != null)
            {
                Brush brush = (Brush)value;
                // if brush is solid brush and color is transparent,
                // replace it with white brush
                if (brush is SolidColorBrush)
                {
                    SolidColorBrush solid = (SolidColorBrush)brush;
                    if (solid.Color == Colors.Transparent)
                        return Brushes.White;
                }
                return brush;
            }
            return Brushes.White;
        }
    }

    /// <summary>
    /// Event args for star state change.
    /// </summary>
    public class StarStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the state before change.
        /// </summary>
        public StarState OldState { get; private set; }

        /// <summary>
        /// GEts the state after change. Same as current state.
        /// </summary>
        public StarState NewState { get; private set; }

        /// <summary>
        /// Creates a new instance of StarStateChangedEventArgs
        /// </summary>
        /// <param name="current">Current star state</param>
        public StarStateChangedEventArgs(StarState current)
        {
            if (current == StarState.On)
            {
                OldState = StarState.Off;
                NewState = StarState.On;
            }
            else
            {
                OldState = StarState.On;
                NewState = StarState.Off;
            }
        }
    }

    /// <summary>
    /// Star states
    /// </summary>
    public enum StarState
    {
        /// <summary>
        /// Star is off
        /// </summary>
        Off = 0,

        /// <summary>
        /// Star is on
        /// </summary>
        On = 1
    }
}
