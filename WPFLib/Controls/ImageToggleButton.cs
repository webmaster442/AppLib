using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFLib.Controls
{
    /// <summary>
    /// ImageButton Control
    /// </summary>
    public class ImageToggleButton : ToggleButton
    {
        static ImageToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageToggleButton), new FrameworkPropertyMetadata(typeof(ImageToggleButton)));
        }

        /// <summary>
        /// ImageOn property.
        /// </summary>
        public static readonly DependencyProperty ImageOnProperty = 
            DependencyProperty.Register("ImageOn", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        /// <summary>
        /// ImageOff property.
        /// </summary>
        public static readonly DependencyProperty ImageOffProperty =
            DependencyProperty.Register("ImageOff", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

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
        /// Gets or sets the control image when checked
        /// </summary>
        public ImageSource ImageOn
        {
            get { return (ImageSource)GetValue(ImageOnProperty); }
            set { SetValue(ImageOnProperty, value); }
        }

        /// <summary>
        /// Gets or sets the control image when unchecked
        /// </summary>
        public ImageSource ImageOff
        {
            get { return (ImageSource)GetValue(ImageOffProperty); }
            set { SetValue(ImageOffProperty, value); }
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
