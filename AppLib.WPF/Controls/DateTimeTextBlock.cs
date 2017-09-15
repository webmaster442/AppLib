using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A textblock that allways shows the current time
    /// </summary>
    public class DateTimeTextBlock: TextBlock
    {

        /// <summary>
        /// Dependency  property for DateTimeFormat
        /// </summary>
        public static readonly DependencyProperty DateTimeFormatProperty = 
            DependencyProperty.Register("DateTimeFormat", typeof(string), typeof(DateTimeTextBlock), new PropertyMetadata(null));


        /// <summary>
        /// Gets or sets the DateTime format string
        /// </summary>
        public string DateTimeFormat
        {
            get { return (string)GetValue(DateTimeFormatProperty); }
            set { SetValue(DateTimeFormatProperty, value); }
        }

        private DispatcherTimer _timer;

        /// <summary>
        /// Creates a new instance of DateTimeTextBlock
        /// </summary>
        public DateTimeTextBlock(): base()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                if (!string.IsNullOrEmpty(DateTimeFormat))
                    this.Text = string.Format("{0:" + DateTimeFormat + "}", DateTime.Now);
                else
                    this.Text = DateTime.Now.ToShortTimeString();

                this.ToolTip = DateTime.Now.ToLongDateString();
            };
            _timer.IsEnabled = true;
        }
    }
}
