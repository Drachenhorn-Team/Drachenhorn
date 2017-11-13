using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Core.Lang
{
    public class LanguageManager
    {
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

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    ResourceSet rs = resourceManager.GetResourceSet(culture, true, false);
                    if (rs != null)
                        result.Add(culture);
                }
                catch (CultureNotFoundException) { }
                catch (ArgumentException) { }
            }

            return result;
        }


    }
}
