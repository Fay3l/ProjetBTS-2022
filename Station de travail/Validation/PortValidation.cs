using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Station_de_travail
{
    class PortValidation : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value.ToString();
                double i;
                double.TryParse(text, out i);
                if (i == 0 || i > 65535)
                    return new ValidationResult(false, "Invalid");

                return ValidationResult.ValidResult;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
