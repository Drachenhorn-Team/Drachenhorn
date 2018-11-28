using System;
using System.Windows.Data;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Lang
{
    public class TranslateTextExtension : Binding
    {
        /// <summary>
        ///     Translates the given TranslateID
        /// </summary>
        /// <param name="text">TranslationText</param>
        public TranslateTextExtension(string text) : base("[" + text + "]")
        {
            try
            {
                Source = SimpleIoc.Default.GetInstance<LanguageManager>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}