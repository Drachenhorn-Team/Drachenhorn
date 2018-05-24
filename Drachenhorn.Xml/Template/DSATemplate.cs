using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Drachenhorn.Xml.Data.AP;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Common;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    /// DSA Templating Class
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class DSATemplate : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private APTable _apTable = new APTable();
        /// <summary>
        /// Gets or sets the AP-Table.
        /// </summary>
        /// <value>
        /// The AP-Table.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Races.
        /// </summary>
        /// <value>
        /// The Races.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Cultures.
        /// </summary>
        /// <value>
        /// The Cultures.
        /// </value>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DSATemplate"/> class.
        /// </summary>
        public DSATemplate()
        {
            _fileName = "New";


            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "HasChanged") HasChanged = true;
            };
            ChildChanged += (sender, args) =>
            {
                HasChanged = true;
            };
        }

        #endregion c'tor

        #region Save/Load

        /// <summary>
        /// Gets the Template BaseDirectory.
        /// </summary>
        /// <value>
        /// The Template BaseDirectory.
        /// </value>
        public static string BaseDirectory
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Drachenhorn", "Templates");
            }
        }

        /// <summary>
        /// The Template Extension
        /// </summary>
        public static readonly string Extension = ".dsat";

        [XmlIgnore]
        private string _fileName;

        /// <summary>
        /// Gets the Current Template FilePath.
        /// </summary>
        /// <value>
        /// The Current Template FilePath.
        /// </value>
        [XmlIgnore]
        public string FilePath
        {
            get
            {
                return Path.Combine(BaseDirectory, _fileName + Extension);
            }
            private set
            {
                if (value.StartsWith(BaseDirectory))
                    _fileName = value.Replace(BaseDirectory, "").Replace("\\", "").Replace(Extension, "");
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
                temp.HasChanged = false;

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
                this.HasChanged = false;
            }

            return true;
        }

        #endregion Save/Load

        #region HasChanged

        [XmlIgnore]
        private bool _hasChanged;
        [XmlIgnore]
        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                if (_hasChanged == value)
                    return;
                _hasChanged = value;
                OnPropertyChanged();
            }
        }

        #endregion HasChanged

        #region Static

        /// <summary>
        /// Gets the available templates.
        /// </summary>
        /// <value>
        /// The available templates.
        /// </value>
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
