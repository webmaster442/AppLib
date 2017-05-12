using System;

namespace AppLib.WPF.Shaders.Transition
{
    /// <summary>
    /// Wave transition
    /// </summary>
    public class TransitionWave : Transition
    {
        /// <summary>
        /// Creates a new instance of wave transition
        /// </summary>
        public TransitionWave() : base(new Uri(TransitionHelper.TransitionWaveEffect))
        {
        }
    }

    /// <summary>
    /// Saturate transition
    /// </summary>
    public class TransitonSaturate: Transition
    {
        /// <summary>
        /// Creates a new instance of saturate transition
        /// </summary>
        public TransitonSaturate() : base(new Uri(TransitionHelper.TransitionSaturateEffect))
        {
        }
    }

    /// <summary>
    /// Ripple transition
    /// </summary>
    public class TransitionRipple: Transition
    {
        /// <summary>
        /// Creates a new instance of ripple transition
        /// </summary>
        public TransitionRipple(): base(new Uri(TransitionHelper.TransitionRippleEffect))
        {
        }
    }

    /// <summary>
    /// Radial blur transition
    /// </summary>
    public class TransitionRadialBlur : Transition
    {
        /// <summary>
        /// Creates a new instance of Radial blur transition
        /// </summary>
        public TransitionRadialBlur() : base(new Uri(TransitionHelper.TransitionRadialBlurEffect))
        {
        }
    }

    /// <summary>
    /// Pixelate out transition
    /// </summary>
    public class TransitionPixelateOut: Transition
    {
        /// <summary>
        /// Creates a new instance of Pixelate out transition
        /// </summary>
        public TransitionPixelateOut() :base(new Uri(TransitionHelper.TransitionPixelateOutEffect))
        {
        }
    }

    /// <summary>
    /// Pixelate transition
    /// </summary>
    public class TransitionPixelate : Transition
    {
        /// <summary>
        /// Creates a new instance of Pixelate transition
        /// </summary>
        public TransitionPixelate() :base(new Uri(TransitionHelper.TransitionPixelateEffect))
        {
        }
    }

    /// <summary>
    /// Most bright transition
    /// </summary>
    public class TransitionMostBright: Transition
    {
        /// <summary>
        /// Creates a new instance of Most bright transition
        /// </summary>
        public TransitionMostBright() : base(new Uri(TransitionHelper.TransitionMostBrightEffect))
        {
        }
    }

    /// <summary>
    /// Least bright transition
    /// </summary>
    public class TransitionLeastBright : Transition
    {
        /// <summary>
        /// Creates a new instance of Least bright transition
        /// </summary>
        public TransitionLeastBright() : base(new Uri(TransitionHelper.TransitionLeastBrightEffect))
        {
        }
    }

    /// <summary>
    /// Fold transition
    /// </summary>
    public class TransitionFold : Transition
    {
        /// <summary>
        /// Creates a new instance of Fold transition
        /// </summary>
        public TransitionFold() : base(new Uri(TransitionHelper.TransitionFoldEffect))
        {
        }
    }

    /// <summary>
    /// Fade transition
    /// </summary>
    public class TransitionFade : Transition
    {
        /// <summary>
        /// Creates a new instance of Fade transition
        /// </summary>
        public TransitionFade() : base(new Uri(TransitionHelper.TransitionFadeEffect))
        {
        }
    }

    /// <summary>
    /// Circle Stretch transition
    /// </summary>
    public class TransitionCircleStretch : Transition
    {
        /// <summary>
        /// Creates a new instance of Circle Stretch transition
        /// </summary>
        public TransitionCircleStretch() : base(new Uri(TransitionHelper.TransitionFadeEffect))
        {
        }
    }
}
