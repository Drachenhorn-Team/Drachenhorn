using System;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Sheet.Skills
{
    [Serializable]
    public class Attribute : BindableBase
    {
        [XmlIgnore]
        private double _startValue;
        [XmlAttribute("StartValue")]
        public double StartValue
        {
            get { return _startValue; }
            set
            {
                if (_startValue == value)
                    return;
                _startValue = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _modifier;
        [XmlAttribute("Modifier")]
        public double Modifier
        {
            get { return _modifier; }
            set
            {
                if (_modifier == value)
                    return;
                _modifier = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _currentValue;
        [XmlAttribute("CurrentValue")]
        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_currentValue == value)
                    return;
                _currentValue = value;
                OnPropertyChanged();
            }
        }
    }
}
