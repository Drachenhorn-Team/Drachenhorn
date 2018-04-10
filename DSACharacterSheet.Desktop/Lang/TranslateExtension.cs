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
    public class TranslateExtension : Binding
    {
        /// <summary>
        /// Translates the given TranslateID
        /// </summary>
        /// <param name="name">TranslateID</param>
        public TranslateExtension(string name) : base("[%" + name + "]")
        {
            this.Source = ServiceLocator.Current.GetInstance<LanguageManager>();
        }
    }
}
