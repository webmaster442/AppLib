using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace AppLib.WPF.Shaders.Transition
{
    /// <summary>
    /// Water transition
    /// </summary>
    public class TransitionWater: Transition
    {

        /// <summary>
        /// Dependency property for Texture map
        /// </summary>
        public static readonly DependencyProperty TextureMapProperty = 
            ShaderEffect.RegisterPixelShaderSamplerProperty("TextureMap", typeof(TransitionWater), 2);

        /// <summary>
        /// Dependency property for random seed
        /// </summary>
        public static readonly DependencyProperty RandomSeedProperty = 
            DependencyProperty.Register("RandomSeed", typeof(double), typeof(TransitionWater), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));


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
        /// creates a new instance of Water transition
        /// </summary>
        public TransitionWater() : base(new Uri(TransitionHelper.TransitionWaterEffect))
        {
            Random r = new Random();
            RandomSeed = r.Next(-1, 0) + r.NextDouble();
            TextureMap = TransitionHelper.GetRandomCloud(r);
        }
    }
}
