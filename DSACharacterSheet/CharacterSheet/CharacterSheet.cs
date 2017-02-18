using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.CharacterSheet.Enums;
using DSACharacterSheet.Dialogs;

namespace DSACharacterSheet.CharacterSheet
{
    [Serializable]
    public class CharacterSheet : INotifyPropertyChanged
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        [XmlIgnore]
        private RaceInformation _race;
        [XmlElement("Race")]
        public RaceInformation Race
        {
            get { return _race; }
            set
            {
                if (_race == value)
                    return;
                _race = value;
                OnPropertyChanged("Race");
            }
        }

        [XmlIgnore]
        private CultureInformation _culture;
        [XmlElement("Culture")]
        public CultureInformation Culture
        {
            get { return _culture; }
            set
            {
                if (_culture == value)
                    return;
                _culture = value;
                OnPropertyChanged("Culture");
            }
        }

        [XmlIgnore]
        private ProfessionInformation _profession;
        [XmlElement("Profession")]
        public ProfessionInformation Profession
        {
            get { return _profession; }
            set
            {
                if (_profession == value)
                    return;
                _profession = value;
                OnPropertyChanged("Profession");
            }
        }

        [XmlIgnore]
        private CharacterInformation _characterInformation;
        [XmlElement("CharacterInformation")]
        public CharacterInformation CharacterInformation
        {
            get { return _characterInformation; }
            set
            {
                if (_characterInformation == value)
                    return;
                _characterInformation = value;
                OnPropertyChanged("CharacterInformation");
            }
        }

        #endregion Properties

        #region Save/Load

        [XmlIgnore]
        private string _filePath;
        [XmlIgnore]
        protected string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath == value)
                    return;
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        public static CharacterSheet Load(String path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterSheet));
                    CharacterSheet temp = (CharacterSheet)serializer.Deserialize(stream);
                    temp.FilePath = path;
                    return temp;
                }
            }
            catch (IOException e)
            {
                var window = new ExceptionMessageBox(e, "Die Datei \"" + path + "\" kann nicht geöffnet werden.");
                window.Show();
            }
            return null;
        }

        public void Save()
        {
            if (!String.IsNullOrEmpty(FilePath))
                Save(FilePath);
        }

        public void Save(String path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterSheet));
                    serializer.Serialize(writer, this);
                }
            }
            catch (IOException e)
            {
                var window = new ExceptionMessageBox(e, "Die Datei \"" + path + "\" kann nicht gespeichert werden.");
                window.Show();
            }
        }

        #endregion Save/Load

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}
