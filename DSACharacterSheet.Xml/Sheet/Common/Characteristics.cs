using System;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Objects;
using DSACharacterSheet.Xml.Sheet.Enums;

namespace DSACharacterSheet.Xml.Sheet.Common
{
    [Serializable]
    public class Characteristics : ChildChangedBase
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
        private string _family;
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

        #endregion Properties
    }
}