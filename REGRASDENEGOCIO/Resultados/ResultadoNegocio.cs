using ACESSOABASEDEDADOS;
using PROPRIEDADES;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REGRASDENEGOCIO
{
    public class ResultadoNegocio
    {
        ResultadoBD EntidadeBD;
        MunicipioBD municipio;
        AssembleiaBD assembleia;

        public ResultadoNegocio()
        {
            EntidadeBD = new ResultadoBD();

            municipio = new MunicipioBD();
            assembleia = new AssembleiaBD();

        }

        //Metodo Gravar()
        public void Gravar(ResultadoPropriedades entidadePropriedade)
        {
            EntidadeBD.Gravar(entidadePropriedade);
        }

        //Metodo Actualizar
        public void Actualizar(ResultadoPropriedades entidadePropriedade)
        {
            EntidadeBD.Actualizar(entidadePropriedade);
        }

        //Metodo Listar
        public List<viewResultado> ListarResultadoNacional()
        {
            var Lista = EntidadeBD.BuscaTotal().OrderBy(x=>x.ConcorrenteID).ToList();

            List<viewResultado> Resultado = new List<viewResultado>();

            foreach(var resultado in Lista)
            {
                
                if (!Resultado.Any(x=>x.ConcorrenteID == resultado.ConcorrenteID))
                {
                    var TotalConcorrente = Lista.Where(x => x.ConcorrenteID == resultado.ConcorrenteID).Sum(x => x.Votos);
                    var percentagem = (Convert.ToDecimal(TotalConcorrente * 100)) /Convert.ToDecimal(Lista.Sum(x => x.Votos));
                    
                    Resultado.Add(new viewResultado
                    {
                        Partido = resultado.Partido,
                        Votos = TotalConcorrente,
                        Percentagem = Math.Round(percentagem,2).ToString()+" %",
                        ConcorrenteID = resultado.ConcorrenteID,   
                    });
                }
            }
            return Resultado;
        }

        //Metodo Listar
        public List<viewResultado> ListarResultadoProvincial(int provincia)
        {
            var Lista = EntidadeBD.BuscaTotal().Where(x=>x.ProvinciaID==provincia).OrderBy(x => x.ConcorrenteID).ToList();

            List<viewResultado> Resultado = new List<viewResultado>();

            foreach (var resultado in Lista)
            {

                if (!Resultado.Any(x => x.ConcorrenteID == resultado.ConcorrenteID))
                {
                    var TotalConcorrente = Lista.Where(x => x.ConcorrenteID == resultado.ConcorrenteID).Sum(x => x.Votos);
                    var percentagem = (Convert.ToDecimal(TotalConcorrente * 100)) / Convert.ToDecimal(Lista.Sum(x => x.Votos));

                    Resultado.Add(new viewResultado
                    {
                        Partido = resultado.Partido,
                        Votos = TotalConcorrente,
                        Percentagem = Math.Round(percentagem, 2).ToString() + " %",
                        ConcorrenteID = resultado.ConcorrenteID,
                        Provincia = resultado.Provincia

                    });
                }
            }
            return Resultado;
        }

        //Metodo Listar
        public List<viewResultado> ListarResultadoMunicipal(int municipio)
        {
            var Lista = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio).OrderBy(x => x.ConcorrenteID).ToList();

            List<viewResultado> Resultado = new List<viewResultado>();

            foreach (var resultado in Lista)
            {

                if (!Resultado.Any(x => x.ConcorrenteID == resultado.ConcorrenteID))
                {
                    var TotalConcorrente = Lista.Where(x => x.ConcorrenteID == resultado.ConcorrenteID).Sum(x => x.Votos);
                    var percentagem = (Convert.ToDecimal(TotalConcorrente * 100)) / Convert.ToDecimal(Lista.Sum(x => x.Votos));

                    Resultado.Add(new viewResultado
                    {
                        Partido = resultado.Partido,
                        Votos = TotalConcorrente,
                        Percentagem = Math.Round(percentagem, 2).ToString() + " %",
                        ConcorrenteID = resultado.ConcorrenteID,
                        Municipio = resultado.Municipio,
                        Provincia = resultado.Provincia
                    });
                }
            }
            return Resultado;
        }

        //Metodo Listar
        public List<viewResultado> ListarResultadoAssembleia(int municipio, int assembleia)
        {
            var Lista = EntidadeBD.BuscaTotal().Where(x => x.MunicipioID == municipio && x.NumeroAssembleia == assembleia).OrderBy(x => x.ConcorrenteID).ToList();

            List<viewResultado> Resultado = new List<viewResultado>();

            foreach (var resultado in Lista)
            {

                if (!Resultado.Any(x => x.ConcorrenteID == resultado.ConcorrenteID))
                {
                    var TotalConcorrente = Lista.Where(x => x.ConcorrenteID == resultado.ConcorrenteID).Sum(x => x.Votos);
                    var percentagem = (Convert.ToDecimal(TotalConcorrente * 100)) / Convert.ToDecimal(Lista.Sum(x => x.Votos));

                    Resultado.Add(new viewResultado
                    {
                        Partido = resultado.Partido,
                        Votos = TotalConcorrente,
                        Percentagem = Math.Round(percentagem, 2).ToString() + " %",
                        ConcorrenteID = resultado.ConcorrenteID,
                        NumeroAssembleia = resultado.NumeroAssembleia,
                        Municipio = resultado.Municipio,
                        Provincia = resultado.Provincia

                    });
                }
            }
            return Resultado;
        }

        
        //Busca Pelo ID
        //public viewActa BuscaID(int ID)
        //{
        //    viewActa EntidadePropriedades = null;
        //    var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.NumeroAssembleia == ID).ToList();

        //    foreach (var entidade in Entidade)
        //    {
        //        EntidadePropriedades = new viewActa();

        //        EntidadePropriedades.NumeroAssembleia = entidade.NumeroAssembleia;
        //        EntidadePropriedades.Foto = entidade.Foto;
        //        EntidadePropriedades.Usuario = entidade.Usuario;
        //        EntidadePropriedades.UsuarioID = entidade.UsuarioID;
        //    }
        //    return EntidadePropriedades;
        //}

        //Verificar
        //public bool Verificar(int numero)
        //{
        //    var Entidade = EntidadeBD.BuscaTotal().Where(entidade => entidade.NumeroAssembleia == numero).ToList();

        //    if (Entidade.Count == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
