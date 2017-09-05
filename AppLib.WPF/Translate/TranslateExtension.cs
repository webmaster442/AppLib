using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Markup;

namespace AppLib.WPF.Translate
{
    /// <summary>
    /// The Translate Markup extension returns a binding to a TranslationData
    /// that provides a translated resource of the specified key
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {

        private static readonly ResourceManager _resourceManager;
        private static readonly CultureInfo _curent;

        private string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateExtension"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public TranslateExtension(string key)
        {
            _key = key;
        }

        static TranslateExtension()
        {
            var caller = Assembly.GetEntryAssembly();
            var path = string.Format("{0}.Properties.Resources", caller.GetName().Name);
            _resourceManager = new ResourceManager(path, caller);
            _curent = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Key of string to translate
        /// </summary>
        [ConstructorArgument("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        
        /// <summary>
        /// Translate a string from ResX
        /// </summary>
        /// <param name="key">Key of string to translate</param>
        /// <returns>Translated text</returns>
        public static string Translate(string key)
        {
            try
            {
                return _resourceManager.GetString(key, _curent);
            }
            catch (Exception)
            {
                try
                {
                    return _resourceManager.GetString(key, new CultureInfo("en"));
                }
                catch (Exception)
                {
                    return string.Format("!{0}!", key);
                }
            }
        }

        /// <summary>
        /// See <see cref="MarkupExtension.ProvideValue" />
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Translate(_key);
        }
    }

}
