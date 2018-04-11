using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Interfaces;

namespace DSACharacterSheet.FileReader.Sheet.Skills
{
    [Serializable]
    public class Attribute : BindableBase, IInfoObject
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
        private double _startValue;
        [XmlAttribute("StartValue")]
        public double StartValue
        {
            get { return _startValue; }
            set
            {
                if (_startValue == value)
                    return;
                _startValue = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _currentValue;
        [XmlAttribute("CurrentValue")]
        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_currentValue == value)
                    return;
                _currentValue = value;
                OnPropertyChanged();
            }
        }


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
