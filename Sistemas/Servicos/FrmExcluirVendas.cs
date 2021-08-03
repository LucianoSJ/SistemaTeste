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

namespace SistemaLoja.Servicos
{
    public partial class FrmExcluirVendas : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmExcluirVendas()
        {
            InitializeComponent();
        }

        private void FiltrarVenda()
        {
            con.AbrirCon();
            sql = "SELECT v.id_Venda, v.id_Cliente, c.nome, v.dataVenda FROM tb_venda as v INNER JOIN tbcliestes as c ON v.id_Cliente = c.id order by v.id_Venda desc";
            cmd = new MySqlCommand(sql, con.con);
           // cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txtIDVenda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
        }

        private void FormatarDG_Parcelas()
        {
            grid.Columns[0].HeaderText = "Nº da Venda";
            grid.Columns[1].HeaderText = "Nº do Cliente";
            grid.Columns[2].HeaderText = "Cliente";
            grid.Columns[3].HeaderText = "Data da Venda";
            grid.Columns[2].Width = 250;
        }

        private void FrmExcluirVendas1_Load(object sender, EventArgs e)
        {
            FiltrarVenda();
            FormatarDG_Parcelas();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDVenda.Text = grid.CurrentRow.Cells[0].Value.ToString();
            txt_IdCliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txt_Data.Text = grid.CurrentRow.Cells[3].Value.ToString();
            btnExcluir.Enabled = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir a Venda?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                con.AbrirCon();
                sql = "DELETE FROM tb_itensVenda where id_Venda = @id_Venda";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txtIDVenda.Text));
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM tb_venda where id_Venda = @id_Venda";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txtIDVenda.Text));
                cmd.ExecuteNonQuery();

                sql = "DELETE FROM tb_Parcelas where id_Venda = @id_Venda";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txtIDVenda.Text));
                cmd.ExecuteNonQuery();

                con.FecharCon();
                FiltrarVenda();
                btnExcluir.Enabled = false;
                txtIDVenda.Clear();
                txtNome.Clear();
                txt_Data.Clear();
                txt_IdCliente.Clear();
            }
        }

        private void txtIDVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se a tecla digitada não for número e nem backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}
