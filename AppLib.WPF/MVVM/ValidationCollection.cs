using System;
using System.Collections;
using System.Collections.Generic;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// Defines a collection for validation rules
    /// </summary>
    public class ValidationCollection: IEnumerable<KeyValuePair<string, List<Func<string>>>>
    {
        private readonly Dictionary<string, List<Func<string>>> _collection;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ValidationCollection()
        {
            _collection = new Dictionary<string, List<Func<string>>>();
        }

        /// <summary>
        /// Add a validation rule
        /// </summary>
        /// <param name="property">property to assign rule to</param>
        /// <param name="validator">Validator function</param>
        public void AddRule(string property, Func<string> validator)
        {
            if (_collection.ContainsKey(property))
            {
                _collection[property].Add(validator);
            }
            else
            {
                _collection.Add(property, new List<Func<string>> { validator });
            }
        }

        /// <summary>
        /// Delete rules assigned to a property
        /// </summary>
        /// <param name="property">property</param>
        public void DeleteRules(string property)
        {
            if (_collection.ContainsKey(property))
                _collection.Remove(property);
        }

        /// <summary>
        /// Delete all validations
        /// </summary>
        public void DeleteAllRules()
        {
            _collection.Clear();
        }

        /// <summary>
        /// Return matching validadion rules for a property
        /// </summary>
        /// <param name="property">property name</param>
        /// <returns>Validation rules for property</returns>
        public IEnumerable<Func<string>> this[string property]
        {
            get
            {
                if (_collection.ContainsKey(property))
                {
                    return _collection[property];
                }
                else
                {
                    return new List<Func<string>>();
                }
            }
        }

        /// <summary>
        /// Exposes the enumerator, which supports a simple iteration over a collection
        /// </summary>
        /// <returns>A KeyValuePair</returns>
        public IEnumerator<KeyValuePair<string, List<Func<string>>>> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
    }
}
