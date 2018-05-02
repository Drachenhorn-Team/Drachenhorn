using DSACharacterSheet.Core.Settings.Update;
using System.Globalization;

namespace DSACharacterSheet.Core.Settings
{
    public interface ISettings
    {
        CultureInfo CurrentCulture { get; set; }
        string Version { get; }
        string GitCommit { get; }

        VisualThemeType VisualTheme { get; set; }

        bool IsUpdateAvailable { get; }
        bool CanCheckUpdate { get; }

        #region Methods

        bool CheckUpdate();

        void CheckUpdateAsync();

        void CheckUpdateAsync(UpdateCheckedHandler checkFinished);

        #endregion Methods
    }
}