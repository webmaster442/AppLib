using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Webmaster442.Applib.Shaders.Transition
{
    /// <summary>
    /// Radial wiggle transition
    /// </summary>
    public class TransitionRadialWiggle: Transition
    {
        /// <summary>
        /// Dependency property for random seed
        /// </summary>
        public static readonly DependencyProperty RandomSeedProperty =
            DependencyProperty.Register("RandomSeed", typeof(double), typeof(TransitionRadialWiggle), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));

        /// <summary>
        /// Dependency property for Texture map
        /// </summary>
        public static readonly DependencyProperty TextureMapProperty = 
            ShaderEffect.RegisterPixelShaderSamplerProperty("TextureMap", typeof(TransitionRadialWiggle), 2);

        /// <summary>
        /// Random seed
        /// </summary>
        public double RandomSeed
        {
            get { return ((double)(GetValue(RandomSeedProperty))); }
            set { SetValue(RandomSeedProperty, value); }
        }

        /// <summary>
        /// Texture map
        /// </summary>
        public Brush TextureMap
        {
            get { return ((Brush)(GetValue(TextureMapProperty))); }
            set { SetValue(TextureMapProperty, value); }
        }

        /// <summary>
        /// Creates a new instance of Radial wiggle transition
        /// </summary>
        public TransitionRadialWiggle() : base(new Uri(TransitionHelper.TransitionRadialWiggleEffect))
        {
            Random r = new Random();
            RandomSeed = r.Next(-1, 0) + r.NextDouble();
            TextureMap = TransitionHelper.GetRandomCloud(r);
        }
    }
}
