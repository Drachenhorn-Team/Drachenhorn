using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Objects
{
    [Serializable]
    public class CurrencyConversion : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private ObservableCollection<Currency> _conversions = new ObservableCollection<Currency>();
        [XmlElement("Conversions")]
        internal ObservableCollection<Currency> Conversions
        {
            get { return _conversions; }
            set
            {
                if (_conversions == value)
                    return;
                _conversions = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Operators

        public Currency this[string currency] => Conversions.Where(x => x.Name == currency)?.First() ??
                                            throw new KeyNotFoundException("Unable to find currency: " + currency);

        #endregion Operators

        #region Conversion

        public string ToString(int amound, string currency)
        {
            return this[currency].ToString(amound);
        }

        #endregion Conversion
    }
}
