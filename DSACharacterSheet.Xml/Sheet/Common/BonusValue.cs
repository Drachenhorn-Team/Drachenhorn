using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Calculation;

namespace DSACharacterSheet.Xml.Sheet.Common
{
    [Serializable]
    public class BonusValue : ChildChangedBase, IFormulaKeyItem
    {
        [XmlIgnore]
        private string _key;
        [XmlAttribute("Key")]
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == value)
                    return;
                _key = value;
                OnPropertyChanged();
            }
        }
        
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
        private double _value;
        [XmlAttribute("Value")]
        public double Value
        {
            get { return _value; }
            set
            {
                if (Math.Abs(_value - value) < Double.Epsilon)
                    return;
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}
