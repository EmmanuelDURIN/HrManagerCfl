using System.Globalization;
using System.Windows.Controls;

namespace HrManager.ValidationRules
{
  public class RequiredValidationRule : ValidationRule
  {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      if (value != null)
      {
        string s = value.ToString();
        if (!string.IsNullOrEmpty(s))
          return ValidationResult.ValidResult;
      }
      return new ValidationResult(false, "Saisie attendue");
    }
  }
}
