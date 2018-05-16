using System;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Sheet.Enums;

namespace DSACharacterSheet.Xml.Sheet.Roll
{
    [Serializable]
    public class RollAttributes : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private AttributeType _roll1;

        [XmlAttribute("Roll_1")]
        public AttributeType Roll1
        {
            get { return _roll1; }
            set
            {
                if (_roll1 == value)
                    return;
                _roll1 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private AttributeType _roll2;

        [XmlAttribute("Roll_2")]
        public AttributeType Roll2
        {
            get { return _roll2; }
            set
            {
                if (_roll2 == value)
                    return;
                _roll2 = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private AttributeType _roll3;

        [XmlAttribute("Roll_3")]
        public AttributeType Roll3
        {
            get { return _roll3; }
            set
            {
                if (_roll3 == value)
                    return;
                _roll3 = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region c'tor

        public RollAttributes()
        {
        }

        public RollAttributes(AttributeType roll1, AttributeType roll2, AttributeType roll3)
        {
            Roll1 = roll1;
            Roll2 = roll2;
            Roll3 = roll3;
        }

        #endregion c'tor

        #region ToString

        public override string ToString()
        {
            return ToString("|");
        }

        public string ToString(string seperator)
        {
            return "%AttributeType." + Roll1 + ".Abbr " + seperator +
                   " %AttributeType." + Roll2 + ".Abbr  " + seperator +
                   " %AttributeType." + Roll3 + ".Abbr";
        }

        #endregion ToString
    }
}