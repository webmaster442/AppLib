using System;
using System.Windows;

namespace AppLib.WPF.Shaders.Transition
{
    /// <summary>
    /// Blinds transition effect
    /// </summary>
    public class TransitionBlinds: Transition
    {
        /// <summary>
        /// Dependency property for NumberOfBlinds
        /// </summary>
        public static readonly DependencyProperty NumberOfBlindsProperty = 
            DependencyProperty.Register("NumberOfBlinds", typeof(double), typeof(TransitionBlinds), new UIPropertyMetadata(((double)(5D)), PixelShaderConstantCallback(1)));

        /// <summary>The number of Blinds strips </summary>
        public double NumberOfBlinds
        {
            get { return ((double)(GetValue(NumberOfBlindsProperty))); }
            set { SetValue(NumberOfBlindsProperty, value); }
        }

        /// <summary>
        /// Creates a new instance of TransitionBlinds
        /// </summary>
        public TransitionBlinds() : base(new Uri(TransitionHelper.TransitionBlindsEffect))
        {
            Random r = new Random();
            NumberOfBlinds = r.Next(1, 30);
        }
    }
}
