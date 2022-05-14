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

namespace SICREE.Actas
{
    public partial class FrmActasLista : Form
    {
        readonly ResultadoNegocio resultadoNegocio;
        readonly ResultadoPropriedades resultadoPropriedades;
        private ProvinciaNegocio provinciaNegocio;
        private MunicipioNegocio municipioNegocio;
        readonly ActaNegocio actaNegocio;
        readonly GeralNegocio geralNegocio;
        readonly UIStyle style;
        public FrmActasLista()
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

            ActaNacional();
        }
        void ComboProvincia()
        {
            var list = provinciaNegocio.Listar();

            CbxProvinciaG.DataSource = list;
            CbxProvinciaG.ValueMember = "Codigo";
            CbxProvinciaG.DisplayMember = "Provincia";
        }
        void ComboMunicipio()
        {
            var list = municipioNegocio.Listar(Convert.ToInt32(CbxProvinciaG.SelectedValue)).ToList();

            CbxMunicipioG.DataSource = list;
            CbxMunicipioG.ValueMember = "MunicipioID";
            CbxMunicipioG.DisplayMember = "Municipio";
        }

        //ACTA NACIONAL
        void ActaNacional()
        {
            var Acta = actaNegocio.DadosNacionais();
            var Geral = geralNegocio.DadosGeraisNacional();
            var Lista = resultadoNegocio.ListarResultadoNacional();

            DGVNacional.DataSource = Lista;

            if (Lista.Count > 0)
            {
                LblTitle.Visible = false;

                DGVNacional.Columns[1].HeaderText = "Número";

                DGVNacional.Columns[0].Visible = false;
                DGVNacional.Columns[5].Visible = false;
                DGVNacional.Columns[6].Visible = false;
                DGVNacional.Columns[7].Visible = false;
                DGVNacional.Columns[8].Visible = false;

                LbVB.Text = Acta.VotosBrancos.ToString();
                LbVN.Text = Acta.VotosNulos.ToString();
                LbVR.Text = Acta.VotosReclamados.ToString();
                LbVV.Text = Acta.VotosValidos.ToString();
            }
            else
            {
                LblTitle.Visible = true;

                LbVB.Text = "";
                LbVN.Text = "";
                LbVR.Text = "";
                LbVV.Text = "";
            }
        }
        //ACTA PROVINCIAL
        void ActaProvincial()
        {
            var Acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));
            var Geral = geralNegocio.DadosGeraisProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

            var Lista = resultadoNegocio.ListarResultadoProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

            DGVProvincial.DataSource = Lista;

            if (Lista.Count > 0)
            {
                LbtT.Visible = false;

                DGVProvincial.Columns[1].HeaderText = "Número";

                DGVProvincial.Columns[0].Visible = false;
                DGVProvincial.Columns[5].Visible = false;
                DGVProvincial.Columns[6].Visible = false;
                DGVProvincial.Columns[7].Visible = false;
                DGVProvincial.Columns[8].Visible = false;

                LbVB1.Text = Acta.VotosBrancos.ToString();
                LbVN1.Text = Acta.VotosNulos.ToString();
                LbVR1.Text = Acta.VotosReclamados.ToString();
                LbVV1.Text = Acta.VotosValidos.ToString();
            }
            else
            {
                LbtT.Visible = true;

                LbVB1.Text = "";
                LbVN1.Text = "";
                LbVR1.Text = "";
                LbVV1.Text = "";
            }
        }
        //ACTA PROVINCIAL
        void ActaMunicipal()
        {
            var Acta = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

            var Lista = resultadoNegocio.ListarResultadoMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

            DGVProvincial.DataSource = Lista;

            if (Lista.Count > 0)
            {
                LbtT.Visible = false;

                DGVProvincial.Columns[1].HeaderText = "Número";

                DGVProvincial.Columns[0].Visible = false;
                DGVProvincial.Columns[5].Visible = false;
                DGVProvincial.Columns[6].Visible = false;
                DGVProvincial.Columns[7].Visible = false;
                DGVProvincial.Columns[8].Visible = false;

                LbVB1.Text = Acta.VotosBrancos.ToString();
                LbVN1.Text = Acta.VotosNulos.ToString();
                LbVR1.Text = Acta.VotosReclamados.ToString();
                LbVV1.Text = Acta.VotosValidos.ToString();
            }
            else
            {
                LbtT.Visible = true;

                LbVB1.Text = "";
                LbVN1.Text = "";
                LbVR1.Text = "";
                LbVV1.Text = "";
            }
        }
        //ACTA ASSEMBLEIA
        void ActaAssembleia()
        {
            var Acta = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue),Convert.ToInt32(TxtAssembleiaG.Text));

            var Lista = resultadoNegocio.ListarResultadoAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue),Convert.ToInt32(TxtAssembleiaG.Text));

            DGVProvincial.DataSource = Lista;

            if (Lista.Count > 0)
            {
                LbtT.Visible = false;

                DGVProvincial.Columns[1].HeaderText = "Número";

                DGVProvincial.Columns[0].Visible = false;
                DGVProvincial.Columns[5].Visible = false;
                DGVProvincial.Columns[6].Visible = false;
                DGVProvincial.Columns[7].Visible = false;
                DGVProvincial.Columns[8].Visible = false;

                LbVB1.Text = Acta.VotosBrancos.ToString();
                LbVN1.Text = Acta.VotosNulos.ToString();
                LbVR1.Text = Acta.VotosReclamados.ToString();
                LbVV1.Text = Acta.VotosValidos.ToString();
            }
            else
            {
                LbtT.Visible = true;

                LbVB1.Text = "";
                LbVN1.Text = "";
                LbVR1.Text = "";
                LbVV1.Text = "";
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGraficoP_Click(object sender, EventArgs e)
        {
            ComboProvincia();
            ComboMunicipio();

            ActaProvincial();
            tabControl1.SelectTab(tabPage2);
        }

        private void CbxProvinciaG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboMunicipio();

                ActaProvincial();

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

        private void DGVNacional_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVNacional.Rows[e.RowIndex];

            if (e.RowIndex % 2 == 0)
            {
                row.DefaultCellStyle.BackColor = style.DGVBackColor2;
                row.DefaultCellStyle.ForeColor = style.DGVForeColor;

                row.DefaultCellStyle.SelectionBackColor = style.DGVBackColorSelecionado;
                row.DefaultCellStyle.SelectionForeColor = style.DGVForeColorSelecionado;
                return;
            }
            row.DefaultCellStyle.BackColor = style.DGVBackColor1;
            row.DefaultCellStyle.ForeColor = style.DGVForeColor;

            row.DefaultCellStyle.SelectionBackColor = style.DGVBackColorSelecionado;
            row.DefaultCellStyle.SelectionForeColor = style.DGVForeColorSelecionado;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                var Acta = actaNegocio.DadosNacionais();
                var Geral = geralNegocio.DadosGeraisNacional();
                var Lista = resultadoNegocio.ListarResultadoNacional();

                var VotosValidos = Acta.VotosValidos;
                var VotosBranco = Acta.VotosBrancos;
                var VotosReclamados = Acta.VotosReclamados;
                var VotosNulos = Acta.VotosNulos;
                var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;

                var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                if (Lista.Count > 0)
                {
                    FrmRelatorio rel = new FrmRelatorio();
                    rel.title = "ACTA NACIONAL DOS RESULTADOS ELEITORAIS";
                    rel.path = "SICREE.Relatorios.ActaNacional.rdlc";

                    rel.Dataset = "DataSet1";
                    rel.obj = Lista;

                    rel.votosValidos = VotosValidos.ToString();
                    rel.votosBrancos = VotosBranco.ToString();
                    rel.votosReclamados = VotosReclamados.ToString();
                    rel.votosNulos = VotosNulos.ToString();
                    rel.votosNaoValidos = QtdVotantes.ToString();

                    rel.votosValidosPercentagem = VotosValidosPercentagem;
                    rel.votosBrancosPercentagem = VotosBrancosPercentagem;
                    rel.votosReclamadosPercentagem = VotosReclamadosPercentagem;
                    rel.votosNulosPercentagem = VotosNulosPercentagem;


                    rel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DGVProvincial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVProvincial.Rows[e.RowIndex];

            if (e.RowIndex % 2 == 0)
            {
                row.DefaultCellStyle.BackColor = style.DGVBackColor2;
                row.DefaultCellStyle.ForeColor = style.DGVForeColor;

                row.DefaultCellStyle.SelectionBackColor = style.DGVBackColorSelecionado;
                row.DefaultCellStyle.SelectionForeColor = style.DGVForeColorSelecionado;
                return;
            }
            row.DefaultCellStyle.BackColor = style.DGVBackColor1;
            row.DefaultCellStyle.ForeColor = style.DGVForeColor;

            row.DefaultCellStyle.SelectionBackColor = style.DGVBackColorSelecionado;
            row.DefaultCellStyle.SelectionForeColor = style.DGVForeColorSelecionado;
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

        private void CbxMunicipioG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RbtMun.Checked)
                    ActaMunicipal();
            }
            catch
            {

            }
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

                ActaAssembleia();
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if(RbtProv.Checked)
            {
                try
                {
                    var Acta = actaNegocio.BuscarDadosMesaProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));
                    //var Geral = geralNegocio.DadosGeraisNacional();
                    var Lista = resultadoNegocio.ListarResultadoProvincial(Convert.ToInt32(CbxProvinciaG.SelectedValue));

                    var VotosValidos = Acta.VotosValidos;
                    var VotosBranco = Acta.VotosBrancos;
                    var VotosReclamados = Acta.VotosReclamados;
                    var VotosNulos = Acta.VotosNulos;
                    var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                    var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;

                    var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                    var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                    var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                    var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                    var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ACTA PROVINCIAL DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.ActaProvincial.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.votosValidos = VotosValidos.ToString();
                        rel.votosBrancos = VotosBranco.ToString();
                        rel.votosReclamados = VotosReclamados.ToString();
                        rel.votosNulos = VotosNulos.ToString();
                        rel.votosNaoValidos = QtdVotantes.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagem;
                        rel.votosBrancosPercentagem = VotosBrancosPercentagem;
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagem;
                        rel.votosNulosPercentagem = VotosNulosPercentagem;


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
                    var Acta = actaNegocio.BuscarDadosMesaMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));
                    //var Geral = geralNegocio.DadosGeraisNacional();
                    var Lista = resultadoNegocio.ListarResultadoMunicipal(Convert.ToInt32(CbxMunicipioG.SelectedValue));

                    var VotosValidos = Acta.VotosValidos;
                    var VotosBranco = Acta.VotosBrancos;
                    var VotosReclamados = Acta.VotosReclamados;
                    var VotosNulos = Acta.VotosNulos;
                    var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                    var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;

                    var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                    var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                    var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                    var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                    var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ACTA MUNICIPAL DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.ActaMunicipal.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.votosValidos = VotosValidos.ToString();
                        rel.votosBrancos = VotosBranco.ToString();
                        rel.votosReclamados = VotosReclamados.ToString();
                        rel.votosNulos = VotosNulos.ToString();
                        rel.votosNaoValidos = QtdVotantes.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagem;
                        rel.votosBrancosPercentagem = VotosBrancosPercentagem;
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagem;
                        rel.votosNulosPercentagem = VotosNulosPercentagem;


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
                    var Acta = actaNegocio.BuscarDadosMesaAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue), Convert.ToInt32(TxtAssembleiaG.Text));
                    //var Geral = geralNegocio.DadosGeraisNacional();
                    var Lista = resultadoNegocio.ListarResultadoAssembleia(Convert.ToInt32(CbxMunicipioG.SelectedValue), Convert.ToInt32(TxtAssembleiaG.Text));

                    var VotosValidos = Acta.VotosValidos;
                    var VotosBranco = Acta.VotosBrancos;
                    var VotosReclamados = Acta.VotosReclamados;
                    var VotosNulos = Acta.VotosNulos;
                    var VotosNaoValidos = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados;


                    var QtdVotantes = Acta.VotosBrancos + Acta.VotosNulos + Acta.VotosReclamados + Acta.VotosValidos;

                    var percentagemVvalidos = Convert.ToDecimal(Acta.VotosValidos * 100) / QtdVotantes;
                    var VotosValidosPercentagem = Math.Round(percentagemVvalidos, 2).ToString() + " %";

                    var percentagemVbrancos = Convert.ToDecimal(Acta.VotosBrancos * 100) / QtdVotantes;
                    var VotosBrancosPercentagem = Math.Round(percentagemVbrancos, 2).ToString() + " %";

                    var percentagemVreclamados = Convert.ToDecimal(Acta.VotosReclamados * 100) / QtdVotantes;
                    var VotosReclamadosPercentagem = Math.Round(percentagemVreclamados, 2).ToString() + " %";

                    var percentagemVnulos = Convert.ToDecimal(Acta.VotosNulos * 100) / QtdVotantes;
                    var VotosNulosPercentagem = Math.Round(percentagemVnulos, 2).ToString() + " %";

                    if (Lista.Count > 0)
                    {
                        FrmRelatorio rel = new FrmRelatorio();
                        rel.title = "ACTA DOS RESULTADOS ELEITORAIS";
                        rel.path = "SICREE.Relatorios.ActaAssembleia.rdlc";

                        rel.Dataset = "DataSet1";
                        rel.obj = Lista;

                        rel.votosValidos = VotosValidos.ToString();
                        rel.votosBrancos = VotosBranco.ToString();
                        rel.votosReclamados = VotosReclamados.ToString();
                        rel.votosNulos = VotosNulos.ToString();
                        rel.votosNaoValidos = QtdVotantes.ToString();

                        rel.votosValidosPercentagem = VotosValidosPercentagem;
                        rel.votosBrancosPercentagem = VotosBrancosPercentagem;
                        rel.votosReclamadosPercentagem = VotosReclamadosPercentagem;
                        rel.votosNulosPercentagem = VotosNulosPercentagem;


                        rel.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("O sistema não conseguio carregar as informações, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void BtnGraficoN_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }
    }
}
