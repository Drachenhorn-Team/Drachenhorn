using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DSACharacterSheet.Xml.Calculation;

namespace DSACharacterSheet.Desktop.Converter.Validation
{
    public class FormulaValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return new ValidationResult(false, null);

            var formula = new Formula();
            formula.Expression = value.ToString();
            
            return new ValidationResult(formula.IsValid, formula.ValidationMessage);
        }
    }
}
