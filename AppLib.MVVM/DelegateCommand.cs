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
        public DelegateCommand(Action<object> action) : base(action)
        {
        }

        /// <summary>
        /// Creates a new instance of DelegateCommand
        /// </summary>
        /// <param name="action">Action to perform</param>
        /// <param name="canExecute">Can Execute function</param>
        public DelegateCommand(Action<object> action, Predicate<object> canExecute) : base(action, canExecute)
        {
        }

        /// <summary>
        /// Creates a new instance of DelegateCommand
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="canExecute">Can Execute function</param>
        /// <param name="state">Update binding on execute state</param>
        public DelegateCommand(Action<object> action, Predicate<object> canExecute, UpdateBindingOnExecute state) : base(action, canExecute, state)
        {
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <param name="state">Update binding on execute state</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand ToCommand(Action action, Func<bool> canDoAction, UpdateBindingOnExecute state)
        {
            Predicate<object> predicate = new Predicate<object>((param) =>
            {
                return canDoAction.Invoke();
            });
            return new DelegateCommand(x => action(), predicate, state);
        }

        /// <summary>
        /// Cretes a command from an action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand ToCommand(Action action)
        {
            return new DelegateCommand(x => action());
        }

        /// <summary>
        /// Cretes a command from an action
        /// </summary>
        ///  <param name="action">Action to transform to command</param>
        /// <param name="state">Update binding on execute state</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand ToCommand(Action action, UpdateBindingOnExecute state)
        {
            return new DelegateCommand(x => action(), null, state);
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand ToCommand(Action action, Func<bool> canDoAction)
        {
            Predicate<object> predicate = new Predicate<object>((param) =>
            {
                return canDoAction.Invoke();
            });
            return new DelegateCommand(x => action(), predicate);
        }
    }
}
