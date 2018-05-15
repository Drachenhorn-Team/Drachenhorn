using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DSACharacterSheet.Xml.Data.AP
{
    [Serializable]
    public class APTable : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private ObservableCollection<APColumn> _apColumns = new ObservableCollection<APColumn>();

        [XmlElement("APColumn")]
        public ObservableCollection<APColumn> APColumns
        {
            get { return _apColumns; }
            set
            {
                if (_apColumns == value)
                    return;
                _apColumns = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region operators

        public APColumn this[string key]
        {
            get
            {
                var column = APColumns.First(x => x.Name == key);

                if (column == null) throw new KeyNotFoundException(key + " is not a valid column");

                return column;
            }
            set
            {
                var column = APColumns.First(x => x.Name == key);

                if (column == null) throw new KeyNotFoundException(key + " is not a valid column");

                APColumns[APColumns.IndexOf(column)] = value;
            }
        }

        #endregion operators

        #region Calculation

        public uint Calculate(string key, int from, int to)
        {
            return this[key].CalculateCosts(from, to);
        }

        #endregion Calculation
    }
}
