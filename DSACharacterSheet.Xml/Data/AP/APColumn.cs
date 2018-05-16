using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace DSACharacterSheet.Xml.Data.AP
{
    [Serializable]
    public class APColumn : ChildChangedBase
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
        private ushort _factor;
        [XmlAttribute("Factor")]
        public ushort Factor
        {
            get { return _factor; }
            set
            {
                if (_factor == value)
                    return;
                _factor = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ushort _negative;
        [XmlAttribute("Negative")]
        public ushort Negative
        {
            get { return _negative; }
            set
            {
                if (_negative == value)
                    return;
                _negative = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private ObservableCollection<ushort> _costs = new ObservableCollection<ushort>();

        [XmlElement("Cost")]
        public ObservableCollection<ushort> Costs
        {
            get { return _costs; }
            set
            {
                if (_costs == value)
                    return;
                _costs = value;
                OnPropertyChanged();
            }
        }

        #endregion Properites

        #region c'tor

        public APColumn() { }

        public APColumn(string name, ushort factor, ushort negative)
        {
            Name = name;
            Factor = factor;
            Negative = negative;
        }

        #endregion c'tor

        #region Calculation

        public uint CalculateCosts(int from, int to)
        {
            uint result = 0;
            int start = from;

            if (from < 0)
            {
                result += Negative * (uint)(from * -1);
                start = 0;
            }


            for (int i = start; i <= to; ++i)
            {
                if (i < Costs.Count)
                    result += Costs[i];
                else
                    result += Costs[Costs.Count - 1];
            }

            return result;
        }

        #endregion Calculation
    }
}
