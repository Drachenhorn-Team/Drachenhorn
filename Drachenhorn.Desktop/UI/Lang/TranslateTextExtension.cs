using System;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Data;

namespace Drachenhorn.Desktop.UI.Lang
{
    public class TranslateTextExtension : Binding
    {
        /// <summary>
        /// Translates the given TranslateID
        /// </summary>
        /// <param name="text">TranslationText</param>
        public TranslateTextExtension(string text) : base("[" + text + "]")
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