using DSACharacterSheet.Core;
using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.Core.Settings;
using DSACharacterSheet.Core.Settings.Update;

namespace DSACharacterSheet.Desktop.UserSettings
{
    [Serializable]
    public class Settings : BindableBase, ISettings
    {
        #region Properties

        [XmlElement("CurrentCulture")]
        public string CurrentCultureString
        {
            get { return CurrentCulture.Name; }
            set
            {
                try
                {
                    CurrentCulture = new CultureInfo(value);
                }
                catch (Exception)
                {
                    CurrentCulture = CultureInfo.CurrentUICulture;
                }
            }
        }

        [XmlIgnore]
        public CultureInfo CurrentCulture
        {
            get { return LanguageManager.CurrentCulture; }
            set
            {
                if (LanguageManager.CurrentCulture == value)
                    return;
                LanguageManager.CurrentCulture = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
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

        [XmlIgnore]
        private string _gitCommit;
        [XmlIgnore]
        public string GitCommit
        {
            get { return _gitCommit; }
            private set
            {
                if (_gitCommit == value)
                    return;
                _gitCommit = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private bool _isUpdateAvailable;
        [XmlIgnore]
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


        #region c'tor

        public Settings()
        {
            this.PropertyChanged += (sender, args) => { this.Save(); };

            var path = Path.Combine(Environment.CurrentDirectory, "commit");
            if (File.Exists(path))
                GitCommit = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "commit")).Replace("\r", "").Replace("\n", "");
            else
                GitCommit = "No Commit found";
        }

        #endregion c'tor


        #region Update

        public bool CanCheckUpdate => ApplicationDeployment.IsNetworkDeployed;

        public event UpdateCheckedHandler UpdateChecked;
        private void OnUpdateChecked(object sender, UpdateCheckedEventArgs args)
        {
            UpdateChecked?.Invoke(sender, args);
        }

        public void CheckUpdateAsync()
        {
            new Task(() => {
                IsUpdateAvailable = NeedsUpdate();
                OnUpdateChecked(this, new UpdateCheckedEventArgs(IsUpdateAvailable));
            }).Start();
        }

        public void CheckUpdateAsync(UpdateCheckedHandler checkFinished)
        {
            void Handler(object sender, UpdateCheckedEventArgs args)
            {
                checkFinished(sender, args);
                UpdateChecked -= Handler;
            }

            UpdateChecked += Handler;
            CheckUpdateAsync();
        }

        /// <summary>
        /// Checks for a ClickOnce-Update
        /// </summary>
        /// <returns>True if update is available.</returns>
        private bool NeedsUpdate()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    return ApplicationDeployment.CurrentDeployment.CheckForUpdate();
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        #endregion Update


        #region Save/Load

        private static readonly string PropertiesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DSACharacterSheet");
        private static string PropertiesPath => Path.Combine(PropertiesDirectory, "config.xml");

        /// <summary>
        /// Loads the Properties from the "PROPERTIESDIRECTORY".
        /// </summary>
        /// <returns>The Loaded Properties.</returns>
        public static Settings Load()
        {
            if (!Directory.Exists(PropertiesDirectory))
                Directory.CreateDirectory(PropertiesDirectory);

            try
            {
                using (var stream = new FileStream(PropertiesPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    var temp = (Settings)serializer.Deserialize(stream);
                    return temp;
                }
            }
            catch (IOException)
            {
                return new Settings();
            }
        }

        /// <summary>
        /// Saves the Properties to the "PROPERTIESDIRECTORY"
        /// </summary>
        public void Save()
        {
            if (!Directory.Exists(PropertiesDirectory))
                Directory.CreateDirectory(PropertiesDirectory);

            try
            {
                using (var stream = new StreamWriter(PropertiesPath))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(stream, this);
                }
            }
            catch (IOException) { }
        }

        #endregion Save/Load
    }
}
