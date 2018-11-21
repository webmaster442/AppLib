using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// Displays the current time as text
    /// </summary>
    public class FuzzyTextClock : TextBlock
    {
        private DispatcherTimer _timer;

        /// <summary>
        /// Creates a new instance of FuzzyClockText
        /// </summary>
        public FuzzyTextClock()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += _timer_tick;
            _timer_tick(this, null); //force current time at startup!
            _timer.IsEnabled = true;
        }

        private void _timer_tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            Text = Time(dt.Hour, dt.Minute);
        }

        /// <summary>
        /// Converts a time to human radable text
        /// </summary>
        /// <param name="hr">hours</param>
        /// <param name="minute">minutes</param>
        /// <returns>The current time as text</returns>
        public static string Time(int hr, int minute)
        {
            string[] hours =
            {
                "one", "two", "three", "four", "five",
                "six", "seven", "eight", "nine", "ten",
                "eleven", "twelve"
            };

            string[] minutes =
            {
                "o-clock", "o-one", "o-two", "o-three", "o-four", "o-five",
                "o-six", "o-seven", "o-eight", "o-nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
                "seventeen", "eighteen", "nineteen", "twenty", "twenty-one",
                "twenty-two", "twenty-three", "twenty-four", "twenty-five",
                "twenty-six", "twenty-seven", "twenty-eight", "twenty-nine",
                "thirty", "thirty-one", "thirty-two", "thirty-three",
                "thirty-four", "thirty-five", "thirty-six", "thirty-seven",
                "thirty-seven", "thirty-one", "forty", "forty-one", "forty-two",
                "forty-three", "forty-four", "forty-five", "forty-six", "forty-seven",
                "forty-eight", "forty-nine", "fifty", "fifty-one", "fifty-two",
                "fifty-three", "fifty-four", "fifty-five", "fifty-six", "fifty-seven",
                "fifty-eight", "fifty-nine"
            };

            string part = null;
            if (hr > 0 && hr < 12) part = "morning";
            else if (hr > 12 && hr < 18) part = "afternoon";
            else if (hr > 18 && hr < 24) part = "evening";

            if (hr > 12) hr -= 12;

            return string.Format("It's {0} {1} in the {2}", hours[hr], minutes[minute], part);
        }
    }
}
