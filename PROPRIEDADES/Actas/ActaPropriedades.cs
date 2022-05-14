using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class ActaPropriedades : PropriedadesComuns
    {
        public AssembleiaPropriedades Assembleia { get; set; } = new AssembleiaPropriedades();
        public int QtdMesa { get; set; }
        public int VotosBrancos { get; set; }
        public int VotosNulos { get; set; }
        public int VotosReclamados { get; set; }
        public int VotosValidos { get; set; }
        public int UsuarioID { get; set; }
        public int BoletinsRecebidos { get; set; }
        public int BoletinsNaoUtilizados { get; set; }
        public int BoletinsInutilizados { get; set; }
    }
    public class viewActa
    {
        public int NumeroAssembleia { get; set; }
        public int QtdMesa { get; set; }
        public int QtdAssembleia { get; set; }
        public int VotosBrancos { get; set; }
        public int VotosNulos { get; set; }
        public int VotosReclamados { get; set; }
        public int VotosValidos { get; set; }
        public int ProvinciaID { get; set; }
        public int MunicipioID { get; set; }
        public string Municipio { get; set; }
        public string Provincia { get; set; }
        public string Usuario { get; set; }
        public int UsuarioID { get; set; }
        public int BoletinsRecebidos { get; set; }
        public int BoletinsUtilizados { get; set; }
        public int BoletinsInutilizados { get; set; }
    }
    public class Grafico
    {
        public string Descricao { get; set; }
        public int Valor { get; set; }
    }
}
