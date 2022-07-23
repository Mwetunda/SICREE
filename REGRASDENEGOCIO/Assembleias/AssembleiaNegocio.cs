using ACESSOABASEDEDADOS;
using PROPRIEDADES;
using System.Collections.Generic;
using System.Linq;

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
        public List<viewAssembleia> ListaMunicipio(int municipioId, bool escrutinada)
        {
            return EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipioId && x.Escrutinada == escrutinada).ToList();
        }

        //Metodo Listar
        public List<viewAssembleia> ListaProvincia(int provinciaId, bool escrutinada)
        {
            List<viewAssembleia> Lista = new List<viewAssembleia>();

            var Municipios = municipio.Listar(provinciaId);

            foreach(var municipio in Municipios)
            {
              var Assembleias =  EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio.MunicipioID && x.Escrutinada == escrutinada).ToList();

              Lista.AddRange(Assembleias);
            }

            return Lista;
        }

        //Busca Pelo ID
        public viewAssembleia BuscaID(int ID)
        {
            return  EntidadeBD.BuscaTotal().FirstOrDefault(entidade => entidade.Numero == ID);
        }

        //Verificar
        public bool Verificar(int numero)
        {
            var Entidade = EntidadeBD.Verificar(numero);
            
            if (Entidade == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Escrutinada(int numero)
        {
            var Entidade = EntidadeBD.Estrutinada(numero);

            if (Entidade == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Busca
        public viewAssembleia EstatisticaNacional()
        {
            viewAssembleia geral = new viewAssembleia();

            geral.Numero = EntidadeBD.BuscaTotal().Count();
            geral.NumeroEleitores = EntidadeBD.BuscaTotal().Sum(x => x.NumeroEleitores);

            return geral;
        }

        public viewAssembleia EstatisticaProvincial(int idProvincia)
        {
            viewAssembleia dadosProvinciais = new viewAssembleia();

            dadosProvinciais.Numero = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaId == idProvincia).Count();
            dadosProvinciais.NumeroEleitores = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaId == idProvincia).Sum(x => x.NumeroEleitores);

            return dadosProvinciais;
        }

        public viewAssembleia EstatisticaMunicipal(int idMunicipio)
        {
            viewAssembleia geral = new viewAssembleia();

            geral.Numero = EntidadeBD.BuscaTotal().Where(x=>x.MunicipioID == idMunicipio).Count();
            geral.NumeroEleitores = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == idMunicipio).Sum(x => x.NumeroEleitores);

            return geral;
        }

        //Busca
        public List<viewAssembleia> EstatisticaNacionalReport()
        {
            List<viewAssembleia> Geral = new List<viewAssembleia>();
            var Lista = EntidadeBD.BuscaTotal();

            var NumeroAssembleia = Lista.Count();
            var NumeroEleitor = Lista.Sum(x => x.NumeroEleitores);

            if (Geral.Count == 0)
            {
                Geral.Add(new viewAssembleia
                {
                    Numero = NumeroAssembleia,
                    NumeroEleitores = NumeroEleitor,
                });
            }

            return Geral;
        }
    }
}
