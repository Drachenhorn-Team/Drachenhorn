using System;
using System.Collections.Generic;
using System.Data;
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
            OnCalculateAll += RecalculateAll;
        }

        ~Formula()
        {
            OnCalculateAll -= RecalculateAll;
        }

        private void RecalculateAll(object sender, CalculateEventArgs e)
        {
            if (e.Sheet == ParentSheet)
                return;

            this.Calculate();
        }

        #endregion c'tor

        public double Calculate()
        {
            if (String.IsNullOrEmpty(Expression))
                return 0;

            var e = new Expression(Expression);


            // Adding All Parameters
            AddParameterList(ref e, ParentSheet.Attributes);


            var result = e.Evaluate();

            if (result != null)
                return (double)result;

            return 0.0;
        }

        #region Parameter
        
        private void AddParameter(ref Expression expression, IFormulaKeyItem item)
        {
            if (String.IsNullOrEmpty(item.Key))
                return;

            expression.Parameters[item.Key] = item.Value;
        }

        private void AddParameterList(ref Expression expression, IEnumerable<IFormulaKeyItem> list)
        {
            foreach (var item in list)
            {
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