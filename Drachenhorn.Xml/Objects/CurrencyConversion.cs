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
        private ObservableCollection<Currency> _currencies = new ObservableCollection<Currency>();
        [XmlElement("Currencies")]
        internal ObservableCollection<Currency> Currencies
        {
            get { return _currencies; }
            set
            {
                if (_currencies == value)
                    return;
                _currencies = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Operators

        /// <summary>
        ///    Gets the Currency with set name.
        /// </summary>
        /// <param name="currency">Name of the Currency.</param>
        /// <returns>Currency with fitting name.</returns>
        public Currency this[string currency] => Currencies.Where(x => x.Name == currency)?.First() ??
                                            throw new KeyNotFoundException("Unable to find currency: " + currency);

        #endregion Operators

        #region Conversion

        public string ToString(int amound, string currency)
        {
            return this[currency].ToString(amound, 'p');
        }

        #endregion Conversion
    }
}
