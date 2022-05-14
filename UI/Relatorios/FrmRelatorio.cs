using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PROPRIEDADES;

namespace SICREE
{
    public partial class FrmRelatorio : Form
    {
        public string Dataset;
        public string Dataset1;
        public string Dataset2;
        public object obj;
        public object obj1;
        public object obj2;
        public string path;
        public string title;

        public string percentagem1;
        public string percentagem2;
        public string percentagem3;
        public string percentagem4;
        public string percentagem5;
        public string percentagem6;

        public string qtdEleitores;
        public string qtdVotantes;
        public string qtdAbstencoes;
        public string votosValidos;
        public string votosBrancos;
        public string votosReclamados;
        public string votosNulos;
        public string votosNaoValidos;
        public string qtdEleitoresPercentagem;
        public string qtdVotantesPercentagem;
        public string qtdAbstencoesPercentagem;
        public string votosValidosPercentagem;
        public string votosBrancosPercentagem;
        public string votosReclamadosPercentagem;
        public string votosNulosPercentagem;
        public string votosNaoValidosPercentagem;
        public string qtdAssembleias;
        public string qtdMesas;
        public string provincia;
        public string municipio;
        public string numero;

        public FrmRelatorio()
        {
            InitializeComponent();
        }

        private void FrmRelatorio_Load(object sender, EventArgs e)
        {
            LbTitle.Text = title;
            try
            {
                CreateReport(path, Dataset, obj, Dataset1, obj1, Dataset2, obj2);
                //txt_numero.Text = "" + ReportViewer1.CurrentPage;

                if(path == "SICREE.Relatorios.GraficoNacional.rdlc" || path == "SICREE.Relatorios.GraficoProvincial.rdlc" || path == "SICREE.Relatorios.GraficoMunicipal.rdlc" || path == "SICREE.Relatorios.GraficoAssembleia.rdlc")
                {
                    SetandoParametros();
                }
                if(path == "SICREE.Relatorios.EstatisticaNacional.rdlc" || path == "SICREE.Relatorios.EstatisticaProvincial.rdlc" || path == "SICREE.Relatorios.EstatisticaMunicipal.rdlc" || path == "SICREE.Relatorios.EstatisticaDaAssembleia.rdlc")
                {
                    SetandoParametros2();
                }
                if (path == "SICREE.Relatorios.ActaNacional.rdlc" || path == "SICREE.Relatorios.ActaProvincial.rdlc" || path == "SICREE.Relatorios.ActaMunicipal.rdlc" || path == "SICREE.Relatorios.ActaAssembleia.rdlc")
                {
                    SetandoParametrosActa();
                }
                ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                ReportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;

            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            this.ReportViewer1.RefreshReport();
        }

        void SetandoParametros()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] Parametros = new ReportParameter[6];

            Parametros[0] = new ReportParameter("Percentagem1", percentagem1);
            Parametros[1] = new ReportParameter("Percentagem2", percentagem2);
            Parametros[2] = new ReportParameter("Percentagem3", percentagem3);
            Parametros[3] = new ReportParameter("Percentagem4", percentagem4);
            Parametros[4] = new ReportParameter("Percentagem5", percentagem5);
            Parametros[5] = new ReportParameter("Percentagem6", percentagem6);

            ReportViewer1.LocalReport.SetParameters(Parametros);
        }
        void SetandoParametros2()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] Parametros2 = new ReportParameter[21];

            Parametros2[0] = new ReportParameter("QtdEleitores", qtdEleitores);
            Parametros2[1] = new ReportParameter("QtdVotantes", qtdVotantes);
            Parametros2[2] = new ReportParameter("QtdAbstencoes", qtdAbstencoes);
            Parametros2[3] = new ReportParameter("VotosValidos", votosValidos);
            Parametros2[4] = new ReportParameter("VotosBrancos", votosBrancos);
            Parametros2[5] = new ReportParameter("VotosReclamados", votosReclamados);
            Parametros2[6] = new ReportParameter("VotosNulos", votosNulos);
            Parametros2[7] = new ReportParameter("VotosNaoValidos", votosNaoValidos);
            Parametros2[8] = new ReportParameter("EleitoresPercentagem", qtdEleitoresPercentagem);
            Parametros2[9] = new ReportParameter("VotantesPercentagem", qtdVotantesPercentagem);
            Parametros2[10] = new ReportParameter("AbstencoesPercentagem", qtdAbstencoesPercentagem);
            Parametros2[11] = new ReportParameter("VotosValidosPercentagem", votosValidosPercentagem);
            Parametros2[12] = new ReportParameter("VotosBrancosPercentagem", votosBrancosPercentagem);
            Parametros2[13] = new ReportParameter("VotosReclamadosPercentagem", votosReclamadosPercentagem);
            Parametros2[14] = new ReportParameter("VotosNulosPercentagem", votosNulosPercentagem);
            Parametros2[15] = new ReportParameter("VotosNaoValidosPercentagem", votosNaoValidosPercentagem);
            Parametros2[16] = new ReportParameter("QtdAssembleias", qtdAssembleias);
            Parametros2[17] = new ReportParameter("QtdMesas", qtdMesas);
            Parametros2[18] = new ReportParameter("Provincia", provincia);
            Parametros2[19] = new ReportParameter("Municipio", municipio);
            Parametros2[20] = new ReportParameter("Numero", numero);


            ReportViewer1.LocalReport.SetParameters(Parametros2);
        }
        void SetandoParametrosActa()
        {
            Microsoft.Reporting.WinForms.ReportParameter[] Parametros = new ReportParameter[9];

            Parametros[0] = new ReportParameter("VotosValidos", votosValidos);
            Parametros[1] = new ReportParameter("VotosBrancos", votosBrancos);
            Parametros[2] = new ReportParameter("VotosReclamados", votosReclamados);
            Parametros[3] = new ReportParameter("VotosNulos", votosNulos);
            Parametros[4] = new ReportParameter("Total", votosNaoValidos);
            Parametros[5] = new ReportParameter("PercentagemV", votosValidosPercentagem);
            Parametros[6] = new ReportParameter("PercentagemB", votosBrancosPercentagem);
            Parametros[7] = new ReportParameter("PercentagemR", votosReclamadosPercentagem);
            Parametros[8] = new ReportParameter("PercentagemN", votosNulosPercentagem);

            ReportViewer1.LocalReport.SetParameters(Parametros);
        }

        void CreateReport(string path, string Dataset, object obj, string Dataset1, object obj1, string Dataset2, object obj2)
        {
            //CompanyBLL company = new CompanyBLL();
            //List<ViewCompany> Lis = new List<ViewCompany>();
            //Lis = company.GetAllID(1);

            ReportViewer1.Reset();
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.ReportEmbeddedResource = path;

            //ReportDataSource dts = new ReportDataSource("Company", Lis);
            //ReportViewer1.LocalReport.DataSources.Add(dts);

            ReportDataSource df = new ReportDataSource(Dataset, obj);
            ReportViewer1.LocalReport.DataSources.Add(df);


            ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.RefreshReport();
        }
    }
}
