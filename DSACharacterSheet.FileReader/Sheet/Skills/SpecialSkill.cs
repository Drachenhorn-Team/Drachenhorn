using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Interfaces;

namespace DSACharacterSheet.FileReader.Sheet.Skills
{
    [Serializable]
    public class SpecialSkill : BindableBase, IInfoObject
    {
        #region Properties

        [XmlIgnore]
        private string _name = "";
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
        private string _description;
        [XmlAttribute("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        public SpecialSkill()
        {
        }

        public SpecialSkill(string name)
        {
            Name = name;
        }

        #endregion c'tor

        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Description)) result.Add("%Info.Description", Description);

            return result;
        }
    }
}
