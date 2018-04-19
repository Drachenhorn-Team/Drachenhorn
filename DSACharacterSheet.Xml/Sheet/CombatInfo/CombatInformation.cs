using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace DSACharacterSheet.Xml.Sheet.CombatInfo
{
    [Serializable]
    public class CombatInformation : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private ObservableCollection<Weapon> _weapons = new ObservableCollection<Weapon>();

        [XmlElement("Weapon")]
        public ObservableCollection<Weapon> Weapons
        {
            get { return _weapons; }
            set
            {
                if (_weapons == value)
                    return;
                _weapons = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<ArmorPart> _armorParts = new ObservableCollection<ArmorPart>();

        [XmlElement("ArmorPart")]
        public ObservableCollection<ArmorPart> ArmorParts
        {
            get { return _armorParts; }
            set
            {
                if (_armorParts == value)
                    return;
                _armorParts = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}