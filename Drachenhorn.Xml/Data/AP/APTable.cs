using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Data.AP
{
    /// <summary>
    ///     AP-Cost-Table for leveling a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class APTable : ChildChangedBase
    {
        #region operators

        /// <summary>
        ///     Gets or sets the <see cref="APColumn" /> with the specified key.
        /// </summary>
        /// <value>
        ///     The <see cref="APColumn" />.
        /// </value>
        /// <param name="key">The Name of the Column.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
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

        /// <summary>
        ///     Calculates the leveling cost for the specified key.
        /// </summary>
        /// <param name="key">The Column-Key.</param>
        /// <param name="from">Current Skill value.</param>
        /// <param name="to">Target Skill value.</param>
        /// <returns></returns>
        public uint Calculate(string key, int from, int to)
        {
            return this[key].CalculateCosts(from, to);
        }

        #endregion Calculation

        #region Properties

        [XmlIgnore] private ObservableCollection<APColumn> _apColumns = new ObservableCollection<APColumn>();

        /// <summary>
        ///     Gets or sets the AP-Columns.
        /// </summary>
        /// <value>
        ///     The AP-Columns.
        /// </value>
        [XmlElement("APColumn")]
        public ObservableCollection<APColumn> APColumns
        {
            get => _apColumns;
            set
            {
                if (_apColumns == value)
                    return;
                _apColumns = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the column names.
        /// </summary>
        /// <value>
        ///     The column names.
        /// </value>
        [XmlIgnore]
        public IEnumerable<string> ColumnNames => from x in APColumns select x.Name;

        #endregion Properties
    }
}