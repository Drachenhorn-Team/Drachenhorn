using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Sheet.InventoryInfo
{
    [Serializable]
    public class Inventory : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private ObservableCollection<InventoryItem> _items = new ObservableCollection<InventoryItem>();

        [XmlElement("Item")]
        public ObservableCollection<InventoryItem> Items
        {
            get { return _items; }
            set
            {
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        public double GetWholeWeight()
        {
            return Items.Sum(item => { return item.Weight; });
        }
    }
}
