using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Template
{
    [Serializable]
    public class TemplateInformation : ChildChangedBase
    {
        #region Properties

        [XmlIgnore] private TemplateGenerationType _generationType = TemplateGenerationType.GenerationPoints;

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