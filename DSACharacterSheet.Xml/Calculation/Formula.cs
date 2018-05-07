using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DSACharacterSheet.Xml.Sheet;
using DSACharacterSheet.Xml.Sheet.Skills;
using NCalc;

namespace DSACharacterSheet.Xml.Calculation
{
    public class Formula : BindableBase
    {
        #region Properties

        public static CharacterSheet CurrentSheet { get; set; }
        
        private CharacterSheet _parentSheet;
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

        public Formula()
        {
            ParentSheet = CurrentSheet;
        }

        #endregion c'tor

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
                    return (double)result;
            }
            catch (ArgumentException) { }
            return 0.0;
        }

        public Task CalculateAsync(Action<double> finished)
        {
            return Task.Run(() => { finished(Calculate()); });
        }

        #region Parameter
        
        private void AddParameter(ref Expression expression, IFormulaKeyItem item)
        {
            if (expression == null || String.IsNullOrEmpty(item?.Key))
                return;

            expression.Parameters[item.Key] = item.Value;
        }

        private void AddParameterList(ref Expression expression, IEnumerable<IFormulaKeyItem> list)
        {
            if (expression == null || list == null) return;

            foreach (var item in list)
            {
                if (Expression.Contains("[" + item.Key + "]"))
                    AddParameter(ref expression, item);
            }
        }

        #endregion Parameter

        #region CalculateEvent

        public delegate void CalculateAllHandler(object sender, CalculateEventArgs e);
        public static event CalculateAllHandler OnCalculateAll;

        public static void RaiseCalculateAll(CharacterSheet sheet)
        {
            var handler = OnCalculateAll;
            handler?.Invoke(typeof(Formula), new CalculateEventArgs(sheet));
        }

        #endregion CalculateEvent
    }
}