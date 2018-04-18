using DSACharacterSheet.Core.Lang;
using System.Windows.Data;
using GalaSoft.MvvmLight.Ioc;

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
            this.Source = SimpleIoc.Default.GetInstance<LanguageManager>();
        }
    }
}