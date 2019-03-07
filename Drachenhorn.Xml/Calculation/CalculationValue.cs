using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet;

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
        protected CalculationValue(CharacterSheet sheet)
        {
            Formula = new Formula(sheet);

            Formula.PropertyChanged += (sender, args) => { StartValue = (int) Math.Round(Formula.Calculate()); };

            Formula.ParameterChanged += (key, value) => { StartValue = (int) Math.Round(Formula.Calculate()); };
        }

        #endregion

        #region Properties

        [XmlIgnore] private int _currentValueDiff;

        [XmlIgnore] private Formula _formula;

        [XmlIgnore] private int _modifier;

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
                OnPropertyChanged("Value");
            }
        }

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
                    return;

                if (Formula.Expression == value)
                    return;

                Formula.Expression = value;
            }
        }

        #endregion

        /// <summary>
        ///     Gets the information.
        /// </summary>
        /// <param name="dictionary">The dictionary to add the Information to.</param>
        public void GetInformation(ref Dictionary<string, string> dictionary)
        {
            if (!string.IsNullOrEmpty(Formula.Expression))
                dictionary.Add("%Info.Formula", Formula.Expression);

            dictionary.Add("%CharacterSheet.StartValue", StartValue.ToString());
            dictionary.Add("%CharacterSheet.Modifier", Modifier.ToString());
            dictionary.Add("%CharacterSheet.Value", Value.ToString());
        }
    }
}