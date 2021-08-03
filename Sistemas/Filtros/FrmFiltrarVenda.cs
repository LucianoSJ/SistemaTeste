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
    public partial class FrmFiltrarVenda : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        public FrmFiltrarVenda()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            con.AbrirCon();
            if (Program.forAberto == "caixa")
            {
                sql = "SELECT tb_venda.id_Cliente, tb_venda.dataVenda, tbcliestes.id, tbcliestes.nome FROM tb_venda INNER JOIN tbcliestes ON tb_venda.id_Cliente = tbcliestes.id where valorPago = 00.0 order by dataVenda asc";
            }
            else
            {
                sql = "SELECT tb_venda.id_Cliente, tb_venda.dataVenda, tbcliestes.id, tbcliestes.nome FROM tb_venda INNER JOIN tbcliestes ON tb_venda.id_Cliente = tbcliestes.id where valorPago > 00.0 order by dataVenda asc";
            }
            //sql = "SELECT * FROM tb_Venda where valorPago = 00.0 order by dataVenda asc";
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
            grid.Columns[0].HeaderText = "Nº da Venda";
            grid.Columns[1].HeaderText = "Data";
            grid.Columns[2].HeaderText = "Nº Cliente";
            grid.Columns[3].HeaderText = "Cliente";
            grid.Columns[0].Width = 50;
            grid.Columns[1].Width = 110;
            grid.Columns[2].Width = 60;
            grid.Columns[3].Width = 320;

        }

        private void FrmFiltrarVenda_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.idVenda = grid.CurrentRow.Cells[0].Value.ToString();
            Program.idcliente = grid.CurrentRow.Cells[2].Value.ToString();
            Program.nomecliente = grid.CurrentRow.Cells[3].Value.ToString();
            Program.forVendas = "Sim";
            Close();
        }
    }
}
