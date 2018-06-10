using System;
using System.Globalization;

namespace Drachenhorn.Core.Lang
{
    public interface INotifyLanguageChanged
    {
        event LanguageChangedEventHandler LanguageChanged;
    }

    public delegate void LanguageChangedEventHandler(object sender, LanguageChangedEventArgs args);

    public class LanguageChangedEventArgs : EventArgs
    {
        public LanguageChangedEventArgs(CultureInfo newCulture)
        {
            NewCulture = newCulture;
        }

        public CultureInfo NewCulture { get; }
    }
}