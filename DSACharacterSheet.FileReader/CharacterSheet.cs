using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Advantages;
using DSACharacterSheet.FileReader.Common;
using DSACharacterSheet.FileReader.Enums;
using DSACharacterSheet.FileReader.Skills;
using DSACharacterSheet.FileReader.Exceptions;
using DSACharacterSheet.FileReader.CombatInfo;

namespace DSACharacterSheet.FileReader
{
    [Serializable]
    public class CharacterSheet : BindableBase
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
        private double _gpBase;
        [XmlAttribute("GPBase")]
        public double GPBase
        {
            get { return _gpBase; }
            set
            {
                if (_gpBase == value)
                    return;
                _gpBase = value;
                OnPropertyChanged("GPBase");
            }
        }

        [XmlIgnore]
        private RaceInformation _race = new RaceInformation();
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
        private CultureInformation _culture = new CultureInformation();
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
        private ProfessionInformation _profession = new ProfessionInformation();
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
        private CharacterInformation _characterInformation = new CharacterInformation();
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

        [XmlIgnore]
        private SocialInformation _socialInformation = new SocialInformation();
        [XmlElement("SocialInformation")]
        public SocialInformation SocialInformation
        {
            get { return _socialInformation; }
            set
            {
                if (_socialInformation == value)
                    return;
                _socialInformation = value;
                OnPropertyChanged("SocialInformation");
            }
        }

        [XmlIgnore]
        private ObservableCollection<Advantage> _advantages = new ObservableCollection<Advantage>();
        [XmlElement("Advantage")]
        public ObservableCollection<Advantage> Advantages
        {
            get { return _advantages; }
            set
            {
                if (_advantages == value)
                    return;
                _advantages = value;
                OnPropertyChanged("Advantages");
            }
        }

        [XmlIgnore]
        private ObservableCollection<Disadvantage> _disadvantages = new ObservableCollection<Disadvantage>();
        [XmlElement("Disadvantage")]
        public ObservableCollection<Disadvantage> Disadvantages
        {
            get { return _disadvantages; }
            set
            {
                if (_disadvantages == value)
                    return;
                _disadvantages = value;
                OnPropertyChanged("Disadvantages");
            }
        }

        [XmlIgnore]
        private CharacterAttributes _attributes = new CharacterAttributes();
        [XmlElement("Attribute")]
        public CharacterAttributes Attributes
        {
            get { return _attributes; }
            set
            {
                if (_attributes == value)
                    return;
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        [XmlIgnore]
        private BaseValues _baseValues = new BaseValues();
        [XmlElement("BaseValue")]
        public BaseValues BaseValues
        {
            get { return _baseValues; }
            set
            {
                if (_baseValues == value)
                    return;
                _baseValues = value;
                OnPropertyChanged("BaseValues");
            }
        }

        [XmlIgnore]
        private AdventurePoints _adventurePoints = new AdventurePoints();
        [XmlElement("AdventurePoints")]
        public AdventurePoints AdventurePoints
        {
            get { return _adventurePoints; }
            set
            {
                if (_adventurePoints == value)
                    return;
                _adventurePoints = value;
                OnPropertyChanged("AdventurePoints");
            }
        }

        [XmlIgnore]
        private CombatInformation _combatInformation = new CombatInformation();
        [XmlElement("CombatInformation")]
        public CombatInformation CombatInformation
        {
            get { return _combatInformation; }
            set
            {
                if (_combatInformation == value)
                    return;
                _combatInformation = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Save/Load

        [XmlIgnore]
        private string _filePath;
        [XmlIgnore]
        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                if (_filePath == value)
                    return;
                _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        /// <summary>
        /// Loads a CharacterSheet from a selected path.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>Loaded CharacterSheet</returns>
        /// <exception cref="SheetLoadingException"/>
        public static CharacterSheet Load(string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterSheet));
                    CharacterSheet temp = (CharacterSheet)serializer.Deserialize(stream);
                    temp.FilePath = path;
                    return temp;
                }
            }
            catch (IOException e)
            {
                throw new SheetLoadingException(path, e);
            }
        }

        /// <summary>
        /// Saves the current CharacterSheet to a selected path.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <exception cref="SheetSavingException"/>
        public void Save(string path)
        {
            try
            {
                using (var stream = new StreamWriter(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterSheet));
                    serializer.Serialize(stream, this);
                }
            }
            catch (IOException e)
            {
                throw new SheetSavingException(path, e);
            }
        }

        #endregion Save/Load
    }
}
