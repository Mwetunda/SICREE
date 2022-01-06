using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROPRIEDADES
{
    public class UsuarioPropriedades : PropriedadesComuns
    {
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Previlegio { get; set; }
        public bool? Estado { get; set; }
    }

    public class ViewUsuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Previlegio { get; set; }
        public bool? Estado { get; set; }
    }
}
