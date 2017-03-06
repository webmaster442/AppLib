using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// Interaction logic for SwitchableContentControl.xaml
    /// </summary>
    public partial class SwitchableContentControl : UserControl
    {
        public SwitchableContentControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SwitcherTitleProperty =
            DependencyProperty.Register("SwitcherTitle", typeof(string), typeof(SwitchableContentControl), new PropertyMetadata("Open views"));

        public static readonly DependencyProperty SwitcherTitleForegroundProperty =
            DependencyProperty.Register("SwitcherTitleForeground", typeof(Brush), typeof(SwitchableContentControl), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static readonly DependencyProperty SwitcherBoderBrushProperty =
            DependencyProperty.Register("SwitcherBorderBrush", typeof(Brush), typeof(SwitchableContentControl), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static readonly DependencyProperty SwitcherBorderThicknessProperty =
            DependencyProperty.Register("SwitcherBorderThickness", typeof(Thickness), typeof(SwitchableContentControl), new PropertyMetadata(new Thickness(2)));

        public static readonly DependencyProperty SwitcherMarginProperty =
            DependencyProperty.Register("SwitcherMargin", typeof(Thickness), typeof(SwitchableContentControl), new PropertyMetadata(new Thickness(20)));

        public string SwitcherTitle
        {
            get { return (string)GetValue(SwitcherTitleProperty); }
            set { SetValue(SwitcherTitleProperty, value); }
        }

        public Brush SwitcherTitleForeground
        {
            get { return (Brush)GetValue(SwitcherTitleForegroundProperty); }
            set { SetValue(SwitcherTitleForegroundProperty, value); }
        }

        public Brush SwitcherBoderBrush
        {
            get { return (Brush)GetValue(SwitcherBoderBrushProperty); }
            set { SetValue(SwitcherBoderBrushProperty, value); }
        }

        public Thickness SwitcherBorderThickness
        {
            get { return (Thickness)GetValue(SwitcherBorderThicknessProperty); }
            set { SetValue(SwitcherBorderThicknessProperty, value); }
        }

        public Thickness SwitcherMargin
        {
            get { return (Thickness)GetValue(SwitcherMarginProperty); }
            set { SetValue(SwitcherMarginProperty, value); }
        }
    }
}
