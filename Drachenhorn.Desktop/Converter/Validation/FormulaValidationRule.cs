using System.Globalization;
using System.Windows.Controls;
using Drachenhorn.Xml.Calculation;

namespace Drachenhorn.Desktop.Converter.Validation
{
    public class FormulaValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string))
                return new ValidationResult(false, null);

            var formula = new Formula(null);
            formula.Expression = value.ToString();

            return new ValidationResult(formula.IsValid, formula.ValidationMessage);
        }
    }
}