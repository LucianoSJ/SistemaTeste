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
    public partial class FrmFiltrarProduto : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        public FrmFiltrarProduto()
        {
            InitializeComponent();
        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbprodutos where nome LIKE @nome order by nome asc";
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
            sql = "SELECT * FROM tbprodutos order by nome asc";
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
            grid.Columns[1].HeaderText = "Cód.";
            grid.Columns[1].HeaderText = "Produto";
            grid.Columns[2].Visible = false;
            grid.Columns[3].Visible = false;
            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
            grid.Columns[6].Visible = false;
            grid.Columns[7].Visible = false;
            grid.Columns[8].Visible = false;
            grid.Columns[9].Visible = false;
            grid.Columns[0].Width = 70;
            grid.Columns[1].Width = 500;

        }

        private void FrmFiltrarProduto_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.CodBarras = grid.CurrentRow.Cells[6].Value.ToString();
            Close();
        }
    }
}
