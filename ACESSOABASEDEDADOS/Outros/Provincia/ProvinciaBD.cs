using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class ProvinciaBD
    {
        private Conexao conexao;

        public ProvinciaBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(ProvinciaPropriedades entidadePropriedades)
        {
            TbProvincia tbEntidade = new  TbProvincia();

            tbEntidade.ProvinciaID = entidadePropriedades.Codigo;
            tbEntidade.Designacao = entidadePropriedades.Nome;

            conexao.BD.AddToTbProvincia(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(ProvinciaPropriedades entidadePropriedades)
        {
            TbProvincia tbEntidade = conexao.BD.TbProvincia.First(Entidade => Entidade.ProvinciaID == entidadePropriedades.Codigo);

            tbEntidade.Designacao = entidadePropriedades.Nome;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<ViewProvincia> BuscaTotal()
        {

            List<ViewProvincia> Registos = new List<ViewProvincia>();
            var Lista = (from Entidade in conexao.BD.TbProvincia orderby Entidade.Designacao select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                ViewProvincia entidadePropriedades = new ViewProvincia();

                entidadePropriedades.Codigo = entidade.ProvinciaID;
                entidadePropriedades.Provincia = entidade.Designacao;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
