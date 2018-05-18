using System;
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
            try
            {
                this.Source = SimpleIoc.Default.GetInstance<LanguageManager>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}