using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using REGRASDENEGOCIO;
using PROPRIEDADES;

namespace SICREE
{
    public partial class FrmUsuario : Form
    {
        readonly UsuarioNegocio EntidadeNegocio;
        readonly UsuarioPropriedades EntidadePropriedades;
        readonly UIStyle style;
        string acesso = FrmLogin.acesso;

        public FrmUsuario()
        {
            InitializeComponent();

            if (acesso != "Administrador")
            {
                BtnUsuario.Visible = false;

                BtnListar.Location = new Point(216,0);
                BtnCredenciais.Location = new Point(412,0);
                tabControl1.SelectTab(tabPage3);
            }

            EntidadeNegocio = new UsuarioNegocio();
            EntidadePropriedades = new UsuarioPropriedades();
            style = new UIStyle();

            style.MaxLength(TxtTelefone, 9);  
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            MENU form = new MENU();
            form.Show();
        }

        bool VerificarCampos()
        {
            if(TxtNome.Text=="" || TxtTelefone.Text=="" || TxtLogin.Text=="" || TxtSenha.Text=="" || CbxPrevilegio.Text=="")
            {
                MessageBox.Show("Preencha os campos vasíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;

            }
            return true;
        }
        void Limpar()
        {
            TxtNome.Text = "";
            TxtTelefone.Text = "";
            TxtLogin.Text = "";
            TxtSenha.Text = "";
            CbxPrevilegio.Text = "";
        }
        void Gravar()
        {
            if(VerificarCampos())
            {
                
                EntidadePropriedades.Nome = TxtNome.Text;
                EntidadePropriedades.Telefone = TxtTelefone.Text;
                EntidadePropriedades.Login = TxtLogin.Text;
                EntidadePropriedades.Senha = TxtSenha.Text;
                EntidadePropriedades.Previlegio = CbxPrevilegio.Text;

                EntidadeNegocio.Gravar(EntidadePropriedades);

                MessageBox.Show("Usuário registado com sucesso", "Sucesso" + MessageBoxIcon.Information);
                Limpar();
                Listar();
                TxtNome.Focus();
            }
        }   
        void DGV()
        {
            DGVUsuario.Columns[0].Visible = false;
            DGVUsuario.Columns[1].HeaderText = "Nome";
            DGVUsuario.Columns[2].HeaderText = "Telefone";
            DGVUsuario.Columns[3].Visible = false;
            DGVUsuario.Columns[4].Visible = false;
            DGVUsuario.Columns[5].HeaderText = "Previlégio";
            DGVUsuario.Columns[6].HeaderText = "Estado";

            DGVUsuario.Columns[1].Width = 300;
            DGVUsuario.Columns[2].Width = 120;
            DGVUsuario.Columns[5].Width = 140;
            DGVUsuario.Columns[6].Width = 80;
        }
        void Listar()
        {
            var lista = EntidadeNegocio.Listar(TxtPesquisar.Text).ToList();
            var listaAdm = EntidadeNegocio.ListarAdministradores(TxtPesquisar.Text).ToList();
            
            if(acesso == "Administrador")
            {
                DGVUsuario.DataSource = lista;
            }
            else
            {
                DGVUsuario.DataSource = listaAdm;
            }

            if (DGVUsuario.Rows.Count > 0)
            {
                DGV();
                LblTitle.Visible = false;
            }
            else
            {
                LblTitle.Visible = true;
            } 
        }
       
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            TxtNome.Focus();
            tabControl1.SelectTab(tabPage1);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            Listar();
            tabControl1.SelectTab(tabPage2);
        }

        private void DGVUsuario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVUsuario.Rows[e.RowIndex];

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

        private void TxtPesquisar_OnValueChanged(object sender, EventArgs e)
        {
            Listar();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            TxtNome.Focus();
        }
        private void TxtTelefone_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtLogin.Focus();
            }
        }

        private void TxtNome_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtTelefone.Focus();
            }
        }

        private void TxtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSenha.Focus();
            }
        }

        private void TxtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CbxPrevilegio.Focus();
                
            }
            BtnGravar.ActiveLineColor = Color.SeaGreen;
        }

        private void CbxPrevilegio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGravar.Focus();
                
            }
        }

        private void BtnGravar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGravar.Focus();
                Gravar();
            }
        }

        private void CbxPrevilegio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnCredenciais_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage3);
        }

        private void BtnActualizarCredenciais_Click(object sender, EventArgs e)
        {
            if (TxtActLogin.Text == "" || TxtActSenha.Text == "" || TxtNovoLogin.Text =="" || TxtNovaSenha.Text == "")
            {
                MessageBox.Show("Preencha os campos vasios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(TxtActLogin.Text==FrmLogin.login && TxtActSenha.Text==FrmLogin.senha)
                {
                    EntidadePropriedades.Codigo = FrmLogin.codigo;
                    EntidadePropriedades.Login = TxtNovoLogin.Text;
                    EntidadePropriedades.Senha = TxtNovaSenha.Text;

                    EntidadeNegocio.ActualizarCredenciais(EntidadePropriedades);

                    MessageBox.Show("Credenciais actualizadas com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listar(); 
                }
                else
                {
                    MessageBox.Show("Credenciais inválidas, informe as suas credenciais actuais", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
 
        }
    }
}
