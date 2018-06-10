using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    ///     Basic Metadata for Template
    /// </summary>
    [Serializable]
    public class TemplateMetadata : ChildChangedBase
    {
        #region Properties

        [XmlIgnore] private double _version;

        /// <summary>
        ///     Version of the Template
        /// </summary>
        [XmlAttribute("Version")]
        public double Version
        {
            get => _version;
            set
            {
                if (Math.Abs(_version - value) < double.Epsilon)
                    return;
                _version = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private string _name;

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        [XmlIgnore]
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

        #endregion Properties


        #region c'tor

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        protected TemplateMetadata()
        {
        }

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        /// <param name="name">Name of the File.</param>
        /// <param name="version">Version of the Template.</param>
        public TemplateMetadata(string name, double version)
        {
            Name = name;
            Version = version;
        }

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        /// <param name="name">Name of the File.</param>
        /// <param name="versionLine">Line containing the Version of the Template. (second line of XML)</param>
        public TemplateMetadata(string name, string versionLine)
        {
            Name = name;
            SetVersionFromXMLLine(versionLine);
        }

        /// <summary>
        ///     Sets Version based on the second line of the XML-File
        /// </summary>
        /// <param name="line">Second line of the XML.</param>
        protected void SetVersionFromXMLLine(string line)
        {
            var match = new Regex("Version=\"[0-9]+[.][0-9]+\"").Match(line).Value;

            if (!string.IsNullOrEmpty(match))
                Version = double.Parse(match.Substring(9, match.Length - 10), CultureInfo.InvariantCulture);
        }

        #endregion c'tor
    }
}