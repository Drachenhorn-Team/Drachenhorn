using DSACharacterSheet.Core.Lang;
using System.Windows.Data;
using GalaSoft.MvvmLight.Ioc;

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
            this.Source = SimpleIoc.Default.GetInstance<LanguageManager>();
        }
    }
}