using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Common
{
    public class SettingsViewModel : ViewModelBase
    {
        #region c'tor

        public SettingsViewModel(ISettings settings)
        {
            Settings = settings;
        }

        #endregion

        #region Properties

        private ISettings _settings;

        public ISettings Settings
        {
            get => _settings;
            set
            {
                if (_settings == value)
                    return;
                _settings = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}