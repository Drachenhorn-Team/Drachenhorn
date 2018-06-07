using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Settings;
using Drachenhorn.Xml;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using System;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Easy.Logger.Interfaces;

namespace Drachenhorn.Desktop.UserSettings
{
    [Serializable]
    public class Settings : BindableBase, ISettings
    {
        #region Properties

        [XmlIgnore]
        private bool _isNew = true;
        [XmlIgnore]
        public bool IsNew
        {
            get { return _isNew; }
            private set
            {
                if (_isNew == value)
                    return;
                _isNew = value;
                OnPropertyChanged();
            }

        }

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
            get { return SimpleIoc.Default.GetInstance<LanguageManager>().CurrentCulture; }
            set
            {
                var temp = SimpleIoc.Default.GetInstance<LanguageManager>();
                if (Equals(temp.CurrentCulture, value))
                    return;
                temp.CurrentCulture = value;
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
        private VisualThemeType _visualTheme;

        [XmlElement("VisualTheme")]
        public VisualThemeType VisualTheme
        {
            get { return _visualTheme; }
            set
            {
                if (_visualTheme == value)
                    return;
                _visualTheme = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private bool? _showConsole = false;
        [XmlElement("ShowConsole")]
        public bool? ShowConsole
        {
            get { return _showConsole;}
            set
            {
                if (_showConsole == value)
                    return;
                _showConsole = value;
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
            var path = Path.Combine(Environment.CurrentDirectory, "commit");
            if (File.Exists(path))
                GitCommit = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "commit")).Replace("\r", "").Replace("\n", "");
            else
                GitCommit = "No Commit found";

            this.PropertyChanged += (sender, args) => { this.Save(); };
        }

        #endregion c'tor

        #region Save/Load

        private static readonly string PropertiesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Drachenhorn");
        private static string PropertiesPath => Path.Combine(PropertiesDirectory, "config.xml");

        /// <summary>
        /// Loads the Properties from the "PROPERTIESDIRECTORY".
        /// </summary>
        /// <returns>The Loaded Properties.</returns>
        public static Settings Load()
        {
            if (!Directory.Exists(PropertiesDirectory))
                Directory.CreateDirectory(PropertiesDirectory);
            
            SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Info("Loading settings.");

            try
            {
                using (var stream = new FileStream(PropertiesPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    var temp = (Settings)serializer.Deserialize(stream);
                    temp.IsNew = false;

                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Info("Finished loading Settings.");

                    return temp;
                }
            }
            catch (IOException)
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Warn("Settings not found. Generating new.");
                return new Settings();
            }
            catch (InvalidOperationException)
            {
                var service = SimpleIoc.Default.GetInstance<IDialogService>();
                service.ShowMessage("%Notification.Settings.Corrupted", "%Notification.Header.Error");

                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Warn("Settings corrupted. Generating new.");
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

            SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Debug("Saving settings.");

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