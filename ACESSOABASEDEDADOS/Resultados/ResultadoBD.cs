using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class ResultadoBD
    {
        private Conexao conexao;

        public ResultadoBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(ResultadoPropriedades entidadePropriedades)
        {
            TbResultadoConcorrente tbEntidade = new TbResultadoConcorrente();

            tbEntidade.ConcorrenteID = entidadePropriedades.Concorrente.Codigo;
            tbEntidade.AssembleiaID = entidadePropriedades.Assembleia.Numero;
            tbEntidade.NumeroVotos = entidadePropriedades.Votos;

            conexao.BD.AddToTbResultadoConcorrente(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(ResultadoPropriedades entidadePropriedades)
        {
            TbResultadoConcorrente tbEntidade = conexao.BD.TbResultadoConcorrente.First(Entidade => Entidade.ResultadoID == entidadePropriedades.Codigo);

            tbEntidade.NumeroVotos = entidadePropriedades.Votos;

            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<viewResultado> BuscaTotal()
        {
            List<viewResultado> Registos = new List<viewResultado>();

            var lista = conexao.BD.ViewResultados.AsNoTracking().AsQueryable();

            //var Lista = (from Entidade in conexao.BD.ViewResultados orderby Entidade.ConcorrenteID select Entidade);

            var Lista1 = lista;

            foreach (var entidade in Lista1)
            {
                viewResultado entidadePropriedades = new viewResultado();

                entidadePropriedades.NumeroAssembleia = entidade.NumeroAssembleia;
                entidadePropriedades.Partido = entidade.Sigla;
                entidadePropriedades.MunicipioID = entidade.MunicipioID;
                entidadePropriedades.ProvinciaID = entidade.ProvinciaID;
                entidadePropriedades.Municipio = entidade.Municipio;
                entidadePropriedades.Provincia = entidade.Provincia;

                entidadePropriedades.Votos = entidade.NumeroVotos;
                entidadePropriedades.ConcorrenteID = entidade.ConcorrenteID;
               
                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
