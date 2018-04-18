using System;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Sheet.Enums;

namespace DSACharacterSheet.Xml.Sheet.Common
{
    [Serializable]
    public class CharacterInformation : BindableBase
    {
        #region Properties

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
                OnPropertyChanged("Gender");
            }
        }

        [XmlIgnore]
        private double _age;

        [XmlAttribute("Age")]
        public double Age
        {
            get { return _age; }
            set
            {
                if (_age == value)
                    return;
                _age = value;
                OnPropertyChanged("Age");
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
                if (_height == value)
                    return;
                _height = value;
                OnPropertyChanged("Height");
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
                if (_weight == value)
                    return;
                _weight = value;
                OnPropertyChanged("Weight");
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
                OnPropertyChanged("HairColor");
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
                OnPropertyChanged("EyeColor");
            }
        }

        [XmlIgnore]
        private string _appearance;

        [XmlAttribute("Appearance")]
        public string Appearance
        {
            get { return _appearance; }
            set
            {
                if (_appearance == value)
                    return;
                _appearance = value;
                OnPropertyChanged("Appearance");
            }
        }

        [XmlIgnore]
        private string _socialClass;

        [XmlAttribute("SocialClass")]
        public string SocialClass
        {
            get { return _socialClass; }
            set
            {
                if (_socialClass == value)
                    return;
                _socialClass = value;
                OnPropertyChanged("SocialClass");
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
                OnPropertyChanged("Title");
            }
        }

        [XmlIgnore]
        private double _socialStatus;

        [XmlAttribute("SocialStatus")]
        public double SocialStatus
        {
            get { return _socialStatus; }
            set
            {
                if (_socialStatus == value)
                    return;
                _socialStatus = value;
                OnPropertyChanged("SocialStatus");
            }
        }

        [XmlIgnore]
        private string _backgroundInformation;

        [XmlAttribute("BackgroundInformation")]
        public string BackgroundInformation
        {
            get { return _backgroundInformation; }
            set
            {
                if (_backgroundInformation == value)
                    return;
                _backgroundInformation = value;
                OnPropertyChanged("BackgroundInformation");
            }
        }

        #endregion Properties
    }
}