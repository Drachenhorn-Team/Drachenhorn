using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Common
{
    public class ProfessionInformation : BindableBase
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
    }
}
