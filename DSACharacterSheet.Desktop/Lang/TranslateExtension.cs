using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.Lang
{
    public class TranslateExtension : Binding
    {
        public TranslateExtension(string name) : base("[" + name + "]")
        {
            this.Source = new LanguageManager();
        }
    }
}
