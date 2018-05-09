﻿using DSACharacterSheet.Xml;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Easy.Logger.Interfaces;

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

        private readonly ResourceManager _resourceManager =
            new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(LanguageManager).Assembly);

        /// <summary>
        /// Returns the translated Text for the TranslateID.
        /// </summary>
        /// <param name="identifier">TranslateID</param>
        /// <returns>Translated Text.</returns>
        public string GetLanguageText(string identifier)
        {
            try
            {
                return _resourceManager.GetString(identifier, CurrentCulture);
            }
            catch (MissingManifestResourceException)
            {
                var logger = SimpleIoc.Default.GetInstance<ILogService>();
                logger.GetLogger<LanguageManager>().Debug("Missing Translation: " + identifier);
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
                    ResourceSet rs = _resourceManager.GetResourceSet(culture, true, false);

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

        public static CultureInfo CurrentUiCulture
        {
            get { return SimpleIoc.Default.GetInstance<LanguageManager>().CurrentCulture; }
            set
            {
                var temp = SimpleIoc.Default.GetInstance<LanguageManager>();

                if (temp.CurrentCulture == value)
                    return;

                temp.CurrentCulture = value;
            }
        }

        public static string Translate(string key)
        {
            var lang = SimpleIoc.Default.GetInstance<LanguageManager>();

            return lang.GetLanguageText(key);
        }

        public static string TextTranslate(string text)
        {
            var lang = SimpleIoc.Default.GetInstance<LanguageManager>();

            return lang.TranslateText(text);
        }

        #endregion static
    }
}