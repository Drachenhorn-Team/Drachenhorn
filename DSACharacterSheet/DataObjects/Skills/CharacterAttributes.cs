using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.DataObjects.Skills
{
    [Serializable]
    public class CharacterAttributes : INotifyPropertyChanged
    {
        #region Properties

        [XmlIgnore]
        private Attribute _courage = new Attribute();
        [XmlElement("Courage")]
        public Attribute Courage
        {
            get { return _courage; }
            set
            {
                if (_courage == value)
                    return;
                _courage = value;
                OnPropertyChanged("Courage");
            }
        }

        [XmlIgnore]
        private Attribute _wisdom = new Attribute();
        [XmlElement("Wisdom")]
        public Attribute Wisdom
        {
            get { return _wisdom; }
            set
            {
                if (_wisdom == value)
                    return;
                _wisdom = value;
                OnPropertyChanged("Wisdom");
            }
        }

        [XmlIgnore]
        private Attribute _intuition = new Attribute();
        [XmlElement("Intuition")]
        public Attribute Intuition
        {
            get { return _intuition; }
            set
            {
                if (_intuition == value)
                    return;
                _intuition = value;
                OnPropertyChanged("Intuition");
            }
        }

        [XmlIgnore]
        private Attribute _charisma = new Attribute();
        [XmlElement("Charisma")]
        public Attribute Charisma
        {
            get { return _charisma; }
            set
            {
                if (_charisma == value)
                    return;
                _charisma = value;
                OnPropertyChanged("Charisma");
            }
        }

        [XmlIgnore]
        private Attribute _prestidigitation = new Attribute();
        [XmlElement("Prestidigitation")]
        public Attribute Prestidigitation
        {
            get { return _prestidigitation; }
            set
            {
                if (_prestidigitation == value)
                    return;
                _prestidigitation = value;
                OnPropertyChanged("Prestidigitation");
            }
        }

        [XmlIgnore]
        private Attribute _finesse = new Attribute();
        [XmlElement("Finesse")]
        public Attribute Finesse
        {
            get { return _finesse; }
            set
            {
                if (_finesse == value)
                    return;
                _finesse = value;
                OnPropertyChanged("Finesse");
            }
        }

        [XmlIgnore]
        private Attribute _constitution = new Attribute();
        [XmlElement("Constitution")]
        public Attribute Constitution
        {
            get { return _constitution; }
            set
            {
                if (_constitution == value)
                    return;
                _constitution = value;
                OnPropertyChanged("Constitution");
            }
        }

        [XmlIgnore]
        private Attribute _physicalStrength = new Attribute();
        [XmlElement("PhysicalStrength")]
        public Attribute PhysicalStrength
        {
            get { return _physicalStrength; }
            set
            {
                if (_physicalStrength == value)
                    return;
                _physicalStrength = value;
                OnPropertyChanged("PhysicalStrength");
            }
        }

        [XmlIgnore]
        private Attribute _speed = new Attribute();
        [XmlElement("Speed")]
        public Attribute Speed
        {
            get { return _speed; }
            set
            {
                if (_speed == value)
                    return;
                _speed = value;
                OnPropertyChanged("Speed");
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
