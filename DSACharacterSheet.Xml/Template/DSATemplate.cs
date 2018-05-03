using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Exceptions;
using DSACharacterSheet.Xml.Sheet;
using DSACharacterSheet.Xml.Sheet.Common;

namespace DSACharacterSheet.Xml.Template
{
    [Serializable]
    public class DSATemplate : BindableBase
    {
        [XmlIgnore]
        private ObservableCollection<RaceInformation> _races;

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


        #region c'tor

        public DSATemplate()
        {
            _fileName = "New";
        }

        #endregion c'tor

        #region Save/Load

        private static string BaseDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "DSACharacterSheet", "Templates");
            }
        }

        [XmlIgnore]
        private string _fileName;

        [XmlIgnore]
        public string FilePath
        {
            get { return Path.Combine(BaseDirectory, _fileName + ".dsat"); }
            private set
            {
                if (value.StartsWith(BaseDirectory))
                    _fileName = value.Replace(BaseDirectory, "").Replace(".dsat", "");
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
            var path = Path.Combine(BaseDirectory, fileName + ".dsat");

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
                    if (!file.EndsWith(".dsat"))
                        continue;

                    result.Add(new FileInfo(file).Name.Replace(".dsat", ""));
                }

                return result;
            }
        }

        #endregion Static
    }
}
