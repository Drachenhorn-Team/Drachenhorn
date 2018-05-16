using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Objects;

namespace DSACharacterSheet.Xml.Sheet.Common
{
    [Serializable]
    public class CoatOfArms : ChildChangedBase
    {
        [XmlIgnore]
        private string _base64String;

        [XmlAttribute("Image")]
        public string Base64String
        {
            get { return _base64String; }
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