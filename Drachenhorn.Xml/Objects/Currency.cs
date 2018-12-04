using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        /// <summary>
        ///     Forms String out of amount.
        /// </summary>
        /// <param name="amount">Amount of money to be converted</param>
        /// <param name="format">
        ///     Format if the string.
        ///     'p' for maximal parts e.g. '1 € 20 c'
        ///     'f' for minimal parts e.g. '120 c'
        /// </param>
        /// <param name="currency">Currency to be converted to.</param>
        /// <returns></returns>
        public static string ToString(int amount, char format, Currency currency)
        {
            throw new NotImplementedException();
        }


        private static string ToMaximumParts(int amount, Currency currency)
        {
            string result = "";

            var currs = from x in currency.CurrencyParts orderby x.Value descending select x;

            foreach (var curr in currs)
            {
                if ((double) amount / curr.Value > 0)
                {
                    result = curr.ToString(amount / curr.Value);
                    amount = amount % curr.Value;
                }
            }

            return result;
        }

        #endregion Conversion
    }
}
