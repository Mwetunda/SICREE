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
        readonly GeralNegocio geralNegocio;
        readonly UIStyle style;

        int qtdMesa;
        int qtdAssembleia;

        int qtdMesaGeral;
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

            geralNegocio = new GeralNegocio();
            
            style = new UIStyle();

            style.MaxLength(TxtAssembleiaG, 5);

            if(MENU.oper==2)
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

                LblUNITA.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblAPN.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblPRS.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblMPLA.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblFNLA.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblCASA.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesa();

                qtdMesa = acta.QtdMesa;
                LblVotosBrancosG1.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG1.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG1.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG1.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                viewGeral geral = new viewGeral();
                geral = geralNegocio.DadosGeraisNacional();

                qtdAssembleiaGeral = geral.NumeroAssembleia;
                qtdEleitoresGeral = geral.NumeroEleitor;
                qtdMesaGeral = geral.NumeroMesa;

                var percentagemMesa = Convert.ToDecimal(qtdMesa * 100) / Convert.ToDecimal(qtdMesaGeral);
                var PercentagemMesa = Math.Round(percentagemMesa, 2).ToString() + " %";

                lbMesas.Text = "Mesas escrutinadas: "+ qtdMesa +", Correspondentes a "+ PercentagemMesa;

                var percentagemAssembleia = Convert.ToDecimal(qtdAssembleia * 100) / Convert.ToDecimal(qtdAssembleiaGeral);
                var PercentagemAssembleia = Math.Round(percentagemAssembleia, 2).ToString() + " %";

                lbAssembleias.Text = "Assembleias escrutinadas: " + qtdAssembleia + ", Correspondentes a " + PercentagemAssembleia;
            }
            else
            {
                LblUNITA.Text = "";
                LblAPN.Text = "";
                LblPRS.Text = "";
                LblMPLA.Text = "";
                LblFNLA.Text = "";
                LblCASA.Text = "";

                lbAssembleias.Text = "";
                lbMesas.Text = "";

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

                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                qtdMesa = acta.QtdMesa;
                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                viewGeral geral = new viewGeral();
                geral = geralNegocio.DadosGeraisProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                qtdAssembleiaGeral = geral.NumeroAssembleia;
                qtdEleitoresGeral = geral.NumeroEleitor;
                qtdMesaGeral = geral.NumeroMesa;

                var percentagemMesa = Convert.ToDecimal(qtdMesa * 100) / Convert.ToDecimal(qtdMesaGeral);
                var PercentagemMesa = Math.Round(percentagemMesa, 2).ToString() + " %";

                lbMesas2.Text = "Mesas escrutinadas: " + qtdMesa + ", Correspondentes a " + PercentagemMesa;

                var percentagemAssembleia = Convert.ToDecimal(qtdAssembleia * 100) / Convert.ToDecimal(qtdAssembleiaGeral);
                var PercentagemAssembleia = Math.Round(percentagemAssembleia, 2).ToString() + " %";

                lbAssembleias2.Text = "Assembleias escrutinadas: " + qtdAssembleia + ", Correspondentes a " + PercentagemAssembleia;
            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;


                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";
                lbMesas2.Text = "";

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

                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

                qtdMesa = acta.QtdMesa;
                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                qtdAssembleia = acta.QtdAssembleia;

                lbAssembleias2.Text = "";
                lbMesas2.Text = "";
            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;

                MessageBox.Show("Não há informações sobre este Município de momento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";
                lbMesas2.Text = "";

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

                LblUNITA1.Text = Lista.Where(x => x.ConcorrenteID == 1).Max(x => x.Percentagem);
                LblAPN1.Text = Lista.Where(x => x.ConcorrenteID == 2).Max(x => x.Percentagem);
                LblPRS1.Text = Lista.Where(x => x.ConcorrenteID == 3).Max(x => x.Percentagem);
                LblMPLA1.Text = Lista.Where(x => x.ConcorrenteID == 4).Max(x => x.Percentagem);
                LblFNLA1.Text = Lista.Where(x => x.ConcorrenteID == 5).Max(x => x.Percentagem);
                LblCASA1.Text = Lista.Where(x => x.ConcorrenteID == 6).Max(x => x.Percentagem);

                viewActa acta = new viewActa();
                acta = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue), Convert.ToInt32(TxtAssembleiaG.Text));

                qtdMesa = acta.QtdMesa;
                LblVotosBrancosG2.Text = acta.VotosBrancos.ToString("N2");
                LblVotosNulosG2.Text = acta.VotosNulos.ToString("N2");
                LblVotosReclamadosG2.Text = acta.VotosReclamados.ToString("N2");
                LblVotosValidosG2.Text = acta.VotosValidos.ToString("N2");

                lbAssembleias2.Text = "";
                lbMesas2.Text = "";
            }
            else
            {
                GraficProvincial.Visible = false;
                Titlo.Visible = true;

                LblUNITA1.Text = "";
                LblAPN1.Text = "";
                LblPRS1.Text = "";
                LblMPLA1.Text = "";
                LblFNLA1.Text = "";
                LblCASA1.Text = "";

                lbAssembleias2.Text = "";
                lbMesas2.Text = "";

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
                var Lista = actaNegocio.BuscarDadosMesa();
                var Lista2 = geralNegocio.DadosGeraisNacional();

                if (Lista != null)
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

                    var TotalVotantes = Lista.VotosBrancos + Lista.VotosNulos + Lista.VotosReclamados + Lista.VotosValidos;
                    var Abstecoes = Lista2.NumeroEleitor - TotalVotantes;
                    var NaoValidos = Lista.VotosBrancos + Lista.VotosNulos + Lista.VotosReclamados;

                    GraficoEstatisticaNacional.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaNacional.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaNacional.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaNacional.Series[0].Points[0].SetValueY(Lista.VotosBrancos);
                    GraficoEstatisticaNacional.Series[0].Points[1].SetValueY(Lista.VotosReclamados);
                    GraficoEstatisticaNacional.Series[0].Points[2].SetValueY(Lista.VotosNulos);


                    GraficoEstatisticaNacionalVotos.Series[0].Points[0].SetValueY(Lista.VotosValidos);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[1].SetValueY(Lista.VotosBrancos);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[2].SetValueY(Lista.VotosReclamados);
                    GraficoEstatisticaNacionalVotos.Series[0].Points[3].SetValueY(Lista.VotosNulos);



                    GraficoEstatisticaNacionalEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaNacionalEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitor);



                    GraficoEstatisticaNacionalVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaNacionalVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitor;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitor;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = Convert.ToDecimal(NaoValidos * 100) / TotalVotantes;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB.Text = PercentagemVBrancos;
                    lbPVN.Text = PercentagemVNulos;
                    lbPVR.Text = PercentagemVReclamados;
                    lbPVV.Text = PercentagemVValidos;
                    lbPV.Text = PercentagemV;
                    lbPA.Text = PercentagemA;
                    lbPTotalN.Text = PercentagemNV;
                    lbTotalN.Text = NaoValidos.ToString("N2");
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
            catch(Exception)
            {

            }
        }

        void EstatisticaProvincial()
        {
            try
            {
                var Lista = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));
                var Lista2 = geralNegocio.DadosGeraisProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));

                if (Lista != null)
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

                    var TotalVotantes = Lista.VotosBrancos + Lista.VotosNulos + Lista.VotosReclamados + Lista.VotosValidos;
                    var Abstecoes = Lista2.NumeroEleitor - TotalVotantes;
                    var NaoValidos = Lista.VotosBrancos + Lista.VotosNulos + Lista.VotosReclamados;

                    GraficoEstatisticaProvincial.Series[0].Points[0].LegendText = "VOTOS BRANCOS";
                    GraficoEstatisticaProvincial.Series[0].Points[1].LegendText = "VOTOS RECLAMADOS";
                    GraficoEstatisticaProvincial.Series[0].Points[2].LegendText = "VOTOS NULOS";
                    GraficoEstatisticaProvincial.Series[0].Points[0].SetValueY(Lista.VotosBrancos);
                    GraficoEstatisticaProvincial.Series[0].Points[1].SetValueY(Lista.VotosReclamados);
                    GraficoEstatisticaProvincial.Series[0].Points[2].SetValueY(Lista.VotosNulos);


                    GraficoEstatisticaProvincialVotos.ResetAutoValues();




                    GraficoEstatisticaProvincialVotos.Series[0].Points[0].SetValueY(Lista.VotosValidos);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[1].SetValueY(Lista.VotosBrancos);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[2].SetValueY(Lista.VotosReclamados);
                    GraficoEstatisticaProvincialVotos.Series[0].Points[3].SetValueY(Lista.VotosNulos);



                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].LegendText = "ELEITORES";
                    GraficoEstatisticaProvincialEleitores.Series[0].Points[0].SetValueY(Lista2.NumeroEleitor);



                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].LegendText = "VOTANTES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].LegendText = "ABISTEÇÕES";
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[0].SetValueY(TotalVotantes);
                    GraficoEstatisticaProvincialVotantes.Series[0].Points[1].SetValueY(Abstecoes);


                    var percentagemVbrancos = Convert.ToDecimal(Lista.VotosBrancos * 100) / TotalVotantes;
                    var PercentagemVBrancos = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Lista.VotosNulos * 100) / TotalVotantes;
                    var PercentagemVNulos = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Lista.VotosReclamados * 100) / TotalVotantes;
                    var PercentagemVReclamados = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVvalidos = Convert.ToDecimal(Lista.VotosValidos * 100) / TotalVotantes;
                    var PercentagemVValidos = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemV = Convert.ToDecimal(TotalVotantes * 100) / Lista2.NumeroEleitor;
                    var PercentagemV = Math.Round(percentagemV, 2).ToString() + " %";

                    var percentagemA = Convert.ToDecimal(Abstecoes * 100) / Lista2.NumeroEleitor;
                    var PercentagemA = Math.Round(percentagemA, 2).ToString() + " %";

                    var percentagemNv = Convert.ToDecimal(NaoValidos * 100) / TotalVotantes;
                    var PercentagemNV = Math.Round(percentagemNv, 2).ToString() + " %";

                    lbPVB2.Text = PercentagemVBrancos;
                    lbPVN2.Text = PercentagemVNulos;
                    lbPVR2.Text = PercentagemVReclamados;
                    lbPVV2.Text = PercentagemVValidos;
                    lbPV2.Text = PercentagemV;
                    lbPA2.Text = PercentagemA;
                    lbPTotal.Text = PercentagemNV;
                    lbTotal.Text = NaoValidos.ToString("N2");
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

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton29_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            
            
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
                var Acta = actaNegocio.BuscarDadosMesa();
                var Geral = geralNegocio.DadosGeraisNacional();

                var QtdEleitores = Geral.NumeroEleitor;
                var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;
                var QtdAbstecoes = Geral.NumeroEleitor - QtdVotantes;

                var VotosValidos = Acta.VotosValidos;
                var VotosBranco = Acta.VotosBrancos;
                var VotosReclamados = Acta.VotosReclamados;
                var VotosNulos = Acta.VotosNulos;
                var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                var QtdEleitoresPercentagem = "100 %";

                var percentagemV = Convert.ToDecimal(QtdVotantes * 100) / Geral.NumeroEleitor;
                var QtdVotantesPercentagem = Math.Round(percentagemV, 2).ToString() + " %";

                var percentagemA = Convert.ToDecimal(QtdAbstecoes * 100) / Geral.NumeroEleitor;
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

                    rel.qtdAssembleias = Geral.NumeroAssembleia.ToString();
                    rel.qtdMesas = Geral.NumeroMesa.ToString();
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
                var Lista = resultadoNegocio.ListarResultadoNacional();
                var Acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));
                var Geral = geralNegocio.DadosGeraisProvincial(Convert.ToInt32(CbxProvinciaL.SelectedValue));

                var QtdEleitores = Geral.NumeroEleitor;
                var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;
                var QtdAbstecoes = Geral.NumeroEleitor - QtdVotantes;

                var VotosValidos = Acta.VotosValidos;
                var VotosBranco = Acta.VotosBrancos;
                var VotosReclamados = Acta.VotosReclamados;
                var VotosNulos = Acta.VotosNulos;
                var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                var QtdEleitoresPercentagem = "100 %";

                var percentagemV = Convert.ToDecimal(QtdVotantes * 100) / Geral.NumeroEleitor;
                var QtdVotantesPercentagem = Math.Round(percentagemV, 2).ToString() + " %";

                var percentagemA = Convert.ToDecimal(QtdAbstecoes * 100) / Geral.NumeroEleitor;
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

                    rel.qtdAssembleias = Geral.NumeroAssembleia.ToString();
                    rel.qtdMesas = Geral.NumeroMesa.ToString();
                    rel.provincia = CbxProvinciaL.Text;


                    rel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
