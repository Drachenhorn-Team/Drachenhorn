using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Roll;

namespace Drachenhorn.Xml.Sheet.Skills
{
    /// <summary>
    ///     Character-Skill
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class Skill : ChildChangedBase, IInfoObject
    {
        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Category)) result.Add("%Info.Description", Category);
            if (!string.IsNullOrEmpty(RollAttributes.ToString(",")))
                result.Add("%Info.RollAttributes", RollAttributes.ToString(", "));

            return result;
        }

        #region Properties

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

        [XmlIgnore] private string _category;
        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>
        ///     The category.
        /// </value>
        [XmlAttribute("Category")]
        public string Category
        {
            get => _category;
            set
            {
                if (_category == value)
                    return;
                _category = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private RollAttributes _rollAttributes = new RollAttributes();

        /// <summary>
        ///     Gets or sets the roll attributes.
        /// </summary>
        /// <value>
        ///     The roll attributes.
        /// </value>
        [XmlElement("RollAttributes")]
        public RollAttributes RollAttributes
        {
            get => _rollAttributes;
            set
            {
                if (_rollAttributes == value)
                    return;
                _rollAttributes = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}