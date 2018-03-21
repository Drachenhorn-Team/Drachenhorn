using DSACharacterSheet.FileReader.Roll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Interfaces;

namespace DSACharacterSheet.FileReader.Skills
{
    [Serializable]
    public class Skill : BindableBase, IInfoObject
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
        private string _category;
        [XmlAttribute("Category")]
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category == value)
                    return;
                _category = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private RollAttributes _rollAttributes = new RollAttributes();
        [XmlElement("RolleAttributes")]
        public RollAttributes RollAttributes
        {
            get { return _rollAttributes; }
            set
            {
                if (_rollAttributes == value)
                    return;
                _rollAttributes = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        public Dictionary<string, string> GetInformation()
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Name)) result.Add("%Info.Name", Name);
            if (!string.IsNullOrEmpty(Category)) result.Add("%Info.Description", Category);
            if (!string.IsNullOrEmpty(RollAttributes.ToString(","))) result.Add("%Info.RollAttributes", RollAttributes.ToString(", "));

            return result;
        }
    }
}
