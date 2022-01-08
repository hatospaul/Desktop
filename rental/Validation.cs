using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace rental
{
    public class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString == "")
            {
                return new ValidationResult(false, "String cannot be empty!");
            }
            else if (aString.Length < 2)
            {
                return new ValidationResult(false, "String must have 2 chars!");
            }
            return new ValidationResult(true, null);
        }
    }

    public class IdValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string aString = value.ToString();
            int result = 0;
            if (int.TryParse(aString, out result))
            {
                return new ValidationResult(false, "Invalid ID, not a valid integer!");
            }
            else if (result < 0)
            {
                return new ValidationResult(false, "Invalid ID, should be a positive integer!");
            }
            return new ValidationResult(true, null);
        }
    }
}
