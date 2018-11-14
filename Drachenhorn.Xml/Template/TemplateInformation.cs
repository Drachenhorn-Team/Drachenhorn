using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Template
{
    /// <inheritdoc />
    /// <summary>
    ///     Basic Template Information
    /// </summary>
    [Serializable]
    public class TemplateInformation : ChildChangedBase
    {
        #region Properties

        [XmlIgnore] private TemplateGenerationType _generationType = TemplateGenerationType.GenerationPoints;

        /// <summary>
        /// Gets or sets the type of the generation.
        /// </summary>
        /// <value>
        /// The type of the generation.
        /// </value>
        [XmlAttribute("GenerationType")]
        public TemplateGenerationType GenerationType
        {
            get => _generationType;
            set
            {
                if (_generationType == value)
                    return;
                _generationType = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}