using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

namespace REGRASDENEGOCIO
{
    public class ConcorrenteNegocio
    {
        ConcorrenteBD EntidadeBD;

        public ConcorrenteNegocio()
        {
            EntidadeBD = new ConcorrenteBD();
        }

        //Metodo Gravar()
        public void Gravar(ConcorrentePropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }
       
        //Metodo Actualizar
        public void Actualizar(ConcorrentePropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }
        
        //Metodo Listar
        public List<ViewConcorrente> Listar()
        {
            return EntidadeBD.BuscaTotal().ToList();
        }

        //Busca Pelo ID
        public ViewConcorrente BuscaID(int ID)
        {
            ViewConcorrente EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Numero == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new ViewConcorrente();

                EntidadePropriedades.Numero = entidade.Numero;
                EntidadePropriedades.Sigla = entidade.Sigla;
                EntidadePropriedades.Partido = entidade.Partido;
                EntidadePropriedades.Presidente = entidade.Presidente;
                EntidadePropriedades.Foto = entidade.Foto;
                EntidadePropriedades.Bandeira = entidade.Bandeira;
            }
            return EntidadePropriedades;
        }

        //Verificar
        public bool Verificar(int numero)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Numero == numero).ToList();

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
