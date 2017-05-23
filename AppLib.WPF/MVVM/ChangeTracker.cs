using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// A simple Model change tracker.
    /// Defines the mechanism for querying the object for changes and resetting of the changed status.
    /// </summary>
    public class ChangeTracker: IChangeTracking
    {
        private readonly Dictionary<string, object> _originalvalues;
        private readonly INotifyPropertyChanged _model;
        private readonly HashSet<string> _changed;
        private readonly HashSet<string> _excluded;

        /// <summary>
        /// Creates a new instance of change tracker. For tracking only those properties will
        /// be monitored, which can be written and are readable
        /// </summary>
        /// <param name="model">Model, whose properties will be wathced</param>
        /// <param name="Excludelist">List of excluded properties ftom tracking</param>
        public ChangeTracker(INotifyPropertyChanged model, IList<string> Excludelist = null)
        {
            _model = model;
            _changed = new HashSet<string>();
            _originalvalues = new Dictionary<string, object>();

            if (Excludelist != null)
                _excluded = new HashSet<string>(Excludelist);
            else
                _excluded = new HashSet<string>();

            AcceptChanges();
            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_originalvalues.ContainsKey(e.PropertyName) || _excluded.Contains(e.PropertyName)) return;

            var o1 = _originalvalues[e.PropertyName];
            var o2 = _model.GetType().GetProperty(e.PropertyName).GetValue(_model);
            var eq = Equals(o1, o2);
            if (_changed.Contains(e.PropertyName))
                _changed.Remove(e.PropertyName);
            else
                _changed.Add(e.PropertyName);
        }

        /// <summary>
        /// Names of changed properties
        /// </summary>
        public HashSet<string> ChangedProperties
        {
            get { return _changed; } 
        }

        /// <summary>
        /// Gets the object's changed status.
        /// </summary>
        public bool IsChanged
        {
            get { return _changed.Any(); }
        }

        /// <summary>
        /// Resets the object’s state to unchanged by accepting the modifications.
        /// </summary>
        public void AcceptChanges()
        {
            _changed.Clear();
            _originalvalues.Clear();

            var properties = (from property in _model.GetType().GetProperties()
                              where property.CanRead && property.CanWrite
                              select property).ToList();



            foreach (var property in properties)
            {
                if (_excluded.Contains(property.Name)) continue;
                _originalvalues.Add(property.Name, property.GetValue(_model));
            }
        }
    }
}
