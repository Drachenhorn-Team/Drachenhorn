using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Core.ViewModels.Sheet
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Properties

        private ISettings _settings;

        public ISettings Settings
        {
            get { return _settings; }
            set
            {
                if (_settings == value)
                    return;
                _settings = value;
                RaisePropertyChanged();
            }
        }

        private bool _isCheckingUpdate = false;

        public bool IsCheckingUpdate
        {
            get { return _isCheckingUpdate; }
            private set
            {
                if (_isCheckingUpdate == value)
                    return;
                _isCheckingUpdate = value;
                RaisePropertyChanged();
            }
        }

        private IDialogService DialogService { get; set; }

        #endregion Properties

        #region c'tor

        public SettingsViewModel(ISettings settings, IDialogService dialogService)
        {
            Settings = settings;

            DialogService = dialogService;
        }

        #endregion c'tor
    }
}