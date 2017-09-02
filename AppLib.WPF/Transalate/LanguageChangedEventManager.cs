using System;
using System.Windows;

namespace AppLib.WPF.Transalate
{
    /// <summary>
    /// Manager for Language change events
    /// </summary>
    public class LanguageChangedEventManager : WeakEventManager
    {
        /// <summary>
        /// Add a Listener
        /// </summary>
        /// <param name="source">Listener source</param>
        /// <param name="listener">Listener to add</param>
        public static void AddListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        /// <summary>
        /// Remove listener
        /// </summary>
        /// <param name="source">Listener source</param>
        /// <param name="listener">Listener to remove</param>
        public static void RemoveListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }

        /// <summary>
        /// Start listening process
        /// </summary>
        /// <param name="source">source of the listening</param>
        protected override void StartListening(object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged += OnLanguageChanged;
        }

        /// <summary>
        /// Stop listening 
        /// </summary>
        /// <param name="source">source of the listening</param>
        protected override void StopListening(object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged -= OnLanguageChanged;
        }

        /// <summary>
        /// Current Manager instance
        /// </summary>
        private static LanguageChangedEventManager CurrentManager
        {
            get
            {
                Type managerType = typeof(LanguageChangedEventManager);
                var manager = (LanguageChangedEventManager)GetCurrentManager(managerType);
                if (manager == null)
                {
                    manager = new LanguageChangedEventManager();
                    SetCurrentManager(managerType, manager);
                }
                return manager;
            }
        }

    }
}
