using DSACharacterSheet.Core.Lang;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Data;

namespace DSACharacterSheet.Desktop.UI.Lang
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