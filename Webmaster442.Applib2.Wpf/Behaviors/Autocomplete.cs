using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Webmaster442.Applib.Behaviors
{
    /// <summary>
    /// Defines an Autocomplete behaviour for TextBox
    /// </summary>
    public static class AutoCompleteBehavior
    {
        private static TextChangedEventHandler _onTextChanged = new TextChangedEventHandler(OnTextChanged);
        private static KeyEventHandler _onKeyDown = new KeyEventHandler(OnPreviewKeyDown);

        /// <summary>
        /// The collection to search for matches from.
        /// </summary>
        public static readonly DependencyProperty AutoCompleteItemsSource =
            DependencyProperty.RegisterAttached
            (
                "AutoCompleteItemsSource",
                typeof(IEnumerable<string>),
                typeof(AutoCompleteBehavior),
                new UIPropertyMetadata(null, OnAutoCompleteItemsSource)
            );
        /// <summary>
        /// Whether or not to ignore case when searching for matches.
        /// </summary>
        public static readonly DependencyProperty AutoCompletestringComparison =
            DependencyProperty.RegisterAttached
            (
                "AutoCompletestringComparison",
                typeof(StringComparison),
                typeof(AutoCompleteBehavior),
                new UIPropertyMetadata(StringComparison.Ordinal)
            );

        /// <summary>
		/// What string should indicate that we should start giving auto-completion suggestions.  For example: @
        /// If this is null or empty, auto-completion suggestions will begin at the beginning of the textbox's text.
		/// </summary>
		public static readonly DependencyProperty AutoCompleteIndicator =
            DependencyProperty.RegisterAttached
            (
                "AutoCompleteIndicator",
                typeof(string),
                typeof(AutoCompleteBehavior),
                new UIPropertyMetadata(string.Empty)
            );

        #region Items Source
        /// <summary>
        /// Getter for AutoCompleteItemsSource
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <returns>AutoCompleteItemsSource</returns>
        public static IEnumerable<string> GetAutoCompleteItemsSource(DependencyObject obj)
        {
            object objRtn = obj.GetValue(AutoCompleteItemsSource);
            if (objRtn is IEnumerable<string>)
                return (objRtn as IEnumerable<string>);

            return null;
        }

        /// <summary>
        /// Setter for AutoCompleteItemsSource
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <param name="value">Value to set</param>
        public static void SetAutoCompleteItemsSource(DependencyObject obj, IEnumerable<string> value)
        {
            obj.SetValue(AutoCompleteItemsSource, value);
        }

        private static void OnAutoCompleteItemsSource(object sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (sender == null)
                return;

            //If we're being removed, remove the callbacks
            //Remove our old handler, regardless of if we have a new one.
            tb.TextChanged -= _onTextChanged;
            tb.PreviewKeyDown -= _onKeyDown;
            if (e.NewValue != null)
            {
                //New source.  Add the callbacks
                tb.TextChanged += _onTextChanged;
                tb.PreviewKeyDown += _onKeyDown;
            }
        }
        #endregion

        #region string Comparison
        /// <summary>
        /// Getter for AutoCompletestringComparison
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <returns>value of AutoCompletestringComparison</returns>
        public static StringComparison GetAutoCompletestringComparison(DependencyObject obj)
        {
            return (StringComparison)obj.GetValue(AutoCompletestringComparison);
        }

        /// <summary>
        /// Setter for AutoCompletestringComparison
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <param name="value">Value to set</param>
        public static void SetAutoCompletestringComparison(DependencyObject obj, StringComparison value)
        {
            obj.SetValue(AutoCompletestringComparison, value);
        }
        #endregion

        #region Indicator
        /// <summary>
        /// Getter for AutoCompleteIndicator
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <returns>value of AutoCompleteIndicator</returns>
        public static string GetAutoCompleteIndicator(DependencyObject obj)
        {
            return (string)obj.GetValue(AutoCompleteIndicator);
        }

        /// <summary>
        /// Setter for AutoCompleteIndicator
        /// </summary>
        /// <param name="obj">an instance of AutoCompleteItemsSource</param>
        /// <param name="value">Value to set</param>
        public static void SetAutoCompleteIndicator(DependencyObject obj, string value)
        {
            obj.SetValue(AutoCompleteIndicator, value);
        }
        #endregion

        /// <summary>
        /// Used for moving the caret to the end of the suggested auto-completion text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            if (!(e.OriginalSource is TextBox tb))
                return;

            //If we pressed enter and if the selected text goes all the way to the end, move our caret position to the end
            if (tb.SelectionLength > 0 && (tb.SelectionStart + tb.SelectionLength == tb.Text.Length))
            {
                tb.SelectionStart = tb.CaretIndex = tb.Text.Length;
                tb.SelectionLength = 0;
            }
        }

        /// <summary>
        /// Search for auto-completion suggestions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if
            (
                (from change in e.Changes where change.RemovedLength > 0 select change).Any() &&
                (from change in e.Changes where change.AddedLength > 0 select change).Any() == false
            )
                return;

            TextBox tb = e.OriginalSource as TextBox;
            if (sender == null)
                return;

            IEnumerable<string> values = GetAutoCompleteItemsSource(tb);
            //No reason to search if we don't have any values.
            if (values == null)
                return;

            //No reason to search if there's nothing there.
            if (string.IsNullOrEmpty(tb.Text))
                return;

            string indicator = GetAutoCompleteIndicator(tb);
            int startIndex = 0; //Start from the beginning of the line.
            string matchingstring = tb.Text;
            //If we have a trigger string, make sure that it has been typed before
            //giving auto-completion suggestions.
            if (!string.IsNullOrEmpty(indicator))
            {
                startIndex = tb.Text.LastIndexOf(indicator);
                //If we haven't typed the trigger string, then don't do anything.
                if (startIndex == -1)
                    return;

                startIndex += indicator.Length;
                matchingstring = tb.Text.Substring(startIndex, (tb.Text.Length - startIndex));
            }

            //If we don't have anything after the trigger string, return.
            if (string.IsNullOrEmpty(matchingstring))
                return;

            int textLength = matchingstring.Length;

            StringComparison comparer = GetAutoCompletestringComparison(tb);
            //Do search and changes here.
            string match =
            (
                from
                    value
                in
                (
                    from subvalue
                    in values
                    where subvalue != null && subvalue.Length >= textLength
                    select subvalue
                )
                where value.Substring(0, textLength).Equals(matchingstring, comparer)
                select value.Substring(textLength, value.Length - textLength)/*Only select the last part of the suggestion*/
            ).FirstOrDefault();

            //Nothing.  Leave 'em alone
            if (string.IsNullOrEmpty(match))
                return;

            int matchStart = (startIndex + matchingstring.Length);
            tb.TextChanged -= _onTextChanged;
            tb.Text += match;
            tb.CaretIndex = matchStart;
            tb.SelectionStart = matchStart;
            tb.SelectionLength = (tb.Text.Length - startIndex);
            tb.TextChanged += _onTextChanged;
        }
    }
}
