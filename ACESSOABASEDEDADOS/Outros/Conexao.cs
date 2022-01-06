using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACESSOABASEDEDADOS
{
    public class Conexao
    {
        public SICREEEntities BD = new SICREEEntities();

        public void Abrir()
        {
            BD.Connection.Open();
        }
        public void Fechar()
        {
            BD.Connection.Close();
        }
        public void Salvar()
        {
            BD.SaveChanges();
        }
    }
}
