using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

namespace REGRASDENEGOCIO
{
    public class AssembleiaNegocio
    {
        AssembleiaBD EntidadeBD;
        MunicipioNegocio municipio;

        public AssembleiaNegocio()
        {
            EntidadeBD = new AssembleiaBD();
            municipio = new MunicipioNegocio();
        }

        //Metodo Gravar()
        public void Gravar(AssembleiaPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Actualizar
        public void Actualizar(AssembleiaPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }

        //Metodo Listar
        public List<viewAssembleia> Listar()
        {
            return EntidadeBD.BuscaTotal();
        }

        //Metodo Listar
        public List<viewAssembleia> ListaMunicipio(int municipioId)
        {
            return EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipioId).ToList();
        }

        //Metodo Listar
        public List<viewAssembleia> ListaProvincia(int provinciaId)
        {
            List<viewAssembleia> Lista = new List<viewAssembleia>();

            var Municipios = municipio.Listar(provinciaId);

            foreach(var municipio in Municipios)
            {
              var Assembleias =  EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio.MunicipioID).ToList();

                foreach(var assembleia in Assembleias)
                {
                    Lista.Add(new viewAssembleia
                    {
                        Numero = assembleia.Numero,
                        Endereco = assembleia.Endereco,
                        Municipio = assembleia.Municipio,
                        MunicipioID = assembleia.MunicipioID
                    });
                }
            }

            return Lista;
        }

        //Busca Pelo ID
        public viewAssembleia BuscaID(int ID)
        {
            viewAssembleia EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.Numero == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new viewAssembleia();

                EntidadePropriedades.MunicipioID = entidade.MunicipioID;
                EntidadePropriedades.Municipio = entidade.Municipio;
                EntidadePropriedades.Numero = entidade.Numero;
                EntidadePropriedades.Endereco = entidade.Endereco;
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
