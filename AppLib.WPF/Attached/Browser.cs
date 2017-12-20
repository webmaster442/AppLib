using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Attached
{
    /// <summary>
    /// Attached property to be able to bind html to WebBrowser
    /// </summary>
    public static class Browser
    {
        /// <summary>
        /// HTML property
        /// </summary>
        public static readonly DependencyProperty HtmlProperty = 
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(Browser), new FrameworkPropertyMetadata(OnHtmlChanged));

        /// <summary>
        /// Get HTML from web browser
        /// </summary>
        /// <param name="browser">WebBrowser instance</param>
        /// <returns>HTML text</returns>
        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser browser)
        {
            return (string)browser.GetValue(HtmlProperty);
        }

        /// <summary>
        /// Set Html text
        /// </summary>
        /// <param name="browser">WebBrowser instance</param>
        /// <param name="htmltext">HTML text</param>
        public static void SetHtml(WebBrowser browser, string htmltext)
        {
            browser.SetValue(HtmlProperty, htmltext);
        }

        static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WebBrowser wb)
                wb.NavigateToString(e.NewValue as string);
        }
    }
}
