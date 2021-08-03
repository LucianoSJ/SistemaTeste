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

namespace SistemaLoja
{
    public partial class FrmLogin : Form
    {
        Conexao con = new Conexao();

        public FrmLogin()
        {
            InitializeComponent();
            pnlLogin.Visible = false;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            pnlLogin.Location = new Point(this.Width / 2 - 166, Height / 2 - 170);
            pnlLogin.Visible = true;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(21, 114, 160);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(8, 72, 103);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ChamarLogin();
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ChamarLogin();
            }
        }

        private void ChamarLogin()
        {
            if (txtUsuario.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o Úsuario!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuario.Text = "";
                txtUsuario.Focus();
                return;
            }
            if (txtSenha.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha a Senha!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Text = "";
                txtSenha.Focus();
                return;
            }
            // Aqui vai o código para login
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT * FROM tbfuncionarios where login = @login and senha = @senha", con.con);
            cmdVerificar.Parameters.AddWithValue("@login", txtUsuario.Text);
            cmdVerificar.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = cmdVerificar.ExecuteReader();

            if (reader.HasRows)
            {
                //Extraindo informações da consulta do login
                while (reader.Read())
                {
                    Program.nomeUsuario = Convert.ToString(reader["nome"]);
                    Program.cargoUsuario = Convert.ToString(reader["cargo"]);
                    Program.idUsuario = Convert.ToString(reader["id"]);
                }

                //MessageBox.Show("Bem Vindo! " + Program.nomeUsuario, "Logado com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmMenu frmMenu = new FrmMenu();
                this.Hide();
                frmMenu.Show();
            }
            else
            {
                MessageBox.Show("Dados Incorretos!", "Verifique os Dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Text = "";
                txtSenha.Focus();
            }

            con.FecharCon();
        }

        private void FrmLogin_Resize(object sender, EventArgs e)
        {
            pnlLogin.Location = new Point(this.Width / 2 - 166, Height / 2 - 170);
        }
    }
}
