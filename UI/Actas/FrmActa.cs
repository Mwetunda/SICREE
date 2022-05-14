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

            style.MaxLength(TxtAssembleia, 5);
            style.MaxLength(TxtNumMesas, 2);
            style.MaxLength(TxtVotoBranco, 2);
            style.MaxLength(TxtVotoNulo, 2);
            style.MaxLength(TxtVotoReclamado, 2);
            style.MaxLength(TxtVotos, 4);
            style.MaxLength(TxtVotoValido, 4);
            style.MaxLength(TxtBoletinsRecebidos, 4);
            style.MaxLength(TxtBoletinsNaoUtilizados, 4);
            style.MaxLength(TxtBoletinsInutilizados, 4);


            Listar();

            DGVResul();
            TxtAssembleia.Focus();
        }

        void Listar()
        {
            var lista = concorrenteNegocio.Listar().ToList();
            DGVEntidade.DataSource = lista;

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
                TxtBoletinsRecebidos.Focus();
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

                        TxtNumAssembleia.Text = TxtAssembleia.Text;
                        LblCabecalho.Text = "DADOS DA ASSEMBLEIA Nº: " + TxtAssembleia.Text;
                        tabControl1.SelectTab(tabPage2);
                        TxtBoletinsRecebidos.Focus();
                    }
                }
            }
        }

        void Avancar()
        {
            if (TxtVotoBranco.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos brancos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoBranco.Focus();
            }
            else if (TxtVotoNulo.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos nulos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoNulo.Focus();
            }
            else if (TxtVotoReclamado.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos reclamados da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoReclamado.Focus();
            }
            else if (TxtVotoValido.Text == "")
            {
                MessageBox.Show("Didite a quantidade de votos válidos da assembleia " + TxtAssembleia.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtVotoValido.Focus();
            }
            else
            {

                TxtAssembleiaF.Text = TxtNumAssembleia.Text;

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
                TxtVotos.Focus();
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

            TxtVotos.Enabled = true;
            TxtVotos.Focus();
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
                        linha.Height = 60;

                        voto = Convert.ToInt32(TxtVotos.Text);

                        TxtVotos.Text = "";
                        TxtVotos.Enabled = false;

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

                    for (int a = 0; a < DGVResultados.Rows.Count - 1; a++)
                    {
                        resultadoPropriedades.Assembleia.Numero = assembleia.Numero;
                        resultadoPropriedades.Votos = Convert.ToInt32(DGVResultados.Rows[a].Cells[1].Value);
                        resultadoPropriedades.Concorrente.Codigo = Convert.ToInt32(DGVResultados.Rows[a].Cells[2].Value);

                        resultadoNegocio.Gravar(resultadoPropriedades);
                    }

                    MessageBox.Show("Assembleia " + TxtAssembleiaF.Text + " escrutinada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TxtAssembleia.Text = "";
                    TxtNumMesas.Text = "";

                    TxtVotoBranco.Text = "";
                    TxtVotoNulo.Text = "";
                    TxtVotoReclamado.Text = "";
                    TxtVotoValido.Text = "";
                    TxtVotos.Text = "";
                    TxtBoletinsNaoUtilizados.Text = "000";
                    TxtBoletinsInutilizados.Text = "000";
                    TxtBoletinsRecebidos.Text = "000";

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
                TxtVotoBranco.Focus();
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
                TxtVotoNulo.Focus();
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
                TxtVotoReclamado.Focus();
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
                TxtVotoValido.Focus();
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
                Avancar();
            }
        }

        private void TxtVotoBranco_OnValueChanged(object sender, EventArgs e)
        {

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
    }
}
