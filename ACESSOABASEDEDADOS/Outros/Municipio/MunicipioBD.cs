using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class MunicipioBD
    {
        private Conexao conexao;

        public MunicipioBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(MunicipioPropriedades entidadePropriedades)
        {
            TbMunicipio tbEntidade = new TbMunicipio();

            tbEntidade.MunicipioID = entidadePropriedades.Codigo;
            tbEntidade.ProvinciaID = entidadePropriedades.ProvinciaID;
            tbEntidade.Designacao = entidadePropriedades.Nome;

            conexao.BD.AddToTbMunicipio(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(MunicipioPropriedades entidadePropriedades)
        {
            TbMunicipio tbEntidade = conexao.BD.TbMunicipio.First(Entidade => Entidade.MunicipioID == entidadePropriedades.Codigo);

            tbEntidade.ProvinciaID = entidadePropriedades.ProvinciaID;
            tbEntidade.Designacao = entidadePropriedades.Nome;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<viewMunicipio> BuscaTotal()
        {
            List<viewMunicipio> Registos = new List<viewMunicipio>();
            var Lista = (from Entidade in conexao.BD.ViewMunicipio orderby Entidade.Municipio select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                viewMunicipio entidadePropriedades = new viewMunicipio();

                entidadePropriedades.MunicipioID = entidade.MunicipioID;
                entidadePropriedades.Municipio = entidade.Municipio;
                entidadePropriedades.ProvinciaID = entidade.ProvinciaID;
                entidadePropriedades.Provincia = entidade.Provincia;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
