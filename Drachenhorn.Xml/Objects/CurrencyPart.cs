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
        private string _symbol;
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

        [XmlIgnore]
        private int _value;
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

        #endregion Properties


        #region ToString

        public int Convert(int amount)
        {
            return amount / Value;
        }

        public string ToString(int amount)
        {
            amount = Convert(amount);

            if (Symbol.Contains("%"))
            {
                return Symbol.Replace("%", amount.ToString());
            }

            return amount + " " + Symbol;
        }

        #endregion ToString
    }
}
