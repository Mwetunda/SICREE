using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class GeralPropriedades
    {
        public ProvinciaPropriedades Provincia { get; set; }
        public int NumeroEleitor { get; set; }
        public int NumeroAssembleia { get; set; }
        public int NumeroMesa { get; set; }
    }
    public class viewGeral
    {
        public string Provincia { get; set; }
        public int NumeroEleitor { get; set; }
        public int NumeroAssembleia { get; set; }
        public int NumeroMesa { get; set; }
        public int ProvinciaID { get; set; }
    }
}
