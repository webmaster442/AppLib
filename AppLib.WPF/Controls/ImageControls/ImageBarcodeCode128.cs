using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppLib.WPF.Controls.ImageControls
{
    /// <summary>
    /// Code 128 barcode image
    /// </summary>
    public class ImageBarcodeCode128 : Image
    {
        /// <summary>
        /// Dependency property for foreground
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageBarcodeCode128), new PropertyMetadata(Brushes.Black, OnTextPropertyChanged));

        /// <summary>
        /// Dependency property for text
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ImageBarcodeCode128), new PropertyMetadata("", OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var image = d as ImageBarcodeCode128;
            if (image == null) return;
            d.SetValue(SourceProperty, CreateImageSource(image.Text, image.Foreground));
        }

        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        private static readonly FontFamily Code128Family =
            new FontFamily(new Uri("pack://application:,,,/AppLib.WPF;component/Resources/"), "./#Code128");


        /// <summary>
        /// Typeface used to generate FontAwesome icon.
        /// </summary>
        private static readonly Typeface Code128Typeface = new Typeface(Code128Family, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

        /// <summary>
        /// Gets or sets the foreground brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// Text to encode as barcode
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Creates an image source
        /// </summary>
        /// <param name="text">Text to encode</param>
        /// <param name="foregroundBrush">Foreground color</param>
        /// <returns>encoded text as image</returns>
        public static ImageSource CreateImageSource(string text, Brush foregroundBrush)
        {
            var bcode = BarcodeConverter128.StringToBarcode(text);
            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawText(
                    new FormattedText(bcode, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, Code128Typeface, 100, foregroundBrush)
                    { TextAlignment = TextAlignment.Center }, new Point(0, 0));
            }
            return new DrawingImage(visual.Drawing);
        }
    }
}
