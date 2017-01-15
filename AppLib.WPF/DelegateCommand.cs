using System;
using System.Windows.Input;

namespace AppLib.WPF
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute)
                       : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute,
                       Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public virtual bool CanExecute(T parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public virtual void Execute(T parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T) parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T) parameter);
        }
    }
}
