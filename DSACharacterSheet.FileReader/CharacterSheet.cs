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
            get => _name;
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
            get => _gpBase;
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
            get => _race;
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
            get => _culture;
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
            get => _profession;
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
            get => _characterInformation;
            set
            {
                if (_characterInformation == value)
                    return;
                _characterInformation = value;
                OnPropertyChanged("CharacterInformation");
            }
        }

        [XmlIgnore]
        private CoatOfArms _coatOfArms = new CoatOfArms();
        [XmlElement("CoatOfArms")]
        public CoatOfArms CoatOfArms
        {
            get => _coatOfArms;
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
            get => _socialInformation;
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
            get => _advantages;
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
            get => _disadvantages;
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
            get => _attributes;
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
            get => _baseValues;
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
            get => _adventurePoints;
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
            get => _skills;
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
            get => _specialSkills;
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
            get => _combatInformation;
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
            get => _filePath;
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
