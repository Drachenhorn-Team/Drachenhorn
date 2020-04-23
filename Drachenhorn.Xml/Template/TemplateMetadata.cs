using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Drachenhorn.Xml.Data;

namespace Drachenhorn.Xml.Template
{
    /// <summary>
    ///     Basic Metadata for Template
    /// </summary>
    [Serializable]
    public class TemplateMetadata : ChildChangedBase, ITemplateMetadata
    {
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
                this.SetVersionAndNameFromXmlLine(sr.ReadLine());
            }
        }

        #endregion

        #region Properties

        [XmlIgnore] private string _name;

        [XmlIgnore] private string _path;

        private SheetTemplate _template;

        [XmlIgnore] private double _version;

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public bool IsInstalled => this.CheckInstalled();

        /// <inheritdoc />
        public bool Install()
        {
            try
            {
                var file = new FileInfo(Path);

                var target = System.IO.Path.Combine(
                    Constants.TemplateBaseDirectory,
                    Name + Constants.TemplateExtension);

                file.CopyTo(target, false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public Task<bool> InstallAsync()
        {
            return Task.Run(() => Install());
        }

        /// <summary>
        ///     Gets the one single instance of the entire template.
        /// </summary>
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


        #region Equals

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is ITemplateMetadata metadata && Equals(metadata);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Version.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 1);
            }
        }

        /// <inheritdoc />
        public bool Equals(ITemplateMetadata obj)
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