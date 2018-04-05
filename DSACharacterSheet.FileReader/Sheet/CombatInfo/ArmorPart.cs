using System;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Sheet.CombatInfo
{
    [Serializable]
    public class ArmorPart : BindableBase
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
        private string _name;
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
        private ArmorRegion _region = ArmorRegion.None;
        [XmlAttribute("Region")]
        public ArmorRegion Region
        {
            get { return _region; }
            set
            {
                if (_region == value)
                    return;
                _region = value;
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
