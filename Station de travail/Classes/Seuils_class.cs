using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station_de_travail
{
    public class Seuils_class
    {
        public int id { get; set; }
        public string id_wasp { get; set; }
        public DateTime last_modif { get; set; }
        public string sensor { get; set; }
        public string min_valeur { get; set; }
        public string max_valeur { get; set; }
    }
}
