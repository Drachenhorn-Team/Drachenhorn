using System;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.InventoryInfo;

namespace Drachenhorn.Xml.Sheet.CombatInfo
{
    [Serializable]
    public class Weapon : InventoryItem
    {
        #region Properties

        [XmlIgnore]
        private int _handicap;

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

        [XmlIgnore]
        private WeaponType _type;

        [XmlAttribute("Type")]
        public WeaponType Type
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

        #endregion Properties
    }
}