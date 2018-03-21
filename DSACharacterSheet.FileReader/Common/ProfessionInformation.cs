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
    public class ProfessionInformation : BindableBase, IInfoObject
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
        private string _description;
        [XmlAttribute("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
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

        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Description)) result.Add("%Info.Description", Description);
            if (Math.Abs(GPCost) > Double.Epsilon) result.Add("%Info.GPCost", GPCost.ToString(CultureInfo.CurrentCulture));

            return result;
        }
    }
}
