using PROPRIEDADES;
using REGRASDENEGOCIO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SICREE
{
    public partial class FrmAuxiliar : Form
    {
        readonly ProvinciaNegocio provinciaNegocio;
        readonly ProvinciaPropriedades provinciaPropriedades;

        readonly MunicipioNegocio municipioNegocio;
        readonly MunicipioPropriedades municipioPropriedades;

        readonly AssembleiaNegocio assembleiaNegocio;
        readonly AssembleiaPropriedades assembleiaPropriedades;

        readonly GeralNegocio geralNegocio;
        readonly GeralPropriedades geralPropriedades;

        int actualizarGerais = 0;
        int actualizarAssembleia = 0;

        readonly UIStyle style;

        public FrmAuxiliar()
        {

            InitializeComponent();

            string acesso = FrmLogin.acesso;

            provinciaNegocio = new ProvinciaNegocio();
            provinciaPropriedades = new ProvinciaPropriedades();

            municipioNegocio = new MunicipioNegocio();
            municipioPropriedades = new MunicipioPropriedades();

            assembleiaNegocio = new AssembleiaNegocio();
            assembleiaPropriedades = new AssembleiaPropriedades();

            geralNegocio = new GeralNegocio();
            geralPropriedades = new GeralPropriedades();

            style = new UIStyle();
            style.MaxLength(TxtAssembleia, 5);
            style.MaxLength(TxtNumEleitores, 4);

            if (MENU.oper == 1)
            {
                BtnProvincia.Visible = false;
                BtnMunicipio.Visible = false;
                BtnGerais.Visible = false;
                BtnListar.Visible = false;
                BtnAddAssembleia.Visible = true;
                BtnListAssembleia.Visible = true;

                BtnAddAssembleia.Location = new Point(221, 0);
                BtnListAssembleia.Location = new Point(397, 0);

                if (acesso != "Administrador")
                {
                    BtnAddAssembleia.Visible = false;

                    BtnListAssembleia.Location = new Point(221, 0);

                    ListarAssembleias();
                    tabControl1.SelectTab(tabPage6);
                }

                tabControl1.SelectTab(tabPage4);
            }

            try
            {
                ListarProvinciaCombo();
                ListarMunicipioCombo();
            }
            catch
            {

            }
            
            ListarProvincia();
        }

        private void BtnMunicipio_Click(object sender, EventArgs e)
        {
            
            ListarMunicipio();
            TxtMunicipio.Focus();
            tabControl1.SelectTab(tabPage3);
        }

        private void BtnDistrito_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage4);
        }

        private void BtnGerais_Click(object sender, EventArgs e)
        {
            TxtEleitores.Focus();
            tabControl1.SelectTab(tabPage5);
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarDados();
            tabControl1.SelectTab(tabPage2);
        }

        private void BtnProvincia_Click(object sender, EventArgs e)
        {
            ListarProvincia();
            TxtProvincia.Focus();
            panel4.Visible = true;
            tabControl1.SelectTab(tabPage1);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FrmAuxiliar_Load(object sender, EventArgs e)
        {
            TxtProvincia.Focus();
        }

        //Província
        void GravarProvincia()
        {
            try
            {
                if(TxtProvincia.Text=="")
                {
                    MessageBox.Show("Digite o nome da província", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtProvincia.Focus();
                }
                else
                {
                    provinciaPropriedades.Nome = TxtProvincia.Text;

                    if (provinciaNegocio.Verificar(TxtProvincia.Text))
                    {
                        MessageBox.Show("Já foi registado uma província com este nome", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtProvincia.Focus();
                    }
                    else
                    {
                        provinciaNegocio.Gravar(provinciaPropriedades);
                        MessageBox.Show("Província registada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarProvincia();
                        TxtProvincia.Text = "";
                        TxtProvincia.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void DGVProvincias()
        {
            DGVProvincia.Columns[0].Visible = false;
            DGVProvincia.Columns[1].HeaderText = "LISTA DE PROVÍNCIAS";
        }

        void ListarProvincia()
        {
            var lista = provinciaNegocio.ListarNome(TxtProvincia.Text).ToList();
            DGVProvincia.DataSource = lista;
            DGVProvincias();

            if (DGVProvincia.Rows.Count > 0)
            {
                LblTitloProvincia.Visible = false;
            }
            else
            {
                LblTitloProvincia.Visible = true;
            }
        }

        void ListarProvinciaCombo()
        {
            var list = provinciaNegocio.Listar();
           
             //CbxProvinciaMunicipio
            CbxProvinciaMunicipio.DataSource = list;
            CbxProvinciaMunicipio.ValueMember = "Codigo";
            CbxProvinciaMunicipio.DisplayMember = "Provincia";

            //CbxProvinciaDistrito
            CbxProvinciaAssembleia.DataSource = list;
            CbxProvinciaAssembleia.ValueMember = "Codigo";
            CbxProvinciaAssembleia.DisplayMember = "Provincia";

            //CbxProvinciaDados
            CbxProvinciaDados.DataSource = list;
            CbxProvinciaDados.ValueMember = "Codigo";
            CbxProvinciaDados.DisplayMember = "Provincia";

            //CbxProvAss
            CbxProvAss.DataSource = list;
            CbxProvAss.ValueMember = "Codigo";
            CbxProvAss.DisplayMember = "Provincia";

        }

        private void DGVProvincia_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVProvincia.Rows[e.RowIndex];

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

        private void BtnGravarProvincia_Click(object sender, EventArgs e)
        {
            GravarProvincia();
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtProvincia_OnValueChanged(object sender, EventArgs e)
        {
            ListarProvincia();
        }

        //Município
        void GravarMunicipio()
        {
            try
            {
                if(TxtMunicipio.Text=="")
                {
                    MessageBox.Show("Digite o nome do município", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtMunicipio.Focus();
                }
                else
                {
                    municipioPropriedades.Nome = TxtMunicipio.Text;
                    municipioPropriedades.ProvinciaID = Convert.ToInt32(CbxProvinciaMunicipio.SelectedValue);

                    if (municipioNegocio.Verificar(TxtMunicipio.Text))
                    {
                        MessageBox.Show("Já foi registado um município com este nome", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtMunicipio.Focus();
                    }
                    else
                    {
                        municipioNegocio.Gravar(municipioPropriedades);
                        MessageBox.Show("Município registado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarMunicipio();
                        TxtMunicipio.Text = "";
                        TxtMunicipio.Focus();
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void DGVMunicipios()
        {
            DGVMunicipio.Columns[0].Visible = false;
            DGVMunicipio.Columns[1].HeaderText = "LISTA DE MUNICÍPIOS DA PROVÍNCIA DO BENGO. TOTAL:"+DGVMunicipio.RowCount;
            DGVMunicipio.Columns[2].Visible = false;
            DGVMunicipio.Columns[3].Visible = false;
        }

        void ListarMunicipio()
        {
            var lista = municipioNegocio.Listar(Convert.ToInt32(CbxProvinciaMunicipio.SelectedValue)).ToList();
            DGVMunicipio.DataSource = lista;

            DGVMunicipios();

            if (DGVMunicipio.Rows.Count > 0)
            {
                LblTitloMunicipio.Visible = false;
            }
            else
            {
                LblTitloMunicipio.Visible = true;
            }
        }

        void ListarMunicipioCombo()
        {
            var list = municipioNegocio.Listar(Convert.ToInt32(CbxProvinciaAssembleia.SelectedValue)).ToList();
            //CbxProvinciaMunicipio
            CbxMunicipioAssembleia.DataSource = list;
            CbxMunicipioAssembleia.ValueMember = "MunicipioID";
            CbxMunicipioAssembleia.DisplayMember = "Municipio";

            //CbxMunAss
            CbxMunAss.DataSource = list;
            CbxMunAss.ValueMember = "MunicipioID";
            CbxMunAss.DisplayMember = "Municipio";

        }

        void ListarMunicipioNome()
        {
            var lista = municipioNegocio.ListarNome(Convert.ToInt32(CbxProvinciaMunicipio.SelectedValue), TxtMunicipio.Text).ToList();
            DGVMunicipio.DataSource = lista;

            DGVMunicipios();

            if (DGVMunicipio.Rows.Count > 0)
            {
                LblTitloMunicipio.Visible = false;
            }
            else
            {
                LblTitloMunicipio.Visible = true;
            }
        }

        private void DGVMunicipio_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVMunicipio.Rows[e.RowIndex];

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

        private void BtnGravarMunicipio_Click(object sender, EventArgs e)
        {
            GravarMunicipio();
        }

        private void CbxProvinciaMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                ListarMunicipio();

                if(CbxProvinciaMunicipio.Text=="Lunda Norte" || CbxProvinciaMunicipio.Text == "Lunda Sul" || CbxProvinciaMunicipio.Text == "Huíla")
                {
                    DGVMunicipio.Columns[1].HeaderText = "LISTA DE MUNICÍPIOS DA PROVÍNCIA DA " + CbxProvinciaMunicipio.Text.ToUpper() + ". TOTAL:" + DGVMunicipio.RowCount;
                }
                else if (CbxProvinciaMunicipio.Text == "Benguela" || CbxProvinciaMunicipio.Text == "Cabinda" || CbxProvinciaMunicipio.Text == "Luanda" || CbxProvinciaMunicipio.Text == "Malange")
                {
                    DGVMunicipio.Columns[1].HeaderText = "LISTA DE MUNICÍPIOS DA PROVÍNCIA DE " + CbxProvinciaMunicipio.Text.ToUpper()+". TOTAL:"+DGVMunicipio.RowCount;
                }
                else 
                {
                    DGVMunicipio.Columns[1].HeaderText = "LISTA DE MUNICÍPIOS DA PROVÍNCIA DO " + CbxProvinciaMunicipio.Text.ToUpper() + ". TOTAL:" + DGVMunicipio.RowCount;
                }
            }
            catch
            {

            }
        }

        private void TxtMunicipio_OnValueChanged(object sender, EventArgs e)
        {
            ListarMunicipioNome();
        }

        private void CbxProvinciaDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListarMunicipioCombo();
            }
            catch
            {

            }
        }

        private void BtnAddAssembleia_Click(object sender, EventArgs e)
        {
            TxtAssembleia.Focus();
            tabControl1.SelectTab(tabPage4);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void BtnListAssembleia_Click(object sender, EventArgs e)
        {
            ListarAssembleiasProvincia();
            ListarAssembleias();
            tabControl1.SelectTab(tabPage6);
        }

        //Assembleia
        void GravarAssembleia()
        {
            try
            {
                if (TxtAssembleia.Text == "")
                {
                    MessageBox.Show("Digite o número da assembleia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAssembleia.Focus();
                }
                else
                {
                    assembleiaPropriedades.Numero = Convert.ToInt32(TxtAssembleia.Text);
                    assembleiaPropriedades.MunicipioID = Convert.ToInt32(CbxMunicipioAssembleia.SelectedValue);
                    assembleiaPropriedades.Endereco = TxtEndereco.Text;
                    assembleiaPropriedades.CoordenadasGeograficas = TxtGeolocalizacao.Text;
                    assembleiaPropriedades.NumeroEleitores = Convert.ToInt32(TxtNumEleitores.Text);

                    if (assembleiaNegocio.Verificar(Convert.ToInt32(TxtAssembleia.Text)))
                    {
                        MessageBox.Show("Já foi registada uma assembleia com este número", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtAssembleia.Focus();
                    }
                    else
                    {
                        assembleiaNegocio.Gravar(assembleiaPropriedades);
                        MessageBox.Show("Assembleia registada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        TxtAssembleia.Text = "";
                        TxtEndereco.Text = "";
                        TxtAssembleia.Focus();

                        ListarAssembleias();
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void ActualizarAssembleia()
        {
            try
            {
                if (TxtAssembleia.Text == "")
                {
                    MessageBox.Show("Digite o número da assembleia", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAssembleia.Focus();
                }
                else
                {
                    assembleiaPropriedades.Numero = Convert.ToInt32(TxtAssembleia.Text);
                    assembleiaPropriedades.MunicipioID = Convert.ToInt32(CbxMunicipioAssembleia.SelectedValue);
                    assembleiaPropriedades.Endereco = TxtEndereco.Text;

                    assembleiaNegocio.Actualizar(assembleiaPropriedades);
                    MessageBox.Show("Assembleia actualizada com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarAssembleias();
                    TxtAssembleia.Text = "";
                    TxtEndereco.Text = "";
                    TxtAssembleia.Focus();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void DGVAssembleias()
        {
            DGVAssembleia.Columns[0].HeaderText = "Número da Assembleia";
            DGVAssembleia.Columns[1].HeaderText = "Endereço";
            DGVAssembleia.Columns[2].HeaderText = "Município";
            DGVAssembleia.Columns[3].Visible = false;
        }

        void ListarAssembleias()
        {
            var lista = assembleiaNegocio.ListaMunicipio(Convert.ToInt32(CbxMunAss.SelectedValue)).ToList();
            DGVAssembleia.DataSource = lista;

            DGVAssembleias();

            if (DGVAssembleia.Rows.Count > 0)
            {
                lblTitloAss.Visible = false;
            }
            else
            {
                lblTitloAss.Visible = true;
            }
        }

        void ListarAssembleiasProvincia()
        {
            var lista = assembleiaNegocio.ListaProvincia(Convert.ToInt32(CbxProvAss.SelectedValue)).ToList();
            DGVAssembleia.DataSource = lista;

            DGVAssembleias();

            if (DGVAssembleia.Rows.Count > 0)
            {
                DGVAssembleia.Columns[0].HeaderText = "Número da Assembleia";
                DGVAssembleia.Columns[1].HeaderText = "Endereço";
                DGVAssembleia.Columns[2].HeaderText = "Município";
                DGVAssembleia.Columns[3].Visible = false;
                lblTitloAss.Visible = false;
            }
            else
            {
                lblTitloAss.Visible = true;
            }
        }

        private void CbxMunAss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListarAssembleias();
            }
            catch
            {

            }
        }

        private void CbxProvAss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListarAssembleiasProvincia();
                
            }
            catch
            {

            }
        }

        private void BtnGravarAssebleia_Click(object sender, EventArgs e)
        {
            if(actualizarAssembleia == 0)
            {
                GravarAssembleia();
            }
            else
            {
                ActualizarAssembleia();
                actualizarAssembleia = 0;
            } 
        }

        private void DGVAssembleia_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow row = DGVAssembleia.Rows[e.RowIndex];

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

        //Geral
        void GravarGeral()
        {
            try
            {
                if (TxtAssembleias.Text == "" || TxtEleitores.Text=="" || TxtMesa.Text =="")
                {
                    MessageBox.Show("Preencha os campos vasíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }
                else
                {
                    geralPropriedades.Provincia = new ProvinciaPropriedades();
                    geralPropriedades.NumeroAssembleia = Convert.ToInt32(TxtAssembleias.Text);
                    geralPropriedades.Provincia.Codigo = Convert.ToInt32(CbxProvinciaDados.SelectedValue);
                    geralPropriedades.NumeroMesa = Convert.ToInt32(TxtMesa.Text);
                    geralPropriedades.NumeroEleitor = Convert.ToInt32(TxtEleitores.Text);

                    if (geralNegocio.Verificar(Convert.ToInt32(CbxProvinciaDados.SelectedValue)))
                    {
                        MessageBox.Show("Já foram registados os dados gerais desta província", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        geralNegocio.Gravar(geralPropriedades);
                        MessageBox.Show("Dados registados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarDados();
                        TxtAssembleias.Text = "";
                        TxtEleitores.Text = "";
                        TxtMesa.Text = "";
                        
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void ActualizarGeral()
        {
            try
            {
                if (TxtAssembleias.Text == "" || TxtEleitores.Text == "" || TxtMesa.Text == "")
                {
                    MessageBox.Show("Preencha os campos vasíos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    geralPropriedades.Provincia = new ProvinciaPropriedades();
                    geralPropriedades.NumeroAssembleia = Convert.ToInt32(TxtAssembleias.Text);
                    geralPropriedades.Provincia.Codigo = Convert.ToInt32(CbxProvinciaDados.SelectedValue);
                    geralPropriedades.NumeroMesa = Convert.ToInt32(TxtMesa.Text);
                    geralPropriedades.NumeroEleitor = Convert.ToInt32(TxtEleitores.Text);

                    geralNegocio.Actualizar(geralPropriedades);
                    MessageBox.Show("Dados actualizados com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarDados();
                    TxtAssembleias.Text = "";
                    TxtEleitores.Text = "";
                    TxtMesa.Text = "";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocorreu um erro" + exc, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        void DGVDados()
        {
            DGVEntidade.Columns[0].HeaderText = "Província";
            DGVEntidade.Columns[1].HeaderText = "Quantidade de eleitores";
            DGVEntidade.Columns[2].HeaderText = "Quantidade de assembléias";
            DGVEntidade.Columns[3].HeaderText = "Quantidade de mesas de voto";
            DGVEntidade.Columns[4].Visible = false;
        }

        void ListarDados()
        {
            var lista = geralNegocio.BuscaTotal();
            DGVEntidade.DataSource = lista;

            DGVDados();

            if (DGVEntidade.Rows.Count > 0)
            {
                LblTitle.Visible = false;
            }
            else
            {
                LblTitle.Visible = true;
            }
        }

        private void BtnGravarDados_Click(object sender, EventArgs e)
        {
            if(actualizarGerais==0)
            {
                GravarGeral();
            }
            else
            {
                ActualizarGeral();
                actualizarGerais = 0;
            }
            
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

        private void TxtAssembleia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtEleitores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtAssembleias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtMesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DGVEntidade_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            CbxProvinciaDados.Text = DGVEntidade.CurrentRow.Cells[0].Value.ToString();
            TxtEleitores.Text = DGVEntidade.CurrentRow.Cells[1].Value.ToString();
            TxtAssembleias.Text = DGVEntidade.CurrentRow.Cells[2].Value.ToString();
            TxtMesa.Text = DGVEntidade.CurrentRow.Cells[3].Value.ToString();

            actualizarGerais = 1;
            tabControl1.SelectTab(tabPage5);
        }

        private void DGVAssembleia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtAssembleia.Text = DGVAssembleia.CurrentRow.Cells[0].Value.ToString();
            TxtEndereco.Text = DGVAssembleia.CurrentRow.Cells[1].Value.ToString();
            CbxMunicipioAssembleia.Text = DGVAssembleia.CurrentRow.Cells[2].Value.ToString();
            CbxProvinciaAssembleia.Text = DGVAssembleia.CurrentRow.Cells[3].Value.ToString();

            actualizarAssembleia = 1;
            tabControl1.SelectTab(tabPage4);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Change the color of the link text by setting LinkVisited
                // to true.
                linkLabel1.LinkVisited = true;
                //Call the Process.Start method to open the default browser
                //with a URL:
                System.Diagnostics.Process.Start("https://www.google.com/maps");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked."+ex);
            }
        }

        private void TxtNumEleitores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNumEleitores_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TxtNumEleitores.Text) > 5000)
                {
                    TxtNumEleitores.Text = "5000";
                }
            }
            catch { }
        }
    }
}
