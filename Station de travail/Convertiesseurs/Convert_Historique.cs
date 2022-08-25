using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;

namespace Station_de_travail
{
    
    class Convert_Historique : IValueConverter
    {
        
        List<Data_Class> datamodels = new List<Data_Class>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            datamodels = new MainWindow().JsonValeurs().Result;   
            if (value == null)
            {
                return true;
            }
            else
            {
                foreach(var sensor in datamodels)
                {
                    if(sensor.sensor == "CO2")
                    {
                        if (double.Parse(value.ToString(), System.Globalization.CultureInfo.InvariantCulture) > sensor.max_valeur)
                        {
                            return Brushes.Red;
                        }
                        else if (double.Parse(value.ToString(), System.Globalization.CultureInfo.InvariantCulture) <= sensor.min_valeur)
                        {
                            return Brushes.Blue;
                        }
                        else
                        {
                            return SystemColors.WindowColor;
                        }
                    }
                }
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
         
       
    }
}
