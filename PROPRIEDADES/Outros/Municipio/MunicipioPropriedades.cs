using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class MunicipioPropriedades:PropriedadesComuns
    {
        public int ProvinciaID { get; set; }
    }

    public class viewMunicipio
    {
        public int MunicipioID { get; set; }
        public string Municipio { get; set; }
        public int ProvinciaID { get; set; }
        public string Provincia { get; set; }
    }
}
