using System;
using System.Windows;
using System.Windows.Input;
using AppLib.WPF.Extensions;

namespace AppLib.MVVM
{
    /// <summary>
    /// A Delegate command implementation
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _action;
        private event EventHandler _internalCanExecuteChanged;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _internalCanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _internalCanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Gets or sets wheather the binding of the focused element uppon firing the command should be updated
        /// </summary>
        public UpdateBindingOnExecute UpdateOnExecute
        {
            get;
            set;
        }

        /// <summary>
        /// Execute if parameter is null?
        /// </summary>
        protected virtual bool ExecuteWithNullParameter
        {
            get { return false; }
        }


        /// <summary>
        /// Creates a new Instance of DelegateCommand
        /// </summary>
        /// <param name="action">Acton to do</param>
        public DelegateCommand(Action<T> action) : this(action, null) { }

        /// <summary>
        /// Creates a new Instance of DelegateCommand
        /// </summary>
        /// <param name="action">Acton to do</param>
        /// <param name="canExecute">canExecute predicate</param>
        public DelegateCommand(Action<T> action, Predicate<T> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Creates a new Instance of DelegateCommand
        /// </summary>
        /// <param name="action">Acton to do</param>
        /// <param name="canExecute">canExecute predicate</param>
        /// <param name="state"></param>
        public DelegateCommand(Action<T> action, Predicate<T> canExecute, UpdateBindingOnExecute state)
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
            if (parameter == null && !ExecuteWithNullParameter)
            {
                return false;
            }
            if (_canExecute == null) return true;
            return _canExecute((T)parameter);
        }

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (!ExecuteWithNullParameter && parameter == null)
            {
                return;
            }

            var focused = Keyboard.FocusedElement as UIElement;

            switch (UpdateOnExecute)
            {
                case UpdateBindingOnExecute.Source:
                    focused?.UpdateAllBindings(BindingDirection.Source);
                    break;
                case UpdateBindingOnExecute.Target:
                    focused?.UpdateAllBindings(BindingDirection.Target);
                    break;
            }
            _action((T)parameter);
        }

        /// <summary>
        /// Invokes the CanExecuteChanged event
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            if (_canExecute != null)
                OnCanExecuteChanged();
        }

        /// <summary>
        /// This method is used to walk the delegate chain and well WPF that
        /// our command execution status has changed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            _internalCanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
