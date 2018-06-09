using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Drachenhorn.Xml.Exceptions;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.CombatInfo;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.InventoryInfo;
using Drachenhorn.Xml.Sheet.Skills;
using Attribute = Drachenhorn.Xml.Sheet.Skills.Attribute;

namespace Drachenhorn.Xml.Sheet
{
    /// <summary>
    /// Object containing all data for a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class CharacterSheet : ChildChangedBase, ISavable
    {
        #region Properties

        [XmlIgnore]
        private Characteristics _characteristics = new Characteristics();
        /// <summary>
        /// Gets or sets the Characteristics.
        /// </summary>
        /// <value>
        /// The Characteristics.
        /// </value>
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
        /// <summary>
        /// Gets or sets the GPBase.
        /// </summary>
        /// <value>
        /// The GPBase.
        /// </value>
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
        /// <summary>
        /// Gets or sets the CoatOfArms.
        /// </summary>
        /// <value>
        /// The CoatOfArms.
        /// </value>
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
        /// <summary>
        /// Gets or sets the (Dis-)Advantages.
        /// </summary>
        /// <value>
        /// The (Dis-)Advantages.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Attributes.
        /// </summary>
        /// <value>
        /// The Attributes.
        /// </value>
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
        /// <summary>
        /// Gets or sets the BaseValues.
        /// </summary>
        /// <value>
        /// The BaseValues.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Adventure Points.
        /// </summary>
        /// <value>
        /// The Adventure Points.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Skills.
        /// </summary>
        /// <value>
        /// The Skills.
        /// </value>
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
        /// <summary>
        /// Gets or sets the Special Skills.
        /// </summary>
        /// <value>
        /// The Special Skills.
        /// </value>
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
        private Inventory _inventory = new Inventory();
        /// <summary>
        /// Gets or sets the Inventory.
        /// </summary>
        /// <value>
        /// The Inventory.
        /// </value>
        [XmlElement("Inventory")]
        public Inventory Inventory
        {
            get { return _inventory; }
            set
            {
                if (_inventory == value)
                    return;
                _inventory = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region c'tor

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSheet"/> class.
        /// </summary>
        public CharacterSheet()
        {
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != "HasChanged" &&
                    args.PropertyName != "FilePath")
                    HasChanged = true;
            };
            ChildChanged += (sender, args) => { HasChanged = true; };
        }

        #endregion c'tor

        #region Save/Load

        /// <summary>
        /// The extension of a CharacterSheet File
        /// </summary>
        public static readonly string Extension = ".dsac";

        [XmlIgnore]
        private bool _hasChanged;
        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has changed; otherwise, <c>false</c>.
        /// </value>
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
        /// <summary>
        /// Gets the Current File Path.
        /// </summary>
        /// <value>
        /// The Current File Path.
        /// </value>
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

        /// <summary>
        /// Saves this Sheet.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if successful; otherwise, <c>false</c>
        /// </returns>
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

        /// <inheritdoc />
        void ISavable.Save(string path)
        {
            Save(path);
        }

        #endregion Save/Load
    }
}