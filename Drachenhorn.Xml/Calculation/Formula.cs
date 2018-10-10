using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Drachenhorn.Xml.Sheet;
using NCalc;

namespace Drachenhorn.Xml.Calculation
{
    /// <summary>
    ///     Formula for Calculation.
    /// </summary>
    /// <seealso cref="Drachenhorn.Xml.ChildChangedBase" />
    public class Formula : ChildChangedBase
    {
        #region c'tor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Formula" /> class.
        /// </summary>
        [Obsolete("Only to be used by Xml-Serializer")]
        internal Formula()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Formula"/> class.
        /// </summary>
        /// <param name="parentSheet">The parent sheet.</param>
        public Formula(CharacterSheet parentSheet)
        {
            ParentSheet = parentSheet;
        }

        #endregion c'tor

        /// <summary>
        ///     Calculates this instance.
        /// </summary>
        /// <returns>The calculated Value.</returns>
        public double Calculate()
        {
            if (string.IsNullOrEmpty(Expression))
                return 0;

            var e = new Expression(Expression);

            // Adding custom Expressions
            e.EvaluateFunction += CustomCalculationExpression;

            // Adding All Parameters
            AddParameterList(ref e, ParentSheet?.Attributes);
            AddParameterList(ref e, ParentSheet?.Characteristics.Race.BaseValues);
            AddParameterList(ref e, ParentSheet?.Characteristics.Culture.BaseValues);

            try
            {
                var result = e.Evaluate();

                if (result != null)
                    return Convert.ToDouble(result);
            }
            catch (ArgumentException)
            {
            }

            return 0.0;
        }

        /// <summary>
        ///     Calculates the value asynchronous.
        /// </summary>
        /// <param name="finished">Action executed on finish.</param>
        /// <returns>The running task.</returns>
        public Task CalculateAsync(Action<double> finished)
        {
            return Task.Run(() => { finished(Calculate()); });
        }

        #region Properties

        private CharacterSheet _parentSheet;

        /// <summary>
        ///     Gets or sets the parent sheet.
        /// </summary>
        /// <value>
        ///     The parent sheet.
        /// </value>
        public CharacterSheet ParentSheet
        {
            get => _parentSheet;
            set
            {
                if (_parentSheet == value)
                    return;
                _parentSheet = value;
                OnPropertyChanged(null);
            }
        }

        private string _expression;

        /// <summary>
        ///     Gets or sets the expression.
        /// </summary>
        /// <value>
        ///     The expression.
        /// </value>
        public string Expression
        {
            get => _expression;
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
        ///     Gets or sets the validation message.
        /// </summary>
        /// <value>
        ///     The validation message.
        /// </value>
        public string ValidationMessage
        {
            get => _validationMessage;
            set
            {
                if (_validationMessage == value)
                    return;
                _validationMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Returns true if Expression is valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Expression))
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

        #region Parameter

        /// <summary>
        ///     Adds the parameter.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="item">The item.</param>
        private void AddParameter(ref Expression expression, IFormulaKeyItem item)
        {
            if (expression == null || string.IsNullOrEmpty(item?.Key))
                return;

            if (expression.Parameters.ContainsKey(item.Key))
            {
                var temp = expression.Parameters[item.Key] as int?;

                if (temp != null)
                    expression.Parameters[item.Key] = temp + item.Value;
            }
            else
                expression.Parameters[item.Key] = item.Value;
        }

        /// <summary>
        ///     Adds the parameter list.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="list">The list.</param>
        private void AddParameterList(ref Expression expression, IEnumerable<IFormulaKeyItem> list)
        {
            if (expression == null || list == null) return;

            foreach (var item in list)
                if (Expression.Contains("[" + item.Key + "]"))
                    AddParameter(ref expression, item);

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
        ///     The Hendler to Calculate all Formulas.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CalculateEventArgs" /> instance containing the event data.</param>
        public delegate void CalculateAllHandler(object sender, CalculateEventArgs e);

        /// <summary>
        ///     Occurs when [on calculate all].
        /// </summary>
        public static event CalculateAllHandler OnCalculateAll;

        /// <summary>
        ///     Raises the calculate all.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        public static void RaiseCalculateAll(CharacterSheet sheet)
        {
            var handler = OnCalculateAll;
            handler?.Invoke(typeof(Formula), new CalculateEventArgs(sheet));
        }

        #endregion CalculateEvent

        #region CustomCalculationFunctions

        private static void CustomCalculationExpression(string name, FunctionArgs args)
        {
            if (name.ToLower() == "random")
            {
                if (args.Parameters.Length == 0)
                {
                    args.Result = new Random().Next();
                }
                else if (args.Parameters.Length == 1)
                {
                    args.Result = new Random().Next((int)args.Parameters[0].Evaluate());
                }
                else if (args.Parameters.Length == 2)
                {
                    args.Result = new Random().Next((int)args.Parameters[0].Evaluate(), (int)args.Parameters[1].Evaluate());
                }
            }
        }

        #endregion CustomCalculationFunctions
    }
}