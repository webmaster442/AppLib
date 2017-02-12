﻿using System;
using System.Windows.Input;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// A Delegate command implementation
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _action;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates a new Instance of DelegateCommand
        /// </summary>
        /// <param name="action">Acton to do</param>
        public DelegateCommand(Action<object> action) : this(action, null) { }

        /// <summary>
        /// Creates a new Instance of DelegateCommand
        /// </summary>
        /// <param name="action">Acton to do</param>
        /// <param name="canExecute">canExecute predicate</param>
        public DelegateCommand(Action<object> action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// The method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _action(parameter);
        }

        /// <summary>
        /// Invokes the CanExecuteChanged event
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
        /// <param name="action">Action to transform to command</param>
        /// <returns>A new Instance of DelegateCommand</returns>
        public static DelegateCommand ToCommand(Action<object> action)
        {
            return new DelegateCommand(action);
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
            return new DelegateCommand(x => action(), x => canDoAction());
        }
    }
}
