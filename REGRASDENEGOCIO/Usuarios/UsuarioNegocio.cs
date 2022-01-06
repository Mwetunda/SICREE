using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACESSOABASEDEDADOS;
using PROPRIEDADES;

namespace REGRASDENEGOCIO
{
    public class UsuarioNegocio
    {
        UsuarioBD EntidadeBD;
        
        public UsuarioNegocio()
        {
            EntidadeBD = new UsuarioBD();
        }

        //Metodo Gravar()
        public void Gravar(UsuarioPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }
        //Metodo Actualizar
        public void Actualizar(UsuarioPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }
        //Metodo ActualizarCredenciais
        public void ActualizarCredenciais(UsuarioPropriedades entidadePropriedade)
        {
            EntidadeBD.ActualizarCredenciais(entidadePropriedade);
        }
        //Metodo Eliminar
        public void Eliminar(UsuarioPropriedades entidadePropriedade)
        {
            EntidadeBD.Excluir(entidadePropriedade);
        }
        //Metodo Listar
        public List<ViewUsuario> Listar(string nome)
        {
            return EntidadeBD.BuscaTotal().Where(entidade=>entidade.Nome.Contains(nome)).ToList();
        }
        //Metodo Listar
        public List<ViewUsuario> ListarAdministradores(string nome)
        {
            return EntidadeBD.BuscaTotal().Where(entidade => entidade.Nome.Contains(nome) && entidade.Previlegio== "Administrador").ToList();
        }

        //Busca Pelo ID
        public UsuarioPropriedades BuscaID(int ID)
        {
            UsuarioPropriedades EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade=>entidade.Codigo== ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new UsuarioPropriedades();

                EntidadePropriedades.Codigo = entidade.Codigo;
                EntidadePropriedades.Nome = entidade.Nome;
                EntidadePropriedades.Telefone = entidade.Telefone;
                EntidadePropriedades.Previlegio = entidade.Previlegio;
                EntidadePropriedades.Estado = entidade.Estado;
            }
            return EntidadePropriedades;
        }

        //Autenticacao
        public UsuarioPropriedades Autenticacao(string login, string senha)
        {
            UsuarioPropriedades EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Login == login && entidade.Senha==senha).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new UsuarioPropriedades();

                EntidadePropriedades.Codigo = entidade.Codigo;
                EntidadePropriedades.Nome = entidade.Nome;
                EntidadePropriedades.Telefone = entidade.Telefone;
                EntidadePropriedades.Previlegio = entidade.Previlegio;
                EntidadePropriedades.Login = entidade.Login;
                EntidadePropriedades.Senha = entidade.Senha;
                EntidadePropriedades.Estado = entidade.Estado;
            }
            return EntidadePropriedades;
        }

        //Verificar
        public bool Verificar(string login, string senha)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Login == login && entidade.Senha==senha).ToList();

            if (Entidade.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
