using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Drachenhorn.Xml.Calculation;
using Drachenhorn.Xml.Interfaces;

namespace Drachenhorn.Xml.Sheet.Skills
{
    /// <summary>
    /// Attribute of a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.Calculation.CalculationValue" />
    /// <seealso cref="Drachenhorn.Xml.Interfaces.IInfoObject" />
    /// <seealso cref="Drachenhorn.Xml.Calculation.IFormulaKeyItem" />
    [Serializable]
    public class Attribute : CalculationValue, IInfoObject, IFormulaKeyItem
    {
        [XmlIgnore]
        private string _key;

        /// <inheritdoc />
        [XmlAttribute("Key")]
        public string Key
        {
            get { return _key; }
            set
            {
                if (_key == value)
                    return;
                _key = value;
                OnPropertyChanged();
            }
        }


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

        /// <inheritdoc />
        [XmlIgnore]
        public double Value
        {
            get { return CurrentValue; }
        }

        #region InfoObject

        /// <inheritdoc />
        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Key))
                result.Add("%Info.Key", Key);
            if (!string.IsNullOrEmpty(Name))
                result.Add("%Info.Name", Name);

            GetInformation(ref result);

            return result;
        }

        #endregion InfoObject
    }
}