using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class ActaBD
    {
        private Conexao conexao;

        public ActaBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(ActaPropriedades entidadePropriedades)
        {
            TbActa tbEntidade = new TbActa();

            tbEntidade.AssembleiaID = entidadePropriedades.Assembleia.Numero;
            tbEntidade.QtdMesa = entidadePropriedades.QtdMesa;
            tbEntidade.VotosBrancos = entidadePropriedades.VotosBrancos;
            tbEntidade.VotosNulos = entidadePropriedades.VotosNulos;
            tbEntidade.VotosReclamados = entidadePropriedades.VotosReclamados;
            tbEntidade.VotosValidos = entidadePropriedades.VotosValidos;
            tbEntidade.UsuarioID = entidadePropriedades.UsuarioID;

            conexao.BD.AddToTbActa(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(ActaPropriedades entidadePropriedades)
        {
            TbActa tbEntidade = conexao.BD.TbActa.First(Entidade => Entidade.AssembleiaID == entidadePropriedades.Codigo);

            tbEntidade.QtdMesa = entidadePropriedades.QtdMesa;
            tbEntidade.VotosBrancos = entidadePropriedades.VotosBrancos;
            tbEntidade.VotosNulos = entidadePropriedades.VotosNulos;
            tbEntidade.VotosReclamados = entidadePropriedades.VotosReclamados;
            tbEntidade.VotosValidos = entidadePropriedades.VotosValidos;

            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<viewActa> BuscaTotal()
        {
            List<viewActa> Registos = new List<viewActa>();
            var Lista = (from Entidade in conexao.BD.ViewActa orderby Entidade.NumeroAssembleia select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                viewActa entidadePropriedades = new viewActa();

                entidadePropriedades.NumeroAssembleia = entidade.NumeroAssembleia;
                entidadePropriedades.Usuario = entidade.Usuario;
                entidadePropriedades.UsuarioID = entidade.UsuarioID;

                entidadePropriedades.QtdMesa = Convert.ToInt32(entidade.QtdMesa);
                entidadePropriedades.VotosBrancos = Convert.ToInt32(entidade.VotosBrancos);
                entidadePropriedades.VotosNulos = Convert.ToInt32(entidade.VotosNulos);
                entidadePropriedades.VotosReclamados = Convert.ToInt32(entidade.VotosReclamados);
                entidadePropriedades.VotosValidos = Convert.ToInt32(entidade.VotosValidos);
                entidadePropriedades.MunicipioID = entidade.MunicipioID;
                entidadePropriedades.ProvinciaID = entidade.ProvinciaID;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
