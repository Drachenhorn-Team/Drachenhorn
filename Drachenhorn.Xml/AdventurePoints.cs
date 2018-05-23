using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml
{
    [Serializable]
    public class AdventurePoints : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private int _total;

        [XmlAttribute("Total")]
        public int Total
        {
            get { return _total; }
            set
            {
                if (_total == value)
                    return;
                _total = value;
                OnPropertyChanged(null);
            }
        }

        [XmlIgnore]
        private int _used;

        [XmlAttribute("Used")]
        public int Used
        {
            get { return _used; }
            set
            {
                if (_used == value)
                    return;
                _used = value;
                OnPropertyChanged(null);
            }
        }

        [XmlIgnore]
        public int CurrentLeft
        {
            get { return Total - Used; }
        }

        #endregion Properties
    }
}