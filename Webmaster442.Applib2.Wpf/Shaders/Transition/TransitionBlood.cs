using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Webmaster442.Applib.Shaders.Transition
{
    /// <summary>
    /// Bood transion effect
    /// </summary>
    public class TransitionBlood: Transition
    {
        /// <summary>
        /// Dependency property for random seed
        /// </summary>
        public static readonly DependencyProperty RandomSeedProperty = 
            DependencyProperty.Register("RandomSeed", typeof(double), typeof(TransitionBlood), new UIPropertyMetadata(((double)(0.3D)), PixelShaderConstantCallback(1)));

        /// <summary>
        /// Dependency property for cloudinput
        /// </summary>
        public static readonly DependencyProperty CloudInputProperty = 
            ShaderEffect.RegisterPixelShaderSamplerProperty("CloudInput", typeof(TransitionBlood), 2);

        /// <summary>
        /// Random seed
        /// </summary>
        public double RandomSeed
        {
            get { return ((double)(GetValue(RandomSeedProperty))); }
            set { SetValue(RandomSeedProperty, value); }
        }

        /// <summary>Another texture passed to the shader to determine drip pattern.</summary>
        public Brush CloudInput
        {
            get { return ((Brush)(GetValue(CloudInputProperty))); }
            set { SetValue(CloudInputProperty, value); }
        }


        /// <summary>
        /// Creates a new instance of blood transition
        /// </summary>
        public TransitionBlood() : base(new Uri(TransitionHelper.TransitionBloodEffect))
        {
            Random r = new Random();
            RandomSeed = r.NextDouble();
            CloudInput = TransitionHelper.GetRandomCloud(r);
            UpdateShaderValue(CloudInputProperty);
        }
    }
}
