using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Controls;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// Abstract View Model implementation for MVVM
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Collection of validation rules;
        /// </summary>
        private readonly Dictionary<string, Func<bool>> _validationRules;

        private bool _hasErrors;

        private readonly List<string> _errors;

        /// <summary>
        /// Creates a new instance of ViewModel
        /// </summary>
        public ViewModel()
        {
            _errors = new List<string>();
            _validationRules = new Dictionary<string, Func<bool>>();
        }

        /// <summary>
        /// Gets the validation state
        /// </summary>
        public bool HasErrors
        {
            get { return _hasErrors; }
            set { SetValue(ref _hasErrors, value); }
        }

        /// <summary>
        /// List of properties that have validation error
        /// </summary>
        public List<String> Errors
        {
            get { return _errors; }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fires the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Validate();
        }

        /// <summary>
        /// Simplifies property setting code
        /// </summary>
        /// <typeparam name="T">type of proprerty</typeparam>
        /// <param name="storage">private variable to store property value</param>
        /// <param name="value">new porperty value</param>
        /// <param name="propertyName">property name. Don't need to pass, uses caller member name</param>
        /// <returns>true, if property was set to new value</returns>
        protected virtual bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Call the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name. If not set, then caller member name will be used</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Adds a validation rule for property value validation.
        /// </summary>
        /// <param name="property">property name.</param>
        /// <param name="rule">rule to add</param>
        public void AddValidationRule(string property, Func<bool> rule)
        {
            if (_validationRules.ContainsKey(property))
                _validationRules[property] = rule;
            else
                _validationRules.Add(property, rule);
        }

        /// <summary>
        /// Validates properties
        /// </summary>
        /// <returns>true, if all values pass validation, false, if they don't</returns>
        public bool Validate()
        {
            _errors.Clear();
            foreach (var rule in _validationRules)
            {
                if (!rule.Value()) _errors.Add(rule.Key);
            }
            HasErrors = _errors.Count == 0;
            return HasErrors;
        }

        /// <summary>
        /// Helper function to get view model of a control
        /// </summary>
        /// <typeparam name="ViewModelType">return model type</typeparam>
        /// <param name="ctrl">cotnrol to get model from</param>
        /// <returns>View Model</returns>
        public static ViewModelType GetViewModel<ViewModelType>(ContentControl ctrl) where ViewModelType: ViewModel
        {
            if (ctrl.DataContext == null) return null;
            return ctrl.DataContext as ViewModelType;
        }
    }
}
