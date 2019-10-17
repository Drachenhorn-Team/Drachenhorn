using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using Drachenhorn.Xml.Template;

namespace Drachenhorn.Core.Settings
{
    public interface ISettings : INotifyPropertyChanged
    {
        #region Properties

        bool IsNew { get; }

        CultureInfo CurrentCulture { get; set; }
        string Version { get; }
        string GitCommit { get; }
        string GitCommitLink { get; }

        VisualThemeType VisualTheme { get; set; }
        string AccentColor { get; set; }

        SheetTemplate CurrentTemplate { get; set; }

        ObservableCollection<string> LastOpenFiles { get; }

        #endregion
    }
}