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
    public partial class FrmLogin : Form
    {
        public static int codigo;
        public static string acesso;
        public static string login;
        public static string senha;
        public static bool estado;

        readonly UsuarioNegocio entidadeNegocio;
        UsuarioPropriedades entidadePropriedades;
        public FrmLogin()
        {
            InitializeComponent();

            entidadeNegocio = new UsuarioNegocio();
            entidadePropriedades = new UsuarioPropriedades();
        }

        void Login()
        {
            try
            {
                if(TxtNome.Text == "")
                {
                    MessageBox.Show("Digite o nome de usuário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtNome.Focus();
                }
                else if(TxtSenha.Text == "")
                {
                    MessageBox.Show("Digite a senha", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtSenha.Focus();
                }
                else
                {
                    var autenticar = entidadeNegocio.Autenticacao(TxtNome.Text, TxtSenha.Text);
                    
                    if (autenticar != null)
                    {
                        entidadePropriedades = autenticar;

                        codigo = entidadePropriedades.Codigo;
                        acesso = entidadePropriedades.Previlegio;
                        login = entidadePropriedades.Login;
                        senha = entidadePropriedades.Senha;
                        estado = (bool) entidadePropriedades.Estado;

                        if(estado)
                        {
                           if(acesso.Equals("Administrador"))
                           {
                                this.Hide();
                                MENU form = new MENU();
                                form.Show();
                           }
                           else
                            {
                                this.Hide();
                                FrmActa formActa = new FrmActa();
                                formActa.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Acesso negado ao usuário "+ TxtNome.Text+ ", consulte o administrador do sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            TxtNome.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Credenciais inválidos", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtNome.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: "+ex, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void TxtNome_KeyUp(object sender, KeyEventArgs e)
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
                Login();
            }
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
