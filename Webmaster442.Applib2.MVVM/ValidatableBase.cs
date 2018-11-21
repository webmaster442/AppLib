using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Webmaster442.Applib
{
    /// <summary>
    /// A class implementing INotifyPropertyChanged and INotifyDataErrorInfo
    /// </summary>
    public abstract class ValidatableBase : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Error Container
        /// </summary>
        protected ConcurrentDictionary<string, List<string>> _errors;
        private object _lock;

        /// <summary>
        /// Trigger validation on property change event
        /// </summary>
        protected bool ValidateOnPropertyChange
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ValidatableBase()
        {
            _errors = new ConcurrentDictionary<string, List<string>>();
            _lock = new object();
        }

        /// <summary>
        /// Errors changed event
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        /// <summary>
        /// Errors changed event invoker
        /// </summary>
        /// <param name="propertyName">property name</param>
        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Property changed override with optional validation checking
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (ValidateOnPropertyChange)
            {
                Validate();
                base.OnPropertyChanged(nameof(HasErrors));
            }
        }

        /// <summary>
        /// Property changed override with optional validation checking
        /// </summary>
        /// <param name="expression">Property name specifier expression</param>
        protected override void OnPropertyChanged(Expression<Func<object>> expression)
        {
            base.OnPropertyChanged(expression);
            if (ValidateOnPropertyChange)
            {
                Validate();
                base.OnPropertyChanged(nameof(HasErrors));
            }
        }

        /// <summary>
        /// Get list of errors for a property
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns>List of errors for property</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                List<string> allerors = new List<string>();
                foreach (var key in _errors.Keys)
                {
                    List<string> errorsForName;
                    _errors.TryGetValue(key, out errorsForName);
                    allerors.AddRange(errorsForName);
                }
                return allerors;
            }
            else
            {
                List<string> errorsForName;
                _errors.TryGetValue(propertyName, out errorsForName);
                return errorsForName;
            }
        }

        /// <summary>
        /// Returns true, if the model has validation errors
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        /// <summary>
        /// Run validation Async
        /// </summary>
        /// <returns></returns>
        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        /// <summary>
        /// Run validation syncronously
        /// </summary>
        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        _errors.TryRemove(kv.Key, out List<string> outLi);
                        OnErrorsChanged(kv.Key);
                        base.OnPropertyChanged(nameof(HasErrors));
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        _errors.TryRemove(prop.Key, out List<string> outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                    base.OnPropertyChanged(nameof(HasErrors));
                }
            }
        }
    }
}
