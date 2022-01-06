using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class ConcorrentePropriedades:PropriedadesComuns
    {
        public string Partido { get; set; }
        public string Sigla { get; set; }
        public byte[] Bandeira { get; set; }
    }
    public class ViewConcorrente
    {
        public int Numero { get; set; }
        public string Sigla { get; set; }
        public string Partido { get; set; }
        public string Presidente { get; set; }
        public byte[] Foto { get; set; }
        public byte[] Bandeira { get; set; }
    }
}
