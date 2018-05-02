using DSACharacterSheet.Core.Lang;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.UI.Lang
{
    public class TranslateExtension : Binding
    {
        /// <summary>
        /// Translates the given TranslateID
        /// </summary>
        /// <param name="name">TranslateID</param>
        public TranslateExtension(string name) : base("[%" + name + "]")
        {
            this.Source = SimpleIoc.Default.GetInstance<LanguageManager>();
        }
    }
}