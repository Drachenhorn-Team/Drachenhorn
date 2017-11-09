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
        public string this[string key]
        {
            get { return GetLanguageText(key); }
        }

        public static string GetLanguageText(string identifier)
        {
            ResourceManager rm = new ResourceManager("DSACharacterSheet.Core.Lang.lang", typeof(LanguageManager).Assembly);
            try
            {
                return rm.GetString(identifier, CultureInfo.CurrentUICulture);
            }
            catch (MissingManifestResourceException)
            {
                return identifier;
            }
        }
    }
}
