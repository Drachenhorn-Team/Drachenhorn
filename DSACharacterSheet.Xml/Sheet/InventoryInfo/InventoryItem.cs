using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DSACharacterSheet.Xml.Sheet.InventoryInfo
{
    [Serializable]
    public class InventoryItem : ChildChangedBase
    {
        #region Properties

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
        private string _storagePlace;
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
