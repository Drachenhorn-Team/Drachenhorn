using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Objects
{
    [Serializable]
    public class CurrencyPart : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        [XmlAttribute("Name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private int _value;
        [XmlAttribute("Value")]
        public int Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;
                _value = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}
