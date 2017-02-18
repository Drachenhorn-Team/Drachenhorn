using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSA_Character_Sheet.CharacterSheet
{
    [Serializable]
    public class CharacterSheet : INotifyPropertyChanged
    {
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
