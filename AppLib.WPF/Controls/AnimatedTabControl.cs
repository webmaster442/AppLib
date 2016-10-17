using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A tab control with pixel shader transition effects
    /// </summary>
    public class AnimatedTabControl: TabControl
    {
        static AnimatedTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedTabControl), new FrameworkPropertyMetadata(typeof(AnimatedTabControl)));
        }
    }
}
