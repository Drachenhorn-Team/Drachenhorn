using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.InventoryInfo;

namespace Drachenhorn.Xml.Sheet.CombatInfo
{
    /// <summary>
    /// Part of an Armor.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.Sheet.InventoryInfo.InventoryItem" />
    [Serializable]
    public class ArmorPart : InventoryItem
    {
        #region Properties

        [XmlIgnore]
        private bool _isActive;
        /// <summary>
        /// Gets or sets a value indicating whether this ArmorPart is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this ArmorPart is active; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute("IsActive")]
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value)
                    return;
                _isActive = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ArmorType _type = ArmorType.None;
        /// <summary>
        /// Gets or sets the ArmorType.
        /// </summary>
        /// <value>
        /// The ArmorType.
        /// </value>
        [XmlAttribute("Type")]
        public ArmorType Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private int _handicap;
        /// <summary>
        /// Gets or sets the Handicap.
        /// </summary>
        /// <value>
        /// The Handicap.
        /// </value>
        [XmlAttribute("Handicap")]
        public int Handicap
        {
            get { return _handicap; }
            set
            {
                if (_handicap == value)
                    return;
                _handicap = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}