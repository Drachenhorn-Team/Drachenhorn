using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Drachenhorn.Core.Lang
{
    public interface INotifyLanguageChanged
    {
        event LanguageChangedEventHandler LanguageChanged;
    }

    public delegate void LanguageChangedEventHandler(object sender, LanguageChangedEventArgs args);

    public class LanguageChangedEventArgs : EventArgs
    {
        public CultureInfo NewCulture { get; }

        public LanguageChangedEventArgs(CultureInfo newCulture)
        {
            NewCulture = newCulture;
        }
    }
}
