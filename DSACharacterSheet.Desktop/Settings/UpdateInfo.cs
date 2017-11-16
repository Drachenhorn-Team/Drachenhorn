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

        private bool _needUpdate = false;
        public bool NeedUpdate
        {
            get { return _needUpdate; }
            private set
            {
                if (_needUpdate == value)
                    return;
                _needUpdate = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region Update

        public bool CheckForUpdate()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo info = ad.CheckForDetailedUpdate();
                    NeedUpdate = info.IsUpdateRequired;
                    NewVersion = info.AvailableVersion.ToString();
                }
                
                return true;
            }
            catch (Exception e)
            {
                new ExceptionMessageBox(e, "test").Show();
                return false;
            }
        }

        public bool DoUpdate()
        {
            if (NeedUpdate)
                try
                {
                    if (ApplicationDeployment.IsNetworkDeployed)
                    {
                        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                        UpdateCheckInfo info = ad.CheckForDetailedUpdate();
                        if (info.UpdateAvailable)
                        {
                            ad.Update();
                        }
                    }

                    return true;
                }
                catch (Exception) { }
            return false;
        }

        #endregion Update
    }
}
