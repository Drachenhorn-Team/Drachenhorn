using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Drachenhorn.Xml.Sheet;
using Drachenhorn.Xml.Sheet.Skills;
using NCalc;

namespace Drachenhorn.Xml.Calculation
{
    public class Formula : ChildChangedBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current sheet.
        /// </summary>
        /// <value>
        /// The current sheet.
        /// </value>
        public static CharacterSheet CurrentSheet { get; set; }
        
        private CharacterSheet _parentSheet;
        /// <summary>
        /// Gets or sets the parent sheet.
        /// </summary>
        /// <value>
        /// The parent sheet.
        /// </value>
        public CharacterSheet ParentSheet
        {
            get { return _parentSheet; }
            set
            {
                if (_parentSheet == value)
                    return;
                _parentSheet = value;
            }
        }

        private string _expression;
        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public string Expression
        {
            get { return _expression; }
            set
            {
                if (_expression == value)
                    return;
                _expression = value;
                OnPropertyChanged();
            }
        }


        #region Validation

        private string _validationMessage;
        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        /// <value>
        /// The validation message.
        /// </value>
        public string ValidationMessage
        {
            get { return _validationMessage; }
            set
            {
                if (_validationMessage == value)
                    return;
                _validationMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Returns true if Expression is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                if (String.IsNullOrEmpty(Expression))
                    return true;

                var ex = new Expression(Expression);

                if (!ex.HasErrors())
                    return true;

                ValidationMessage = ex.Error;
                return false;
            }
        }

        #endregion Validation

        #endregion Properties

        #region c'tor

        /// <summary>
        /// Initializes a new instance of the <see cref="Formula"/> class.
        /// </summary>
        public Formula()
        {
            ParentSheet = CurrentSheet;
        }

        #endregion c'tor

        /// <summary>
        /// Calculates this instance.
        /// </summary>
        /// <returns>The calculated Value.</returns>
        public double Calculate()
        {
            if (String.IsNullOrEmpty(Expression))
                return 0;

            var e = new Expression(Expression);
            
            // Adding All Parameters
            AddParameterList(ref e, ParentSheet?.Attributes);

            try
            {
                var result = e.Evaluate();

                if (result != null)
                    return Convert.ToDouble(result);
            }
            catch (ArgumentException) { }
            return 0.0;
        }

        /// <summary>
        /// Calculates the value asynchronous.
        /// </summary>
        /// <param name="finished">Action executed on finish.</param>
        /// <returns>The running task.</returns>
        public Task CalculateAsync(Action<double> finished)
        {
            return Task.Run(() => { finished(Calculate()); });
        }

        #region Parameter

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="item">The item.</param>
        private void AddParameter(ref Expression expression, IFormulaKeyItem item)
        {
            if (expression == null || String.IsNullOrEmpty(item?.Key))
                return;

            expression.Parameters[item.Key] = item.Value;
        }

        /// <summary>
        /// Adds the parameter list.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="list">The list.</param>
        private void AddParameterList(ref Expression expression, IEnumerable<IFormulaKeyItem> list)
        {
            if (expression == null || list == null) return;

            foreach (var item in list)
            {
                if (Expression.Contains("[" + item.Key + "]"))
                    AddParameter(ref expression, item);
            }

            foreach (var match in Regex.Matches(Expression, "\\[[a-zA-Z0-9]*\\]"))
            {
                var parameter = match.ToString().Replace("[", "").Replace("]", "");

                if (!expression.Parameters.ContainsKey(parameter))
                    expression.Parameters[parameter] = 0;
            }
        }

        #endregion Parameter

        #region CalculateEvent

        /// <summary>
        /// The Hendler to Calculate all Formulas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CalculateEventArgs"/> instance containing the event data.</param>
        public delegate void CalculateAllHandler(object sender, CalculateEventArgs e);

        /// <summary>
        /// Occurs when [on calculate all].
        /// </summary>
        public static event CalculateAllHandler OnCalculateAll;

        /// <summary>
        /// Raises the calculate all.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        public static void RaiseCalculateAll(CharacterSheet sheet)
        {
            var handler = OnCalculateAll;
            handler?.Invoke(typeof(Formula), new CalculateEventArgs(sheet));
        }

        #endregion CalculateEvent
    }
}