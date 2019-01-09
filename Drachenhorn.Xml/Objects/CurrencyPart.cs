using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Objects
{
    /// <inheritdoc />
    /// <summary>
    ///     Single part of a currency (e.g. cent, Euro)
    /// </summary>
    [Serializable]
    public class CurrencyPart : BindableBase
    {
        #region Properties

        [XmlIgnore] private string _name;

        [XmlIgnore] private string _symbol;

        [XmlIgnore] private int _value;

        /// <summary>
        ///     Gets or sets the name of the CurrencyPart.
        /// </summary>
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

        /// <summary>
        ///     Converts the basic amount to the value of the CurrencyPart.
        /// </summary>
        /// <param name="amount">Internal amount.</param>
        /// <returns>Value in the current CurrencyPart.</returns>
        public long Convert(long amount)
        {
            return amount / Value;
        }

        /// <summary>
        ///     Converts an amount of money to a string
        /// </summary>
        /// <param name="amount">Internal amount.</param>
        /// <returns>String for the current CurrencyPart.</returns>
        public string ToString(long amount)
        {
            amount = Convert(amount);

            if (Symbol.Contains("%")) return Symbol.Replace("%", amount.ToString());

            return amount + Symbol;
        }

        public long Parse(string value)
        {
            var containsChar = Symbol.Contains("%");

            var regex = containsChar ? Symbol.Replace("%", "[0-9]*") : "[0-9]*" + Symbol;

            var part = Regex.Match(value, regex).Value;

            var before = containsChar ? Symbol.Substring(0, Symbol.IndexOf('%')) : "";
            var after  = containsChar ? Symbol.Substring(Symbol.IndexOf('%') + 1) : Symbol;

            if (!String.IsNullOrEmpty(before))
                part = part.Replace(before, "");
            if (!String.IsNullOrEmpty(after))
                part = part.Replace(after, "");

            long.TryParse(part, out var result);

            return result * Value;
        }

        #endregion ToString
    }
}