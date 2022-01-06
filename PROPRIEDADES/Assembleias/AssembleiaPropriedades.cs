using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class AssembleiaPropriedades
    {
        public int Numero { get; set; }
        public int MunicipioID { get; set; }
        public string Endereco { get; set; }
    }

    public class viewAssembleia
    {
        public int Numero { get; set; }
        public string Endereco { get; set; }
        public string Municipio { get; set; }
        public int MunicipioID { get; set; }
    }
}
