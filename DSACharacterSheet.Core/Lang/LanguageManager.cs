using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using SimpleLogger;

namespace DSACharacterSheet.Core.Lang
{
    public class LanguageManager
    {
        private static readonly string[] Cultures = { "de-DE", "en" };

        private static CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
        public static CultureInfo CurrentCulture
        {
            get { return _currentCulture; }
            set
            {
                if (_currentCulture == value)
                    return;
                _currentCulture = value;
            }
        }

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="key">TranslateID</param>
        /// <returns>Translated Text</returns>
        public string this[string key] => GetLanguageText(key);

        private static readonly ResourceManager ResourceManager = new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(LanguageManager).Assembly);

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="identifier">TranslateID</param>
        /// <returns>Translated Text.</returns>
        public static string GetLanguageText(string identifier)
        {
            try
            {
                return ResourceManager.GetString(identifier, CurrentCulture);
            }
            catch (MissingManifestResourceException)
            {
                Logger.Debug.Log("Missing Transformation: " + identifier);
                return identifier;
            }
        }

        /// <summary>
        /// Returns the Native Names of all supported cultures.
        /// </summary>
        /// <returns>List of native culture names</returns>
        public static List<string> GetAllCultureStrings()
        {
            return GetAllCultures().ConvertAll<string>(x => x.NativeName);
        }

        /// <summary>
        /// Returns all supported cultures.
        /// </summary>
        /// <returns>List of supported cultures.</returns>
        public static List<CultureInfo> GetAllCultures()
        {
            var result = new List<CultureInfo>();

            foreach (var culture in Cultures)
                try
                {
                    result.Add(new CultureInfo(culture));
                }
                catch (CultureNotFoundException) { }

            return result;
        }
    }
}
