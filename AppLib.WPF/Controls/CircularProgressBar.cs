using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Controls
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
    }
}
