using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

namespace REGRASDENEGOCIO
{
    public class ProvinciaNegocio
    {
        ProvinciaBD EntidadeBD;

        public ProvinciaNegocio()
        {
            EntidadeBD = new ProvinciaBD();
        }

        //Metodo Gravar()
        public void Gravar(ProvinciaPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Actualizar
        public void Actualizar(ProvinciaPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }

        //Metodo Listar
        public List<ViewProvincia> Listar()
        {
            return EntidadeBD.BuscaTotal();
        }
        //Metodo Listar
        public List<ViewProvincia> ListarNome(string nome)
        {
            return EntidadeBD.BuscaTotal().Where(x=>x.Provincia.Contains(nome)).ToList();
        }

        //Busca Pelo ID
        public ViewProvincia BuscaID(int ID)
        {
            ViewProvincia EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Codigo == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new ViewProvincia();

                EntidadePropriedades.Codigo = entidade.Codigo;
                EntidadePropriedades.Provincia = entidade.Provincia;
                
            }
            return EntidadePropriedades;
        }

        //Verificar
        public bool Verificar(string provincia)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Provincia == provincia).ToList();

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
