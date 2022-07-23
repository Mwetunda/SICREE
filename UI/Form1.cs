using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SICREE.Actas;

namespace SICREE
{
    public partial class MENU : Form
    {
        public static int oper;
        public MENU()
        {
            InitializeComponent();

            string acesso = FrmLogin.acesso;

            if(acesso!="Administrador")
            {
                BtnNacional.Visible = false;
                bunifuFlatButton1.Visible = false;
                BtnProvincia.Visible = false;

                BtnUsuario.Location = new Point(3, 73);
                BtnAssembleia.Location = new Point(3, 136);
                bunifuFlatButton4.Location = new Point(3, 197);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        
        private void BtnUsuario_Click(object sender, EventArgs e)
        {
          
            FrmUsuario form = new FrmUsuario();
            form.ShowDialog();
        }

        private void BtnAssembleia_Click(object sender, EventArgs e)
        {
            oper = 1;
            FrmAuxiliar form = new FrmAuxiliar();
            form.Show();
        }

        private void BtnDelegado_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnNacional_Click(object sender, EventArgs e)
        {
            oper = 1;
            FrmResultados form = new FrmResultados();
            form.ShowDialog();
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnProvincia_Click(object sender, EventArgs e)
        {
            FrmConcorrente form = new FrmConcorrente();
            form.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            oper = 2;
            FrmAuxiliar form = new FrmAuxiliar();
            form.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            FrmActa form = new FrmActa();
            form.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            oper = 2;
            FrmResultados form = new FrmResultados();
            form.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            FrmActasLista form = new FrmActasLista();
            form.Show();
        }

        private void bunifuCustomLabel40_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin form = new FrmLogin();
            form.Show();
        }
    }
}
