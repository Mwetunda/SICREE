using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class GeralBD
    {
        private Conexao conexao;

        public GeralBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(GeralPropriedades entidadePropriedades)
        {
            TbGeral tbEntidade = new TbGeral();

            tbEntidade.ProvinciaID = entidadePropriedades.Provincia.Codigo ;
            tbEntidade.QuantidadeAssembleias = entidadePropriedades.NumeroAssembleia ;
            tbEntidade.QuantidadeEleitores = entidadePropriedades.NumeroEleitor;
            tbEntidade.QuantidadeMesasVotos = entidadePropriedades.NumeroMesa;

            conexao.BD.AddToTbGeral(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(GeralPropriedades entidadePropriedades)
        {
            TbGeral tbEntidade = conexao.BD.TbGeral.First(Entidade => Entidade.ProvinciaID == entidadePropriedades.Provincia.Codigo);

            tbEntidade.QuantidadeAssembleias = entidadePropriedades.NumeroAssembleia;
            tbEntidade.QuantidadeEleitores = entidadePropriedades.NumeroEleitor;
            tbEntidade.QuantidadeMesasVotos = entidadePropriedades.NumeroMesa;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<viewGeral> BuscaTotal()
        {
            List<viewGeral> Registos = new List<viewGeral>();
            var Lista = (from Entidade in conexao.BD.ViewGeral orderby Entidade.Provincia select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                viewGeral entidadePropriedades = new viewGeral();

                entidadePropriedades.Provincia = entidade.Provincia;
                entidadePropriedades.NumeroAssembleia = Convert.ToInt32(entidade.QuantidadeAssembleias);
                entidadePropriedades.NumeroEleitor = Convert.ToInt32(entidade.QuantidadeEleitores);
                entidadePropriedades.NumeroMesa = Convert.ToInt32(entidade.QuantidadeMesasVotos);
                entidadePropriedades.ProvinciaID = Convert.ToInt32(entidade.ProvinciaID);

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
