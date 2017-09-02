using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace AppLib.WPF.Transalate
{
    /// <summary>
    /// ResX translation provider
    /// </summary>
    public class ResxTranslationProvider : ITranslationProvider
    {
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResxTranslationProvider"/> class.
        /// </summary>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="assembly">The assembly.</param>
        public ResxTranslationProvider(string baseName, Assembly assembly)
        {
            _resourceManager = new ResourceManager(baseName, assembly);
        }

        /// <summary>
        /// Get available languages
        /// </summary>
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                //Get all culture 
                CultureInfo[] culture = CultureInfo.GetCultures(CultureTypes.AllCultures);

                //Find the location where application installed.
                string exeLocation = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

                //Return all culture for which satellite folder found with culture code.
                return culture.Where(cultureInfo => Directory.Exists(Path.Combine(exeLocation, cultureInfo.Name)));
            }
        }

        /// <summary>
        /// See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public object Translate(string key)
        {
            return _resourceManager.GetString(key);
        }
    }
}
