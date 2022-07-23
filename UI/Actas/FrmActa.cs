using PROPRIEDADES;
using REGRASDENEGOCIO;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SICREE
{
    public partial class FrmActa : Form
    {
        
        readonly ConcorrenteNegocio concorrenteNegocio;
        
        readonly ActaNegocio actaNegocio;
        readonly ActaPropriedades actaPropriedades;

        readonly ResultadoPropriedades resultadoPropriedades;
        readonly ResultadoNegocio resultadoNegocio;

        viewAssembleia assembleia = new viewAssembleia();
        AssembleiaNegocio assembleiaNegocio = new AssembleiaNegocio();

        readonly UIStyle style;

        int votos;
        int voto;

        public FrmActa()
        {
            InitializeComponent();


            concorrenteNegocio = new ConcorrenteNegocio();

            actaNegocio = new ActaNegocio();
            actaPropriedades = new ActaPropriedades();

            resultadoNegocio = new ResultadoNegocio();
            resultadoPropriedades = new ResultadoPropriedades();
            
            style = new UIStyle();
            Tamanho();


            Listar();

            DGVResul();
            TxtAssembleia.Focus();
        }
        private void Tamanho()
        {
            style.MaxLength(TxtAssembleia, 5);
            style.MaxLength(TxtNumMesas, 2);

            style.MaxLength(TxtBoletinsRecebidosM1, 3);
            style.MaxLength(TxtBoletinsInutilizadosM1, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM1, 3);
            style.MaxLength(TxtVotoBrancoM1, 3);
            style.MaxLength(TxtVotoNuloM1, 3);
            style.MaxLength(TxtVotoReclamadoM1, 3);
            style.MaxLength(TxtVotoValidoM1, 3);

            style.MaxLength(TxtBoletinsRecebidosM2, 3);
            style.MaxLength(TxtBoletinsInutilizadosM2, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM2, 3);
            style.MaxLength(TxtVotoBrancoM2, 3);
            style.MaxLength(TxtVotoNuloM2, 3);
            style.MaxLength(TxtVotoReclamadoM2, 3);
            style.MaxLength(TxtVotoValidoM2, 3);

            style.MaxLength(TxtBoletinsRecebidosM3, 3);
            style.MaxLength(TxtBoletinsInutilizadosM3, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM3, 3);
            style.MaxLength(TxtVotoBrancoM3, 3);
            style.MaxLength(TxtVotoNuloM3, 3);
            style.MaxLength(TxtVotoReclamadoM3, 3);
            style.MaxLength(TxtVotoValidoM3, 3);

            style.MaxLength(TxtBoletinsRecebidosM4, 3);
            style.MaxLength(TxtBoletinsInutilizadosM4, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM4, 3);
            style.MaxLength(TxtVotoBrancoM4, 3);
            style.MaxLength(TxtVotoNuloM4, 3);
            style.MaxLength(TxtVotoReclamadoM4, 3);
            style.MaxLength(TxtVotoValidoM4, 3);

            style.MaxLength(TxtBoletinsRecebidosM5, 3);
            style.MaxLength(TxtBoletinsInutilizadosM5, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM5, 3);
            style.MaxLength(TxtVotoBrancoM5, 3);
            style.MaxLength(TxtVotoNuloM5, 3);
            style.MaxLength(TxtVotoReclamadoM5, 3);
            style.MaxLength(TxtVotoValidoM5, 3);

            style.MaxLength(TxtBoletinsRecebidosM6, 3);
            style.MaxLength(TxtBoletinsInutilizadosM6, 3);
            style.MaxLength(TxtBoletinsNaoUtilizadosM6, 3);
            style.MaxLength(TxtVotoBrancoM6, 3);
            style.MaxLength(TxtVotoNuloM6, 3);
            style.MaxLength(TxtVotoReclamadoM6, 3);
            style.MaxLength(TxtVotoValidoM6, 3);


            style.MaxLength(TxtVotosM1, 3);
            style.MaxLength(TxtVotosM2, 3);
            style.MaxLength(TxtVotosM3, 3);
            style.MaxLength(TxtVotosM4, 3);
            style.MaxLength(TxtVotosM5, 3);
            style.MaxLength(TxtVotosM6, 3);
        }

        private void CalcularBoletinsRecebidos()
        {
            try
            {
                if(TxtBoletinsRecebidosM1.Text == "")
                {
                    TxtBoletinsRecebidosM1.Text = "0";
                }
                if (TxtBoletinsRecebidosM2.Text == "")
                {
                    TxtBoletinsRecebidosM2.Text = "0";
                }
                if (TxtBoletinsRecebidosM3.Text == "")
                {
                    TxtBoletinsRecebidosM3.Text = "0";
                }
                if (TxtBoletinsRecebidosM4.Text == "")
                {
                    TxtBoletinsRecebidosM4.Text = "0";
                }
                if (TxtBoletinsRecebidosM5.Text == "")
                {
                    TxtBoletinsRecebidosM5.Text = "0";
                }
                if (TxtBoletinsRecebidosM6.Text == "")
                {
                    TxtBoletinsRecebidosM6.Text = "0";
                }

                TxtBoletinsRecebidos.Text = 
                    (Convert.ToInt32(TxtBoletinsRecebidosM1.Text)+
                    Convert.ToInt32(TxtBoletinsRecebidosM2.Text)+
                    Convert.ToInt32(TxtBoletinsRecebidosM3.Text)+
                    Convert.ToInt32(TxtBoletinsRecebidosM4.Text)+
                    Convert.ToInt32(TxtBoletinsRecebidosM5.Text)+
                    Convert.ToInt32(TxtBoletinsRecebidosM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularBoletinsInutilizados()
        {
            try
            {
                if (TxtBoletinsInutilizadosM1.Text == "")
                {
                    TxtBoletinsInutilizadosM1.Text = "0";
                }
                if (TxtBoletinsInutilizadosM2.Text == "")
                {
                    TxtBoletinsInutilizadosM2.Text = "0";
                }
                if (TxtBoletinsInutilizadosM3.Text == "")
                {
                    TxtBoletinsInutilizadosM3.Text = "0";
                }
                if (TxtBoletinsInutilizadosM4.Text == "")
                {
                    TxtBoletinsInutilizadosM4.Text = "0";
                }
                if (TxtBoletinsInutilizadosM5.Text == "")
                {
                    TxtBoletinsInutilizadosM5.Text = "0";
                }
                if (TxtBoletinsInutilizadosM6.Text == "")
                {
                    TxtBoletinsInutilizadosM6.Text = "0";
                }

                TxtBoletinsInutilizados.Text =
                    (Convert.ToInt32(TxtBoletinsInutilizadosM1.Text) +
                    Convert.ToInt32(TxtBoletinsInutilizadosM2.Text) +
                    Convert.ToInt32(TxtBoletinsInutilizadosM3.Text) +
                    Convert.ToInt32(TxtBoletinsInutilizadosM4.Text) +
                    Convert.ToInt32(TxtBoletinsInutilizadosM5.Text) +
                    Convert.ToInt32(TxtBoletinsInutilizadosM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularBoletinsNaoUtilizados()
        {
            try
            {
                if (TxtBoletinsNaoUtilizadosM1.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM1.Text = "0";
                }
                if (TxtBoletinsNaoUtilizadosM2.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM2.Text = "0";
                }
                if (TxtBoletinsNaoUtilizadosM3.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM3.Text = "0";
                }
                if (TxtBoletinsNaoUtilizadosM4.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM4.Text = "0";
                }
                if (TxtBoletinsNaoUtilizadosM5.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM5.Text = "0";
                }
                if (TxtBoletinsNaoUtilizadosM6.Text == "")
                {
                    TxtBoletinsNaoUtilizadosM6.Text = "0";
                }

                TxtBoletinsNaoUtilizados.Text =
                    (Convert.ToInt32(TxtBoletinsNaoUtilizadosM1.Text) +
                    Convert.ToInt32(TxtBoletinsNaoUtilizadosM2.Text) +
                    Convert.ToInt32(TxtBoletinsNaoUtilizadosM3.Text) +
                    Convert.ToInt32(TxtBoletinsNaoUtilizadosM4.Text) +
                    Convert.ToInt32(TxtBoletinsNaoUtilizadosM5.Text) +
                    Convert.ToInt32(TxtBoletinsNaoUtilizadosM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularVotosBrancos()
        {
            try
            {
                if (TxtVotoBrancoM1.Text == "")
                {
                    TxtVotoBrancoM1.Text = "0";
                }
                if (TxtVotoBrancoM2.Text == "")
                {
                    TxtVotoBrancoM2.Text = "0";
                }
                if (TxtVotoBrancoM3.Text == "")
                {
                    TxtVotoBrancoM3.Text = "0";
                }
                if (TxtVotoBrancoM4.Text == "")
                {
                    TxtVotoBrancoM4.Text = "0";
                }
                if (TxtVotoBrancoM5.Text == "")
                {
                    TxtVotoBrancoM5.Text = "0";
                }
                if (TxtVotoBrancoM6.Text == "")
                {
                    TxtVotoBrancoM6.Text = "0";
                }

                TxtVotoBranco.Text =
                    (Convert.ToInt32(TxtVotoBrancoM1.Text) +
                    Convert.ToInt32(TxtVotoBrancoM2.Text) +
                    Convert.ToInt32(TxtVotoBrancoM3.Text) +
                    Convert.ToInt32(TxtVotoBrancoM4.Text) +
                    Convert.ToInt32(TxtVotoBrancoM5.Text) +
                    Convert.ToInt32(TxtVotoBrancoM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularVotosNulos()
        {
            try
            {
                if (TxtVotoNuloM1.Text == "")
                {
                    TxtVotoNuloM1.Text = "0";
                }
                if (TxtVotoNuloM2.Text == "")
                {
                    TxtVotoNuloM2.Text = "0";
                }
                if (TxtVotoNuloM3.Text == "")
                {
                    TxtVotoNuloM3.Text = "0";
                }
                if (TxtVotoNuloM3.Text == "")
                {
                    TxtVotoNuloM4.Text = "0";
                }
                if (TxtVotoNuloM5.Text == "")
                {
                    TxtVotoNuloM5.Text = "0";
                }
                if (TxtVotoNuloM6.Text == "")
                {
                    TxtVotoNuloM6.Text = "0";
                }

                TxtVotoNulo.Text =
                    (Convert.ToInt32(TxtVotoNuloM1.Text) +
                    Convert.ToInt32(TxtVotoNuloM2.Text) +
                    Convert.ToInt32(TxtVotoNuloM3.Text) +
                    Convert.ToInt32(TxtVotoNuloM4.Text) +
                    Convert.ToInt32(TxtVotoNuloM5.Text) +
                    Convert.ToInt32(TxtVotoNuloM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularVotosReclamados()
        {
            try
            {
                if (TxtVotoReclamadoM1.Text == "")
                {
                    TxtVotoReclamadoM1.Text = "0";
                }
                if (TxtVotoReclamadoM2.Text == "")
                {
                    TxtVotoReclamadoM2.Text = "0";
                }
                if (TxtVotoReclamadoM3.Text == "")
                {
                    TxtVotoReclamadoM3.Text = "0";
                }
                if (TxtVotoReclamadoM4.Text == "")
                {
                    TxtVotoReclamadoM4.Text = "0";
                }
                if (TxtVotoReclamadoM5.Text == "")
                {
                    TxtVotoReclamadoM5.Text = "0";
                }
                if (TxtVotoReclamadoM6.Text == "")
                {
                    TxtVotoReclamadoM6.Text = "0";
                }

                TxtVotoReclamado.Text =
                    (Convert.ToInt32(TxtVotoReclamadoM1.Text) +
                    Convert.ToInt32(TxtVotoReclamadoM2.Text) +
                    Convert.ToInt32(TxtVotoReclamadoM3.Text) +
                    Convert.ToInt32(TxtVotoReclamadoM4.Text) +
                    Convert.ToInt32(TxtVotoReclamadoM5.Text) +
                    Convert.ToInt32(TxtVotoReclamadoM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularVotosValidos()
        {
            try
            {
                if (TxtVotoValidoM1.Text == "")
                {
                    TxtVotoValidoM1.Text = "0";
                }
                if (TxtVotoValidoM2.Text == "")
                {
                    TxtVotoValidoM2.Text = "0";
                }
                if (TxtVotoValidoM3.Text == "")
                {
                    TxtVotoValidoM3.Text = "0";
                }
                if (TxtVotoValidoM4.Text == "")
                {
                    TxtVotoValidoM4.Text = "0";
                }
                if (TxtVotoValidoM5.Text == "")
                {
                    TxtVotoValidoM5.Text = "0";
                }
                if (TxtVotoValidoM6.Text == "")
                {
                    TxtVotoValidoM6.Text = "0";
                }

                TxtVotoValido.Text =
                    (Convert.ToInt32(TxtVotoValidoM1.Text) +
                    Convert.ToInt32(TxtVotoValidoM2.Text) +
                    Convert.ToInt32(TxtVotoValidoM3.Text) +
                    Convert.ToInt32(TxtVotoValidoM4.Text) +
                    Convert.ToInt32(TxtVotoValidoM5.Text) +
                    Convert.ToInt32(TxtVotoValidoM6.Text)).ToString();
            }
            catch
            {

            }
        }
        private void CalcularVotos()
        {
            try
            {
                if (TxtVotosM1.Text == "")
                {
                    TxtVotosM1.Text = "0";
                }
                if (TxtVotosM2.Text == "")
                {
                    TxtVotosM2.Text = "0";
                }
                if (TxtVotosM3.Text == "")
                {
                    TxtVotosM3.Text = "0";
                }
                if (TxtVotosM4.Text == "")
                {
                    TxtVotosM4.Text = "0";
                }
                if (TxtVotosM5.Text == "")
                {
                    TxtVotosM5.Text = "0";
                }
                if (TxtVotosM6.Text == "")
                {
                    TxtVotosM6.Text = "0";
                }

                TxtVotos.Text =
                    (Convert.ToInt32(TxtVotosM1.Text) +
                    Convert.ToInt32(TxtVotosM2.Text) +
                    Convert.ToInt32(TxtVotosM3.Text) +
                    Convert.ToInt32(TxtVotosM4.Text) +
                    Convert.ToInt32(TxtVotosM5.Text) +
                    Convert.ToInt32(TxtVotosM6.Text)).ToString();
            }
            catch
            {

            }
        }

        void Listar()
        {
            var lista = concorrenteNegocio.Listar().ToList();
            DGVEntidade.DataSource = lista;

            DGVEntidade.Columns[0].Width = 30;

            DGVEntidade.Columns[0].HeaderText = "Número";
            DGVEntidade.Columns[1].HeaderText = "Partido";
            DGVEntidade.Columns[2].Visible = false;
            DGVEntidade.Columns[3].Visible = false;
            DGVEntidade.Columns[4].Visible = false;
            DGVEntidade.Columns[5].Visible = false;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Continuar()
        {
            if (TxtAssembleia.Text == "")
            {
                MessageBox.Show("Digite o número da assembleia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBoletinsRecebidosM1.Focus();
            }
            else
            {
                assembleia = assembleiaNegocio.BuscaID(Convert.ToInt32(TxtAssembleia.Text));


                if (assembleia == null)
                {
                    MessageBox.Show("Assembleia inexistente no sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAssembleia.Focus();
                }
                else
                {

                    if (actaNegocio.Verificar(assembleia.Numero))
                    {
                        MessageBox.Show("A assembleia " + assembleia.Numero + " já foi escrutinada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtAssembleia.Focus();
                    }
                    else
                    {
                        LblCabecalho.Text = "DADOS DA ASSEMBLEIA Nº: " + TxtAssembleia.Text;
                        tabControl1.SelectTab(tabPage2);
                        TxtBoletinsRecebidosM1.Focus();
                    }
                }
            }
        }

        void Avancar()
        {
            if (TxtVotoBrancoM1.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos brancos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoBrancoM1.Focus();
            }
            else if (TxtVotoNuloM1.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos nulos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoNuloM1.Focus();
            }
            else if (TxtVotoReclamadoM1.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos reclamados da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoReclamadoM1.Focus();
            }
            else if (TxtVotoValidoM1.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos válidos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoValidoM1.Focus();
            }
            else
            {

                LbAss.Text = TxtAssembleia.Text;
                LbVotoB.Text = TxtVotoBranco.Text;
                LbVotoN.Text = TxtVotoNulo.Text;
                LbVotoR.Text = TxtVotoReclamado.Text;
                LbVotosV.Text = TxtVotoValido.Text;

                PanelCabecalho.Visible = true;

                LblEleitores.Text = assembleia.NumeroEleitores.ToString();

                LblAbstecoes.Text = (assembleia.NumeroEleitores 
                    - Convert.ToInt32(TxtVotoBranco.Text) 
                    - Convert.ToInt32(TxtVotoNulo.Text)
                    - Convert.ToInt32(TxtVotoReclamado.Text)
                    - Convert.ToInt32(TxtVotoValido.Text)
                    ).ToString();

                tabControl1.SelectTab(tabPage6);
                //TxtVotos.Focus();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Continuar();
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            Avancar();
        }

        private void DGVEntidade_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVEntidade.Rows[e.RowIndex];

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

        private void DGVEntidade_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LblNumero.Text = DGVEntidade.CurrentRow.Cells[0].Value.ToString();
            LblPartido.Text = DGVEntidade.CurrentRow.Cells[1].Value.ToString();
            
            MemoryStream ms = new MemoryStream((byte[])DGVEntidade.CurrentRow.Cells[5].Value);
            MemoryStream pr = new MemoryStream((byte[])DGVEntidade.CurrentRow.Cells[4].Value);

            pictureBox4.Image = Image.FromStream(ms);
            pictureBox1.Image = Image.FromStream(pr);

            TxtVotosM1.Enabled = true;
            TxtVotosM2.Enabled = true;
            TxtVotosM3.Enabled = true;
            TxtVotosM4.Enabled = true;
            TxtVotosM5.Enabled = true;
            TxtVotosM6.Enabled = true;

            TxtVotosM1.Focus();
        }

        void DGVResul()
        {
            
            DGVResultados.Columns.Add("partido", "Partido");
            DGVResultados.Columns.Add("votos", "Número de votos obtidos");
            DGVResultados.Columns.Add("numero", "Numero");

            DGVResultados.Columns[0].Width = 90;
            DGVResultados.Columns[2].Visible = false;
            DGVResultados.Columns[2].Width = 10;
        }

        private void TxtAssembleia_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        void Adicionar()
        {
            bool partido = false;

            if (TxtVotos.Text == "")
            {
                MessageBox.Show("Insira a quantidade de votos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotos.Focus();
            }
            else
            {
                //if (DGVResultados.Rows.Count > 1)
                //{
                    for (int i = 0; i < DGVResultados.Rows.Count - 1; i++)
                    {
                        if (LblPartido.Text == DGVResultados.Rows[i].Cells[0].Value.ToString())
                        {
                            partido = true;
                        }
                    }

                    if (partido)
                    {
                        MessageBox.Show("Já foi introduzido a quantidade de votos obtidos pelo partido " + LblPartido.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DataGridViewRow linha = new DataGridViewRow();
                        linha.CreateCells(DGVResultados);

                        linha.Cells[0].Value = LblPartido.Text;
                        linha.Cells[1].Value = TxtVotos.Text;
                        linha.Cells[2].Value = DGVEntidade.CurrentRow.Cells[0].Value;

                        DGVResultados.Rows.Add(linha);
                        linha.Height = 45;

                        voto = Convert.ToInt32(TxtVotos.Text);

                        TxtVotosM1.Text = "0";
                        TxtVotosM2.Text = "0";
                        TxtVotosM3.Text = "0";
                        TxtVotosM4.Text = "0";
                        TxtVotosM5.Text = "0";
                        TxtVotosM6.Text = "0";
                        TxtVotos.Text = "0";

                        TxtVotos.Enabled = false;
                        TxtVotosM1.Enabled = false;
                        TxtVotosM2.Enabled = false;
                        TxtVotosM3.Enabled = false;
                        TxtVotosM4.Enabled = false;
                        TxtVotosM5.Enabled = false;
                        TxtVotosM6.Enabled = false;

                        votos += voto;

                        lbVotos.Text = votos.ToString("N2");
                    }
                //    }
                //    else
                //    {
                //        DataGridViewRow linha = new DataGridViewRow();
                //        linha.CreateCells(DGVResultados);

                //        linha.Cells[0].Value = LblPartido.Text;
                //        linha.Cells[1].Value = TxtVotos.Text;
                //        linha.Cells[2].Value = DGVEntidade.CurrentRow.Cells[0].Value;

                //        DGVResultados.Rows.Add(linha);
                //        linha.Height = 60;


                //        voto =Convert.ToInt32(TxtVotos.Text);

                //        TxtVotos.Text = "";
                //        TxtVotos.Enabled = false;
                //    }

                
            }
  
        }
        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Adicionar();
        }

        private void DGVResultados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVResultados.Rows[e.RowIndex];

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

        private void BtnRegistar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVEntidade.Rows.Count == DGVResultados.Rows.Count - 1)
                {
                    actaPropriedades.Assembleia.Numero = assembleia.Numero;
                    actaPropriedades.QtdMesa = 0;
                    actaPropriedades.VotosBrancos = Convert.ToInt32(TxtVotoBranco.Text);
                    actaPropriedades.VotosNulos = Convert.ToInt32(TxtVotoNulo.Text);
                    actaPropriedades.VotosReclamados = Convert.ToInt32(TxtVotoReclamado.Text);
                    actaPropriedades.VotosValidos = Convert.ToInt32(TxtVotoValido.Text);
                    actaPropriedades.BoletinsInutilizados = Convert.ToInt32(TxtBoletinsInutilizados.Text);
                    actaPropriedades.BoletinsRecebidos = Convert.ToInt32(TxtBoletinsRecebidos.Text);
                    actaPropriedades.BoletinsNaoUtilizados = Convert.ToInt32(TxtBoletinsNaoUtilizados.Text);
                    actaPropriedades.UsuarioID = FrmLogin.codigo;

                    actaNegocio.Gravar(actaPropriedades);

                    assembleiaNegocio.Escrutinada(assembleia.Numero);

                    for (int a = 0; a < DGVResultados.Rows.Count - 1; a++)
                    {
                        resultadoPropriedades.Assembleia.Numero = assembleia.Numero;
                        resultadoPropriedades.Votos = Convert.ToInt32(DGVResultados.Rows[a].Cells[1].Value);
                        resultadoPropriedades.Concorrente.Codigo = Convert.ToInt32(DGVResultados.Rows[a].Cells[2].Value);

                        resultadoNegocio.Gravar(resultadoPropriedades);
                    }

                    MessageBox.Show("Assembleia " + LbAss.Text + " escrutinada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpar();
                    Listar();
                    DGVResultados.Rows.Clear();
                    PanelCabecalho.Visible = false;

                    tabControl1.SelectTab(tabPage1);
                    TxtAssembleia.Focus();

                    lbVotos.Text = "0,00";
                    votos = 0;

                    MemoryStream ms = new MemoryStream((byte[])DGVEntidade.Rows[0].Cells[5].Value);
                    MemoryStream pr = new MemoryStream((byte[])DGVEntidade.Rows[0].Cells[4].Value);

                    pictureBox4.Image = Image.FromStream(ms);
                    pictureBox1.Image = Image.FromStream(pr);
                }
                else
                {
                    MessageBox.Show("Não foi informado a quantidade de votos obtidos de 1 ou mais concorrentes", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro"+ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Limpar()
        {
            TxtAssembleia.Text = "";
            TxtNumMesas.Text = "";

            TxtBoletinsNaoUtilizadosM1.Text = "0";
            TxtBoletinsInutilizadosM1.Text = "0";
            TxtBoletinsRecebidosM1.Text = "0";
            TxtVotoBrancoM1.Text = "0";
            TxtVotoNuloM1.Text = "0";
            TxtVotoReclamadoM1.Text = "0";
            TxtVotoValidoM1.Text = "0";

            TxtBoletinsNaoUtilizadosM2.Text = "0";
            TxtBoletinsInutilizadosM2.Text = "0";
            TxtBoletinsRecebidosM2.Text = "0";
            TxtVotoBrancoM2.Text = "0";
            TxtVotoNuloM2.Text = "0";
            TxtVotoReclamadoM2.Text = "0";
            TxtVotoValidoM2.Text = "0";

            TxtBoletinsNaoUtilizadosM3.Text = "0";
            TxtBoletinsInutilizadosM3.Text = "0";
            TxtBoletinsRecebidosM3.Text = "0";
            TxtVotoBrancoM3.Text = "0";
            TxtVotoNuloM3.Text = "0";
            TxtVotoReclamadoM3.Text = "0";
            TxtVotoValidoM3.Text = "0";

            TxtBoletinsNaoUtilizadosM4.Text = "0";
            TxtBoletinsInutilizadosM4.Text = "0";
            TxtBoletinsRecebidosM4.Text = "0";
            TxtVotoBrancoM4.Text = "0";
            TxtVotoNuloM4.Text = "0";
            TxtVotoReclamadoM4.Text = "0";
            TxtVotoValidoM4.Text = "0";

            TxtBoletinsNaoUtilizadosM5.Text = "0";
            TxtBoletinsInutilizadosM5.Text = "0";
            TxtBoletinsRecebidosM5.Text = "0";
            TxtVotoBrancoM5.Text = "0";
            TxtVotoNuloM5.Text = "0";
            TxtVotoReclamadoM5.Text = "0";
            TxtVotoValidoM5.Text = "0";

            TxtBoletinsNaoUtilizadosM6.Text = "0";
            TxtBoletinsInutilizadosM6.Text = "0";
            TxtBoletinsRecebidosM6.Text = "0";
            TxtVotoBrancoM6.Text = "0";
            TxtVotoNuloM6.Text = "0";
            TxtVotoReclamadoM6.Text = "0";
            TxtVotoValidoM6.Text = "0";

            TxtBoletinsNaoUtilizados.Text = "0";
            TxtBoletinsInutilizados.Text = "0";
            TxtBoletinsRecebidos.Text = "0";
            TxtVotoBranco.Text = "0";
            TxtVotoNulo.Text = "0";
            TxtVotoReclamado.Text = "0";
            TxtVotoValido.Text = "0";

            TxtVotosM1.Text = "0";
            TxtVotosM2.Text = "0";
            TxtVotosM3.Text = "0";
            TxtVotosM4.Text = "0";
            TxtVotosM5.Text = "0";
            TxtVotosM6.Text = "0";
            TxtVotos.Text = "0";

        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void BtnRetroceder_Click(object sender, EventArgs e)
        {
            PanelCabecalho.Visible = false;
            tabControl1.SelectTab(tabPage2);
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TxtVotos_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void TxtVotos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Adicionar();
            }
        }

        private void TxtVotos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtAssembleia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtNumMesas.Focus();
            }
        }

        private void TxtAssembleia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNumMesas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoBrancoM1.Focus();
                Continuar();
            }
        }

        private void TxtNumMesas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBranco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoNuloM1.Enabled = true;
                TxtVotoNuloM1.Focus();
            }
        }

        private void TxtVotoBranco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoReclamadoM1.Enabled = true;
                TxtVotoReclamadoM1.Focus();
            }
        }

        private void TxtVotoNulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoValidoM1.Enabled = true;
                TxtVotoValidoM1.Focus();
                
            }
        }

        private void TxtVotoReclamado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(DGVResultados.Rows.Count > 1)
            {
                votos = votos - Convert.ToInt32(DGVResultados.CurrentRow.Cells[1].Value);
                lbVotos.Text = votos.ToString("N2");

                DGVResultados.Rows.Remove(DGVResultados.CurrentRow);
            }
            
        }

        private void BtnRetroceder_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
        }

        private void TxtVotoValido_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsRecebidosM2.Enabled = true;
                TxtBoletinsRecebidosM2.Focus();
                
            }
        }

        private void TxtBoletinsRecebidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizadosM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizadosM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void TxtVotoBrancoM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNuloM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamadoM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValidoM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizadosM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizadosM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBrancoM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNuloM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamadoM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValidoM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizadosM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizadosM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBrancoM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNuloM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamadoM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValidoM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizadosM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizadosM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBrancoM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNuloM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamadoM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValidoM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizadosM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizadosM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBrancoM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNuloM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamadoM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValidoM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidos_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsInutilizados_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsNaoUtilizados_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoBranco_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoNulo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoReclamado_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotoValido_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVotosM6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtBoletinsRecebidosM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsRecebidosM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsRecebidosM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsRecebidosM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsRecebidosM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsRecebidosM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsRecebidos();
        }

        private void TxtBoletinsInutilizadosM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsInutilizadosM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsInutilizadosM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsInutilizadosM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsInutilizadosM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsInutilizadosM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsInutilizados();
        }

        private void TxtBoletinsNaoUtilizadosM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtBoletinsNaoUtilizadosM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtBoletinsNaoUtilizadosM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtBoletinsNaoUtilizadosM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtBoletinsNaoUtilizadosM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtBoletinsNaoUtilizadosM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularBoletinsNaoUtilizados();
        }

        private void TxtVotoBrancoM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoBrancoM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoBrancoM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoBrancoM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoBrancoM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoBrancoM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosBrancos();
        }

        private void TxtVotoNuloM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoNuloM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoNuloM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoNuloM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoNuloM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoNuloM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosNulos();
        }

        private void TxtVotoReclamadoM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoReclamadoM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoReclamadoM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoReclamadoM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoReclamadoM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoReclamadoM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosReclamados();
        }

        private void TxtVotoValidoM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotoValidoM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotoValidoM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotoValidoM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotoValidoM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotoValidoM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotosValidos();
        }

        private void TxtVotosM1_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtVotosM2_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtVotosM3_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtVotosM4_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtVotosM5_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtVotosM6_OnValueChanged(object sender, EventArgs e)
        {
            CalcularVotos();
        }

        private void TxtBoletinsRecebidosM1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsInutilizadosM1.Enabled = true;
                TxtBoletinsInutilizadosM1.Focus();
            }
        }

        private void TxtBoletinsInutilizadosM1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsNaoUtilizadosM1.Enabled = true;
                TxtBoletinsNaoUtilizadosM1.Focus();
            }
        }

        private void TxtBoletinsNaoUtilizadosM1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoBrancoM1.Enabled = true;
                TxtVotoBrancoM1.Focus();
            }
        }

        private void TxtBoletinsRecebidosM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsInutilizadosM2.Enabled = true;
                TxtBoletinsInutilizadosM2.Focus();
                
            }
        }

        private void TxtBoletinsInutilizadosM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsNaoUtilizadosM2.Enabled = true;
                TxtBoletinsNaoUtilizadosM2.Focus();
                
            }
        }

        private void TxtBoletinsNaoUtilizadosM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoBrancoM2.Focus();
                TxtVotoBrancoM2.Enabled = true;
            }
        }

        private void TxtVotoBrancoM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoNuloM2.Enabled = true;
                TxtVotoNuloM2.Focus();
            }
        }

        private void TxtVotoNuloM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoReclamadoM2.Enabled = true;
                TxtVotoReclamadoM2.Focus();
            }
        }

        private void TxtVotoReclamadoM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoValidoM2.Enabled = true;
                TxtVotoValidoM2.Focus();
            }
        }

        private void TxtVotoValidoM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsRecebidosM3.Enabled = true;
                TxtBoletinsRecebidosM3.Focus();
                
            }
        }

        private void TxtBoletinsRecebidosM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsInutilizadosM3.Enabled = true;
                TxtBoletinsInutilizadosM3.Focus();
                
            }
        }

        private void TxtBoletinsInutilizadosM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBoletinsNaoUtilizadosM3.Enabled = true;
                TxtBoletinsNaoUtilizadosM3.Focus();
                
            }
        }

        private void TxtBoletinsNaoUtilizadosM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoBrancoM3.Enabled = true;
                TxtVotoBrancoM3.Focus();
                
            }
        }

        private void TxtVotoBrancoM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoNuloM3.Enabled = true;
                TxtVotoNuloM3.Focus();
                
            }
        }

        private void TxtVotoNuloM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtVotoReclamadoM3.Enabled = true;
                TxtVotoReclamadoM3.Focus();
                
            }
        }

        private void TxtVotoReclamadoM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoValidoM3.Enabled = true;
                TxtVotoValidoM3.Focus();
            }
        }

        private void TxtVotoValidoM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsRecebidosM4.Enabled = true;
                TxtBoletinsRecebidosM4.Focus();
            }
        }

        private void TxtBoletinsRecebidosM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsInutilizadosM4.Enabled = true;
                TxtBoletinsInutilizadosM4.Focus();
            }
        }

        private void TxtBoletinsInutilizadosM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsNaoUtilizadosM4.Enabled = true;
                TxtBoletinsNaoUtilizadosM4.Focus();
            }
        }

        private void TxtBoletinsNaoUtilizadosM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoBrancoM4.Enabled = true;
                TxtVotoBrancoM4.Focus();
            }
        }

        private void TxtVotoBrancoM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoNuloM4.Enabled = true;
                TxtVotoNuloM4.Focus();
            }
        }

        private void TxtVotoNuloM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoReclamadoM4.Enabled = true;
                TxtVotoReclamadoM4.Focus();
            }
        }

        private void TxtVotoReclamadoM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoValidoM4.Enabled = true;
                TxtVotoValidoM4.Focus();
            }
        }

        private void TxtVotoValidoM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsRecebidosM5.Enabled = true;
                TxtBoletinsRecebidosM5.Focus();
            }
        }

        private void TxtBoletinsRecebidosM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsInutilizadosM5.Enabled = true;
                TxtBoletinsInutilizadosM5.Focus();
            }
        }

        private void TxtBoletinsInutilizadosM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsNaoUtilizadosM5.Enabled = true;
                TxtBoletinsNaoUtilizadosM5.Focus();
            }
        }

        private void TxtBoletinsNaoUtilizadosM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoBrancoM5.Enabled = true;
                TxtVotoBrancoM5.Focus();
            }
        }

        private void TxtVotoBrancoM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoNuloM5.Enabled = true;
                TxtVotoNuloM5.Focus();
            }
        }

        private void TxtVotoNuloM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoReclamadoM5.Enabled = true;
                TxtVotoReclamadoM5.Focus();
            }
        }

        private void TxtVotoReclamadoM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoValidoM5.Enabled = true;
                TxtVotoValidoM5.Focus();
            }
        }

        private void TxtVotoValidoM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsRecebidosM6.Enabled = true;
                TxtBoletinsRecebidosM6.Focus();
            }
        }

        private void TxtBoletinsRecebidosM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsInutilizadosM6.Enabled = true;
                TxtBoletinsInutilizadosM6.Focus();
            }
        }

        private void TxtBoletinsInutilizadosM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtBoletinsNaoUtilizadosM6.Enabled = true;
                TxtBoletinsNaoUtilizadosM6.Focus();
            }
        }

        private void TxtBoletinsNaoUtilizadosM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoBrancoM6.Enabled = true;
                TxtVotoBrancoM6.Focus();
            }
        }

        private void TxtVotoBrancoM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoNuloM6.Enabled = true;
                TxtVotoNuloM6.Focus();
            }
        }

        private void TxtVotoNuloM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoReclamadoM6.Enabled = true;
                TxtVotoReclamadoM6.Focus();
            }
        }

        private void TxtVotoReclamadoM6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotoValidoM6.Enabled = true;
                TxtVotoValidoM6.Focus();
            }
        }

        private void TxtVotosM1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotosM2.Enabled = true;
                TxtVotosM2.Focus();
            }
        }

        private void TxtVotosM2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotosM3.Enabled = true;
                TxtVotosM3.Focus();
            }
        }

        private void TxtVotosM3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotosM4.Enabled = true;
                TxtVotosM4.Focus();
            }
        }

        private void TxtVotosM4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotosM5.Enabled = true;
                TxtVotosM5.Focus();
            }
        }

        private void TxtVotosM5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TxtVotosM6.Enabled = true;
                TxtVotosM6.Focus();
            }
        }

        private void bunifuCustomLabel40_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin form = new FrmLogin();
            form.Show();
        }
    }
}
