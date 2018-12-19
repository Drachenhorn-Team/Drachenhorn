using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.InventoryInfo;

namespace Drachenhorn.Xml.Sheet.CombatInfo
{
    /// <summary>
    ///     Weapons that can be used by a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.Sheet.InventoryInfo.InventoryItem" />
    [Serializable]
    public class Weapon : InventoryItem
    {
        #region Properties

        [XmlIgnore] private int _handicap;

        [XmlIgnore] private string _range;

        [XmlIgnore] private string _skill;

        [XmlIgnore] private DamageType _type;

        /// <summary>
        ///     Gets or sets the Skill needed to use the weapon.
        /// </summary>
        [XmlAttribute("Skill")]
        public string Skill
        {
            get => _skill;
            set
            {
                if (_skill == value)
                    return;
                _skill = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the Handicap.
        /// </summary>
        /// <value>
        ///     The Handicap.
        /// </value>
        [XmlAttribute("Handicap")]
        public int Handicap
        {
            get => _handicap;
            set
            {
                if (_handicap == value)
                    return;
                _handicap = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the WeaponType.
        /// </summary>
        /// <value>
        ///     The WeaponType.
        /// </value>
        [XmlAttribute("Type")]
        public DamageType Type
        {
            get => _type;
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the WeaponRange.
        /// </summary>
        /// <value>
        ///     The WeaponRange.
        /// </value>
        [XmlAttribute("Range")]
        public string Range
        {
            get => _range;
            set
            {
                if (_range == value)
                    return;
                _range = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}