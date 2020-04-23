using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private void ResetParameters()
        {
            _parameters.Clear();

            // Adding All Parameters
            AddParameterList(ParentSheet?.Attributes);
            AddParameterList(ParentSheet?.Characteristics.Race.BaseValues);
            AddParameterList(ParentSheet?.Characteristics.Culture.BaseValues);
            AddParameterList(ParentSheet?.Characteristics.Profession.BaseValues);

            // Set Unknown Parameters to 0
            foreach (var match in Regex.Matches(Expression, "\\[[a-zA-Z0-9]*\\]"))
            {
                var parameter = match.ToString().Replace("[", "").Replace("]", "");

                if (!_parameters.ContainsKey(parameter))
                    _parameters[parameter] = 0;
            }
        }

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

            e.Parameters = _parameters;

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

        #region CustomCalculationFunctions

        private static void CustomCalculationExpression(string name, FunctionArgs args)
        {
            if (name.ToLower() == "random")
                if (args.Parameters.Length == 0)
                    args.Result = new Random().Next();
                else if (args.Parameters.Length == 1)
                    args.Result = new Random().Next((int) args.Parameters[0].Evaluate());
                else if (args.Parameters.Length == 2)
                    args.Result = new Random().Next((int) args.Parameters[0].Evaluate(),
                        (int) args.Parameters[1].Evaluate());
        }

        #endregion CustomCalculationFunctions

        #region c'tor

        /// <summary>
        ///     Initializes a new instance of the <see cref="Formula" /> class.
        /// </summary>
        [Obsolete("Only to be used by Xml-Serializer")]
        internal Formula()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Formula" /> class.
        /// </summary>
        /// <param name="parentSheet">The parent sheet.</param>
        public Formula(CharacterSheet parentSheet)
        {
            _parentSheet = parentSheet;
        }

        #endregion

        #region Properties

        private string _expression;

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
                ResetParameters();
                OnPropertyChanged(null);
            }
        }

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
                ResetParameters();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Contains all used parameters for the used Expression
        /// </summary>
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        #endregion


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

        #region Parameter

        /// <summary>
        ///     Adds the parameter.
        /// </summary>
        /// <param name="item">The item.</param>
        private void AddParameter(IFormulaKeyItem item)
        {
            if (string.IsNullOrEmpty(item?.Key) || string.IsNullOrEmpty(Expression))
                return;

            if (!Expression.Contains("[" + item.Key + "]"))
                return;

            if (_parameters.ContainsKey(item.Key))
            {
                var temp = _parameters[item.Key] as int?;

                if (temp != null)
                    _parameters[item.Key] = temp + item.Value;
            }
            else
            {
                _parameters[item.Key] = item.Value;
            }

            void ChangedHandler(object sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName == "Value")
                {
                    RaiseParameterChanged(item.Key, item.Value);
                    item.PropertyChanged -= ChangedHandler;
                }
            }

            item.PropertyChanged += ChangedHandler;
        }

        /// <summary>
        ///     Adds the parameter list.
        /// </summary>
        /// <param name="list">The list.</param>
        private void AddParameterList(IEnumerable<IFormulaKeyItem> list)
        {
            if (list == null) return;

            foreach (var item in list)
                AddParameter(item);
        }

        /// <summary>
        ///     The Handler to recalculate if Parameters change
        /// </summary>
        public delegate void ParameterChangedHandler(string key, int value);

        /// <summary>
        ///     Occures when [parameter changed].
        /// </summary>
        public event ParameterChangedHandler ParameterChanged;

        private void RaiseParameterChanged(string key, int value)
        {
            ResetParameters();
            ParameterChanged?.Invoke(key, value);
        }

        #endregion Parameter
    }
}