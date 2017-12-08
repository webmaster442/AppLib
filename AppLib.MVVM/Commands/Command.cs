using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.MVVM
{
    public static class Command
    {
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

        /// <summary>
        /// Cretes a command from an action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action)
        {
            return new DelegateCommand<T>(x => action(x));
        }

        /// <summary>
        /// Cretes a command from an action
        /// </summary>
        ///  <param name="action">Action to transform to command</param>
        /// <param name="state">Update binding on execute state</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action, UpdateBindingOnExecute state)
        {
            return new DelegateCommand<T>(x => action(x), null, state);
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action, Predicate<T> canDoAction)
        {
            return new DelegateCommand<T>(x => action(x), x => canDoAction(x));
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action, Func<bool> canDoAction)
        {
            Predicate<T> predicate = new Predicate<T>((param) =>
            {
                return canDoAction.Invoke();
            });
            return new DelegateCommand<T>(x => action(x), predicate);
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <param name="state">Update binding on execute state</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action, Predicate<T> canDoAction, UpdateBindingOnExecute state)
        {
            return new DelegateCommand<T>(x => action(x), x => canDoAction(x), state);
        }

        /// <summary>
        /// Cretes a command from an action and a can do action
        /// </summary>
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        /// <param name="canDoAction">can do action</param>
        /// <param name="state">Update binding on execute state</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand<T> ToCommand<T>(Action<T> action, Func<bool> canDoAction, UpdateBindingOnExecute state)
        {
            Predicate<T> predicate = new Predicate<T>((param) =>
            {
                return canDoAction.Invoke();
            });
            return new DelegateCommand<T>(x => action(x), predicate, state);
        }
    }
}
