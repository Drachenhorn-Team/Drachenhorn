using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Drachenhorn.Xml.Calculation;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    /// Bonus Values for Character Generation
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Calculation.IFormulaKeyItem" />
    [Serializable]
    public class BonusValue : ChildChangedBase, IFormulaKeyItem
    {
        [XmlIgnore]
        private string _key;
        /// <inheritdoc />
        [XmlAttribute("Key")]
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == value)
                    return;
                _key = value;
                OnPropertyChanged();
            }
        }
        
        [XmlIgnore]
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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
        private double _value;
        /// <inheritdoc />
        [XmlAttribute("Value")]
        public double Value
        {
            get { return _value; }
            set
            {
                if (Math.Abs(_value - value) < Double.Epsilon)
                    return;
                _value = value;
                OnPropertyChanged();
            }
        }
    }
}
