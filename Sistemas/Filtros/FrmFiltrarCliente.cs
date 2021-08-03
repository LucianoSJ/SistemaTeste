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

namespace SistemaLoja.Filtros
{
    public partial class FrmFiltrarCliente : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        public FrmFiltrarCliente()
        {
            InitializeComponent();
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

        private void FormatarDG()
        {
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[0].Visible = false;
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;
            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;
            grid.Columns[1].Width = 250;

        }

        private void FrmFiltrarCliente_Load(object sender, EventArgs e)
        {
           Listar();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.idcliente = grid.CurrentRow.Cells[0].Value.ToString();
            Program.nomecliente = grid.CurrentRow.Cells[1].Value.ToString();
            Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }
    }
}
