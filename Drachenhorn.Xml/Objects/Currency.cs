using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Objects
{
    [Serializable]
    public class Currency : BindableBase
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
        private ObservableCollection<CurrencyPart> _currencyParts = new ObservableCollection<CurrencyPart>();
        [XmlElement("CurrencyPart")]
        public ObservableCollection<CurrencyPart> CurrencyParts
        {
            get => _currencyParts;
            set
            {
                if (_currencyParts == value)
                    return;
                _currencyParts = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Conversion

        public string ToString(int amound)
        {
            throw new NotImplementedException();
        }

        #endregion Conversion
    }
}
