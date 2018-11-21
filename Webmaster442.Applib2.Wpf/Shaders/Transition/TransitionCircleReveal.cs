using System;
using System.Windows;

namespace Webmaster442.Applib.Shaders.Transition
{
    /// <summary>
    /// Circle reveal transition
    /// </summary>
    public class TransitionCircleReveal : Transition
    {
        /// <summary>
        /// Dependency property for FuzzyAmount
        /// </summary>
        public static readonly DependencyProperty FuzzyAmountProperty = 
            DependencyProperty.Register("FuzzyAmount", typeof(double), typeof(TransitionCircleReveal), new UIPropertyMetadata(((double)(0.01D)), PixelShaderConstantCallback(1)));


        /// <summary>The fuzziness factor. </summary>
        public double FuzzyAmount
        {
            get { return ((double)(GetValue(FuzzyAmountProperty))); }
            set { SetValue(FuzzyAmountProperty, value); }
        }

        /// <summary>
        /// Creates a new instance of circle reveal
        /// </summary>
        public TransitionCircleReveal() : base(new Uri(TransitionHelper.TransitionCircleRevealEffect))
        {
            Random r = new Random();
            FuzzyAmount = r.NextDouble();
        }
    }
}
