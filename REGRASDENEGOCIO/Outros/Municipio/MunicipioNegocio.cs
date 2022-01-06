using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

namespace REGRASDENEGOCIO
{
    public class MunicipioNegocio
    {
        MunicipioBD EntidadeBD;

        public MunicipioNegocio()
        {
            EntidadeBD = new MunicipioBD();
        }

        //Metodo Gravar()
        public void Gravar(MunicipioPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Actualizar
        public void Actualizar(MunicipioPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }

        //Metodo Listar
        public List<viewMunicipio> Listar(int provinciaId)
        {
            return EntidadeBD.BuscaTotal().Where(x=>x.ProvinciaID==provinciaId).ToList();
        }
        //Metodo Listar
        public List<viewMunicipio> ListarNome(int provinciaId, string nome)
        {
            return EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID==provinciaId && x.Municipio.Contains(nome)).ToList();
        }

        //Busca Pelo ID
        public viewMunicipio BuscaID(int ID)
        {
            viewMunicipio EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.MunicipioID == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new viewMunicipio();

                EntidadePropriedades.MunicipioID = entidade.MunicipioID;
                EntidadePropriedades.Municipio = entidade.Municipio;
                EntidadePropriedades.Provincia = entidade.Provincia;
            }
            return EntidadePropriedades;
        }

        //Verificar
        public bool Verificar(string municipio)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Municipio==municipio).ToList();

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
