using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFLib.Extensions
{
    /// <summary>
    /// FrameWorkElement class extension methoods
    /// </summary>
    public static class FrameWorkElementExtensions
    {
        /// <summary>
        /// Renders a FrameworkElement into a RenderTargetBitmap, which is then bindable
        /// </summary>
        /// <param name="element">Element to Render</param>
        /// <returns>FrameWorkElement rendered to a RenderTargetBitmap</returns>
        public static ImageSource Render(this FrameworkElement element)
        {
            var rtb = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight,
                                             96, 96, PixelFormats.Pbgra32);
            rtb.Render(element);
            return rtb;
        }
    }
}
