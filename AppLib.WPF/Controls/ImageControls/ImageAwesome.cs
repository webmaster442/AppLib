using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppLib.WPF.Controls.ImageControls
{
    /// <summary>
    /// Stripped from FontAwesome.WPF
    /// </summary>
    public class ImageAwesome: Image
    {
        /// <summary>
        /// Dependency property for Icon
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FaIcons), typeof(ImageAwesome), new PropertyMetadata(FaIcons.fa_none, OnIconPropertyChanged));

        /// <summary>
        /// Dependency property for foreground
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(Brushes.Black, OnIconPropertyChanged));

        /// <summary>
        /// FontAwesome FontFamily.
        /// </summary>
        private static readonly FontFamily FontAwesomeFontFamily = 
            new FontFamily(new Uri("pack://application:,,,/AppLib.WPF;component/Resources/"), "./#FontAwesome");
        

        /// <summary>
        /// Typeface used to generate FontAwesome icon.
        /// </summary>
        private static readonly Typeface FontAwesomeTypeface = new Typeface(FontAwesomeFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

        /// <summary>
        /// Gets or sets the foreground brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        /// <summary>
        /// Gets or sets the FontAwesome icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public FaIcons Icon
        {
            get { return (FaIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var imageAwesome = d as ImageAwesome;

            if (imageAwesome == null) return;

            d.SetValue(SourceProperty, CreateImageSource(imageAwesome.Icon, imageAwesome.Foreground));
        }

        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static ImageSource CreateImageSource(FaIcons icon, Brush foregroundBrush)
        {
            var charIcon = char.ConvertFromUtf32((int)icon);

            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawText(
                    new FormattedText(charIcon,CultureInfo.InvariantCulture, FlowDirection.LeftToRight, FontAwesomeTypeface, 100, foregroundBrush)
                    { TextAlignment = TextAlignment.Center }, new Point(0, 0));
            }
            return new DrawingImage(visual.Drawing);
        }
    }
}
