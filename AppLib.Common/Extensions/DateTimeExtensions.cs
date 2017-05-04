using System;

namespace AppLib.Common.Extensions
{
    /// <summary>
    /// Date &amp; time extensions from:
    /// http://www.danylkoweb.com/Blog/10-extremely-useful-net-extension-methods-8J
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Figure out how old something (or someone) is.
        /// </summary>
        /// <param name="dateTime">A DateTime instance</param>
        /// <returns>the age of something</returns>
        public static int CalculateAge(this DateTime dateTime)
        {
            var age = DateTime.Now.Year - dateTime.Year;
            if (DateTime.Now < dateTime.AddYears(age)) age--;
            return age;
        }

        /// <summary>
        /// Converts a datetime instance to a human readable text
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToReadableTime(this DateTime value)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }


        /// <summary>
        /// Returns true, if the date specified by the date is a work day
        /// </summary>
        /// <param name="date">A DateTime instance</param>
        /// <returns>true, if the date specified by the date is a work day, otherwise false</returns>
        public static bool WorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns true, if the date specified by the date is on weekend
        /// </summary>
        /// <param name="date">A DateTime instance</param>
        /// <returns>true, if the date specified by the date is on weekend, otherwise false</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns the date of the next workday from the specified date
        /// </summary>
        /// <param name="date">A DateTime instance</param>
        /// <returns>The next workday as a DateTime</returns>
        public static DateTime NextWorkday(this DateTime date)
        {
            var nextDay = date;
            while (!nextDay.WorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        /// <summary>
        /// Returns the next occurance date of the specified day.
        /// </summary>
        /// <param name="current">A DateTime instance</param>
        /// <param name="dayOfWeek">Day of week to calculate</param>
        /// <returns>next occurance date of the specified day.</returns>
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }
    }
}
