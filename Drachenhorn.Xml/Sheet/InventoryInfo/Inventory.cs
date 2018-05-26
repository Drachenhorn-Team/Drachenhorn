using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Sheet.InventoryInfo
{
    /// <summary>
    /// Data about the Inventory of a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class Inventory : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private ObservableCollection<InventoryItem> _items = new ObservableCollection<InventoryItem>();
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
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

        /// <summary>
        /// Gets the whole weight in the Inventory.
        /// </summary>
        /// <returns></returns>
        public double GetWholeWeight()
        {
            return Items.Sum(item => { return item.Weight; });
        }
    }
}
