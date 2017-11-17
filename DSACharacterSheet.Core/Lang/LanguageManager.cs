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

namespace DSACharacterSheet.Core.Lang
{
    public class LanguageManager
    {
        private static readonly string[] CULTURES = { "de-DE", "en", "nds-DE" };

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

        public string this[string key]
        {
            get { return GetLanguageText(key); }
        }

        private static ResourceManager resourceManager = new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(LanguageManager).Assembly);

        public static string GetLanguageText(string identifier)
        {
            try
            {
                return resourceManager.GetString(identifier, CurrentCulture);
            }
            catch (MissingManifestResourceException)
            {
                return identifier;
            }
        }

        public static List<string> GetAllCultureStrings()
        {
            return GetAllCultures().ConvertAll<string>(x => x.DisplayName);
        }

        public static List<CultureInfo> GetAllCultures()
        {
            var result = new List<CultureInfo>();

            foreach (var culture in CULTURES)
                try
                {
                    result.Add(new CultureInfo(culture));
                }
                catch (CultureNotFoundException) { }

            return result;
        }


    }
}
