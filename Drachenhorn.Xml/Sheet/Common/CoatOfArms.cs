using System;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Sheet.Common
{
    /// <summary>
    ///     The CoatOfArms of a Character
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    [Serializable]
    public class CoatOfArms : ChildChangedBase
    {
        [XmlIgnore] private string _base64String;

        /// <summary>
        ///     Gets or sets the base64 string.
        /// </summary>
        /// <value>
        ///     The base64 string.
        /// </value>
        [XmlAttribute("Image")]
        public string Base64String
        {
            get => _base64String;
            set
            {
                if (_base64String == value)
                    return;
                _base64String = value;
                OnPropertyChanged();
            }
        }
    }
}