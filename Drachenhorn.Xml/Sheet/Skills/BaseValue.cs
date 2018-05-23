using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Drachenhorn.Xml.Calculation;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Xml.Sheet.Skills
{
    [Serializable]
    public class BaseValue : CalculationValue, IInfoObject, IFormulaKeyItem
    {
        #region Properties

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
        public double Value
        {
            get { return CurrentValue; }
        }

        #endregion Properties


        #region InfoObject

        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Key))
                result.Add("%Info.Key", Key);
            if (!string.IsNullOrEmpty(Name))
                result.Add("%Info.Name", Name);

            GetInformation(ref result);

            return result;
        }

        #endregion InfoObject
    }
}