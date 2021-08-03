using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaLoja.Cadastros
{
    public partial class FrmFornecedores : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string cnpjAntigo;

        public FrmFornecedores()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbfornecedores order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Endereço";
            grid.Columns[3].HeaderText = "Bairro";
            grid.Columns[4].HeaderText = "Cidade";
            grid.Columns[5].HeaderText = "CEP";
            grid.Columns[6].HeaderText = "E-mail";
            grid.Columns[6].HeaderText = "EI";
            grid.Columns[6].HeaderText = "CNPJ";
            grid.Columns[6].HeaderText = "Data Cad.";
            grid.Columns[6].HeaderText = "1º Tel. Fixo";
            grid.Columns[6].HeaderText = "2º Tel. Fixo";
            grid.Columns[6].HeaderText = "Tel. Celular";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Width = 300;
            grid.Columns[4].Width = 80;
            grid.Columns[6].Width = 80;
        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbfornecedores where nome LIKE @nome order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscar.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void habilitarCampos()
        {
            txtBairro.Enabled = true;
            txtBuscar.Enabled = true;
            txtCEP.Enabled = true;
            txtCidade.Enabled = true;
            txtCNPJ.Enabled = true;
            txtEMail.Enabled = true;
            txtEndereco.Enabled = true;
            txtNome.Enabled = true;
            txtTelefoneCelular.Enabled = true;
            txtTelefoneFixo1.Enabled = true;
            txtTelefoneFixo2.Enabled = true;
            txtTelefoneCelular.Enabled = true;
            txtIE.Enabled = true;
            btnNovo.Enabled = false;
        }

        private void desabilitarCampos()
        {
            txtBairro.Enabled = false;
            txtBuscar.Enabled = false;
            txtCEP.Enabled = false;
            txtCidade.Enabled = false;
            txtCNPJ.Enabled = false;
            txtEMail.Enabled = false;
            txtEndereco.Enabled = false;
            txtNome.Enabled = false;
            txtTelefoneCelular.Enabled = false;
            txtTelefoneFixo1.Enabled = false;
            txtTelefoneFixo2.Enabled = false;
            txtTelefoneCelular.Enabled = false;
            txtIE.Enabled = false;
            btnNovo.Enabled = true;
        }

        private void limparCampos()
        {
            txtBairro.Text = "";
            txtBuscar.Text = "";
            txtCEP.Text = "";
            txtCidade.Text = "";
            txtCNPJ.Text = "";
            txtEMail.Text = "";
            txtEndereco.Text = "";
            txtNome.Text = "";
            txtTelefoneCelular.Text = "";
            txtTelefoneFixo1.Text = "";
            txtTelefoneFixo2.Text = "";
            txtTelefoneCelular.Text = "";
            txtIE.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o nome!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            //Código do botão para salvar
            con.AbrirCon();
            sql = "INSERT INTO tbfornecedores (nome, endereco, bairro, cidade, cep, email, ie, cnpj, telefoneFixou, telefoneFixod, telefoneCel) VALUES (@nome, @endereco, @bairro, @cidade, @cep, @email, @ie, @cnpj, @telefoneFixou, @telefoneFixod, @telefoneCel)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
            cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
            cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
            cmd.Parameters.AddWithValue("@email", txtEMail.Text);
            cmd.Parameters.AddWithValue("@ie", txtIE.Text);
            cmd.Parameters.AddWithValue("@CNPJ", txtCNPJ.Text);
            cmd.Parameters.AddWithValue("@telefoneFixou", txtTelefoneFixo1.Text);
            cmd.Parameters.AddWithValue("@telefoneFixod", txtTelefoneFixo2.Text);
            cmd.Parameters.AddWithValue("@telefoneCel", txtTelefoneCelular.Text);


            // Verificar se o CNPJ do usuário já exeiste
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM tbfornecedores where cnpj = @cnpj", con.con);
            cmdVerificar.Parameters.AddWithValue("@cnpj", txtCNPJ.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("CNPJ já Registrado!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCNPJ.Text = "";
                txtCNPJ.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            con.FecharCon();
            Listar();

            MessageBox.Show("Registro Salvo com Sucesso!", "Dados Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            limparCampos();
            desabilitarCampos();
            Listar();
        }

        private void FrmFornecedores_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtNome.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            {
                if (txtNome.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o nome!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    txtNome.Focus();
                    return;
                }


                //Código do botão para editar
                con.AbrirCon();
                sql = "UPDATE tbfornecedores SET nome = @nome, endereco = @endereco, bairro = @bairro, cidade = @cidade, cep = @cep, email = @email, ie = @ie, cnpj = @cnpj, telefoneFixou = @telefoneFixou, telefoneFixod = @telefoneFixod, telefoneCel = @telefoneCel where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
                cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
                cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
                cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
                cmd.Parameters.AddWithValue("@email", txtEMail.Text);
                cmd.Parameters.AddWithValue("@ie", txtIE.Text);
                cmd.Parameters.AddWithValue("@cnpj", txtCNPJ.Text);
                cmd.Parameters.AddWithValue("@telefoneFixou", txtTelefoneFixo1.Text);
                cmd.Parameters.AddWithValue("@telefoneFixod", txtTelefoneFixo2.Text);
                cmd.Parameters.AddWithValue("@telefoneCel", txtTelefoneCelular.Text);
                // Verifica se o login já existe no Banco

                if (txtCNPJ.Text != cnpjAntigo)
                {
                    MySqlCommand cmdVerificar;
                    cmdVerificar = new MySqlCommand("SELECT * FROM tbfornecedores where cnpj = @cnpj", con.con);
                    cmdVerificar.Parameters.AddWithValue("@cnpj", txtCNPJ.Text);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("CNPJ já Registrado!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCNPJ.Text = "";
                        txtCNPJ.Focus();
                        return;
                    }
                }

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Registro Editado com Sucesso!", "Dados Editado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
            }


        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            {
                var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //Código do botão para Excluir
                    con.AbrirCon();
                    sql = "DELETE FROM tbfornecedores where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();


                    MessageBox.Show("Registro Excluido com Sucesso!", "Dados Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNovo.Enabled = true;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                }
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtCidade.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtCEP.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtEMail.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtIE.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtCNPJ.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtTelefoneFixo1.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtTelefoneFixo2.Text = grid.CurrentRow.Cells[10].Value.ToString();
            txtTelefoneCelular.Text = grid.CurrentRow.Cells[11].Value.ToString();
            //txtDataDoCadastro.Text = grid.CurrentRow.Cells[12].Value.ToString();

            cnpjAntigo = grid.CurrentRow.Cells[8].Value.ToString();
        }
    }
}
