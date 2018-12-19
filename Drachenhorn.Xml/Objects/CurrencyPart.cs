using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Objects
{
    [Serializable]
    public class CurrencyPart : BindableBase
    {
        #region Properties

        [XmlIgnore] private string _name;

        [XmlIgnore] private string _symbol;

        [XmlIgnore] private int _value;

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

        /// <summary>
        ///     Symbol of the Currency. '%' will be replaced with the value.
        ///     If there is no '%' the Symbol will be added to the end.
        /// </summary>
        [XmlAttribute("Symbol")]
        public string Symbol
        {
            get => _symbol;
            set
            {
                if (_symbol == value)
                    return;
                _symbol = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Value compared to minimum of all Currencies.
        /// </summary>
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

        #endregion


        #region ToString

        public long Convert(long amount)
        {
            return amount / Value;
        }

        public string ToString(long amount)
        {
            amount = Convert(amount);

            if (Symbol.Contains("%")) return Symbol.Replace("%", amount.ToString());

            return amount + " " + Symbol;
        }

        #endregion ToString
    }
}