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

        private int _progressPercentage = -1;
        public int ProgressPercentage
        {
            get { return _progressPercentage; }
            set
            {
                if (_progressPercentage == value)
                    return;
                _progressPercentage = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region Update

        public void CheckForUpdate()
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
                    ad.CheckForUpdate();
                }
            }
            catch (InvalidOperationException) { }
        }

        public void DoUpdateAsync(AsyncCompletedEventHandler handler)
        {
            if (IsUpdateAvailable)
                try
                {
                    if (ApplicationDeployment.IsNetworkDeployed)
                    {
                        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                        ad.UpdateProgressChanged += (sender, args) =>
                        {
                            ProgressPercentage = args.ProgressPercentage;
                        };
                        ad.UpdateCompleted += handler;
                        ad.UpdateCompleted += (sender, args) =>
                        {
                            ProgressPercentage = -1;
                        };
                        UpdateCheckInfo info = ad.CheckForDetailedUpdate();
                        if (info.UpdateAvailable)
                        {
                            ad.UpdateAsync();
                        }
                    }
                }
                catch (Exception)
                {
                    //new ExceptionMessageBox(e, "test").Show();
                }
        }

        #endregion Update
    }
}
