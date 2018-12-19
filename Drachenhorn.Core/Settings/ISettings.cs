using System.Globalization;
using Drachenhorn.Xml.Template;

namespace Drachenhorn.Core.Settings
{
    public interface ISettings
    {
        #region Properties

        bool IsNew { get; }

        CultureInfo CurrentCulture { get; set; }
        string Version { get; }
        string GitCommit { get; }
        string GitCommitLink { get; }

        VisualThemeType VisualTheme { get; set; }
        string AccentColor { get; set; }

        bool? ShowConsole { get; set; }

        SheetTemplate CurrentTemplate { get; set; }

        #endregion
    }
}