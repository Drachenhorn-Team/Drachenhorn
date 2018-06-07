using System.Globalization;

namespace Drachenhorn.Core.Settings
{
    public interface ISettings
    {
        bool IsNew { get; }

        CultureInfo CurrentCulture { get; set; }
        string Version { get; }
        string GitCommit { get; }

        VisualThemeType VisualTheme { get; set; }

        bool? ShowConsole { get; set; }
    }
}