using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class UsuarioBD
    {
        private Conexao conexao;
        
        public UsuarioBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(UsuarioPropriedades entidadePropriedades)
        {
            TbUsuario tbEntidade = new TbUsuario();

            tbEntidade.Nome = entidadePropriedades.Nome;
            tbEntidade.Telefone = entidadePropriedades.Telefone;
            tbEntidade.Login = entidadePropriedades.Login;
            tbEntidade.Senha = entidadePropriedades.Senha;
            tbEntidade.Previlegio = entidadePropriedades.Previlegio;
            tbEntidade.Estado = true;

            conexao.BD.AddToTbUsuario(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(UsuarioPropriedades entidadePropriedades)
        {
            TbUsuario tbEntidade = conexao.BD.TbUsuario.First(Entidade => Entidade.UsuarioID == entidadePropriedades.Codigo);
            tbEntidade.Nome = entidadePropriedades.Nome;
            tbEntidade.Telefone = entidadePropriedades.Telefone;
            tbEntidade.Previlegio = entidadePropriedades.Previlegio;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo ActualizarCredenciais
        public void ActualizarCredenciais(UsuarioPropriedades entidadePropriedades)
        {
            TbUsuario tbEntidade = conexao.BD.TbUsuario.First(Entidade => Entidade.UsuarioID == entidadePropriedades.Codigo);
            tbEntidade.Login = entidadePropriedades.Login;
            tbEntidade.Senha = entidadePropriedades.Senha;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo "Excluir"
        public void Excluir(UsuarioPropriedades entidadePropriedades)
        {
            TbUsuario tbEntidade = conexao.BD.TbUsuario.First(Entidade => Entidade.UsuarioID == entidadePropriedades.Codigo);
            tbEntidade.Estado = false;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<ViewUsuario> BuscaTotal()
        {

            List<ViewUsuario> Registos = new List<ViewUsuario>();
            var Lista = (from Entidade in conexao.BD.TbUsuario orderby Entidade.Nome select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                ViewUsuario entidadePropriedades = new ViewUsuario();
                entidadePropriedades.Codigo = entidade.UsuarioID;
                entidadePropriedades.Nome = entidade.Nome;
                entidadePropriedades.Telefone = entidade.Telefone;
                entidadePropriedades.Login = entidade.Login;
                entidadePropriedades.Senha = entidade.Senha;
                entidadePropriedades.Previlegio = entidade.Previlegio;
                entidadePropriedades.Estado = entidade.Estado;

                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
