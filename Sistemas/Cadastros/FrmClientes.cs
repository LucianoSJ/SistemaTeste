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
    public partial class FrmClientes : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string loginAntigo;

        public FrmClientes()  
        {
            InitializeComponent();
        }

        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbcliestes order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbcliestes where nome LIKE @nome order by nome asc";
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

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "RG";
            grid.Columns[3].HeaderText = "CPF";
            grid.Columns[4].HeaderText = "Endereço";
            grid.Columns[5].HeaderText = "Bairro";
            grid.Columns[6].HeaderText = "Cidade";
            grid.Columns[7].HeaderText = "CEP";
            grid.Columns[8].HeaderText = "Limite de Crédito";
            grid.Columns[9].HeaderText = "Data de Nasc.";
            grid.Columns[10].HeaderText = "Telefone Fixo";
            grid.Columns[11].HeaderText = "Celular";
            grid.Columns[12].HeaderText = "E-mail";
            grid.Columns[0].Visible = false;
            grid.Columns[1].Width = 200;
        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtRG.Enabled = true;
            txtCEP.Enabled = true;
            txtCPF.Enabled = true;
            txtEndereco.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            txtCEP.Enabled = true;
            txtLimiteDeCredito.Enabled = true;
            txtDataDeNascimento.Enabled = true;
            txtTelefoneCelular.Enabled = true;
            txtTelefoneFixo.Enabled = true;
            txtEMail.Enabled = true;
            txtNome.Focus();
        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtRG.Enabled = false;
            txtCPF.Enabled = false;
            txtCEP.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            txtLimiteDeCredito.Enabled = false;
            txtDataDeNascimento.Enabled = false;
            txtDataDoCadastro.Enabled = false;
            txtTelefoneCelular.Enabled = false;
            txtTelefoneFixo.Enabled = false;
            txtEMail.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            img.Enabled = false;
        }

        private void limparCampos()
        {
            txtNome.Text = "";
            txtRG.Text = "";
            txtCEP.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtBairro.Text = "";
            txtCEP.Text = "";
            txtLimiteDeCredito.Text = "";
            txtDataDeNascimento.Text = "";
            txtDataDoCadastro.Text = "";
            txtTelefoneCelular.Text = "";
            txtTelefoneFixo.Text = "";
            txtEMail.Text = "";
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            desabilitarCampos();
            Listar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limparCampos();
            btnSalvar.Enabled = true;
            img.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnNovo.Enabled = false;
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
            sql = "INSERT INTO tbcliestes (nome, rg, cpf, endereco, bairro, cidade, cep, limiteDeCredito, dataNasc, telefonefixo, telefonecel, email, dataCadastro) VALUES (@nome, @rg, @cpf, @endereco, @bairro, @cidade, @cep, @limiteDeCredito, @dataNasc, @telefonefixo, @telefonecel, @email, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@rg", txtRG.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
            cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
            cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
            cmd.Parameters.AddWithValue("@limiteDeCredito", txtLimiteDeCredito.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@dataNasc", txtDataDeNascimento.Text);
            cmd.Parameters.AddWithValue("@telefonefixo", txtTelefoneFixo.Text);
            cmd.Parameters.AddWithValue("@telefonecel", txtTelefoneCelular.Text);
            cmd.Parameters.AddWithValue("@email", txtEMail.Text);

            // Verificar se o CPF do usuário já exeiste
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM tbcliestes where cpf = @cpf", con.con);
            cmdVerificar.Parameters.AddWithValue("@cpf", txtCPF.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Usuário já Registrado!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPF.Text = "";
                txtCPF.Focus();
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

        private void btnEditar_Click(object sender, EventArgs e)
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
            sql = "UPDATE tbcliestes SET nome = @nome, rg = @rg, cpf = @cpf, endereco = @endereco, bairro = @bairro, cidade = @cidade, cep = @cep, limiteDeCredito = @limiteDeCredito, dataNasc = @dataNasc, telefonefixo = @telefonefixo, telefonecel = @telefonecel, email = @email where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@rg", txtRG.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@bairro", txtBairro.Text);
            cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
            cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
            cmd.Parameters.AddWithValue("@limiteDeCredito", txtLimiteDeCredito.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@dataNasc", txtDataDeNascimento.Text);
            cmd.Parameters.AddWithValue("@telefonefixo", txtTelefoneFixo.Text);
            cmd.Parameters.AddWithValue("@telefonecel", txtTelefoneCelular.Text);
            cmd.Parameters.AddWithValue("@email", txtEMail.Text);

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //Código do botão para Excluir
                con.AbrirCon();
                sql = "DELETE FROM tbcliestes where id = @id";
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

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtRG.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtCPF.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txtEndereco.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtBairro.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtCidade.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtCEP.Text = grid.CurrentRow.Cells[7].Value.ToString();
            txtLimiteDeCredito.Text = grid.CurrentRow.Cells[8].Value.ToString();
            txtDataDeNascimento.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtTelefoneFixo.Text = grid.CurrentRow.Cells[10].Value.ToString();
            txtTelefoneCelular.Text = grid.CurrentRow.Cells[11].Value.ToString();
            txtEMail.Text = grid.CurrentRow.Cells[12].Value.ToString();
        }

        private void txtLimiteDeCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txt.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }
    }
}
