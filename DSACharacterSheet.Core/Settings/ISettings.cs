using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSACharacterSheet.Core.Settings.Update;

namespace DSACharacterSheet.Core.Settings
{
    public interface ISettings
    {
        CultureInfo CurrentCulture { get; set; }
        string Version { get; }
        string GitCommit { get; }

        bool IsUpdateAvailable { get; }
        bool CanCheckUpdate { get; }


        #region Methods

        bool CheckUpdate();
        void CheckUpdateAsync();
        void CheckUpdateAsync(UpdateCheckedHandler checkFinished);

        #endregion Methods
    }
}
