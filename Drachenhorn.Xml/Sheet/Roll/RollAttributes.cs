using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.Enums;

namespace Drachenhorn.Xml.Sheet.Roll
{
    /// <summary>
    /// Roll Attributes.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class RollAttributes : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private string _roll1;
        /// <summary>
        /// Gets or sets the roll1.
        /// </summary>
        /// <value>
        /// The roll1.
        /// </value>
        [XmlAttribute("Roll_1")]
        public string Roll1
        {
            get { return _roll1; }
            set
            {
                if (_roll1 == value)
                    return;
                _roll1 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _roll2;
        /// <summary>
        /// Gets or sets the roll2.
        /// </summary>
        /// <value>
        /// The roll2.
        /// </value>
        [XmlAttribute("Roll_2")]
        public string Roll2
        {
            get { return _roll2; }
            set
            {
                if (_roll2 == value)
                    return;
                _roll2 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _roll3;
        /// <summary>
        /// Gets or sets the roll3.
        /// </summary>
        /// <value>
        /// The roll3.
        /// </value>
        [XmlAttribute("Roll_3")]
        public string Roll3
        {
            get { return _roll3; }
            set
            {
                if (_roll3 == value)
                    return;
                _roll3 = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        /// <summary>
        /// Initializes a new instance of the <see cref="RollAttributes"/> class.
        /// </summary>
        public RollAttributes()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollAttributes"/> class.
        /// </summary>
        /// <param name="roll1">The roll1.</param>
        /// <param name="roll2">The roll2.</param>
        /// <param name="roll3">The roll3.</param>
        public RollAttributes(string roll1, string roll2, string roll3)
        {
            Roll1 = roll1;
            Roll2 = roll2;
            Roll3 = roll3;
        }

        #endregion c'tor

        #region ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ToString("|");
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="seperator">The seperator.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string ToString(string seperator)
        {
            return Roll1 + seperator + Roll2 + seperator + Roll3;
        }

        #endregion ToString
    }
}