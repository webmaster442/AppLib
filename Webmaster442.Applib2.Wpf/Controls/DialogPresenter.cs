using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// A Dialog presenter
    /// </summary>
    public class DialogPresenter: Control
    {
        private Button _BtnOk;
        private Button _BtnCancel;

        static DialogPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogPresenter), new FrameworkPropertyMetadata(typeof(DialogPresenter)));
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            _BtnOk = GetTemplateChild("PART_OK") as Button;
            if (_BtnOk != null)
                _BtnOk.Click += _BtnOk_Click;

            _BtnCancel = GetTemplateChild("PART_CANCEL") as Button;
            if (_BtnCancel != null)
                _BtnCancel.Click += _BtnCancel_Click;

            base.OnApplyTemplate();
        }

        private void _BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (CancelCommand != null)
            {
                CancelCommand.Execute(null);
            }
            Visibility = Visibility.Collapsed;
        }

        private void _BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (OkCommand != null)
            {
                OkCommand.Execute(null);
            }
            Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Dialog content
        /// </summary>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Dependency property for content
        /// </summary>
        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DialogPresenter), new PropertyMetadata(null));

        /// <summary>
        /// Command to execute when Ok Clicked
        /// </summary>
        public ICommand OkCommand
        {
            get { return (ICommand)GetValue(OkCommandProperty); }
            set { SetValue(OkCommandProperty, value); }
        }

        /// <summary>
        /// Dependency property for OkCommand
        /// </summary>
        // Using a DependencyProperty as the backing store for OkCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OkCommandProperty =
            DependencyProperty.Register("OkCommand", typeof(ICommand), typeof(DialogPresenter), new PropertyMetadata(null));

        /// <summary>
        /// Command to execute when Cancel clicked
        /// </summary>
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }


        /// <summary>
        /// Dependency property for CancelCommand
        /// </summary>
        // Using a DependencyProperty as the backing store for CancelCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(DialogPresenter), new PropertyMetadata(null));

        /// <summary>
        /// Dialog Title
        /// </summary>
        public object DialogTitle
        {
            get { return (object)GetValue(DialogTitleProperty); }
            set { SetValue(DialogTitleProperty, value); }
        }

        /// <summary>
        /// Dependency Property for Dialog title
        /// </summary>
        // Using a DependencyProperty as the backing store for DialogTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DialogTitleProperty =
            DependencyProperty.Register("DialogTitle", typeof(object), typeof(DialogPresenter), new PropertyMetadata(null));

    }
}
