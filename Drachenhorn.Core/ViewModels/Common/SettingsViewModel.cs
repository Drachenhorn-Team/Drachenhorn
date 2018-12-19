using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Common
{
    public class SettingsViewModel : ViewModelBase
    {
        #region c'tor

        public SettingsViewModel(ISettings settings, IDialogService dialogService)
        {
            Settings = settings;

            DialogService = dialogService;
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

        private IDialogService DialogService { get; }

        #endregion
    }
}