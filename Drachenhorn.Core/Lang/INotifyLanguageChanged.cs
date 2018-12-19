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
        #region c'tor

        public LanguageChangedEventArgs(CultureInfo newCulture)
        {
            NewCulture = newCulture;
        }

        #endregion

        #region Properties

        public CultureInfo NewCulture { get; }

        #endregion
    }
}