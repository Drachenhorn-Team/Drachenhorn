using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Calculation;

namespace DSACharacterSheet.FileReader.Skills
{
    [Serializable]
    public class BaseValue : BindableBase
    {
        #region Properties

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
