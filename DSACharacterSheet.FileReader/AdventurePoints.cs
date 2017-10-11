using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader
{
    [Serializable]
    public class AdventurePoints : INotifyPropertyChanged
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

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}
