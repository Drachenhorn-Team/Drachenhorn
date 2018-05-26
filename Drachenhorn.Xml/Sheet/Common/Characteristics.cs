using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Objects;
using Drachenhorn.Xml.Sheet.Enums;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    /// The Characteristics of a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class Characteristics : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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
        private string _family;
        /// <summary>
        /// Gets or sets the family.
        /// </summary>
        /// <value>
        /// The family.
        /// </value>
        [XmlAttribute("Family")]
        public string Family
        {
            get { return _family; }
            set
            {
                if (_family == value)
                    return;
                _family = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _placeOfBirth;
        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        /// <value>
        /// The place of birth.
        /// </value>
        [XmlAttribute("PlaceOfBirth")]
        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set
            {
                if (_placeOfBirth == value)
                    return;
                _placeOfBirth = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private DSADate _birthDate = new DSADate();
        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        [XmlElement("BirthDate")]
        public DSADate BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate == value)
                    return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private Gender _gender;
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [XmlAttribute("Gender")]
        public Gender Gender
        {
            get { return _gender; }
            set
            {
                if (_gender == value)
                    return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private uint _age;
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        [XmlAttribute("Age")]
        public uint Age
        {
            get { return _age; }
            set
            {
                if (_age == value)
                    return;
                _age = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _height;
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [XmlAttribute("Height")]
        public double Height
        {
            get { return _height; }
            set
            {
                if (Math.Abs(_height - value) < Double.Epsilon)
                    return;
                _height = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _weight;
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [XmlAttribute("Weight")]
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (Math.Abs(_weight - value) < Double.Epsilon)
                    return;
                _weight = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _hairColor;
        /// <summary>
        /// Gets or sets the color of the hair.
        /// </summary>
        /// <value>
        /// The color of the hair.
        /// </value>
        [XmlAttribute("HairColor")]
        public string HairColor
        {
            get { return _hairColor; }
            set
            {
                if (_hairColor == value)
                    return;
                _hairColor = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _eyeColor;
        /// <summary>
        /// Gets or sets the color of the eye.
        /// </summary>
        /// <value>
        /// The color of the eye.
        /// </value>
        [XmlAttribute("EyeColor")]
        public string EyeColor
        {
            get { return _eyeColor; }
            set
            {
                if (_eyeColor == value)
                    return;
                _eyeColor = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private uint _socialStatus;
        /// <summary>
        /// Gets or sets the social status.
        /// </summary>
        /// <value>
        /// The social status.
        /// </value>
        [XmlAttribute("SocialStatus")]
        public uint SocialStatus
        {
            get { return _socialStatus; }
            set
            {
                if (_socialStatus == value)
                    return;
                _socialStatus = value;
                OnPropertyChanged();
            }
        }
        
        [XmlIgnore]
        private string _other;
        /// <summary>
        /// Gets or sets the other information.
        /// </summary>
        /// <value>
        /// The other information.
        /// </value>
        [XmlAttribute("Other")]
        public string Other
        {
            get { return _other; }
            set
            {
                if (_other == value)
                    return;
                _other = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [XmlAttribute("Title")]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private RaceInformation _race = new RaceInformation();
        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>
        /// The race.
        /// </value>
        [XmlElement("Race")]
        public RaceInformation Race
        {
            get { return _race; }
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
        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
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
        /// <summary>
        /// Gets or sets the profession.
        /// </summary>
        /// <value>
        /// The profession.
        /// </value>
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

        #endregion Properties
    }
}