using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Station_de_travail
{
    public class ValeurValidation : ValidationRule
    {
        public double MinValeur { get; set; }
        public double MaxValeur { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string text = value.ToString();
                double i;
                double.TryParse(text, out i);
                if(i > MaxValeur)
                    return new ValidationResult(false, "Invalid");
                if (i < MinValeur)
                    return new ValidationResult(false, "Invalid");
                if (i < 0)
                    return new ValidationResult(false, "Entrez une valeur");
                return ValidationResult.ValidResult;
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
       
    }
}
