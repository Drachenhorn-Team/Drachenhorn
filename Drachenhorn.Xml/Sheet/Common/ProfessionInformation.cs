using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    ///     Profession-Information
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class ProfessionInformation : ChildChangedBase, IInfoObject
    {
        #region Properties

        [XmlIgnore] private ObservableCollection<BonusValue> _baseValues = new ObservableCollection<BonusValue>();

        [XmlIgnore] private string _description;

        [XmlIgnore] private double _gpCost;

        [XmlIgnore] private string _name;

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
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        [XmlAttribute("Description")]
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gp cost.
        /// </summary>
        /// <value>
        ///     The gp cost.
        /// </value>
        [XmlAttribute("GPCost")]
        public double GpCost
        {
            get => _gpCost;
            set
            {
                if (_gpCost == value)
                    return;
                _gpCost = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the base values.
        /// </summary>
        /// <value>
        ///     The base values.
        /// </value>
        [XmlElement("BaseValue")]
        public ObservableCollection<BonusValue> BaseValues
        {
            get => _baseValues;
            set
            {
                if (_baseValues == value)
                    return;
                _baseValues = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Description)) result.Add("%Info.Description", Description);
            if (Math.Abs(GpCost) > double.Epsilon)
                result.Add("%Info.GPCost", GpCost.ToString(CultureInfo.CurrentCulture));

            return result;
        }
    }
}