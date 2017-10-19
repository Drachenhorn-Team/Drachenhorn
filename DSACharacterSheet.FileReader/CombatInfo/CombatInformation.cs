using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.CombatInfo
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

        #endregion Properties
    }
}
