using System;
using System.Windows.Markup;

namespace AppLib.WPF.Translate
{
    /// <summary>
    /// The Translate Markup extension returns a binding to a TranslationData
    /// that provides a translated resource of the specified key
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {

        private static ResxTranslator _translator;
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
            _translator = new ResxTranslator();
        }

        [ConstructorArgument("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// See <see cref="MarkupExtension.ProvideValue" />
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _translator.Translate(_key);
        }
    }

}
