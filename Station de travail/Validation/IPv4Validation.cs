using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Station_de_travail
{
    class IPv4Validation : ValidationRule
    {
        

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //IPAddress address; 
            string str = value as string;
            
            try
            {
                //if (IPAddress.TryParse(value.ToString(), out IPAddress address))
                //{
                //    return ValidationResult.ValidResult;
                //}
                //return new ValidationResult(false, "Invalid");

                if (String.IsNullOrEmpty(str))
                {
                    return new ValidationResult(false,
                      "Entrez une adresse IP.");
                }

                var parts = str.Split('.');
                if (parts.Length != 4)
                {
                    return new ValidationResult(false,
                      "Adresse IP invalide.");
                }

                foreach (var p in parts)
                {
                    int intPart;
                    if (!int.TryParse(p, NumberStyles.Integer,
                      cultureInfo.NumberFormat, out intPart))
                    {
                        return new ValidationResult(false,
                          "Adresse IP invalide.");
                    }

                    if (intPart < 0 || intPart > 255)
                    {
                        return new ValidationResult(false,
                          "Adresse IP invalide.");
                    }
                }
                return ValidationResult.ValidResult;
            }
            catch(Exception e)
            {
                return new ValidationResult(false, e.Message);
            
            }


            
        }
    }
}
    

