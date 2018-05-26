using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Sheet.InventoryInfo
{
    /// <summary>
    /// Basic inventory item.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class InventoryItem : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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
        private string _storagePlace;
        /// <summary>
        /// Gets or sets the storage place.
        /// </summary>
        /// <value>
        /// The storage place.
        /// </value>
        [XmlAttribute("StoragePlace")]
        public string StoragePlace
        {
            get { return _storagePlace; }
            set
            {
                if (_storagePlace == value)
                    return;
                _storagePlace = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _weight;
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        [XmlAttribute("Weight")]
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (Math.Abs(_weight - value) < Double.Epsilon)
                    return;
                _weight = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}
