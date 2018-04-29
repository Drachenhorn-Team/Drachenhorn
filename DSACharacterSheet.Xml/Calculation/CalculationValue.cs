using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Sheet;

namespace DSACharacterSheet.Xml.Calculation
{
    public abstract class CalculationValue : BindableBase
    {
        #region Properties

        [XmlIgnore]
        public int StartValue
        {
            get
            {
                return (int)Math.Round(Formula.Calculate());
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
                OnPropertyChanged(null);
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
                OnPropertyChanged(null);
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
                OnPropertyChanged(null);
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
    }
}
