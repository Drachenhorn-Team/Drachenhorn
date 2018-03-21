using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Interfaces;

namespace DSACharacterSheet.FileReader.Common
{
    [Serializable]
    public class CultureInformation : BindableBase, IInfoObject
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
        private string _specification;
        [XmlAttribute("Specification")]
        public string Specification
        {
            get { return _specification; }
            set
            {
                if (_specification == value)
                    return;
                _specification = value;
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
                OnPropertyChanged("GPCost");
            }
        }

        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (Math.Abs(GPCost) > Double.Epsilon) result.Add("%Info.GPCost", GPCost.ToString(CultureInfo.CurrentCulture));

            return result;
        }
    }
}
