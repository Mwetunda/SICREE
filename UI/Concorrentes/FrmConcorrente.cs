using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROPRIEDADES;
using REGRASDENEGOCIO;

namespace SICREE
{
    public partial class FrmConcorrente : Form
    {
        readonly ConcorrenteNegocio EntidadeNegocio;
        readonly ConcorrentePropriedades EntidadePropriedades;
        readonly UIStyle style;
        public FrmConcorrente()
        {
            InitializeComponent();

            EntidadeNegocio = new ConcorrenteNegocio();
            EntidadePropriedades = new ConcorrentePropriedades();
            style = new UIStyle();
        }
        
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if(VerificarCamposInicio())
            tabControl1.SelectTab(tabPage3);
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Gravar(); 
        }

        bool VerificarCamposInicio()
        {
            if (CbxNumero.Text=="")
            {
                MessageBox.Show("Selecione o número de ordem do boletim de voto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CbxNumero.Focus();
                return false;
                
            }
            else if (TxtSigla.Text == "")
            {
                MessageBox.Show("Digite a sigla do partido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtSigla.Focus();
                return false;

            }
            else if (TxtPartido.Text == "")
            {
                MessageBox.Show("Digite o nome do partido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtPartido.Focus();
                return false;
                
            }
            else
            {
                return true;
            }
            
        }
        bool VerificarCamposFim()
        {
            
            if (TxtCandidato.Text == "")
            {
                MessageBox.Show("Digite o nome do presidente do partido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCandidato.Focus();
                return false;
                
            }
            else
            {
                return true;
            }
        }
        void Limpar()
        {
            PbBandeira.ImageLocation = null;
            PbFoto.ImageLocation = null;
            CbxNumero.Text = "";
            TxtPartido.Text = "";
            TxtSigla.Text = "";
            TxtCandidato.Text = "";
        }

        private void BtnBandeira_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.FileName = "";
                op.Filter = "All Files (*.*)|*.*";
                op.ShowDialog();
                PbBandeira.Load(op.FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar a foto, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        void Gravar()
        {
            try
            {
                if (VerificarCamposFim())
                {

                    byte[] bandeira;
                    if (PbBandeira.ImageLocation != null)
                    {
                        bandeira = File.ReadAllBytes(PbBandeira.ImageLocation);
                        EntidadePropriedades.Bandeira = bandeira;
                    }
                    else
                    {
                        MessageBox.Show("Insira a bandeira do partido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectTab(tabPage1);
                        BtnBandeira.Focus();
                        return;
                    }

                    byte[] foto;
                    if (PbFoto.ImageLocation != null)
                    {
                        foto = File.ReadAllBytes(PbFoto.ImageLocation);
                        EntidadePropriedades.Foto = foto;
                    }
                    else
                    {
                        MessageBox.Show("Insira a foto do presidente do partido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        BtnFoto.Focus();
                        return;
                    }

                    EntidadePropriedades.Codigo = Convert.ToInt32(CbxNumero.Text);
                    EntidadePropriedades.Sigla = TxtSigla.Text;
                    EntidadePropriedades.Partido = TxtPartido.Text;
                    EntidadePropriedades.Nome = TxtCandidato.Text;

                    if(EntidadeNegocio.Verificar(Convert.ToInt32(CbxNumero.Text)))
                    {
                        MessageBox.Show("Já foi registado um concorrente com este número, selecione outro número", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CbxNumero.Focus();
                        tabControl1.SelectTab(tabPage1);
                    }
                    else
                    {
                        EntidadeNegocio.Gravar(EntidadePropriedades);
                        MessageBox.Show("Concorrente registado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpar();
                        Listar();
                        tabControl1.SelectTab(tabPage1);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show("Ocorreu um erro"+exc ,"Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnFoto_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.FileName = "";
                op.Filter = "All Files (*.*)|*.*";
                op.ShowDialog();
                PbFoto.Load(op.FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("O sistema não conseguio carregar a foto, " + ex, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        void DGV()
        {
            DGVEntidade.Columns[0].HeaderText = "Número";
            DGVEntidade.Columns[1].HeaderText = "Partido";
            DGVEntidade.Columns[2].HeaderText = "Partido por extenso";
            DGVEntidade.Columns[3].HeaderText = "Presidente do Partido";
            DGVEntidade.Columns[4].Visible = false;
            DGVEntidade.Columns[5].Visible = false;

            DGVEntidade.Columns[0].Width = 20;
            DGVEntidade.Columns[1].Width = 25;
            DGVEntidade.Columns[2].Width = 100;
            DGVEntidade.Columns[3].Width = 400;
        }
        void Listar()
        {
            var lista = EntidadeNegocio.Listar().ToList();
            DGVEntidade.DataSource = lista;
            DGV();
            if (DGVEntidade.Rows.Count > 0)
            {
                
                LblTitle.Visible = false;
            }
            else
            {
                LblTitle.Visible = true;
            }
        }
        private void BtnListar_Click(object sender, EventArgs e)
        {
            
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

        private void BtnConcorrente_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnConcorrente_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
            CbxNumero.Focus();
        }

        private void BtnListar_Click_1(object sender, EventArgs e)
        {
            Listar();
            tabControl1.SelectTab(tabPage2);
        }

        private void DGVEntidade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbN.Text = DGVEntidade.CurrentRow.Cells[0].Value.ToString();
            lbAbrev.Text = DGVEntidade.CurrentRow.Cells[1].Value.ToString();
            lbPartido.Text = DGVEntidade.CurrentRow.Cells[2].Value.ToString();
            lbPresidente.Text = DGVEntidade.CurrentRow.Cells[3].Value.ToString();

            MemoryStream ms = new MemoryStream((byte[])DGVEntidade.CurrentRow.Cells[5].Value);
            MemoryStream ms1 = new MemoryStream((byte[])DGVEntidade.CurrentRow.Cells[4].Value);

            pictureBox4.Image = Image.FromStream(ms);
            pictureBox3.Image = Image.FromStream(ms1);
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
