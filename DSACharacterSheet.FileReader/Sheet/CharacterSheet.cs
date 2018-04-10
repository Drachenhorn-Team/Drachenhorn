using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Exceptions;
using DSACharacterSheet.FileReader.Sheet.Advantages;
using DSACharacterSheet.FileReader.Sheet.CombatInfo;
using DSACharacterSheet.FileReader.Sheet.Common;
using DSACharacterSheet.FileReader.Sheet.Skills;
using Attribute = DSACharacterSheet.FileReader.Sheet.Skills.Attribute;

namespace DSACharacterSheet.FileReader.Sheet
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
                OnPropertyChanged();
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
                if (Math.Abs(_gpBase - value) < double.Epsilon)
                    return;
                _gpBase = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private RaceInformation _race = new RaceInformation();
        [XmlElement("Race")]
        public RaceInformation Race
        {
            get { return _race;}
            set
            {
                if (_race == value)
                    return;
                _race = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private CoatOfArms _coatOfArms = new CoatOfArms();
        [XmlElement("CoatOfArms")]
        public CoatOfArms CoatOfArms
        {
            get { return _coatOfArms; }
            set
            {
                if (_coatOfArms == value)
                    return;
                _coatOfArms = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<Attribute> _attributes = new ObservableCollection<Attribute>();
        [XmlElement("Attribute")]
        public ObservableCollection<Attribute> Attributes
        {
            get { return _attributes; }
            set
            {
                if (_attributes == value)
                    return;
                _attributes = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();
        [XmlElement("Skill")]
        public ObservableCollection<Skill> Skills
        {
            get { return _skills; }
            set
            {
                if (_skills == value)
                    return;
                _skills = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<SpecialSkill> _specialSkills = new ObservableCollection<SpecialSkill>();
        [XmlElement("SpecialSkill")]
        public ObservableCollection<SpecialSkill> SpecialSkills
        {
            get { return _specialSkills; }
            set
            {
                if (_specialSkills == value)
                    return;
                _specialSkills = value;
                OnPropertyChanged();
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
        private bool _isChanged = false;

        [XmlIgnore]
        public bool IsChanged
        {
            get { return _isChanged; }
            private set
            {
                if (_isChanged == value)
                    return;
                _isChanged = value;
                OnPropertyChanged();
            }
        }

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
                OnPropertyChanged();
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
                    var serializer = new XmlSerializer(typeof(CharacterSheet));
                    var temp = (CharacterSheet)serializer.Deserialize(stream);
                    temp.FilePath = path;
                    return temp;
                }
            }
            catch (IOException e)
            {
                throw new SheetLoadingException(path, e);
            }
        }


        public bool Save()
        {
            return !string.IsNullOrEmpty(FilePath) && Save(FilePath);
        }

        /// <summary>
        /// Saves the current CharacterSheet to a selected path.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <exception cref="SheetSavingException"/>
        public bool Save(string path)
        {
            try
            {
                using (var stream = new StreamWriter(path))
                {
                    var serializer = new XmlSerializer(typeof(CharacterSheet));
                    serializer.Serialize(stream, this);
                }

                return true;
            }
            catch (IOException e)
            {
                throw new SheetSavingException(path, e);
            }
        }

        #endregion Save/Load
    }
}
