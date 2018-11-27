using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Webmaster442.Applib.Extensions
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
            var w = element.ActualWidth > 0 ? element.ActualWidth : element.Width;
            var h = element.ActualHeight > 0 ? element.ActualHeight : element.Height;

            if (element.ActualHeight == 0 || element.ActualWidth == 0)
            {
                element.Measure(new Size(w, h));
                element.Arrange(new Rect(0, 0, w, h));
            }

            var rtb = new RenderTargetBitmap((int)w, (int)h,
                                             96, 96, PixelFormats.Pbgra32);
            rtb.Render(element);
            return rtb;
        }

        /// <summary>
        /// Set focus to a named element of a container
        /// </summary>
        /// <param name="parent">parent element</param>
        /// <param name="name">name of control to set focus to</param>
        public static void SetFocus(this FrameworkElement parent, string name)
        {
            var ctrl = DependencyObjectExtensions.FindChild<FrameworkElement>(parent, name);
            if (ctrl != null && ctrl.Focusable)
                ctrl.Focus();
        }
    }
}
