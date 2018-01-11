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
    public class BaseValues : BindableBase
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private BaseValue _magicResistance = new BaseValue();
        [XmlElement("MagicResistance")]
        public BaseValue MagicResistance
        {
            get { return _magicResistance; }
            set
            {
                if (_magicResistance == value)
                    return;
                _magicResistance = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}
