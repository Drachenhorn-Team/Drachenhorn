using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Skills
{
    [Serializable]
    public class BaseValues : INotifyPropertyChanged
    {
        #region Properties

        [XmlIgnore]
        private BaseValue _healthPoints = new BaseValue();
        [XmlElement("HealthPoints")]
        public BaseValue HealthPoints
        {
            get { return _healthPoints; }
            set
            {
                if (_healthPoints == value)
                    return;
                _healthPoints = value;
                OnPropertyChanged("HealthPoints");
            }
        }

        [XmlIgnore]
        private BaseValue _endurance = new BaseValue();
        [XmlElement("Endurance")]
        public BaseValue Endurance
        {
            get { return _endurance; }
            set
            {
                if (_endurance == value)
                    return;
                _endurance = value;
                OnPropertyChanged("Endurance");
            }
        }

        [XmlIgnore]
        private BaseValue _astralEnergy = new BaseValue();
        [XmlElement("AstralEnergy")]
        public BaseValue AstralEnergy
        {
            get { return _astralEnergy; }
            set
            {
                if (_astralEnergy == value)
                    return;
                _astralEnergy = value;
                OnPropertyChanged("AstralEnergy");
            }
        }

        [XmlIgnore]
        private BaseValue _karmaEnergy = new BaseValue();
        [XmlElement("KarmaEnergy")]
        public BaseValue KarmaEnergy
        {
            get { return _karmaEnergy; }
            set
            {
                if (_karmaEnergy == value)
                    return;
                _karmaEnergy = value;
                OnPropertyChanged("KarmaEnergy");
            }
        }

        [XmlIgnore]
        private BaseValue _magicResistance = new BaseValue();
        [XmlElement("MagicResistance")]
        public BaseValue MagicResitance
        {
            get { return _magicResistance; }
            set
            {
                if (_magicResistance == value)
                    return;
                _magicResistance = value;
                OnPropertyChanged("MagicResistance");
            }
        }

        [XmlIgnore]
        private BaseValue _baseInitiative = new BaseValue();
        [XmlElement("BaseInitiative")]
        public BaseValue BaseInitiative
        {
            get { return _baseInitiative; }
            set
            {
                if (_baseInitiative == value)
                    return;
                _baseInitiative = value;
                OnPropertyChanged("BaseInitiative");
            }
        }

        [XmlIgnore]
        private BaseValue _baseAttack = new BaseValue();
        [XmlElement("BaseAttack")]
        public BaseValue BaseAttack
        {
            get { return _baseAttack; }
            set
            {
                if (_baseAttack == value)
                    return;
                _baseAttack = value;
                OnPropertyChanged("BaseAttack");
            }
        }

        [XmlIgnore]
        private BaseValue _baseParry = new BaseValue();
        [XmlElement("BaseParry")]
        public BaseValue BaseParry
        {
            get { return _baseParry; }
            set
            {
                if (_baseParry == value)
                    return;
                _baseParry = value;
                OnPropertyChanged("BaseParry");
            }
        }

        [XmlIgnore]
        private BaseValue _baseLongRangeAttack = new BaseValue();
        [XmlElement("BaseLongRangeAttack")]
        public BaseValue BaseLongRangeAttack
        {
            get { return _baseLongRangeAttack; }
            set
            {
                if (_baseLongRangeAttack == value)
                    return;
                _baseLongRangeAttack = value;
                OnPropertyChanged("BaseLongRangeAttack");
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
