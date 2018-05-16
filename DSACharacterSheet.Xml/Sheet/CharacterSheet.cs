using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Exceptions;
using DSACharacterSheet.Xml.Sheet.CombatInfo;
using DSACharacterSheet.Xml.Sheet.Common;
using DSACharacterSheet.Xml.Sheet.Skills;
using Attribute = DSACharacterSheet.Xml.Sheet.Skills.Attribute;

namespace DSACharacterSheet.Xml.Sheet
{
    [Serializable]
    public class CharacterSheet : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private Characteristics _characteristics = new Characteristics();

        [XmlElement("Characteristics")]
        public Characteristics Characteristics
        {
            get { return _characteristics; }
            set
            {
                if (_characteristics == value)
                    return;
                _characteristics = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _gpBase;

        [XmlAttribute("GPBase")]
        public double GpBase
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
        private ObservableCollection<DisAdvantage> _disAdvantages = new ObservableCollection<DisAdvantage>();

        [XmlElement("DisAdvantage")]
        public ObservableCollection<DisAdvantage> DisAdvantages
        {
            get { return _disAdvantages; }
            set
            {
                if (_disAdvantages == value)
                    return;
                _disAdvantages = value;
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
        private ObservableCollection<BaseValue> _baseValues = new ObservableCollection<BaseValue>();

        [XmlElement("BaseValue")]
        public ObservableCollection<BaseValue> BaseValues
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


        #region c'tor

        public CharacterSheet()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "HasChanged") HasChanged = true;
            };
            ChildChanged += (sender, args) => { HasChanged = true; };
        }

        #endregion c'tor

        #region Save/Load

        public static readonly string Extension = ".dsac";

        [XmlIgnore]
        private bool _hasChanged;

        [XmlIgnore]
        public bool HasChanged
        {
            get { return _hasChanged; }
            private set
            {
                if (_hasChanged == value)
                    return;
                _hasChanged = value;
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

                    SetFormulaParent(ref temp);

                    temp.HasChanged = false;

                    return temp;
                }
            }
            catch (IOException e)
            {
                throw new SheetLoadingException(path, e);
            }
        }

        private static void SetFormulaParent(ref CharacterSheet sheet)
        {
            if (sheet == null)
                return;

            foreach (var baseValue in sheet.BaseValues)
                baseValue.Formula.ParentSheet = sheet;
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
                    this.HasChanged = false;
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