using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace AppLib.WPF.Converters
{
    /// <summary>
    /// Base for converters to be usesd as Markup extensions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConverterBase<T> : MarkupExtension where T: new()
    {
        private static T _converter;

        /// <summary>
        /// Returns an object that is provided as the value of the target property for this markup extension
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new T();
            }
            return _converter;
        }
    }
}
