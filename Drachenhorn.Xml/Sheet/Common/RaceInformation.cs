using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    ///     Race-Information
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class RaceInformation : ChildChangedBase, IInfoObject
    {
        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Description)) result.Add("%Info.Description", Description);
            //if (Math.Abs(GPCost) > Double.Epsilon) result.Add("%Info.GPCost", GPCost.ToString(CultureInfo.CurrentCulture));

            var baseValues = "";
            foreach (var baseValue in BaseValues)
                baseValues += baseValue.Name + ": " + baseValue.Value + " (" + baseValue.Key + ")" + "\n";
            if (!string.IsNullOrEmpty(baseValues))
                result.Add("%Info.BaseValues", baseValues.Substring(0, baseValues.Length - 1));


            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        #region Properties

        [XmlIgnore] private ObservableCollection<BonusValue> _baseValues = new ObservableCollection<BonusValue>();

        [XmlIgnore] private string _description;

        [XmlIgnore] private string _name;

        [XmlIgnore] private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

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
        ///     Gets or sets the base values.
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the skills.
        /// </summary>
        [XmlElement("Skill")]
        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set
            {
                if (_skills == value)
                    return;
                _skills = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}