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
    public class SpecialSkill : BindableBase
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

        #endregion Properties

        #region c'tor

        public SpecialSkill()
        {
            Name = "new";
        }

        public SpecialSkill(string name)
        {
            Name = name;
        }

        #endregion c'tor
    }
}
