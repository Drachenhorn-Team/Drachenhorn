using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    ///     Basic Metadata for Template
    /// </summary>
    [Serializable]
    public class TemplateMetadata : ChildChangedBase, IEquatable<TemplateMetadata>
    {
        /// <summary>
        ///     The Template Extension
        /// </summary>
        public static readonly string Extension = ".dsat";

        #region c'tor

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        protected TemplateMetadata()
        {
            Name = "unnamed";
        }

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        /// <param name="name">Path of the File.</param>
        /// <param name="version">Version of the Template.</param>
        public TemplateMetadata(string name, double version)
        {
            Name = name;
            Version = version;
        }

        /// <summary>
        ///     Basic constructor.
        /// </summary>
        /// <param name="path">Path of the File.</param>
        public TemplateMetadata(string path)
        {
            Path = path;

            using (var sr = new StreamReader(File.OpenRead(path)))
            {
                sr.ReadLine();
                SetVersionAndNameFromXmlLine(sr.ReadLine());
            }
        }

        #endregion

        #region Properties

        [XmlIgnore] private string _name;

        [XmlIgnore] private string _path;

        private SheetTemplate _template;

        [XmlIgnore] private double _version;

        /// <summary>
        ///     Gets the Template BaseDirectory.
        /// </summary>
        /// <value>
        ///     The Template BaseDirectory.
        /// </value>
        public static string BaseDirectory => System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Drachenhorn", "Templates");

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

        /// <summary>
        ///     Path of the Template
        /// </summary>
        [XmlIgnore]
        public string Path
        {
            get => _path;
            set
            {
                if (_path == value)
                    return;
                _path = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
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

        public SheetTemplate EntireTemplate
        {
            get
            {
                if (_template == null && !string.IsNullOrEmpty(Path))
                    _template = SheetTemplate.Load(Path);
                return _template;
            }
        }

        #endregion

        /// <summary>
        ///     Sets Version based on the second line of the XML-File
        /// </summary>
        /// <param name="line">Second line of the XML.</param>
        protected void SetVersionAndNameFromXmlLine(string line)
        {
            var versionMatch = new Regex("Version=\"[0-9]+[.]?[0-9]*\"").Match(line).Value;

            if (!string.IsNullOrEmpty(versionMatch))
                Version = double.Parse(versionMatch.Substring(9, versionMatch.Length - 10),
                    CultureInfo.InvariantCulture);

            var nameMatch = new Regex("Name=\"[^\"]*\"").Match(line).Value;

            Name = !string.IsNullOrEmpty(nameMatch) ? nameMatch.Substring(6, nameMatch.Length - 7) : "unnamed";
        }


        #region Equals

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is TemplateMetadata metadata && Equals(metadata);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Version.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Checks if objects are Equal.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>True if Equal</returns>
        public bool Equals(TemplateMetadata obj)
        {
            if (obj == null) return false;

            return Name == obj.Name && Math.Abs(Version - obj.Version) < double.Epsilon;
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(TemplateMetadata lhs, TemplateMetadata rhs)
        {
            if ((object) lhs == null)
                return (object) rhs == null;

            return lhs.Equals(rhs);
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(TemplateMetadata lhs, TemplateMetadata rhs)
        {
            return !(rhs == lhs);
        }

        #endregion Equals
    }
}