using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            tbEntidade.BoletinsRecebidos = entidadePropriedades.BoletinsRecebidos;
            tbEntidade.BoletinsUtilizados = entidadePropriedades.BoletinsNaoUtilizados;
            tbEntidade.BoletinsInutilizados = entidadePropriedades.BoletinsInutilizados;

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
            tbEntidade.BoletinsRecebidos = entidadePropriedades.BoletinsRecebidos;
            tbEntidade.BoletinsUtilizados = entidadePropriedades.BoletinsNaoUtilizados;
            tbEntidade.BoletinsInutilizados = entidadePropriedades.BoletinsInutilizados;

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
                entidadePropriedades.BoletinsInutilizados = Convert.ToInt32(entidade.BoletinsInutilizados);
                entidadePropriedades.BoletinsRecebidos = Convert.ToInt32(entidade.BoletinsRecebidos);
                entidadePropriedades.BoletinsUtilizados = Convert.ToInt32(entidade.BoletinsUtilizados);
                entidadePropriedades.MunicipioID = entidade.MunicipioID;
                entidadePropriedades.ProvinciaID = entidade.ProvinciaID;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
        public List<ViewActa> ListarDadosProvincial(int idProvincia)
        {
            var actas = conexao.BD.ViewActa
                .AsNoTracking()
                .Where(x => x.ProvinciaID == idProvincia)
                .ToList();

            return actas;
        }
        public List<ViewActa> ListarDadosNacional()
        {
            var actas = conexao.BD.ViewActa
                .AsNoTracking()
                .ToList();

            return actas;
        }
        public List<ViewActa> ListarDadosMunicipal(int idMunicipio)
        {
            var actas = conexao.BD.ViewActa
                .AsNoTracking()
                .Where(x => x.MunicipioID == idMunicipio)
                .ToList();

            return actas;
        }
        public List<ViewActa> ListarDadosDaAssembleia(int idMunicipio, int idAssembleia)
        {
            var actas = conexao.BD.ViewActa
                .AsNoTracking()
                .Where(x => x.MunicipioID == idMunicipio && x.NumeroAssembleia == idAssembleia)
                .ToList();

            return actas;
        }
    }
}
