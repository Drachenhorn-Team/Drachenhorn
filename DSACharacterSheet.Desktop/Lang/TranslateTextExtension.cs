using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CommonServiceLocator;

namespace DSACharacterSheet.Desktop.Lang
{
    public class TranslateTextExtension : Binding
    {
        /// <summary>
        /// Translates the given TranslateID
        /// </summary>
        /// <param name="text">TranslationText</param>
        public TranslateTextExtension(string text) : base("[" + text + "]")
        {
            this.Source = ServiceLocator.Current.GetInstance<LanguageManager>();
        }
    }
}
