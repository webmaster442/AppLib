using System;
using System.Windows;

namespace Webmaster442.Applib.Shaders.Transition
{
    /// <summary>
    /// Banded swirl transition
    /// </summary>
    public class TransitionBandedSwirl: Transition
    {
        /// <summary>
        /// Dependency property for TwistAmount
        /// </summary>
        public static readonly DependencyProperty TwistAmountProperty = 
            DependencyProperty.Register("TwistAmount", typeof(double), typeof(TransitionBandedSwirl), new UIPropertyMetadata(((double)(1D)), PixelShaderConstantCallback(1)));

        /// <summary>
        /// Dependency property for Frequency
        /// </summary>
        public static readonly DependencyProperty FrequencyProperty = 
            DependencyProperty.Register("Frequency", typeof(double), typeof(TransitionBandedSwirl), new UIPropertyMetadata(((double)(0.2D)), PixelShaderConstantCallback(2)));


        /// <summary>The amount of twist for the Swirl. </summary>
        public double TwistAmount
        {
            get { return ((double)GetValue(TwistAmountProperty)); }
            set { SetValue(TwistAmountProperty, value); }
        }
        /// <summary>The amount of twist for the Swirl. </summary>
        public double Frequency
        {
            get { return ((double)(GetValue(FrequencyProperty))); }
            set { SetValue(FrequencyProperty, value); }
        }

        /// <summary>
        /// Creates a new Banded Swirl transition
        /// </summary>
        public TransitionBandedSwirl() : base(new Uri(TransitionHelper.TransitionBandedSwirlEffect))
        {
            Random r = new Random();
            TwistAmount = (r.NextDouble() + r.Next(0, 9));
            Frequency = r.NextDouble();
        }
    }
}
