using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.CharacterSheet.Enums;

namespace DSACharacterSheet.CharacterSheet
{
    [Serializable]
    public class CharacterInformation : INotifyPropertyChanged
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
        private ushort _age;
        [XmlAttribute("Age")]
        public ushort Age
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
        private ushort _weight;
        [XmlAttribute("Weight")]
        public ushort Weight
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
        private ushort _socialStatus;
        [XmlAttribute("SocialStatus")]
        public ushort SocialStatus
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
