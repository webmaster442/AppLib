using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace AppLib.WPF.Shaders.Transition
{
    /// <summary>
    /// Drop transition
    /// </summary>
    public class TransitionDrop : Transition
    {
        /// <summary>
        /// Dependency property for Texture map
        /// </summary>
        public static readonly DependencyProperty TextureMapProperty = 
            ShaderEffect.RegisterPixelShaderSamplerProperty("TextureMap", typeof(TransitionDrop), 2);

        /// <summary>
        /// Dependency property for random seed
        /// </summary>
        public static readonly DependencyProperty RandomSeedProperty =
            DependencyProperty.Register("RandomSeed", typeof(double), typeof(TransitionDrop), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(1)));

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
        /// Creates a new instance of drop transition
        /// </summary>
        public TransitionDrop() : base(new Uri(TransitionHelper.TransitionDropEffect))
        {
            Random r = new Random();
            RandomSeed = r.Next(-1, 0) + r.NextDouble();
            TextureMap = TransitionHelper.GetRandomCloud(r);
        }
    }
}
