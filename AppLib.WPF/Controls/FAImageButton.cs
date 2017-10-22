using AppLib.WPF.Controls.ImageControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Font Awesome ImageButton Control
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
        /// Dependency property for Icon foreground
        /// </summary>
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register("IconForeground", typeof(Brush), typeof(FaImageButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Image placement property
        /// </summary>
        public static readonly DependencyProperty ImagePlacementProperty =
            DependencyProperty.Register("ImagePlacement", typeof(ImagePlacement), typeof(FaImageButton), new PropertyMetadata(ImagePlacement.Left));

        /// <summary>
        /// Content margin property
        /// </summary>
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(FaImageButton), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// Content Margin
        /// </summary>
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon size
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        /// Icon foreground
        /// </summary>
        public Brush IconForeground
        {
            get { return (Brush)GetValue(IconForegroundProperty); } 
            set { SetValue(IconForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or set the icon
        /// </summary>
        public FaIcons Icon
        {
            get { return (FaIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        /// <summary>
        /// Image placement
        /// </summary>
        public ImagePlacement ImagePlacement
        {
            get { return (ImagePlacement)GetValue(ImagePlacementProperty); }
            set { SetValue(ImagePlacementProperty, value); }
        }
    }
}
