using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Advantages
{
    [Serializable]
    public class AdvantageBase : BindableBase
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
                OnPropertyChanged("Name");
            }
        }

        [XmlIgnore]
        private string _specialisation;
        [XmlAttribute("Specialisation")]
        public string Specialisation
        {
            get { return _specialisation; }
            set
            {
                if (_specialisation == value)
                    return;
                _specialisation = value;
                OnPropertyChanged("Specialisation");
            }
        }

        [XmlIgnore]
        private double _gpCost;
        [XmlAttribute("GPCost")]
        public double GPCost
        {
            get { return _gpCost; }
            set
            {
                if (_gpCost == value)
                    return;
                _gpCost = value;
                OnPropertyChanged("GPCost");
            }
        }

        #endregion Properties
    }
}
