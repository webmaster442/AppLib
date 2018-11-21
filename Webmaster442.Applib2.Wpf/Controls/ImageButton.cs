using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// Image Placement
    /// </summary>
    public enum ImagePlacement
    {
        /// <summary>
        /// Left
        /// </summary>
        Left,
        /// <summary>
        /// Right
        /// </summary>
        Right
    }

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
        /// Image placement property
        /// </summary>
        public static readonly DependencyProperty ImagePlacementProperty =
            DependencyProperty.Register("ImagePlacement", typeof(ImagePlacement), typeof(ImageButton), new PropertyMetadata(ImagePlacement.Left));


        /// <summary>
        /// Content margin property
        /// </summary>
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// Content Margin
        /// </summary>
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets the control image
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// Image placement
        /// </summary>
        public ImagePlacement ImagePlacement
        {
            get { return (ImagePlacement)GetValue(ImagePlacementProperty); }
            set { SetValue(ImagePlacementProperty, value); }
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
}
