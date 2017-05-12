using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppLib.WPF.Shaders.Transition
{
    internal static class TransitionHelper
    {
        public const string TransitionBandedSwirlEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionBandedSwirlEffect.ps";
        public const string TransitionBlindsEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionBlindsEffect.ps";
        public const string TransitionBloodEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionBloodEffect.ps";
        public const string TransitionCircleRevealEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionCircleRevealEffect.ps";
        public const string TransitionCircleStretchEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionCircleStretchEffect.ps";
        public const string TransitionColorizeEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionColorizeEffect.ps";
        public const string TransitionDropEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionDropEffect.ps";
        public const string TransitionFadeEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionFadeEffect.ps";
        public const string TransitionFoldEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionFoldEffect.ps";
        public const string TransitionLeastBrightEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionLeastBrightEffect.ps";
        public const string TransitionMostBrightEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionMostBrightEffect.ps";
        public const string TransitionPixelateEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionPixelateEffect.ps";
        public const string TransitionPixelateOutEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionPixelateOutEffect.ps";
        public const string TransitionRadialBlurEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionRadialBlurEffect.ps";
        public const string TransitionRadialWiggleEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionRadialWiggleEffect.ps";
        public const string TransitionRandomCircleRevealEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionRandomCircleRevealEffect.ps";
        public const string TransitionRippleEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionRippleEffect.ps";
        public const string TransitionSaturateEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionSaturateEffect.ps";
        public const string TransitionSlideInEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionSlideInEffect.ps";
        public const string TransitionSwirlEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionSwirlEffect.ps";
        public const string TransitionWaterEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionWaterEffect.ps";
        public const string TransitionWaveEffect = "pack://application:,,,/AppLib.WPF;component/Resources/Transition/;TransitionWaveEffect.ps";

        public static Brush GetRandomCloud(Random r)
        {
            string basepath = string.Format("pack://application:,,,/AppLib.WPF;component/Resources/tex0{0}.png", r.Next(1, 5));
            var bi = new BitmapImage(new Uri(basepath));
            return new ImageBrush(bi);
        }
    }
}
