using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROPRIEDADES;
using REGRASDENEGOCIO;

namespace SICREE
{
    public partial class FrmResultados : Form
    {
        readonly ResultadoNegocio resultadoNegocio;
        readonly ResultadoPropriedades resultadoPropriedades;
        private ProvinciaNegocio provinciaNegocio;
        private MunicipioNegocio municipioNegocio;
        readonly ActaNegocio actaNegocio;
        readonly AssembleiaNegocio assembleiaNegocio;
        readonly UIStyle style;

        int qtdAssembleia;

        int qtdAssembleiaGeral;
        int qtdEleitoresGeral;
       

        public FrmResultados()
        {
            InitializeComponent();

            resultadoNegocio = new ResultadoNegocio();
            resultadoPropriedades = new ResultadoPropriedades();

            provinciaNegocio = new ProvinciaNegocio();
            municipioNegocio = new MunicipioNegocio();

            actaNegocio = new ActaNegocio();

            assembleiaNegocio = new AssembleiaNegocio();
            
            style = new UIStyle();

            style.MaxLength(TxtAssembleiaG, 5);
            style.MaxLength(TxtNumAssembleia, 5);

            if (MENU.oper==2)
            {
                BtnGraficoN.Visible = false;
                BtnGraficoP.Visible = false;
                BtnListaN.Visible = true;
                BtnListaP.Visible = true;

                BtnListaN.Location = new Point(216, 0);
                BtnListaP.Location = new Point(486, 0);

                EstatisticaNacional();
                tabControl1.SelectTab(tabPage3);

            }

            GraficoNacional();
        }

        void ComboProvincia()
        {
            var list = provinciaNegocio.Listar();

            //CbxProvinciaG
            CbxProvinciaG.DataSource = list;
            CbxProvinciaG.ValueMember = "Codigo";
            CbxProvinciaG.DisplayMember = "Provincia";

            //CbxProvinciaL
            CbxProvinciaL.DataSource = list;
            CbxProvinciaL.ValueMember = "Codigo";
            CbxProvinciaL.DisplayMember = "Provincia";
        }
        void ComboMunicipio()
        {
            var list = municipioNegocio.Listar(Convert.ToInt32(CbxProvinciaG.SelectedValue)).ToList();
            //CbxMunicipioG
            CbxMunicipioG.DataSource = list;
            CbxMunicipioG.ValueMember = "MunicipioID";
            CbxMunicipioG.DisplayMember = "Municipio";

            CbxMunicipioEstatistica.DataSource = list;
            CbxMunicipioEstatistica.ValueMember = "MunicipioID";
            CbxMunicipioEstatistica.DisplayMember = "Municipio";
        }

        //RESULTADOS NACIONAIS
        void GraficoNacional()
        {
            var Lista = resultadoNegocio.ListarResultadoNacional();

            if (Lista.Count > 0)
            {
                GraficNacional.Series[0].LegendText = "RESULTADOS NACIONAIS";

                GraficNacional.DataSource = Lista;

                GraficNacional.Series[0].XValueMember = "Partido";
                GraficNacional.Series[0].YValueMembers = "Votos";

                GraficNacional.Series[1].XValueMember = "Partido";
                GraficNacional.Series[1].YValueMembers = "Votos";

                LblPHA.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblPNJANGO.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblUNITA.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblFNLA.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblCASA.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblAPN.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                LblPRS.Text = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                LblMPLA.Text = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);
                
                

                viewActa acta = new viewActa();
                acta = actaNegocio.DadosNacionais();

                LblVotosBrancosG1.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG1.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG1.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG1.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                viewAssembleia geral = new viewAssembleia();
                geral = assembleiaNegocio.EstatisticaNacional();

                qtdAssembleiaGeral = geral.Numero;
                qtdEleitoresGeral = geral.NumeroEleitores; 

                var percentagemAssembleia = Convert.ToDecimal(qtdAssembleia * 100) / Convert.ToDecimal(qtdAssembleiaGeral);
                var PercentagemAssembleia = Math.Round(percentagemAssembleia, 2).ToString() + " %";

                lbAssembleias.Text = "Assembleias escrutinadas: " + qtdAssembleia + ", Correspondentes a " + PercentagemAssembleia + " de " + qtdAssembleiaGeral + " Assembleias registadas";
            }
            else
            {
                LblPHA.Text = "";
                LblPNJANGO.Text = "";
                LblUNITA.Text = "";
                LblAPN.Text = "";
                LblPRS.Text = "";
                LblMPLA.Text = "";
                LblFNLA.Text = "";
                LblCASA.Text = "";

                lbAssembleias.Text = "";

                LblVotosBrancosG1.Text = "";
                LblVotosNulosG1.Text = "";
                LblVotosReclamadosG1.Text = "";
                LblVotosValidosG1.Text = "";
            }

        }

        //RESULTADOS PROVINCIAIS
        void GraficoProvicial()
        {
            var Lista = resultadoNegocio.ListarResultadoProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

            if (Lista.Count > 0)
            {
                GraficProvincial.Visible = true;
                Titlo.Visible = false;

                GraficProvincial.DataSource = Lista;
                GraficProvincial.Series[0].LegendText = "RESULTADOS Provinciais";

                GraficProvincial.Series[0].XValueMember = "Partido";
                GraficProvincial.Series[0].YValueMembers = "Votos";

                GraficProvincial.Series[1].XValueMember = "Partido";
                GraficProvincial.Series[1].YValueMembers = "Votos";

                LblPHA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblPNJANGO1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                viewAssembleia geral = new viewAssembleia();
                geral = assembleiaNegocio.EstatisticaProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                qtdAssembleiaGeral = geral.Numero;
                qtdEleitoresGeral = geral.NumeroEleitores;

                var percentagemAssembleia = Convert.ToDecimal(qtdAssembleia * 100) / Convert.ToDecimal(qtdAssembleiaGeral);
                var PercentagemAssembleia = Math.Round(percentagemAssembleia, 2).ToString() + " %";

                lbAssembleias2.Text = "Assembleias escrutinadas: " + qtdAssembleia + ", Correspondentes a " + PercentagemAssembleia + " de " + qtdAssembleiaGeral + " Assembleias registadas";
            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;

                LblPHA1.Text = "";
                LblPNJANGO1.Text = "";
                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";

                LblVotosBrancosG2.Text = "";
                LblVotosNulosG2.Text = "";
                LblVotosReclamadosG2.Text = "";
                LblVotosValidosG2.Text = "";

                MessageBox.Show("Não há informações sobre esta província de momento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    

        //RESULTADOS MUNICIPAIS
        void GraficoMunicipal()
        {
            var Lista = resultadoNegocio.ListarResultadoMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

            if (Lista.Count > 0)
            {
                GraficProvincial.Visible = true;
                Titlo.Visible = false;

                GraficProvincial.DataSource = Lista;
                GraficProvincial.Series[0].LegendText = "RESULTADOS Provinciais";

                GraficProvincial.Series[0].XValueMember = "Partido";
                GraficProvincial.Series[0].YValueMembers = "Votos";

                GraficProvincial.Series[1].XValueMember = "Partido";
                GraficProvincial.Series[1].YValueMembers = "Votos";

                LblPHA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblPNJANGO1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                viewAssembleia geral = new viewAssembleia();
                geral = assembleiaNegocio.EstatisticaMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

                qtdAssembleiaGeral = geral.Numero;
                qtdEleitoresGeral = geral.NumeroEleitores;


                var percentagemAssembleia = Convert.ToDecimal(qtdAssembleia * 100) / Convert.ToDecimal(qtdAssembleiaGeral);
                var PercentagemAssembleia = Math.Round(percentagemAssembleia, 2).ToString() + " %";

                lbAssembleias2.Text = "Assembleias escrutinadas: " + qtdAssembleia + ", Correspondentes a " + PercentagemAssembleia +" de "+ qtdAssembleiaGeral +" Assembleias registadas";

            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;

                MessageBox.Show("Não há informações sobre este Município de momento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LblPHA1.Text = "";
                LblPNJANGO1.Text = "";
                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";

                LblVotosBrancosG2.Text = "";
                LblVotosNulosG2.Text = "";
                LblVotosReclamadosG2.Text = "";
                LblVotosValidosG2.Text = "";
            }
        }
        
        //RESULTADOS DE CADA ASSEMBLEIA
        void GraficoAssembleia()
        {
            var Lista = resultadoNegocio.ListarResultadoAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue),Convert.ToInt32(TxtAssembleiaG.Text));

            if (Lista.Count > 0)
            {
                GraficProvincial.Visible = true;
                Titlo.Visible = false;

                GraficProvincial.DataSource = Lista;
                GraficProvincial.Series[0].LegendText = "RESULTADOS Provinciais";

                GraficProvincial.Series[0].XValueMember = "Partido";
                GraficProvincial.Series[0].YValueMembers = "Votos";

                GraficProvincial.Series[1].XValueMember = "Partido";
                GraficProvincial.Series[1].YValueMembers = "Votos";

                LblPHA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblPNJANGO1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue), Convert.ToInt32(TxtAssembleiaG.Text));

          
                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                lbAssembleias2.Text = "";
            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;

                LblPHA1.Text = "";
                LblPNJANGO1.Text = "";
                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";

                LblVotosBrancosG2.Text = "";
                LblVotosNulosG2.Text = "";
                LblVotosReclamadosG2.Text = "";
                LblVotosValidosG2.Text = "";

                MessageBox.Show("Não existe assembleia  " + TxtAssembleiaG.Text + " neste município", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //DADOS ESTATÍSTICOS
        void EstatisticaNacional()
        {
            try
            {
                var Lista = actaNegocio.DadosNacionais();
                var Lista2 = assembleiaNegocio.EstatisticaNacional();

                if (Lista2 != null)
                {
                    GraficoEstatisticaNacional.DataSource = Lista;
                    GraficoEstatisticaNacionalVotos.DataSource = Lista;

                    GraficoEstatisticaNacionalEleitores.DataSource = Lista2;
                    GraficoEstatisticaNacionalVotantes.DataSource = Lista2;


                    GraficoEstatisticaNacional.Visible = true;
                    GraficoEstatisticaNacionalVotos.Visible = true;
                    GraficoEstatisticaNacionalEleitores.Visible = true;
                    GraficoEstatisticaNacionalVotantes.Visible = true;
                    //Titlo.Visible = false;

                    var TotalVotantes = (Lista != null? Lista.VotosBrancos : 0) 
                        +(Lista != null ? Lista.VotosNulos : 0) 
                        +(Lista != null ? Lista.VotosReclamados : 0) 
                        +(Lista != null ? Lista.VotosValidos : 0);

                    var Abstecoes = Lista2.NumeroEleitores - TotalVotantes;
                    var NaoValidos = (Lista != null ? Lista.VotosBrancos : 0) 
                        +(Lista != null ? Lista.VotosNulos : 0)
                        +(Lista != null ? Lista.VotosReclamados : 0);

                    GraficoEstatisticaNacional.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaNacional.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaNacional.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaNacional.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaNacional.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaNacional.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosNulos : 0);


                    GraficoEstatisticaNacionalVotos.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosValidos : 0);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[3].SetValueY(Lista != null ? Lista.VotosNulos : 0);



                    GraficoEstatisticaNacionalEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaNacionalEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitores);



                    GraficoEstatisticaNacionalVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Lista != null ? Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes : 0;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Lista != null ? Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes : 0;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Lista != null ? Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes : 0;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Lista != null ? Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes : 0;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitores;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitores;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = TotalVotantes != 0 ? Convert.ToDecimal(NaoValidos * 100) / TotalVotantes : 0;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB.Text = PercentagemVBrancos;
                    lbPVN.Text = PercentagemVNulos;
                    lbPVR.Text = PercentagemVReclamados;
                    lbPVV.Text = PercentagemVValidos;
                    lbPV.Text = PercentagemV;
                    lbPA.Text = PercentagemA;
                    lbPTotalN.Text = PercentagemNV;
                    lbTotalN.Text = NaoValidos.ToString("N0");
                }
                else
                {
                    GraficoEstatisticaNacional.Visible = false;

                    GraficoEstatisticaNacionalVotos.Visible = false;

                    GraficoEstatisticaNacionalEleitores.Visible = false;

                    GraficoEstatisticaNacionalVotantes.Visible = false;
                    //Titlo.Visible = true;

                    //LblVotosBrancosG2.Text = "";
                    //LblVotosNulosG2.Text = "";
                    //LblVotosReclamadosG2.Text = "";
                    //LblVotosValidosG2.Text = "";

                    MessageBox.Show("Sem dados estatísticos de momento  ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {

            }
        }

        void EstatisticaProvincial()
        {
            try
            {
                var Lista = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));
                var Lista2 = assembleiaNegocio.EstatisticaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));

                if (Lista2 != null)
                {
                    GraficoEstatisticaProvincial.DataSource = Lista;
                    GraficoEstatisticaProvincialVotos.DataSource = Lista;

                    GraficoEstatisticaProvincialEleitores.DataSource = Lista2;
                    GraficoEstatisticaProvincialVotantes.DataSource = Lista2;


                    GraficoEstatisticaProvincial.Visible = true;
                    GraficoEstatisticaProvincialVotos.Visible = true;
                    GraficoEstatisticaProvincialEleitores.Visible = true;
                    GraficoEstatisticaProvincialVotantes.Visible = true;
                    //Titlo.Visible = false;

                    var TotalVotantes = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0)
                        + (Lista != null ? Lista.VotosValidos : 0);

                    var Abstecoes = Lista2.NumeroEleitores - TotalVotantes;
                    var NaoValidos = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0);

                    GraficoEstatisticaProvincial.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaProvincial.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaProvincial.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaProvincial.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosNulos : 0);

                    GraficoEstatisticaProvincialVotos.ResetAutoValues();

                    GraficoEstatisticaProvincialVotos.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosValidos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[3].SetValueY(Lista != null ? Lista.VotosNulos : 0);



                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitores);



                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Lista != null ? Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes : 0;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Lista != null ? Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes : 0;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Lista != null ? Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes : 0;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Lista != null ? Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes : 0;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitores;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitores;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = TotalVotantes != 0 ? Convert.ToDecimal(NaoValidos * 100) / TotalVotantes : 0;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB2.Text = PercentagemVBrancos;
                    lbPVN2.Text = PercentagemVNulos;
                    lbPVR2.Text = PercentagemVReclamados;
                    lbPVV2.Text = PercentagemVValidos;
                    lbPV2.Text = PercentagemV;
                    lbPA2.Text = PercentagemA;
                    lbPTotal.Text = PercentagemNV;
                    lbTotal.Text = NaoValidos.ToString("N0");
                }
                else
                {
                    lbPVB2.Text = "";
                    lbPVN2.Text = "";
                    lbPVR2.Text = "";
                    lbPVV2.Text = "";
                    lbPV2.Text = "";
                    lbPA2.Text = "";

                    GraficoEstatisticaProvincial.Visible = false;

                    GraficoEstatisticaProvincialVotos.Visible = false;


                    GraficoEstatisticaProvincialEleitores.Visible = false;

                    GraficoEstatisticaProvincialVotantes.Visible = false;
                    //Titlo.Visible = true;

                    //LblVotosBrancosG2.Text = "";
                    //LblVotosNulosG2.Text = "";
                    //LblVotosReclamadosG2.Text = "";
                    //LblVotosValidosG2.Text = "";

                    MessageBox.Show("Sem dados estatísticos de momento  ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception)
            {

            }
        }

        void EstatisticaMunicipal()
        {
            try
            {
                var Lista = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue));
                var Lista2 = assembleiaNegocio.EstatisticaMunicipal(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue));

                if (Lista2 != null)
                {
                    GraficoEstatisticaProvincial.DataSource = Lista;
                    GraficoEstatisticaProvincialVotos.DataSource = Lista;

                    GraficoEstatisticaProvincialEleitores.DataSource = Lista2;
                    GraficoEstatisticaProvincialVotantes.DataSource = Lista2;


                    GraficoEstatisticaProvincial.Visible = true;
                    GraficoEstatisticaProvincialVotos.Visible = true;
                    GraficoEstatisticaProvincialEleitores.Visible = true;
                    GraficoEstatisticaProvincialVotantes.Visible = true;
                    //Titlo.Visible = false;

                    var TotalVotantes = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0)
                        + (Lista != null ? Lista.VotosValidos : 0);

                    var Abstecoes = Lista2.NumeroEleitores - TotalVotantes;
                    var NaoValidos = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0);

                    GraficoEstatisticaProvincial.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaProvincial.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaProvincial.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaProvincial.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosNulos : 0);

                    GraficoEstatisticaProvincialVotos.ResetAutoValues();

                    GraficoEstatisticaProvincialVotos.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosValidos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[3].SetValueY(Lista != null ? Lista.VotosNulos : 0);



                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitores);



                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Lista != null ? Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes : 0;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Lista != null ? Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes : 0;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Lista != null ? Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes : 0;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Lista != null ? Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes : 0;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitores;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitores;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = TotalVotantes != 0 ? Convert.ToDecimal(NaoValidos * 100) / TotalVotantes : 0;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB2.Text = PercentagemVBrancos;
                    lbPVN2.Text = PercentagemVNulos;
                    lbPVR2.Text = PercentagemVReclamados;
                    lbPVV2.Text = PercentagemVValidos;
                    lbPV2.Text = PercentagemV;
                    lbPA2.Text = PercentagemA;
                    lbPTotal.Text = PercentagemNV;
                    lbTotal.Text = NaoValidos.ToString("N0");
                }
                else
                {
                    lbPVB2.Text = "";
                    lbPVN2.Text = "";
                    lbPVR2.Text = "";
                    lbPVV2.Text = "";
                    lbPV2.Text = "";
                    lbPA2.Text = "";

                    GraficoEstatisticaProvincial.Visible = false;

                    GraficoEstatisticaProvincialVotos.Visible = false;


                    GraficoEstatisticaProvincialEleitores.Visible = false;

                    GraficoEstatisticaProvincialVotantes.Visible = false;
                    //Titlo.Visible = true;

                    //LblVotosBrancosG2.Text = "";
                    //LblVotosNulosG2.Text = "";
                    //LblVotosReclamadosG2.Text = "";
                    //LblVotosValidosG2.Text = "";

                    MessageBox.Show("Sem dados estatísticos de momento  ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

            }
        }

        void EstatisticaDaAssembleia()
        {
            try
            {
                var Lista = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue), Convert.ToInt32(TxtNumAssembleia.Text));
                var Lista2 = assembleiaNegocio.BuscaID(Convert.ToInt32(TxtNumAssembleia.Text));

                if (Lista2 != null)
                {
                    GraficoEstatisticaProvincial.DataSource = Lista;
                    GraficoEstatisticaProvincialVotos.DataSource = Lista;

                    GraficoEstatisticaProvincialEleitores.DataSource = Lista2;
                    GraficoEstatisticaProvincialVotantes.DataSource = Lista2;


                    GraficoEstatisticaProvincial.Visible = true;
                    GraficoEstatisticaProvincialVotos.Visible = true;
                    GraficoEstatisticaProvincialEleitores.Visible = true;
                    GraficoEstatisticaProvincialVotantes.Visible = true;
                    //Titlo.Visible = false;

                    var TotalVotantes = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0)
                        + (Lista != null ? Lista.VotosValidos : 0);

                    var Abstecoes = Lista2.NumeroEleitores - TotalVotantes;
                    var NaoValidos = (Lista != null ? Lista.VotosBrancos : 0)
                        + (Lista != null ? Lista.VotosNulos : 0)
                        + (Lista != null ? Lista.VotosReclamados : 0);

                    GraficoEstatisticaProvincial.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaProvincial.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaProvincial.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaProvincial.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincial.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosNulos : 0);

                    GraficoEstatisticaProvincialVotos.ResetAutoValues();

                    GraficoEstatisticaProvincialVotos.Series[0].Points[0].SetValueY(Lista != null ? Lista.VotosValidos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[1].SetValueY(Lista != null ? Lista.VotosBrancos : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[2].SetValueY(Lista != null ? Lista.VotosReclamados : 0);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[3].SetValueY(Lista != null ? Lista.VotosNulos : 0);



                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitores);



                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Lista != null ? Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes : 0;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Lista != null ? Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes : 0;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Lista != null ? Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes : 0;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Lista != null ? Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes : 0;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitores;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitores;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = TotalVotantes != 0 ? Convert.ToDecimal(NaoValidos * 100) / TotalVotantes : 0;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB2.Text = PercentagemVBrancos;
                    lbPVN2.Text = PercentagemVNulos;
                    lbPVR2.Text = PercentagemVReclamados;
                    lbPVV2.Text = PercentagemVValidos;
                    lbPV2.Text = PercentagemV;
                    lbPA2.Text = PercentagemA;
                    lbPTotal.Text = PercentagemNV;
                    lbTotal.Text = NaoValidos.ToString("N0");
                }
                else
                {
                    lbPVB2.Text = "";
                    lbPVN2.Text = "";
                    lbPVR2.Text = "";
                    lbPVV2.Text = "";
                    lbPV2.Text = "";
                    lbPA2.Text = "";

                    GraficoEstatisticaProvincial.Visible = false;

                    GraficoEstatisticaProvincialVotos.Visible = false;


                    GraficoEstatisticaProvincialEleitores.Visible = false;

                    GraficoEstatisticaProvincialVotantes.Visible = false;
                    
                    //Titlo.Visible = true;

                    //LblVotosBrancosG2.Text = "";
                    //LblVotosNulosG2.Text = "";
                    //LblVotosReclamadosG2.Text = "";
                    //LblVotosValidosG2.Text = "";

                    MessageBox.Show("Sem dados estatísticos de momento  ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {

            }
        }


        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            this.Close();
           
        }

        private void BtnGraficoN_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void BtnGraficoP_Click(object sender, EventArgs e)
        {
            RbtMun.Checked = false;
            RbtAss.Checked = false;
            TxtAssembleiaG.Enabled = false;
            CbxProvinciaG.Enabled = true;
            CbxMunicipioG.Enabled = false;
            PnProv.BackColor = Color.FromArgb(71, 202, 94);
            PnMun.BackColor = Color.Gray;
            PnAss.BackColor = Color.Gray;

            ComboProvincia();
            ComboMunicipio();
            
            GraficoProvicial();
            tabControl1.SelectTab(tabPage2);
        }

        private void BtnListaN_Click(object sender, EventArgs e)
        {
            EstatisticaNacional();
            tabControl1.SelectTab(tabPage3);
        }

        private void BtnListaP_Click(object sender, EventArgs e)
        {
            ComboProvincia();
            EstatisticaProvincial();
            tabControl1.SelectTab(tabPage4);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void CbxProvinciaG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboMunicipio();
                GraficoProvicial();

                if (CbxProvinciaG.Text == "Lunda Norte" || CbxProvinciaG.Text == "Lunda Sul" || CbxProvinciaG.Text == "Huíla")
                {
                    LbT.Text = "GRÁFICO DOS RESULTADOS ELEITORAIS DA PROVÍNCIA DA " + CbxProvinciaG.Text.ToUpper();
                }
                else if (CbxProvinciaG.Text == "Benguela" || CbxProvinciaG.Text == "Cabinda" || CbxProvinciaG.Text == "Luanda" || CbxProvinciaG.Text == "Malange")
                {
                    LbT.Text = "GRÁFICO DOS RESULTADOS ELEITORAIS DA PROVÍNCIA DE " + CbxProvinciaG.Text.ToUpper();
                }
                else
                {
                    LbT.Text = "GRÁFICO DOS RESULTADOS ELEITORAIS DA PROVÍNCIA DO " + CbxProvinciaG.Text.ToUpper();
                }
            }
            catch
            {

            }
        }

        private void BtnGravarMunicipio_Click(object sender, EventArgs e)
        {
            
            
        }

        private void GraficProvincial_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton210_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            
            
        }

        private void TxtAssembleiaG_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void TxtMesaG_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            if (TxtAssembleiaG.Text == "")
            {
                MessageBox.Show("Digite o número da assembleia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtAssembleiaG.Focus();
            }
            else
            {
                
                GraficoAssembleia();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GraficoNacional();
        }

        private void TxtAssembleiaG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtMesaG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtAssembleiaL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtMesaL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CbxMunicipioG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(RbtMun.Checked)
                GraficoMunicipal();
            }
            catch
            {

            }
        }

        private void RbtProv_CheckedChanged(object sender, EventArgs e)
        {
            RbtMun.Checked = false;
            RbtAss.Checked = false;

            TxtAssembleiaG.Enabled = false;

            CbxProvinciaG.Enabled = true;
            CbxMunicipioG.Enabled = false;

            PnProv.BackColor = Color.FromArgb(71, 202, 94);
            PnMun.BackColor = Color.Gray;
            PnAss.BackColor = Color.Gray;

        }

        private void RbtMun_CheckedChanged(object sender, EventArgs e)
        {
            RbtProv.Checked = false;
            RbtAss.Checked = false;

            TxtAssembleiaG.Enabled = false;

            CbxProvinciaG.Enabled = false;
            CbxMunicipioG.Enabled = true;

            PnMun.BackColor = Color.FromArgb(71, 202, 94);
            PnProv.BackColor = Color.Gray;
            PnAss.BackColor = Color.Gray;
        }

        private void RbtAss_CheckedChanged(object sender, EventArgs e)
        {
            RbtMun.Checked = false;
            RbtProv.Checked = false;

            TxtAssembleiaG.Enabled = true;

            CbxProvinciaG.Enabled = false;
            CbxMunicipioG.Enabled = false;

            PnAss.BackColor = Color.FromArgb(71, 202, 94);
            PnMun.BackColor = Color.Gray;
            PnProv.BackColor = Color.Gray;
        }

        private void RbtMesa_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GraficoEstatisticaNacionalVotantes_Click(object sender, EventArgs e)
        {

        }

        private void CbxProvinciaL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                ComboMunicipio();
                EstatisticaProvincial();

                if (CbxProvinciaL.Text == "Lunda Norte" || CbxProvinciaL.Text == "Lunda Sul" || CbxProvinciaL.Text == "Huíla")
                {
                    LblListaP.Text = "DADOS ESTATÍSTICOS DA PROVÍNCIA DA " + CbxProvinciaL.Text.ToUpper();
                }
                else if (CbxProvinciaL.Text == "Benguela" || CbxProvinciaL.Text == "Cabinda" || CbxProvinciaL.Text == "Luanda" || CbxProvinciaL.Text == "Malange")
                {
                    LblListaP.Text = "DADOS ESTATÍSTICOS DA PROVÍNCIA DE " + CbxProvinciaL.Text.ToUpper();
                }
                else
                {
                    LblListaP.Text = "DADOS ESTATÍSTICOS DA PROVÍNCIA DO " + CbxProvinciaL.Text.ToUpper();
                }
            }
            catch
            {

            }
        }

        private void GraficoEstatisticaProvincialVotos_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            
            try
            {
                var Lista = resultadoNegocio.ListarResultadoNacional();

                if (Lista.Count > 0)
                {
                    FrmRelatorio rel = new FrmRelatorio();
                    rel.title = "GRÁFICO NACIONAL DOS RESULTADOS ELEITORAIS";
                    rel.path = "SICREE.Relatorios.GraficoNacional.rdlc";

                    rel.Dataset = "DataSet1";
                    rel.obj = Lista;

                    rel.percentagem1 = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                    rel.percentagem2 = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                    rel.percentagem3 = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                    rel.percentagem4 = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                    rel.percentagem5 = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                    rel.percentagem6 = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                    rel.percentagem7 = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                    rel.percentagem8 = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                    rel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if(RbtProv.Checked)
            {
                try
                {
                    var Lista = resultadoNegocio.ListarResultadoProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "GRÁFICO PROVINCIAL DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.GraficoProvincial.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.percentagem1 = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                        rel.percentagem2 = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                        rel.percentagem3 = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                        rel.percentagem4 = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                        rel.percentagem5 = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                        rel.percentagem6 = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                        rel.percentagem7 = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                        rel.percentagem8 = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                        rel.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else if(RbtMun.Checked)
            {
                try
                {
                    var Lista = resultadoNegocio.ListarResultadoMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "GRÁFICO MUNICIPAL DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.GraficoMunicipal.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.percentagem1 = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                        rel.percentagem2 = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                        rel.percentagem3 = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                        rel.percentagem4 = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                        rel.percentagem5 = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                        rel.percentagem6 = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                        rel.percentagem7 = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                        rel.percentagem8 = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                        rel.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                try
                {
                    var Lista = resultadoNegocio.ListarResultadoAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue), Convert.ToInt32(TxtAssembleiaG.Text));

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "GRÁFICO DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.GraficoAssembleia.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.percentagem1 = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                        rel.percentagem2 = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                        rel.percentagem3 = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                        rel.percentagem4 = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                        rel.percentagem5 = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                        rel.percentagem6 = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);
                        rel.percentagem7 = Lista.Where(x => x.ConcorrenteID == 7).Max(x => x.Percentagem);
                        rel.percentagem8 = Lista.Where(x => x.ConcorrenteID == 8).Max(x => x.Percentagem);

                        rel.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                var Lista = resultadoNegocio.ListarResultadoNacional();
                var Acta = actaNegocio.DadosNacionais();
                var Geral = assembleiaNegocio.EstatisticaNacional();

                var QtdEleitores = Geral.NumeroEleitores;
                var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;
                var QtdAbstecoes = Geral.NumeroEleitores - QtdVotantes;

                var VotosValidos = Acta.VotosValidos;
                var VotosBranco = Acta.VotosBrancos;
                var VotosReclamados = Acta.VotosReclamados;
                var VotosNulos = Acta.VotosNulos;
                var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                var QtdEleitoresPercentagem = "100 %";

                var percentagemV = Convert.ToDecimal(QtdVotantes * 100) / Geral.NumeroEleitores;
                var QtdVotantesPercentagem = Math.Round(percentagemV, 2).ToString() + " %";

                var percentagemA = Convert.ToDecimal(QtdAbstecoes * 100) / Geral.NumeroEleitores;
                var QtdAbstencoesPercentagem = Math.Round(percentagemA, 2).ToString() + " %";


                var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                var percentagemNv = Convert.ToDecimal(VotosNaoValidos * 100) / QtdVotantes;
                var VotosNaoValidosPercentagem = Math.Round(percentagemNv, 2).ToString() + " %";


                if (Lista.Count > 0)
                {
                    FrmRelatorio rel = new FrmRelatorio();
                    rel.title = "ESTATÍSTICA NACIONAL";
                    rel.path = "SICREE.Relatorios.EstatisticaNacional.rdlc";

                    rel.Dataset = "DataSet1";
                    rel.obj = Lista;

                    rel.qtdEleitores = QtdEleitores.ToString();
                    rel.qtdVotantes = QtdVotantes.ToString();
                    rel.qtdAbstencoes = QtdAbstecoes.ToString();

                    rel.votosValidos = VotosValidos.ToString();
                    rel.votosBrancos = VotosBranco.ToString();
                    rel.votosReclamados = VotosReclamados.ToString();
                    rel.votosNulos = VotosNulos.ToString();
                    rel.votosNaoValidos = VotosNaoValidos.ToString();


                    rel.qtdEleitoresPercentagem = QtdEleitoresPercentagem.ToString();
                    rel.qtdVotantesPercentagem = QtdVotantesPercentagem.ToString();
                    rel.qtdAbstencoesPercentagem = QtdAbstencoesPercentagem.ToString();

                    rel.votosValidosPercentagem = VotosValidosPercentagem.ToString();
                    rel.votosBrancosPercentagem = VotosBrancosPercentagem.ToString();
                    rel.votosReclamadosPercentagem = VotosReclamadosPercentagem.ToString();
                    rel.votosNulosPercentagem = VotosNulosPercentagem.ToString();
                    rel.votosNaoValidosPercentagem = VotosNaoValidosPercentagem.ToString();

                    rel.qtdAssembleias = Geral.Numero.ToString();
                    rel.provincia = "Angola";


                    rel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            try
            {
                if (RbtProvE.Checked)
                {
                    var Lista = resultadoNegocio.ListarResultadoNacional();
                    var Acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));
                    var Geral = assembleiaNegocio.EstatisticaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));

                    var QtdEleitores = Geral.NumeroEleitores;
                    var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;
                    var QtdAbstecoes = Geral.NumeroEleitores - QtdVotantes;

                    var VotosValidos = Acta.VotosValidos;
                    var VotosBranco = Acta.VotosBrancos;
                    var VotosReclamados = Acta.VotosReclamados;
                    var VotosNulos = Acta.VotosNulos;
                    var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                    var QtdEleitoresPercentagem = "100 %";

                    var percentagemV = Convert.ToDecimal(QtdVotantes * 100) / Geral.NumeroEleitores;
                    var QtdVotantesPercentagem = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(QtdAbstecoes * 100) / Geral.NumeroEleitores;
                    var QtdAbstencoesPercentagem = Math.Round(percentagemA, 2).ToString() + " %";


                    var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                    var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                    var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                    var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                    var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemNv = Convert.ToDecimal(VotosNaoValidos * 100) / QtdVotantes;
                    var VotosNaoValidosPercentagem = Math.Round(percentagemNv, 2).ToString() + " %";


                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ESTATÍSTICA PROVINCIAL";
                        rel.path = "SICREE.Relatorios.EstatisticaProvincial.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.qtdEleitores = QtdEleitores.ToString();
                        rel.qtdVotantes = QtdVotantes.ToString();
                        rel.qtdAbstencoes = QtdAbstecoes.ToString();

                        rel.votosValidos = VotosValidos.ToString();
                        rel.votosBrancos = VotosBranco.ToString();
                        rel.votosReclamados = VotosReclamados.ToString();
                        rel.votosNulos = VotosNulos.ToString();
                        rel.votosNaoValidos = VotosNaoValidos.ToString();


                        rel.qtdEleitoresPercentagem = QtdEleitoresPercentagem.ToString();
                        rel.qtdVotantesPercentagem = QtdVotantesPercentagem.ToString();
                        rel.qtdAbstencoesPercentagem = QtdAbstencoesPercentagem.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagem.ToString();
                        rel.votosBrancosPercentagem = VotosBrancosPercentagem.ToString();
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagem.ToString();
                        rel.votosNulosPercentagem = VotosNulosPercentagem.ToString();
                        rel.votosNaoValidosPercentagem = VotosNaoValidosPercentagem.ToString();

                        rel.qtdAssembleias = Geral.Numero.ToString();
                        rel.provincia = CbxProvinciaL.Text;


                        rel.ShowDialog();
                    }
                }

                if (RbtMunE.Checked)
                {
                    var ListaM = resultadoNegocio.ListarResultadoNacional();
                    var ActaM = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue));
                    var GeralM = assembleiaNegocio.EstatisticaMunicipal(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue));

                    var QtdEleitoresM = GeralM.NumeroEleitores;
                    var QtdVotantesM = ActaM.VotosBrancos + ActaM.VotosNulos + ActaM.VotosReclamados + ActaM.VotosValidos;
                    var QtdAbstecoesM = GeralM.NumeroEleitores - QtdVotantesM;

                    var VotosValidosM = ActaM.VotosValidos;
                    var VotosBrancoM = ActaM.VotosBrancos;
                    var VotosReclamadosM = ActaM.VotosReclamados;
                    var VotosNulosM = ActaM.VotosNulos;
                    var VotosNaoValidosM = ActaM.VotosBrancos + ActaM.VotosNulos + ActaM.VotosReclamados;


                    var QtdEleitoresPercentagemM = "100 %";

                    var percentagemVM = Convert.ToDecimal(QtdVotantesM * 100) / GeralM.NumeroEleitores;
                    var QtdVotantesPercentagemM = Math.Round(percentagemVM, 2).ToString() + " %";

                    var percentagemAM = Convert.ToDecimal(QtdAbstecoesM * 100) / GeralM.NumeroEleitores;
                    var QtdAbstencoesPercentagemM = Math.Round(percentagemAM, 2).ToString() + " %";


                    var percentagemVvalidosM = Convert.ToDecimal(ActaM.VotosValidos * 100) / QtdVotantesM;
                    var VotosValidosPercentagemM = Math.Round(percentagemVvalidosM, 2).ToString() + " %";

                    var percentagemVbrancosM = Convert.ToDecimal(ActaM.VotosBrancos * 100) / QtdVotantesM;
                    var VotosBrancosPercentagemM = Math.Round(percentagemVbrancosM, 2).ToString() + " %";

                    var percentagemVreclamadosM = Convert.ToDecimal(ActaM.VotosReclamados * 100) / QtdVotantesM;
                    var VotosReclamadosPercentagemM = Math.Round(percentagemVreclamadosM, 2).ToString() + " %";

                    var percentagemVnulosM = Convert.ToDecimal(ActaM.VotosNulos * 100) / QtdVotantesM;
                    var VotosNulosPercentagemM = Math.Round(percentagemVnulosM, 2).ToString() + " %";

                    var percentagemNvM = Convert.ToDecimal(VotosNaoValidosM * 100) / QtdVotantesM;
                    var VotosNaoValidosPercentagemM = Math.Round(percentagemNvM, 2).ToString() + " %";


                    if (ListaM.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ESTATÍSTICA MUNICIPAL";
                        rel.path = "SICREE.Relatorios.EstatisticaMunicipal.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = ListaM;

                        rel.qtdEleitores = QtdEleitoresM.ToString();
                        rel.qtdVotantes = QtdVotantesM.ToString();
                        rel.qtdAbstencoes = QtdAbstecoesM.ToString();

                        rel.votosValidos = VotosValidosM.ToString();
                        rel.votosBrancos = VotosBrancoM.ToString();
                        rel.votosReclamados = VotosReclamadosM.ToString();
                        rel.votosNulos = VotosNulosM.ToString();
                        rel.votosNaoValidos = VotosNaoValidosM.ToString();


                        rel.qtdEleitoresPercentagem = QtdEleitoresPercentagemM.ToString();
                        rel.qtdVotantesPercentagem = QtdVotantesPercentagemM.ToString();
                        rel.qtdAbstencoesPercentagem = QtdAbstencoesPercentagemM.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagemM.ToString();
                        rel.votosBrancosPercentagem = VotosBrancosPercentagemM.ToString();
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagemM.ToString();
                        rel.votosNulosPercentagem = VotosNulosPercentagemM.ToString();
                        rel.votosNaoValidosPercentagem = VotosNaoValidosPercentagemM.ToString();

                        rel.qtdAssembleias = GeralM.Numero.ToString();
                        rel.provincia = CbxProvinciaL.Text;
                        rel.municipio = CbxMunicipioEstatistica.Text;


                        rel.ShowDialog();
                    }
                }

                if (RbtAssE.Checked)
                {
                    var ListaM = resultadoNegocio.ListarResultadoNacional();
                    var ActaM = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioEstatistica.SelectedValue),Convert.ToInt32(TxtNumAssembleia.Text));
                    var GeralM = assembleiaNegocio.BuscaID(Convert.ToInt32(TxtNumAssembleia.Text));

                    var QtdEleitoresM = GeralM.NumeroEleitores;
                    var Numero = GeralM.Numero;
                    var QtdVotantesM = ActaM.VotosBrancos + ActaM.VotosNulos + ActaM.VotosReclamados + ActaM.VotosValidos;
                    var QtdAbstecoesM = GeralM.NumeroEleitores - QtdVotantesM;

                    var VotosValidosM = ActaM.VotosValidos;
                    var VotosBrancoM = ActaM.VotosBrancos;
                    var VotosReclamadosM = ActaM.VotosReclamados;
                    var VotosNulosM = ActaM.VotosNulos;
                    var VotosNaoValidosM = ActaM.VotosBrancos + ActaM.VotosNulos + ActaM.VotosReclamados;


                    var QtdEleitoresPercentagemM = "100 %";

                    var percentagemVM = Convert.ToDecimal(QtdVotantesM * 100) / GeralM.NumeroEleitores;
                    var QtdVotantesPercentagemM = Math.Round(percentagemVM, 2).ToString() + " %";

                    var percentagemAM = Convert.ToDecimal(QtdAbstecoesM * 100) / GeralM.NumeroEleitores;
                    var QtdAbstencoesPercentagemM = Math.Round(percentagemAM, 2).ToString() + " %";


                    var percentagemVvalidosM = Convert.ToDecimal(ActaM.VotosValidos * 100) / QtdVotantesM;
                    var VotosValidosPercentagemM = Math.Round(percentagemVvalidosM, 2).ToString() + " %";

                    var percentagemVbrancosM = Convert.ToDecimal(ActaM.VotosBrancos * 100) / QtdVotantesM;
                    var VotosBrancosPercentagemM = Math.Round(percentagemVbrancosM, 2).ToString() + " %";

                    var percentagemVreclamadosM = Convert.ToDecimal(ActaM.VotosReclamados * 100) / QtdVotantesM;
                    var VotosReclamadosPercentagemM = Math.Round(percentagemVreclamadosM, 2).ToString() + " %";

                    var percentagemVnulosM = Convert.ToDecimal(ActaM.VotosNulos * 100) / QtdVotantesM;
                    var VotosNulosPercentagemM = Math.Round(percentagemVnulosM, 2).ToString() + " %";

                    var percentagemNvM = Convert.ToDecimal(VotosNaoValidosM * 100) / QtdVotantesM;
                    var VotosNaoValidosPercentagemM = Math.Round(percentagemNvM, 2).ToString() + " %";


                    if (ListaM.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ESTATÍSTICA DA ASSEMBLEIA DE VOTO";
                        rel.path = "SICREE.Relatorios.EstatisticaDaAssembleia.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = ListaM;

                        rel.numero = Numero.ToString();
                        rel.qtdEleitores = QtdEleitoresM.ToString();
                        rel.qtdVotantes = QtdVotantesM.ToString();
                        rel.qtdAbstencoes = QtdAbstecoesM.ToString();

                        rel.votosValidos = VotosValidosM.ToString();
                        rel.votosBrancos = VotosBrancoM.ToString();
                        rel.votosReclamados = VotosReclamadosM.ToString();
                        rel.votosNulos = VotosNulosM.ToString();
                        rel.votosNaoValidos = VotosNaoValidosM.ToString();


                        rel.qtdEleitoresPercentagem = QtdEleitoresPercentagemM.ToString();
                        rel.qtdVotantesPercentagem = QtdVotantesPercentagemM.ToString();
                        rel.qtdAbstencoesPercentagem = QtdAbstencoesPercentagemM.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagemM.ToString();
                        rel.votosBrancosPercentagem = VotosBrancosPercentagemM.ToString();
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagemM.ToString();
                        rel.votosNulosPercentagem = VotosNulosPercentagemM.ToString();
                        rel.votosNaoValidosPercentagem = VotosNaoValidosPercentagemM.ToString();

                        rel.qtdAssembleias = GeralM.Numero.ToString();
                        rel.provincia = CbxProvinciaL.Text;
                        rel.municipio = CbxMunicipioEstatistica.Text;


                        rel.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void CbxMunicipioEstatistica_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(RbtMunE.Checked)
                {
                    EstatisticaMunicipal();
                }
            }
            catch { }
        }

        private void BtnDadosAssembleia_Click(object sender, EventArgs e)
        {
            try
            {
                if(RbtAssE.Checked)
                {
                    if (TxtNumAssembleia.Text == "")
                    {
                        MessageBox.Show("Digite o número da assembleia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtNumAssembleia.Focus();
                    }
                    else
                    {
                        EstatisticaDaAssembleia();
                    }
                }
            }
            catch { }
        }

        private void RbtProvE_CheckedChanged(object sender, EventArgs e)
        {
            RbtMunE.Checked = false;
            RbtAssE.Checked = false;

            TxtNumAssembleia.Enabled = false;

            CbxProvinciaL.Enabled = true;
            CbxMunicipioEstatistica.Enabled = false;

            PnProvE.BackColor = Color.FromArgb(71, 202, 94);
            PnMunE.BackColor = Color.Gray;
            PnAssE.BackColor = Color.Gray;
        }

        private void RbtMunE_CheckedChanged(object sender, EventArgs e)
        {
            RbtProvE.Checked = false;
            RbtAssE.Checked = false;

            TxtNumAssembleia.Enabled = false;

            CbxProvinciaL.Enabled = false;
            CbxMunicipioEstatistica.Enabled = true;

            PnMunE.BackColor = Color.FromArgb(71, 202, 94);
            PnProvE.BackColor = Color.Gray;
            PnAssE.BackColor = Color.Gray;
        }

        private void RbtAssE_CheckedChanged(object sender, EventArgs e)
        {
            RbtMunE.Checked = false;
            RbtProvE.Checked = false;

            TxtNumAssembleia.Enabled = true;

            CbxProvinciaL.Enabled = false;
            CbxMunicipioEstatistica.Enabled = false;

            PnAssE.BackColor = Color.FromArgb(71, 202, 94);
            PnMunE.BackColor = Color.Gray;
            PnProvE.BackColor = Color.Gray;
        }

        private void TxtNumAssembleia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
