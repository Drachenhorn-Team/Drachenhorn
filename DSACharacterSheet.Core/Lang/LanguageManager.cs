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
                if (Equals(_currentCulture, value))
                    return;
                _currentCulture = value;
            }
        }

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="key">TranslateID</param>
        /// <returns>Translated Text</returns>
        public string this[string key] => TranslateText(key);

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
                Logger.Debug.Log("Missing Translation: " + identifier);
                return identifier;
            }
        }

        /// <summary>
        /// Translates the text with the parameters
        /// </summary>
        /// <param name="text">The Text to translate. (% for the beginning of the parameters to translate)</param>
        /// <returns>Translated Text</returns>
        public static string TranslateText(string text)
        {
            List<int> indexes = new List<int>();
            for (int index = 0; ; ++index)
            {
                index = text.IndexOf('%', index);
                if (index == -1)
                    break;
                indexes.Add(index);
            }

            indexes.Reverse();

            foreach (var index in indexes)
            {
                var tempIndex = text.IndexOf(' ', index);

                if (tempIndex == -1) tempIndex = text.Length;

                var temp = text.Substring(index + 1, tempIndex - index - 1);

                text = text.Replace('%' + temp, GetLanguageText(temp));
            }

            return text;
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
