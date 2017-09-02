using System;
using System.ComponentModel;
using System.Windows;

namespace AppLib.WPF.Transalate
{
    /// <summary>
    /// Translation Data
    /// </summary>
    public class TranslationData : IWeakEventListener, INotifyPropertyChanged, IDisposable
    {
        private string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationData"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslationData(string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(
                      TranslationManager.Instance, this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TranslationData"/> is reclaimed by garbage collection.
        /// </summary>
        ~TranslationData()
        {
            Dispose(true);
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="IDisposable.Dispose"/>
        /// </summary>
        /// <param name="disposing">dispose native resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                LanguageChangedEventManager.RemoveListener(
                          TranslationManager.Instance, this);
            }
        }

        /// <summary>
        /// Provides value
        /// </summary>
        public object Value
        {
            get { return TranslationManager.Instance.Translate(_key); }
        }

        /// <summary>
        /// Recieves an event
        /// </summary>
        /// <param name="managerType">Messager type</param>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        /// <returns>true, if event is handled, false if not.</returns>
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }

        /// <summary>
        /// <see cref="INotifyPropertyChanged.PropertyChanged"/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
