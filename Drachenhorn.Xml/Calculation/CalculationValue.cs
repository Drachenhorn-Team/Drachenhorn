using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Drachenhorn.Xml.Calculation
{
    /// <summary>
    ///     Base-Class for calculated values.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.BindableBase" />
    public abstract class CalculationValue : BindableBase
    {
        #region c'tor  

        /// <summary>
        ///     Initializes a new instance of the <see cref="CalculationValue" /> class.
        /// </summary>
        public CalculationValue()
        {
            Formula.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Expression")
                    StartValue = (int) Math.Round(Formula.Calculate());
            };

            Formula.OnCalculateAll += (sender, args) =>
            {
                Formula.CalculateAsync(x => { StartValue = (int) Math.Round(x); });
            };
        }

        #endregion c'tor

        /// <summary>
        ///     Gets the information.
        /// </summary>
        /// <param name="dictionary">The dictionary to add the Information to.</param>
        public void GetInformation(ref Dictionary<string, string> dictionary)
        {
            if (!string.IsNullOrEmpty(Formula.Expression))
                dictionary.Add("%Info.Formula", Formula.Expression);
        }

        #region Properties

        [XmlIgnore] private int _startValue;

        /// <summary>
        ///     Gets the StartValue.
        /// </summary>
        /// <value>
        ///     The StartValue.
        /// </value>
        [XmlIgnore]
        public int StartValue
        {
            get => _startValue;
            private set
            {
                if (_startValue == value)
                    return;
                _startValue = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentValue");
            }
        }

        [XmlIgnore] private int _modifier;

        /// <summary>
        ///     Gets or sets the modifier.
        /// </summary>
        /// <value>
        ///     The modifier.
        /// </value>
        [XmlAttribute("Modifier")]
        public int Modifier
        {
            get => _modifier;
            set
            {
                if (_modifier == value)
                    return;
                _modifier = value;
                OnPropertyChanged();
                OnPropertyChanged("Value");
            }
        }

        [XmlIgnore] private int _currentValueDiff;

        /// <summary>
        ///     Gets or sets the current value.
        /// </summary>
        /// <value>
        ///     The current value.
        /// </value>
        [XmlAttribute("Value")]
        public int Value
        {
            get => StartValue + Modifier + _currentValueDiff;
            set
            {
                _currentValueDiff = value - StartValue - Modifier;
                OnPropertyChanged();
            }
        }

        [XmlIgnore] private Formula _formula = new Formula();

        /// <summary>
        ///     Gets or sets the formula.
        /// </summary>
        /// <value>
        ///     The formula.
        /// </value>
        [XmlIgnore]
        public Formula Formula
        {
            get => _formula;
            set
            {
                if (_formula == value)
                    return;
                _formula = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the formula text.
        /// </summary>
        /// <value>
        ///     The formula text.
        /// </value>
        [XmlAttribute("Formula")]
        public string FormulaText
        {
            get => Formula.Expression;
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