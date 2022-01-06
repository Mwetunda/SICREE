using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

namespace REGRASDENEGOCIO
{
    public class GeralNegocio
    {
        GeralBD EntidadeBD;

        public GeralNegocio()
        {
            EntidadeBD = new GeralBD();
            
        }

        //Metodo Gravar()
        public void Gravar(GeralPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Actualizar
        public void Actualizar(GeralPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }

        //Busca 
        public List<viewGeral> BuscaTotal()
        {
            
            return EntidadeBD.BuscaTotal();
        }

        //Busca Pelo ID
        public viewGeral DadosGeraisProvincial(int ID)
        {
            viewGeral EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.ProvinciaID == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new viewGeral();

                EntidadePropriedades.ProvinciaID = entidade.ProvinciaID;
                EntidadePropriedades.NumeroAssembleia = entidade.NumeroAssembleia ;
                EntidadePropriedades.NumeroEleitor = entidade.NumeroEleitor;
                EntidadePropriedades.NumeroMesa = entidade.NumeroMesa;
            }
            return EntidadePropriedades;
        }

        //Busca
        public viewGeral DadosGeraisNacional()
        {
            viewGeral geral = new viewGeral();

            geral.NumeroAssembleia = EntidadeBD.BuscaTotal().Sum(x => x.NumeroAssembleia);
            geral.NumeroEleitor = EntidadeBD.BuscaTotal().Sum(x => x.NumeroEleitor);
            geral.NumeroMesa = EntidadeBD.BuscaTotal().Sum(x => x.NumeroMesa);

            return geral;
        }

        //Busca
        public List<viewGeral> DadosGeraisNacionalReport()
        {
            List<viewGeral> Geral = new List<viewGeral>();
            var Lista = EntidadeBD.BuscaTotal();

            var NumeroAssembleia = Lista.Sum(x => x.NumeroAssembleia);
            var NumeroEleitor = Lista.Sum(x => x.NumeroEleitor);
            var NumeroMesa = Lista.Sum(x => x.NumeroMesa);

            if(Geral.Count==0)
            {
                Geral.Add(new viewGeral
                {
                    NumeroAssembleia = NumeroAssembleia,
                    NumeroEleitor = NumeroEleitor,
                    NumeroMesa = NumeroMesa
                });
            }

            return Geral;
        }

        //Verificar
        public bool Verificar(int numero)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.ProvinciaID == numero).ToList();

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
