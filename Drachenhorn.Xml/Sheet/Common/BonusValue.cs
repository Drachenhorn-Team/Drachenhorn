using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Calculation;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    ///     Bonus Values for Character Generation
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Calculation.IFormulaKeyItem" />
    [Serializable]
    public class BonusValue : ChildChangedBase, IFormulaKeyItem
    {
        [XmlIgnore] private string _key;

        [XmlIgnore] private string _name;

        [XmlIgnore] private int _value;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
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

        /// <inheritdoc />
        [XmlAttribute("Key")]
        public string Key
        {
            get => _key;
            set
            {
                if (_key == value)
                    return;
                _key = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        [XmlAttribute("Value")]
        public int Value
        {
            get => _value;
            set
            {
                if (Math.Abs(_value - value) < double.Epsilon)
                    return;
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}