using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station_de_travail
{
    public class Data_Class
    {
        
        public int id
        {
            get;  set;
        }
        public string id_wasp { get; set; }
        public string sensor { get; set; }
        public string value { get ; set; }
        public DateTime timestamp { get ; set; }
        public double min_valeur { get; set; }
        public double max_valeur { get; set; }
        
       
    }

}
