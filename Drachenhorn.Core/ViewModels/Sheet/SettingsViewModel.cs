using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Sheet
{
    public class SettingsViewModel : ViewModelBase
    {
        #region c'tor

        public SettingsViewModel(ISettings settings, IDialogService dialogService)
        {
            Settings = settings;

            DialogService = dialogService;
        }

        #endregion c'tor

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

        private bool _isCheckingUpdate;

        public bool IsCheckingUpdate
        {
            get => _isCheckingUpdate;
            private set
            {
                if (_isCheckingUpdate == value)
                    return;
                _isCheckingUpdate = value;
                RaisePropertyChanged();
            }
        }

        private IDialogService DialogService { get; }

        #endregion Properties
    }
}