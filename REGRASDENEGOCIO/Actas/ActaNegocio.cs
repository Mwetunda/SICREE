using ACESSOABASEDEDADOS;
using PROPRIEDADES;
using System.Collections.Generic;
using System.Linq;

namespace REGRASDENEGOCIO
{
    public class ActaNegocio
    {
        ActaBD EntidadeBD;
        

        public ActaNegocio()
        {
            EntidadeBD = new ActaBD();
        }

        //Metodo Gravar()
        public void Gravar(ActaPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Listar
        public List<viewActa> Listar()
        {
            return EntidadeBD.BuscaTotal().ToList();
        }

        //Busca Pelo ID
        public viewActa BuscaID(int ID)
        {
            var Entidade = EntidadeBD.BuscaTotal().FirstOrDefault(entidade => entidade.NumeroAssembleia == ID);

            return Entidade;
        }

        //Buscr ultima acta
        public int BuscarActa(int ID)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.UsuarioID == ID).Last().NumeroAssembleia;
            return Entidade;  
        }

        //BuscarActaProvincia
        public int BuscarActaProvincia(int ID)
        {
           
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.UsuarioID == ID).Last().NumeroAssembleia;
            return Entidade;
        }

        // BuscarDadosActa
        public viewActa DadosNacionais()
        {
            viewActa Acta = new viewActa();

            var actas = EntidadeBD.ListarDadosNacional();

            if (actas.Count > 0)
            {

                Acta.VotosBrancos = (int)actas.Sum(x => x.VotosBrancos);
                Acta.VotosNulos = (int)actas.Sum(x => x.VotosNulos);
                Acta.VotosReclamados = (int)actas.Sum(x => x.VotosReclamados);
                Acta.VotosValidos = (int)actas.Sum(x => x.VotosValidos);
                Acta.BoletinsUtilizados = (int)actas.Sum(x => x.BoletinsUtilizados);
                Acta.BoletinsRecebidos = (int)actas.Sum(x => x.BoletinsRecebidos);
                Acta.BoletinsInutilizados = (int)actas.Sum(x => x.BoletinsInutilizados);

                Acta.QtdAssembleia = (int)actas.Count;

                return Acta;
            }
            else
            {
                return null;
            }
        }

        // BuscarDadosActaProvincial
        public viewActa BuscarDadosMesaProvincial(int provincia)
        {
            viewActa Acta = new viewActa();

            var actas = EntidadeBD.ListarDadosProvincial(provincia);

            if (actas.Count > 0)
            {

                Acta.VotosBrancos = (int)actas.Sum(x => x.VotosBrancos);
                Acta.VotosNulos = (int)actas.Sum(x => x.VotosNulos);
                Acta.VotosReclamados = (int)actas.Sum(x => x.VotosReclamados);
                Acta.VotosValidos = (int)actas.Sum(x => x.VotosValidos);
                Acta.BoletinsUtilizados = (int)actas.Sum(x => x.BoletinsUtilizados);
                Acta.BoletinsRecebidos = (int)actas.Sum(x => x.BoletinsRecebidos);
                Acta.BoletinsInutilizados = (int)actas.Sum(x => x.BoletinsInutilizados);

                Acta.QtdAssembleia = (int)actas.Count;

                return Acta;
            }
            else
            {
                return null;
            }
        }

        // BuscarDadosActaMunicipal
        public viewActa BuscarDadosMesaMunicipal(int municipio)
        {
            viewActa Acta = new viewActa();

            var actas = EntidadeBD.ListarDadosMunicipal(municipio);

            if (actas.Count > 0)
            {

                Acta.VotosBrancos = (int)actas.Sum(x => x.VotosBrancos);
                Acta.VotosNulos = (int)actas.Sum(x => x.VotosNulos);
                Acta.VotosReclamados = (int)actas.Sum(x => x.VotosReclamados);
                Acta.VotosValidos = (int)actas.Sum(x => x.VotosValidos);
                Acta.BoletinsUtilizados = (int)actas.Sum(x => x.BoletinsUtilizados);
                Acta.BoletinsRecebidos = (int)actas.Sum(x => x.BoletinsRecebidos);
                Acta.BoletinsInutilizados = (int)actas.Sum(x => x.BoletinsInutilizados);

                Acta.QtdAssembleia = (int)actas.Count;

                return Acta;
            }
            else
            {
                return null;
            }
        }

        //Busca BuscarDadosMesaAssembleia
        public viewActa BuscarDadosMesaAssembleia(int municipio, int assembleia)
        {
            viewActa Acta = new viewActa();

            var actas = EntidadeBD.ListarDadosDaAssembleia(municipio, assembleia);

            if(actas.Count > 0)
            {
                
                Acta.VotosBrancos = (int) actas.Sum(x => x.VotosBrancos);
                Acta.VotosNulos = (int)actas.Sum(x => x.VotosNulos);
                Acta.VotosReclamados = (int)actas.Sum(x => x.VotosReclamados);
                Acta.VotosValidos = (int)actas.Sum(x => x.VotosValidos);
                Acta.BoletinsUtilizados = (int)actas.Sum(x => x.BoletinsUtilizados);
                Acta.BoletinsRecebidos = (int)actas.Sum(x => x.BoletinsRecebidos);
                Acta.BoletinsInutilizados = (int)actas.Sum(x => x.BoletinsInutilizados);

                return Acta;
            }
            else
            {
                return null;
            }      
        }

        // BuscarDadosActa
        public List<viewActa> BuscarDadosMesaReport()
        {
            List<viewActa> Acta = new List<viewActa>();
            var Lista = EntidadeBD.BuscaTotal();

            var QtdAssembleia = Lista.Count();
            var QtdMesa = Lista.Sum(x => x.QtdMesa);
            var VotosBrancos = Lista.Sum(x => x.VotosBrancos);
            var VotosNulos = Lista.Sum(x => x.VotosNulos);
            var VotosReclamados = Lista.Sum(x => x.VotosReclamados);
            var VotosValidos = Lista.Sum(x => x.VotosValidos);


            if (Acta.Count == 0)
            {
                Acta.Add(new viewActa
                {
                    QtdAssembleia = QtdAssembleia,
                    QtdMesa = QtdMesa,
                    VotosBrancos = VotosBrancos,
                    VotosNulos = VotosNulos,
                    VotosReclamados = VotosReclamados,
                    VotosValidos = VotosValidos
                });
            }
            return Acta;
        }

        //Verificar
        public bool Verificar(int numero)
        {
            var Entidade = EntidadeBD.BuscaTotal()
                .FirstOrDefault(entidade => entidade.NumeroAssembleia == numero);

            if (Entidade == null)
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
