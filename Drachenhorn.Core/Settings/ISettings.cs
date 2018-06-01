﻿using Drachenhorn.Core.Settings.Update;
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

        bool IsUpdateAvailable { get; }
        bool CanCheckUpdate { get; }

        #region Methods

        bool CheckUpdate();

        void CheckUpdateAsync();

        void CheckUpdateAsync(UpdateCheckedHandler checkFinished);

        #endregion Methods
    }
}