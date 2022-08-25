using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Station_de_travail.Validation
{
    class DateValidation : ValidationRule
    {
        public string Date { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value.ToString();
            
            //if(text )
            //{

            //}
            return ValidationResult.ValidResult;
        }
    }
}
