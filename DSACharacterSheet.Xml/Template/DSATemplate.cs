using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Data.AP;
using DSACharacterSheet.Xml.Sheet.Common;

namespace DSACharacterSheet.Xml.Template
{
    [Serializable]
    public class DSATemplate : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private APTable _apTable = new APTable();
        [XmlElement("APTable")]
        public APTable APTable
        {
            get { return _apTable; }
            set
            {
                if (_apTable == value)
                    return;
                _apTable = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<RaceInformation> _races = new ObservableCollection<RaceInformation>();
        [XmlElement("Race")]
        public ObservableCollection<RaceInformation> Races
        {
            get { return _races; }
            set
            {
                if (_races == value)
                    return;
                _races = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<CultureInformation> _cultures = new ObservableCollection<CultureInformation>();
        [XmlElement("Culture")]
        public ObservableCollection<CultureInformation> Cultures
        {
            get { return _cultures; }
            set
            {
                if (_cultures == value)
                    return;
                _cultures = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        public DSATemplate()
        {
            _fileName = "New";
        }

        #endregion c'tor

        #region Save/Load

        public static string BaseDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "DSACharacterSheet", "Templates");
            }
        }

        public static readonly string Extension = ".dsat";

        [XmlIgnore]
        private string _fileName;

        [XmlIgnore]
        public string FilePath
        {
            get { return Path.Combine(BaseDirectory, _fileName + Extension); }
            private set
            {
                if (value.StartsWith(BaseDirectory))
                    _fileName = value.Replace(BaseDirectory, "").Replace(Extension, "");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Loads a CharacterSheet from a selected path.
        /// </summary>
        /// <param name="fileName">Name of Template File</param>
        /// <returns>Loaded CharacterSheet</returns>
        public static DSATemplate Load(string fileName)
        {
            var path = Path.Combine(BaseDirectory, fileName + Extension);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var serializer = new XmlSerializer(typeof(DSATemplate));
                var temp = (DSATemplate) serializer.Deserialize(stream);
                temp.FilePath = path;

                return temp;
            }
        }

        /// <summary>
        /// Saves the current CharacterSheet to a selected path.
        /// </summary>
        public bool Save()
        {
            using (var stream = new StreamWriter(FilePath))
            {
                var serializer = new XmlSerializer(typeof(DSATemplate));
                serializer.Serialize(stream, this);
            }

            return true;
        }

        #endregion Save/Load

        #region Static

        public static IEnumerable<string> AvailableTemplates
        {
            get
            {
                if (!Directory.Exists(BaseDirectory))
                    Directory.CreateDirectory(BaseDirectory);

                var result = new List<string>();
                var files = Directory.GetFiles(BaseDirectory);

                foreach (var file in files)
                {
                    if (!file.EndsWith(Extension))
                        continue;

                    result.Add(new FileInfo(file).Name.Replace(Extension, ""));
                }

                return result;
            }
        }

        #endregion Static
    }
}
