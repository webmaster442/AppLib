using System.Windows;
using System.Windows.Controls;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// Represents a Circular style progress bar
    /// </summary>
    public class CircularProgressBar: ProgressBar
    {
        static CircularProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgressBar), new FrameworkPropertyMetadata(typeof(CircularProgressBar)));
        }

        /// <summary>
        /// Stroke thickness
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Dependency property for StrokeThickness
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CircularProgressBar), new PropertyMetadata(10.0d));
    }
}
