using DSACharacterSheet.FileReader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Roll
{
    [Serializable]
    public class RollAttributes : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private AttributeType _roll_1;
        [XmlAttribute("Roll_1")]
        public AttributeType Roll_1
        {
            get { return _roll_1; }
            set
            {
                if (_roll_1 == value)
                    return;
                _roll_1 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private AttributeType _roll_2;
        [XmlAttribute("Roll_2")]
        public AttributeType Roll_2
        {
            get { return _roll_2; }
            set
            {
                if (_roll_2 == value)
                    return;
                _roll_2 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private AttributeType _roll_3;
        [XmlAttribute("Roll_3")]
        public AttributeType Roll_3
        {
            get { return _roll_3; }
            set
            {
                if (_roll_3 == value)
                    return;
                _roll_3 = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        public RollAttributes() { }

        public RollAttributes(AttributeType roll_1, AttributeType roll_2, AttributeType roll_3)
        {
            Roll_1 = roll_1;
            Roll_2 = roll_2;
            Roll_3 = roll_3;
        }

        #endregion c'tor


        #region ToString

        public override string ToString()
        {
            return ToString("|");
        }

        public string ToString(string seperator)
        {
            return "%AttributeType." + Roll_1 + ".Abbr " + seperator +
                   " %AttributeType." + Roll_2 + ".Abbr  " + seperator +
                   " %AttributeType." + Roll_3 + ".Abbr";
        }

        #endregion ToString
    }
}
