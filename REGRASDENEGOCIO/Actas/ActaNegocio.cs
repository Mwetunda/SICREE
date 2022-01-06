using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROPRIEDADES;
using ACESSOABASEDEDADOS;

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
            viewActa EntidadePropriedades = null;
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.NumeroAssembleia == ID).ToList();

            foreach (var entidade in Entidade)
            {
                EntidadePropriedades = new viewActa();

                EntidadePropriedades.NumeroAssembleia = entidade.NumeroAssembleia;
                EntidadePropriedades.Usuario = entidade.Usuario;
                EntidadePropriedades.UsuarioID = entidade.UsuarioID;
            }
            return EntidadePropriedades;
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
        public viewActa BuscarDadosMesa()
        {
            viewActa Acta = new viewActa();

            Acta.QtdMesa = EntidadeBD.BuscaTotal().Sum(x => x.QtdMesa);
            Acta.VotosBrancos = EntidadeBD.BuscaTotal().Sum(x => x.VotosBrancos);
            Acta.VotosNulos = EntidadeBD.BuscaTotal().Sum(x => x.VotosNulos);
            Acta.VotosReclamados = EntidadeBD.BuscaTotal().Sum(x => x.VotosReclamados);
            Acta.VotosValidos = EntidadeBD.BuscaTotal().Sum(x => x.VotosValidos);

            Acta.QtdAssembleia = EntidadeBD.BuscaTotal().Count();
            return Acta;
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
            

            if(Acta.Count==0)
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

        // BuscarDadosActaProvincial
        public viewActa BuscarDadosMesaProvincial(int provincia)
        {
            viewActa Acta = new viewActa();

            Acta.QtdMesa = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Sum(x => x.QtdMesa);
            Acta.VotosBrancos = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Sum(x => x.VotosBrancos);
            Acta.VotosNulos = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Sum(x => x.VotosNulos);
            Acta.VotosReclamados = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Sum(x => x.VotosReclamados);
            Acta.VotosValidos = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Sum(x => x.VotosValidos);

            Acta.QtdAssembleia = EntidadeBD.BuscaTotal().Where(x => x.ProvinciaID == provincia).Count();
            return Acta;
        }

        // BuscarDadosActaMunicipal
        public viewActa BuscarDadosMesaMunicipal(int municipio)
        {
            viewActa Acta = new viewActa();

            Acta.QtdMesa = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Sum(x => x.QtdMesa);
            Acta.VotosBrancos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Sum(x => x.VotosBrancos);
            Acta.VotosNulos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Sum(x => x.VotosNulos);
            Acta.VotosReclamados = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Sum(x => x.VotosReclamados);
            Acta.VotosValidos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Sum(x => x.VotosValidos);

            Acta.QtdAssembleia = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).Count();
            return Acta;
        }

        //Busca BuscarDadosMesaAssembleia
        public viewActa BuscarDadosMesaAssembleia(int municipio, int assembleia)
        {
            viewActa Acta = new viewActa();

            Acta.QtdMesa = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).Sum(x => x.QtdMesa);
            Acta.VotosBrancos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).Sum(x => x.VotosBrancos);
            Acta.VotosNulos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).Sum(x => x.VotosNulos);
            Acta.VotosReclamados = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).Sum(x => x.VotosReclamados);
            Acta.VotosValidos = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).Sum(x => x.VotosValidos);

            return Acta;
        }

        //Verificar
        public bool Verificar(int numero)
        {
            var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.NumeroAssembleia == numero).ToList();

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
