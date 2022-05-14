using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class AssembleiaBD
    {
        private Conexao conexao;

        public AssembleiaBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(AssembleiaPropriedades entidadePropriedades)
        {
            TbAssembleia tbEntidade = new TbAssembleia();

            tbEntidade.MunicipioID = entidadePropriedades.MunicipioID;
            tbEntidade.NumeroAssembleia = entidadePropriedades.Numero;
            tbEntidade.Endereco = entidadePropriedades.Endereco;
            tbEntidade.CoordenadasGeograficas = entidadePropriedades.CoordenadasGeograficas;
            tbEntidade.NumEleitores = entidadePropriedades.NumeroEleitores;

            conexao.BD.AddToTbAssembleia(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(AssembleiaPropriedades entidadePropriedades)
        {
            TbAssembleia tbEntidade = conexao.BD.TbAssembleia.First(Entidade => Entidade.NumeroAssembleia == entidadePropriedades.Numero);

            tbEntidade.MunicipioID = entidadePropriedades.MunicipioID;
            tbEntidade.Endereco = entidadePropriedades.Endereco;
            tbEntidade.CoordenadasGeograficas = entidadePropriedades.CoordenadasGeograficas;
            tbEntidade.NumEleitores = entidadePropriedades.NumeroEleitores;

            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<viewAssembleia> BuscaTotal()
        {
            List<viewAssembleia> Registos = new List<viewAssembleia>();
            var Lista = (from Entidade in conexao.BD.ViewAssembleia orderby Entidade.NumeroAssembleia select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                viewAssembleia entidadePropriedades = new viewAssembleia();

                entidadePropriedades.MunicipioID = entidade.MunicipioID;
                entidadePropriedades.Municipio = entidade.Municipio;
                entidadePropriedades.Numero = entidade.NumeroAssembleia;
                entidadePropriedades.Provincia = entidade.Provincia;
                entidadePropriedades.ProvinciaId = entidade.ProvinciaID;
                entidadePropriedades.Endereco = entidade.Endereco;
                entidadePropriedades.CoordenadasGeograficas = entidade.CoordenadasGeograficas;
                entidadePropriedades.NumeroEleitores =(int) entidade.NumEleitores;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }

        public ViewAssembleia Verificar(int numeroAssembleia)
        {
            
            var assembleia = conexao.BD.ViewAssembleia.FirstOrDefault(x=>x.NumeroAssembleia== numeroAssembleia);
            return assembleia;
        }
    }
}
