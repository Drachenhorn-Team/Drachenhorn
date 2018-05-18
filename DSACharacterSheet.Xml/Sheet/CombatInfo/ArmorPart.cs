using System;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Sheet.InventoryInfo;

namespace DSACharacterSheet.Xml.Sheet.CombatInfo
{
    [Serializable]
    public class ArmorPart : InventoryItem
    {
        #region Properties

        [XmlIgnore]
        private bool _isActive;

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