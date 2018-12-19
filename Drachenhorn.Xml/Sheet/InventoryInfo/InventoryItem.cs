using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Sheet.InventoryInfo
{
    /// <summary>
    ///     Basic inventory item.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class InventoryItem : ChildChangedBase
    {
        #region Properties

        [XmlIgnore] private string _name;

        [XmlIgnore] private int _price;

        [XmlIgnore] private string _storagePlace;

        [XmlIgnore] private double _weight;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [XmlAttribute("Name")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the storage place.
        /// </summary>
        /// <value>
        ///     The storage place.
        /// </value>
        [XmlAttribute("StoragePlace")]
        public string StoragePlace
        {
            get => _storagePlace;
            set
            {
                if (_storagePlace == value)
                    return;
                _storagePlace = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the weight.
        /// </summary>
        /// <value>
        ///     The weight.
        /// </value>
        [XmlAttribute("Weight")]
        public double Weight
        {
            get => _weight;
            set
            {
                if (Math.Abs(_weight - value) < double.Epsilon)
                    return;
                _weight = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("Price")]
        public int Price
        {
            get => _price;
            set
            {
                if (_price == value)
                    return;
                _price = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}