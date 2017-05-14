using System;
using System.Windows;

namespace AppLib.WPF.Shaders.Transition
{
    /// <summary>
    /// Swirl transition
    /// </summary>
    public class TransitionSwirl: Transition
    {
        /// <summary>
        /// Dependency property for twist ammount
        /// </summary>
        public static readonly DependencyProperty TwistAmountProperty =
            DependencyProperty.Register("TwistAmount", typeof(double), typeof(TransitionSwirl), new UIPropertyMetadata(((double)(30D)), PixelShaderConstantCallback(1)));

        /// <summary>
        /// Twist amount
        /// </summary>
        public double TwistAmount
        {
            get{ return ((double)(GetValue(TwistAmountProperty))); }
            set{ SetValue(TwistAmountProperty, value); }
        }

        /// <summary>
        /// Creates a new instance of Swirl transition
        /// </summary>
        public TransitionSwirl() : base(new Uri(TransitionHelper.TransitionSwirlEffect))
        {
            Random r = new Random();
            TwistAmount = r.Next(-70, 69) + r.NextDouble();
        }
    }
}
