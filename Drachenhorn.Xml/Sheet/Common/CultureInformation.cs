using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    /// Cultural Information
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    [Serializable]
    public class CultureInformation : ChildChangedBase, IInfoObject
    {
        [XmlIgnore]
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private string _specification;
        /// <summary>
        /// Gets or sets the specification.
        /// </summary>
        /// <value>
        /// The specification.
        /// </value>
        [XmlAttribute("Specification")]
        public string Specification
        {
            get { return _specification; }
            set
            {
                if (_specification == value)
                    return;
                _specification = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<BonusValue> _baseValues = new ObservableCollection<BonusValue>();
        /// <summary>
        /// Gets or sets the base values.
        /// </summary>
        /// <value>
        /// The base values.
        /// </value>
        [XmlElement("BaseValue")]
        public ObservableCollection<BonusValue> BaseValues
        {
            get { return _baseValues; }
            set
            {
                if (_baseValues == value)
                    return;
                _baseValues = value;
                OnPropertyChanged();
            }
        }
        
        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);

            var baseValues = "";
            foreach (var baseValue in BaseValues)
            {
                baseValues += baseValue.Name + ": " + baseValue.Value + "\n";
            }
            if (!string.IsNullOrEmpty(baseValues)) result.Add("%Info.BaseValues", baseValues);


            return result;
        }
    }
}