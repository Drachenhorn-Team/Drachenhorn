using System;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Calculation;

namespace DSACharacterSheet.Xml.Sheet.Skills
{
    [Serializable]
    public class BaseValue : BindableBase
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
                OnPropertyChanged(null);
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
                OnPropertyChanged(null);
            }
        }

        [XmlIgnore]
        private Formula _formula = new Formula();

        [XmlElement("Formula")]
        public Formula Formula
        {
            get { return _formula; }
            set
            {
                if (_formula == value)
                    return;
                _formula = value;
                OnPropertyChanged(null);
            }
        }

        [XmlIgnore]
        public double Value
        {
            //TODO: Implementieren
            get { return 0; }
        }

        #endregion Properties
    }
}