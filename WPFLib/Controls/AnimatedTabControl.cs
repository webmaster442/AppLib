using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFLib.Controls
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
