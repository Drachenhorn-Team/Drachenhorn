using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Calculation;
using DSACharacterSheet.Xml.Interfaces;

namespace DSACharacterSheet.Xml.Sheet.Skills
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

            if (!string.IsNullOrEmpty(Name))
                result.Add("%Info.Name", Name);
            if (Math.Abs(Modifier) > Double.Epsilon)
                result.Add("%Info.Modifier", Modifier.ToString(CultureInfo.CurrentCulture));
            if (Math.Abs(StartValue) > Double.Epsilon)
                result.Add("%Info.StartValue", StartValue.ToString(CultureInfo.CurrentCulture));
            if (Math.Abs(CurrentValue) > Double.Epsilon)
                result.Add("%Info.CurrentValue", CurrentValue.ToString(CultureInfo.CurrentCulture));

            return result;
        }

        #endregion InfoObject
    }
}