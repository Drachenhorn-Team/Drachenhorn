using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Interfaces;

namespace DSACharacterSheet.Xml.Sheet.Common
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
                OnPropertyChanged();
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
        private ObservableCollection<BonusValue> _baseValues;

        [XmlElement("BaseValue")]
        public ObservableCollection<BonusValue> BaseValues
        {
            get { return _baseValues; }
            set
            {
                if (_baseValues == value)
                    return;
                _baseValues = value;
                OnPropertyChanged();
            }
        }



        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            var baseValues = "";
            foreach (var baseValue in BaseValues)
            {
                baseValues += baseValue.Name + ": " + baseValue.Value + "\n";
            }
            if (!string.IsNullOrEmpty(baseValues)) result.Add("%Info.BaseValues", baseValues);


            return result;
        }
    }
}