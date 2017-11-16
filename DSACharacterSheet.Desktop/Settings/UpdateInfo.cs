using DSACharacterSheet.Desktop.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSACharacterSheet.Desktop.Settings
{
    public class UpdateInfo : BindableBase
    {
        #region Properties

        public string Version
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                else
                    return "Application not installed.";
            }
        }

        private string _newVersion = null;
        public string NewVersion
        {
            get { return _newVersion; }
            private set
            {
                if (_newVersion == value)
                    return;
                _newVersion = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdateAvailable = false;
        public bool IsUpdateAvailable
        {
            get { return _isUpdateAvailable; }
            private set
            {
                if (_isUpdateAvailable == value)
                    return;
                _isUpdateAvailable = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region Update

        public void CheckForUpdateAsync()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                    ad.CheckForUpdateCompleted += (sender, args) =>
                    {
                        IsUpdateAvailable = args.UpdateAvailable;
                        NewVersion = args.AvailableVersion.ToString();
                    };
                    ad.CheckForUpdateAsync();
                }
            }
            catch (Exception e)
            {
                new ExceptionMessageBox(e, "test").Show();
            }
        }

        public void DoUpdateAsync(AsyncCompletedEventHandler handler)
        {
            if (IsUpdateAvailable)
                try
                {
                    if (ApplicationDeployment.IsNetworkDeployed)
                    {
                        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                        ad.UpdateCompleted += handler;
                        UpdateCheckInfo info = ad.CheckForDetailedUpdate();
                        if (info.UpdateAvailable)
                        {
                            ad.UpdateAsync();
                        }
                    }
                }
                catch (Exception e)
                {
                    new ExceptionMessageBox(e, "test").Show();
                }
        }

        #endregion Update
    }
}
