using System;
using System.Windows.Data;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Lang
{
    public class TranslateExtension : Binding
    {
        /// <summary>
        ///     Translates the given TranslateID
        /// </summary>
        /// <param name="name">TranslateID</param>
        public TranslateExtension(string name) : base("[%" + name + "]")
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