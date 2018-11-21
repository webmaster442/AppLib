using System.Windows;
using System.Windows.Controls;

namespace Webmaster442.Applib.Controls
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
