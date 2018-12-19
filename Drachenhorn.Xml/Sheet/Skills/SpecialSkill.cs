using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Xml.Sheet.Skills
{
    /// <summary>
    ///     Specialskill of a Character.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.BindableBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class SpecialSkill : BindableBase, IInfoObject
    {
        #region c'tor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpecialSkill" /> class.
        /// </summary>
        public SpecialSkill()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpecialSkill" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SpecialSkill(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        [XmlIgnore] private string _description;

        [XmlIgnore] private string _name = "";

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

        #endregion

        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Description)) result.Add("%Info.Description", Description);

            return result;
        }
    }
}