using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Enums;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    ///     (Dis-)Advantages
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class DisAdvantage : ChildChangedBase, IInfoObject
    {
        /// <inheritdoc />
        public virtual Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Specialization)) result.Add("%Info.Specialization", Specialization);

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

        [XmlIgnore] private string _specialization;

        /// <summary>
        ///     Gets or sets the specialization.
        /// </summary>
        /// <value>
        ///     The specialization.
        /// </value>
        [XmlAttribute("Specialization")]
        public string Specialization
        {
            get => _specialization;
            set
            {
                if (_specialization == value)
                    return;
                _specialization = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private DisAdvantageType _type;

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [XmlAttribute("Type")]
        public DisAdvantageType Type
        {
            get => _type;
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}