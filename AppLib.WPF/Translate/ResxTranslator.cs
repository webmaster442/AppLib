using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace AppLib.WPF.Translate
{
    /// <summary>
    /// ResX File Translator 
    /// </summary>
    public sealed class ResxTranslator
    {
        private readonly ResourceManager _resourceManager;
        private readonly CultureInfo _curent;

        /// <summary>
        /// Creates a new instance of ResxTranslator
        /// </summary>
        public ResxTranslator()
        {
            var caller = Assembly.GetEntryAssembly();
            var path = string.Format("{0}.Properties.Resources", caller.GetName().Name);
            _resourceManager = new ResourceManager(path, caller);
            _curent = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Translates a key to a corresponding text
        /// </summary>
        /// <param name="key">Translation key</param>
        /// <returns>translated text</returns>
        public string Translate(string key)
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
    }
}
