using AppLib.WPF.Controls.FontAwesome;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// ImageButton Control
    /// </summary>
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        /// <summary>
        /// Image property
        /// </summary>
        public static readonly DependencyProperty ImageProperty = 
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        /// <summary>
        /// Image height property
        /// </summary>
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ImageButton), new PropertyMetadata(Double.NaN));

        /// <summary>
        /// Image Width property
        /// </summary>
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(ImageButton), new PropertyMetadata(Double.NaN));

        /// <summary>
        /// Gets or sets the control image
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the image heiht
        /// </summary>
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the image width
        /// </summary>
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

    }

    /// <summary>
    /// ImageButton Control
    /// </summary>
    public class FaImageButton : Button
    {
        static FaImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FaImageButton), new FrameworkPropertyMetadata(typeof(FaImageButton)));
        }

        /// <summary>
        /// Image property
        /// </summary>
        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register("Icon", typeof(FaIcons), typeof(FaImageButton), new PropertyMetadata(FaIcons.fa_none));

        /// <summary>
        /// Image Size property
        /// </summary>
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(FaImageButton), new PropertyMetadata(Double.NaN));

        /// <summary>
        /// Gets or sets the icon size
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        /// Gets or set the icon
        /// </summary>
        public FaIcons Icon
        {
            get { return (FaIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }

    /// <summary>
    /// Visibility converter class for ImageButton internals
    /// </summary>
    internal class VisibilityConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
