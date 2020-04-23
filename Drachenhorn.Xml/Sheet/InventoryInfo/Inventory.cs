using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.CombatInfo;

namespace Drachenhorn.Xml.Sheet.InventoryInfo
{
    /// <summary>
    ///     Data about the Inventory of a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class Inventory : ChildChangedBase
    {
        /// <summary>
        ///     Gets the whole weight in the Inventory.
        /// </summary>
        /// <returns></returns>
        public double GetWholeWeight()
        {
            return Items.Sum(item => { return item.Weight; });
        }

        #region Properties

        [XmlIgnore] private ObservableCollection<InventoryItem> _items = new ObservableCollection<InventoryItem>();

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        [XmlElement("Item", typeof(InventoryItem))]
        [XmlElement("Weapon", typeof(Weapon))]
        [XmlElement("ArmorPart", typeof(ArmorPart))]
        public ObservableCollection<InventoryItem> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                _items = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the total weight of all items.
        /// </summary>
        /// <value>
        ///     The total weight.
        /// </value>
        [XmlIgnore]
        public double TotalWeight
        {
            get
            {
                var result = 0d;
                foreach (var item in Items)
                    result += item.Weight;

                return result;
            }
        }

        #endregion
    }
}