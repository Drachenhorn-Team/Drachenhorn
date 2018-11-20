using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.Settings;
using Drachenhorn.Xml;
using Drachenhorn.Xml.Template;
using Easy.Logger.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro;
using Squirrel;

namespace Drachenhorn.Desktop.UserSettings
{
    [Serializable]
    public class Settings : BindableBase, ISettings
    {
        #region c'tor

        public Settings()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "commit");
            if (File.Exists(path))
                GitCommit = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "commit")).Replace("\r", "")
                    .Replace("\n", "");
            else
                GitCommit = "No Commit found";

            PropertyChanged += (sender, args) => { Save(); };
        }

        #endregion c'tor

        #region Properties

        [XmlIgnore] private bool _isNew = true;

        [XmlIgnore]
        public bool IsNew
        {
            get => _isNew;
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
            get => CurrentCulture.Name;
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
            get => SimpleIoc.Default.GetInstance<LanguageManager>().CurrentCulture;
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
                DesktopBridge.Helpers helpers = new DesktopBridge.Helpers();
                if (helpers.IsRunningAsUwp())
                    return "Version managed by Windows-Store";

                try
                {
                    using (var mgr = new UpdateManager(null))
                        return mgr.CurrentlyInstalledVersion().ToString();
                }
                catch(Exception e)
                {
                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>().Debug("Unable to load Squirrel Version.", e);
                }

                return "Application not installed.";
            }
        }

        [XmlIgnore] private string _gitCommit;

        [XmlIgnore]
        public string GitCommit
        {
            get => _gitCommit;
            private set
            {
                if (_gitCommit == value)
                    return;
                _gitCommit = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        public string GitCommitLink
        {
            get
            {
                if (GitCommit.Contains(" "))
                    return null;
                return @"https://github.com/lightlike/Drachenhorn/commit/" + GitCommit;
            }
        }

        [XmlIgnore] private VisualThemeType _visualTheme;

        [XmlElement("VisualTheme")]
        public VisualThemeType VisualTheme
        {
            get => _visualTheme;
            set
            {
                if (_visualTheme == value)
                    return;
                _visualTheme = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private string _accentColor;

        [XmlElement("AccentColor")]
        public string AccentColor
        {
            get => _accentColor;
            set
            {
                if (_accentColor == value)
                    return;
                _accentColor = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private bool? _showConsole = false;

        [XmlElement("ShowConsole")]
        public bool? ShowConsole
        {
            get => _showConsole;
            set
            {
                if (_showConsole == value)
                    return;
                _showConsole = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private TemplateMetadata _currentTemplate;

        [XmlElement("CurrentTemplate")]
        public TemplateMetadata CurrentTemplate
        {
            get => _currentTemplate;
            set
            {
                if (_currentTemplate == value)
                    return;
                _currentTemplate = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private bool _isUpdateAvailable;

        [XmlIgnore]
        public bool IsUpdateAvailable
        {
            get => _isUpdateAvailable;
            private set
            {
                if (_isUpdateAvailable == value)
                    return;
                _isUpdateAvailable = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Save/Load

        private static readonly string PropertiesDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Drachenhorn");

        private static string PropertiesPath => Path.Combine(PropertiesDirectory, "config.xml");

        /// <summary>
        ///     Loads the Properties from the "PROPERTIESDIRECTORY".
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
                    var temp = (Settings) serializer.Deserialize(stream);
                    temp.IsNew = false;

                    SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>()
                        .Debug("Finished loading Settings.");

                    return temp;
                }
            }
            catch (IOException)
            {
                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>()
                    .Warn("Settings not found. Generating new.");
                var s = new Settings();
                s.Save();
                return s;
            }
            catch (InvalidOperationException)
            {
                var service = SimpleIoc.Default.GetInstance<IDialogService>();
                service.ShowMessage("%Notification.Settings.Corrupted", "%Notification.Header.Error");

                SimpleIoc.Default.GetInstance<ILogService>().GetLogger<Settings>()
                    .Warn("Settings corrupted. Generating new.");
                var s = new Settings();
                s.Save();
                return s;
            }
        }

        /// <summary>
        ///     Saves the Properties to the "PROPERTIESDIRECTORY"
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
            catch (IOException)
            {
            }
        }

        #endregion Save/Load

        #region AccentColors

        public static IEnumerable<string> GetAccents()
        {
            return ThemeManager.Accents.Select(x => x.Name);
        }

        #endregion AccentColors
    }
}