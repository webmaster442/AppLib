using System;

namespace AppLib.MVVM
{
    /// <summary>
    /// An object based delegate command
    /// </summary>
    public class DelegateCommand : DelegateCommand<object>
    {
        /// <summary>
        /// Creates a new instance of DelegateCommand
        /// </summary>
        /// <param name="action">Action to perform</param>
        internal DelegateCommand(Action<object> action) : 
            base(action)
        {
        }

        /// <summary>
        /// Creates a new instance of DelegateCommand
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <param name="canExecute">Can Execute function</param>
        internal DelegateCommand(Action<object> action, Predicate<object> canExecute) :
            base(action, canExecute)
        {
        }

        /// <summary>
        /// Creates a new instance of DelegateCommand
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="canExecute">Can Execute function</param>
        /// <param name="state">Update binding on execute state</param>
        internal DelegateCommand(Action<object> action, Predicate<object> canExecute, UpdateBindingOnExecute state) : 
            base(action, canExecute, state)
        {
        }

        /// <summary>
        /// Parameter can be null, so execute the command
        /// </summary>
        protected override bool ExecuteWithNullParameter
        {
            get { return true; }
        }
    }
}
