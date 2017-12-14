using DSACharacterSheet.FileReader.Roll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Skills
{
    [Serializable]
    public class Skill : BindableBase
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
    }
}
