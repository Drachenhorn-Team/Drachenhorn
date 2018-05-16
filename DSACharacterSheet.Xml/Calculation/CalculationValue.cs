using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Interfaces;
using DSACharacterSheet.Xml.Sheet;

namespace DSACharacterSheet.Xml.Calculation
{
    public abstract class CalculationValue : ChildChangedBase
    {
        #region Properties

        [XmlIgnore]
        private int _startValue;
        [XmlIgnore]
        public int StartValue
        {
            get { return _startValue;}
            private set
            {
                if (_startValue == value)
                    return;
                _startValue = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentValue");
            }
        }

        [XmlIgnore]
        private int _modifier;

        [XmlAttribute("Modifier")]
        public int Modifier
        {
            get { return _modifier; }
            set
            {
                if (_modifier == value)
                    return;
                _modifier = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentValue");
            }
        }

        [XmlIgnore]
        private int _currentValueDiff;
        [XmlAttribute("CurrentValue")]
        public int CurrentValue
        {
            get
            {
                return StartValue + Modifier + _currentValueDiff;
            }
            set
            {
                _currentValueDiff = value - StartValue - Modifier;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private Formula _formula = new Formula();
        [XmlIgnore]
        public Formula Formula
        {
            get { return _formula; }
            set
            {
                if (_formula == value)
                    return;
                _formula = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("Formula")]
        public string FormulaText
        {
            get { return Formula.Expression; }
            set
            {
                if (Formula == null)
                    Formula = new Formula();

                if (Formula.Expression == value)
                    return;

                Formula.Expression = value;
            }
        }

        #endregion Properties

        #region c'tor

        public CalculationValue()
        {
            Formula.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Expression")
                    Formula.CalculateAsync(x => StartValue = (int)Math.Round(x));
            };

            Formula.OnCalculateAll += (sender, args) =>
            {
                Formula.CalculateAsync(x => StartValue = (int)Math.Round(x));
            };
        }

        #endregion c'tor

        public void GetInformation(ref Dictionary<string, string> dictionary)
        {
            if (!String.IsNullOrEmpty(Formula.Expression))
                dictionary.Add("%Info.Formula", Formula.Expression);
        }
    }
}
