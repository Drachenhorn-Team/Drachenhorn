using DSACharacterSheet.Core;
using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.Desktop.Settings
{
    [Serializable]
    public class Properties : BindableBase
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

        public Properties()
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

        public void CheckUpdateAsync()
        {
            new Task(() => {
                IsUpdateAvailable = NeedsUpdate();
                OnUpdateChecked(this, new UpdateCheckedEventArgs(IsUpdateAvailable));
            }).Start();
        }

        /// <summary>
        /// Checks for a ClickOnce-Update
        /// </summary>
        /// <returns>True if update is available.</returns>
        public bool NeedsUpdate()
        {
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    return ApplicationDeployment.CurrentDeployment.CheckForUpdate();
            }
            catch (Exception) { }
            return false;
        }

        public event UpdateCheckedHandler OnUpdateChecked;

        #endregion Update


        #region Save/Load

        private static readonly string PROPERTIESDIRECTORY = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DSACharacterSheet");
        private static string PropertiesPath { get { return Path.Combine(PROPERTIESDIRECTORY, "config.xml"); } }

        /// <summary>
        /// Loads the Properties from the "PROPERTIESDIRECTORY".
        /// </summary>
        /// <returns>The Loaded Properties.</returns>
        public static Properties Load()
        {
            if (!Directory.Exists(PROPERTIESDIRECTORY))
                Directory.CreateDirectory(PROPERTIESDIRECTORY);

            try
            {
                using (var stream = new FileStream(PropertiesPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Properties));
                    Properties temp = (Properties)serializer.Deserialize(stream);
                    return temp;
                }
            }
            catch (IOException)
            {
                return new Properties();
            }
        }

        /// <summary>
        /// Saves the Properties to the "PROPERTIESDIRECTORY"
        /// </summary>
        public void Save()
        {
            if (!Directory.Exists(PROPERTIESDIRECTORY))
                Directory.CreateDirectory(PROPERTIESDIRECTORY);

            try
            {
                using (var stream = new StreamWriter(PropertiesPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Properties));
                    serializer.Serialize(stream, this);
                }
            }
            catch (IOException) { }
        }

        #endregion Save/Load
    }
}
