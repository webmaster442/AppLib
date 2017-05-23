using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppLib.WPF.Shaders.Transition
{
    internal static class TransitionHelper
    {
        public const string TransitionBandedSwirlEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionBandedSwirlEffect.ps";
        public const string TransitionBlindsEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionBlindsEffect.ps";
        public const string TransitionBloodEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionBloodEffect.ps";
        public const string TransitionCircleRevealEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionCircleRevealEffect.ps";
        public const string TransitionCircleStretchEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionCircleStretchEffect.ps";
        public const string TransitionColorizeEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionColorizeEffect.ps";
        public const string TransitionDropEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionDropEffect.ps";
        public const string TransitionFadeEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionFadeEffect.ps";
        public const string TransitionFoldEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionFoldEffect.ps";
        public const string TransitionLeastBrightEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionLeastBrightEffect.ps";
        public const string TransitionMostBrightEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionMostBrightEffect.ps";
        public const string TransitionPixelateEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionPixelateEffect.ps";
        public const string TransitionPixelateOutEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionPixelateOutEffect.ps";
        public const string TransitionRadialBlurEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionRadialBlurEffect.ps";
        public const string TransitionRadialWiggleEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionRadialWiggleEffect.ps";
        public const string TransitionRandomCircleRevealEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionRandomCircleRevealEffect.ps";
        public const string TransitionRippleEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionRippleEffect.ps";
        public const string TransitionSaturateEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionSaturateEffect.ps";
        public const string TransitionSlideInEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionSlideInEffect.ps";
        public const string TransitionSwirlEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionSwirlEffect.ps";
        public const string TransitionWaterEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionWaterEffect.ps";
        public const string TransitionWaveEffect = "pack://application:,,,/AppLib.WPF;component/Resources/TransitionWaveEffect.ps";

        public static Brush GetRandomCloud(Random r)
        {
            string basepath = string.Format("pack://application:,,,/AppLib.WPF;component/Resources/tex0{0}.png", r.Next(1, 5));
            var bi = new BitmapImage(new Uri(basepath));
            return new ImageBrush(bi);
        }
    }
}
