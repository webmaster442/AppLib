using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace AppLib.WPF.Translate
{
    /// <summary>
    /// Translation manager
    /// </summary>
    public class TranslationManager
    {
        private static TranslationManager _translationManager;

        /// <summary>
        /// Language changed event
        /// </summary>
        public event EventHandler LanguageChanged;

        /// <summary>
        /// Gets or sets the current language
        /// </summary>
        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }

        /// <summary>
        /// Gets the available languages
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }

        /// <summary>
        /// Current instance
        /// </summary>
        public static TranslationManager Instance
        {
            get
            {
                if (_translationManager == null)
                    _translationManager = new TranslationManager();
                return _translationManager;
            }
        }

        /// <summary>
        /// Gets or sets the Translation provider
        /// </summary>
        public ITranslationProvider TranslationProvider { get; set; }

        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Translates a given key
        /// </summary>
        /// <param name="key">key to get translation for</param>
        /// <returns>translated text</returns>
        public object Translate(string key)
        {
            if (TranslationProvider != null)
            {
                object translatedValue = TranslationProvider.Translate(key);
                if (translatedValue != null)
                {
                    return translatedValue;
                }
            }
            return string.Format("!{0}!", key);
        }
    }
}
