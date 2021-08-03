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
    public partial class FrmUsuarios : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string loginAntigo;

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbfuncionarios order by nome asc";
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
            sql = "SELECT * FROM tbfuncionarios where nome LIKE @nome order by nome asc";
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
            grid.Columns[2].HeaderText = "Login";
            grid.Columns[3].HeaderText = "Senha";
            grid.Columns[4].HeaderText = "Perfil";
            grid.Columns[5].HeaderText = "Cargo";
            grid.Columns[6].HeaderText = "Dada";
            grid.Columns[0].Visible = false;
            grid.Columns[3].Visible = false;
            grid.Columns[1].Width = 300;
            grid.Columns[4].Width = 80;
            grid.Columns[6].Width = 80;
        }


        private void CarregarCombobox()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbcargos order by cargo asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxCargo.DataSource = dt;
            //cbxCargo.ValueMember = "id";
            cbxCargo.DisplayMember = "cargo";
            con.FecharCon();
        }

        private void habilitarCampos()
        {
            txtNome.Enabled = true;
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
            cbxPerfil.Enabled = true;
            cbxCargo.Enabled = true;
            txtNome.Focus();
        }

        private void desabilitarCampos()
        {
            txtNome.Enabled = false;
            txtLogin.Enabled = false;
            txtSenha.Enabled = false;
            cbxPerfil.Enabled = false;
            cbxCargo.Enabled = false;
        }

        private void limparCampos()
        {
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtBuscar.Clear();
            cbxPerfil.Text = "Comum";
            cbxCargo.Text = "";
            txtData.Text = DateTime.Now.ToShortDateString();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CarregarCombobox();
            cbxCargo.Text = "";
            Listar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limparCampos();
            btnSalvar.Enabled = true;
            btnNovo.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtNome.Focus();
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
            if (txtLogin.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o login!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLogin.Text = "";
                txtLogin.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Text = "";
                txtSenha.Focus();
                return;
            }
            if (cbxPerfil.Text == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxPerfil.Focus();
                return;
            }
            if (cbxCargo.Text == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxCargo.Focus();
                return;
            }

            //Código do botão para salvar
            con.AbrirCon();
            sql = "INSERT INTO tbfuncionarios (nome, login, senha, perfil, cargo, data) VALUES (@nome, @login, @senha, @perfil, @cargo, curDate())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("@login", txtLogin.Text);
            cmd.Parameters.AddWithValue("@perfil", cbxPerfil.Text);
            cmd.Parameters.AddWithValue("@cargo", cbxCargo.Text);

            // Verificar se o login do usuário já exeiste
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM tbfuncionarios where login = @login", con.con);
            cmdVerificar.Parameters.AddWithValue("@login", txtLogin.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Usuário já Registrado, Escolha outro Login!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Text = "";
                txtLogin.Focus();
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
            if (txtLogin.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o login!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLogin.Text = "";
                txtLogin.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Text = "";
                txtSenha.Focus();
                return;
            }
            if (cbxPerfil.Text == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxPerfil.Focus();
                return;
            }
            if (cbxCargo.Text == "")
            {
                MessageBox.Show("Preencha a senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxCargo.Focus();
                return;
            }

            //Código do botão para editar
            con.AbrirCon();
            sql = "UPDATE tbfuncionarios SET nome = @nome, login = @login, senha = @senha, perfil = @perfil, cargo = @cargo where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@login", txtLogin.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            cmd.Parameters.AddWithValue("@perfil", cbxPerfil.Text);
            cmd.Parameters.AddWithValue("@cargo", cbxCargo.Text);

            // Verifica se o login já existe no Banco

            if(txtLogin.Text != loginAntigo)
            {
                MySqlCommand cmdVerificar;
                cmdVerificar = new MySqlCommand("SELECT * FROM tbfuncionarios where login = @login", con.con);
                cmdVerificar.Parameters.AddWithValue("@login", txtLogin.Text);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmdVerificar;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Usuário já Registrado, Escolha outro Login!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLogin.Text = "";
                    txtLogin.Focus();
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                //Código do botão para Excluir
                con.AbrirCon();
                sql = "DELETE FROM tbfuncionarios where id = @id";
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

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtLogin.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtSenha.Text = grid.CurrentRow.Cells[3].Value.ToString();
            cbxPerfil.Text = grid.CurrentRow.Cells[4].Value.ToString();
            cbxCargo.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtData.Text = grid.CurrentRow.Cells[6].Value.ToString();

            loginAntigo = grid.CurrentRow.Cells[2].Value.ToString();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }
    }
}
