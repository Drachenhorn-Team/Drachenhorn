using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using SimpleLogger;

namespace DSACharacterSheet.Core.Lang
{
    public class LanguageManager : BindableBase
    {
        private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
        public CultureInfo CurrentCulture
        {
            get { return _currentCulture; }
            set
            {
                if (Equals(_currentCulture, value))
                    return;
                _currentCulture = value;
                OnPropertyChanged(null);
            }
        }

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="key">TranslateID</param>
        /// <returns>Translated Text</returns>
        public string this[string key] => TranslateText(key);

        private readonly ResourceManager ResourceManager = new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(LanguageManager).Assembly);

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="identifier">TranslateID</param>
        /// <returns>Translated Text.</returns>
        public string GetLanguageText(string identifier)
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
        public string TranslateText(string text)
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

                text = text.Remove(index, temp.Length + 1).Insert(index, GetLanguageText(temp));
            }

            return text;
        }

        /// <summary>
        /// Returns the Native Names of all supported cultures.
        /// </summary>
        /// <returns>List of native culture names</returns>
        public IEnumerable<string> GetAllCultureStrings()
        {
            return GetAllCultures().Select(x => x.NativeName);
        }

        /// <summary>
        /// Returns all supported cultures.
        /// </summary>
        /// <returns>List of supported cultures.</returns>
        public IEnumerable<CultureInfo> GetAllCultures()
        {
            var result = new List<CultureInfo>();

            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (var culture in cultures)
            {
                try
                {
                    ResourceSet rs = ResourceManager.GetResourceSet(culture, true, false);

                    if (rs != null)
                    {
                        result.Add(culture);
                    }
                }
                catch (CultureNotFoundException) { }
            }

            return result;
        }


        #region static

        public static CultureInfo CurrentUICulture
        {
            get { return ServiceLocator.Current.GetInstance<LanguageManager>().CurrentCulture; }
            set
            {
                var temp = ServiceLocator.Current.GetInstance<LanguageManager>();

                if (temp.CurrentCulture == value)
                    return;

                temp.CurrentCulture = value;
            }
        }

        public static string Translate(string key)
        {
            var lang = ServiceLocator.Current.GetInstance<LanguageManager>();

            return lang.GetLanguageText(key);
        }

        public static string TextTranslate(string text)
        {
            var lang = ServiceLocator.Current.GetInstance<LanguageManager>();

            return lang.TranslateText(text);
        }

        #endregion static
    }
}
