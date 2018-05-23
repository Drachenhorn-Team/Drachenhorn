using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Sheet.Enums;

namespace Drachenhorn.Xml.Sheet.Common
{
    [Serializable]
    public class DisAdvantage : ChildChangedBase, IInfoObject
    {
        #region Properties

        [XmlIgnore]
        private string _name;

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
        private string _specialization;

        [XmlAttribute("Specialization")]
        public string Specialization
        {
            get { return _specialization; }
            set
            {
                if (_specialization == value)
                    return;
                _specialization = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private DisAdvantageType _type;
        [XmlAttribute("Type")]
        public DisAdvantageType Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        public virtual Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Specialization)) result.Add("%Info.Specialization", Specialization);

            return result;
        }
    }
}
