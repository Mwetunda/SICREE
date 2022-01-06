using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class ResultadoPropriedades:PropriedadesComuns
    {
        public AssembleiaPropriedades Assembleia { get; set; } = new AssembleiaPropriedades();
        public ConcorrentePropriedades Concorrente { get; set; } = new ConcorrentePropriedades();
        public int Votos { get; set; }
    }
    public class viewResultado
    {
        public int NumeroAssembleia { get; set; }//0
        public int ConcorrenteID { get; set; }//1
        public string Partido { get; set; }//2
        public int Votos { get; set; }//3
        public string Percentagem { get; set; }//4
        public int MunicipioID { get; set; }//5
        public int ProvinciaID { get; set; }//6

        public string Municipio { get; set; }//7
        public string Provincia { get; set; }//8

    }
}
