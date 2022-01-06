using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;

namespace ACESSOABASEDEDADOS
{
    public class ConcorrenteBD
    {
        private Conexao conexao;

        public ConcorrenteBD()
        {
            conexao = new Conexao();
        }

        //Metodo Gravar
        public void Gravar(ConcorrentePropriedades entidadePropriedades)
        {
            TbConcorrente tbEntidade = new TbConcorrente();

            tbEntidade.NumeroOrdem = entidadePropriedades.Codigo;
            tbEntidade.Sigla = entidadePropriedades.Sigla;
            tbEntidade.NomePartido = entidadePropriedades.Partido;
            tbEntidade.NomePresidente = entidadePropriedades.Nome;
            tbEntidade.FotoPresidente = entidadePropriedades.Foto;
            tbEntidade.BandeiraPartido = entidadePropriedades.Bandeira;
    
            conexao.BD.AddToTbConcorrente(tbEntidade);
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Alterar
        public void Actualizar(ConcorrentePropriedades entidadePropriedades)
        {
            TbConcorrente tbEntidade = conexao.BD.TbConcorrente.First(Entidade => Entidade.NumeroOrdem == entidadePropriedades.Codigo);
            tbEntidade.Sigla = entidadePropriedades.Sigla;
            tbEntidade.NomePartido = entidadePropriedades.Partido;
            tbEntidade.NomePresidente = entidadePropriedades.Nome;
            tbEntidade.FotoPresidente = entidadePropriedades.Foto;
            tbEntidade.BandeiraPartido = entidadePropriedades.Bandeira;
            conexao.Abrir();
            conexao.Salvar();
            conexao.Fechar();
        }

        //Metodo Listar
        public List<ViewConcorrente> BuscaTotal()
        {

            List<ViewConcorrente> Registos = new List<ViewConcorrente>();
            var Lista = (from Entidade in conexao.BD.TbConcorrente orderby Entidade.NumeroOrdem select Entidade).ToList();

            foreach (var entidade in Lista)
            {
                ViewConcorrente entidadePropriedades = new ViewConcorrente();

                entidadePropriedades.Numero = entidade.NumeroOrdem;
                entidadePropriedades.Sigla = entidade.Sigla;
                entidadePropriedades.Partido = entidade.NomePartido;
                entidadePropriedades.Presidente = entidade.NomePresidente;
                entidadePropriedades.Foto = entidade.FotoPresidente;
                entidadePropriedades.Bandeira = entidade.BandeiraPartido;
                
                Registos.Add(entidadePropriedades);
            }
            return Registos;
        }
    }
}
