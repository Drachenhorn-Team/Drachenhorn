using DSACharacterSheet.Core.Lang;
using System;
using System.Collections.Generic;
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
        public string CurrentCulture
        {
            get { return LanguageManager.CurrentCulture.DisplayName; }
            set
            {
                if (LanguageManager.CurrentCulture.DisplayName == value)
                    return;
                LanguageManager.CurrentCulture = LanguageManager.GetAllCultures().First(x => x.DisplayName == value);
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        public Properties()
        {
            this.PropertyChanged += (sender, args) => { this.Save(); };
        }

        #endregion c'tor


        #region Save/Load

        private static readonly string PROPERTIESDIRECTORY = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DSACharacterSheet");
        private static string PropertiesPath { get { return Path.Combine(PROPERTIESDIRECTORY, "config.xml"); } }

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
