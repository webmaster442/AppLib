using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Webmaster442.Applib.Shaders.Transition
{
    internal static class TransitionHelper
    {
        public const string TransitionBandedSwirlEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionBandedSwirlEffect.ps";
        public const string TransitionBlindsEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionBlindsEffect.ps";
        public const string TransitionBloodEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionBloodEffect.ps";
        public const string TransitionCircleRevealEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionCircleRevealEffect.ps";
        public const string TransitionCircleStretchEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionCircleStretchEffect.ps";
        public const string TransitionColorizeEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionColorizeEffect.ps";
        public const string TransitionDropEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionDropEffect.ps";
        public const string TransitionFadeEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionFadeEffect.ps";
        public const string TransitionFoldEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionFoldEffect.ps";
        public const string TransitionLeastBrightEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionLeastBrightEffect.ps";
        public const string TransitionMostBrightEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionMostBrightEffect.ps";
        public const string TransitionPixelateEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionPixelateEffect.ps";
        public const string TransitionPixelateOutEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionPixelateOutEffect.ps";
        public const string TransitionRadialBlurEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionRadialBlurEffect.ps";
        public const string TransitionRadialWiggleEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionRadialWiggleEffect.ps";
        public const string TransitionRandomCircleRevealEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionRandomCircleRevealEffect.ps";
        public const string TransitionRippleEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionRippleEffect.ps";
        public const string TransitionSaturateEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionSaturateEffect.ps";
        public const string TransitionSlideInEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionSlideInEffect.ps";
        public const string TransitionSwirlEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionSwirlEffect.ps";
        public const string TransitionWaterEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionWaterEffect.ps";
        public const string TransitionWaveEffect = "pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/TransitionWaveEffect.ps";

        public static Brush GetRandomCloud(Random r)
        {
            string basepath = string.Format("pack://application:,,,/Webmaster442.Applib2.Wpf;component/Resources/tex0{0}.png", r.Next(1, 5));
            var bi = new BitmapImage(new Uri(basepath));
            return new ImageBrush(bi);
        }
    }
}
