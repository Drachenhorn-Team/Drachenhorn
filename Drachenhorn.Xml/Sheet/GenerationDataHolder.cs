using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Common;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Xml.Sheet
{
    /// <summary>
    ///     Base Class for Character Generation
    /// </summary>
    public class GenerationDataHolder : ChildChangedBase, IInfoObject
    {
        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (GenerationCost != 0)
                result.Add("%Info.GPCost", GenerationCost.ToString(CultureInfo.CurrentCulture));

            foreach (var baseValue in BaseValues)
                result.Add(baseValue.Name, baseValue.Value.ToString());

            foreach (var skill in Skills)
                result.Add(skill.Name, skill.Value.ToString());

            return result;
        }

        #region Properties

        [XmlIgnore] private int _generationCost;

        [XmlIgnore] private ObservableCollection<BonusValue> _baseValues = new ObservableCollection<BonusValue>();

        [XmlIgnore] private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

        /// <summary>
        ///     Gets or sets the gp cost.
        /// </summary>
        /// <value>
        ///     The gp cost.
        /// </value>
        [XmlAttribute("GenerationCost")]
        public int GenerationCost
        {
            get => _generationCost;
            set
            {
                if (_generationCost == value)
                    return;
                _generationCost = value;
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


        /// <summary>
        ///     Gets or sets the skills.
        /// </summary>
        /// <value>
        ///     The skills.
        /// </value>
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