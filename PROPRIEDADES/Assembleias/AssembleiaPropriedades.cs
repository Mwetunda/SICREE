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
        public string CoordenadasGeograficas { get; set; }
        public string CodAssembleia { get; set; }
        public int NumeroEleitores { get; set; }
    }

    public class viewAssembleia
    {
        public int Numero { get; set; }
        public string Endereco { get; set; }
        public string Municipio { get; set; }
        public int MunicipioID { get; set; }
        public string Provincia { get; set; }
        public int ProvinciaId { get; set; }
        public string CoordenadasGeograficas { get; set; }
        public int NumeroEleitores { get; set; }
    }
}
