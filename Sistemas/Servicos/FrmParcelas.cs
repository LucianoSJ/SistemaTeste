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
    public partial class FrmParcelas : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmParcelas()
        {
            InitializeComponent();
        }

        private void MarcarParcelaComoPagaSim()
        {
            if (ck_Pagar.Checked == true)
            {
                con.AbrirCon();
                sql = "UPDATE tb_parcelas SET Pago = 'Sim' where id_Venda = @id_Venda And N_da_Parcela = @N_da_Parcela";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txt_N_Venda.Text));
                cmd.Parameters.AddWithValue("@N_da_Parcela", txt_Parcela.Text);
                cmd.ExecuteNonQuery();
                con.FecharCon();
            }
            else
            {
                MessageBox.Show("Antes de Confirmar o Pagamento, Clique na Opção: Pagar, do Lado Esquerdo do Botão Confirmar!", "ATENÇÂO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SaldoDeLimite()
        {
            try
            {
                decimal l = Convert.ToDecimal(txt_Limite.Text.Replace("R$", ""));
                decimal t = Convert.ToDecimal(txt_TotalEAberto.Text.Replace("R$", ""));
                decimal s = l - t;
                txt_Saldo.Text = String.Format("{0:C}", s);

                if (s < 0)
                {
                    txt_Saldo.BackColor = Color.LightCoral;
                    txt_Saldo.ForeColor = Color.Black;
                }
                else
                {
                    txt_Saldo.BackColor = Color.LightGreen;
                }
            }
            catch (Exception)
            {
                txt_Saldo.Text = String.Format("{0:C}", txt_Limite.Text);
                txt_Saldo.BackColor = Color.LightGreen;
            }
        }

        private void ValorTotalParcelasEmAberto()
        {
            con.AbrirCon();
            sql = "SELECT sum(tb_parcelas.Valor_Parcela) as Total, tb_venda.id_Cliente FROM tb_parcelas INNER JOIN tb_venda ON tb_parcelas.id_Venda = tb_venda.id_Venda WHERE id_Cliente = @id_Cliente And Pago = @Pago";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_Cliente", int.Parse(txt_ID_Cliente.Text));
            cmd.Parameters.AddWithValue("@Pago", "Não");
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txt_TotalEAberto.Text = String.Format("{0:C}", reader["Total"]);
                }
            }
            con.FecharCon();
        }

        private void ConsultarLimite()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbcliestes where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id", int.Parse(txt_ID_Cliente.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txt_Limite.Text = String.Format("{0:C}", reader["limiteDeCredito"]);
                }
            }
            con.FecharCon();
        }

        private void ValorTotalParcelasEmAbertoVendaSelecionada()
        {
            con.AbrirCon();
            sql = "SELECT sum(Valor_Parcela) as Total FROM tb_parcelas WHERE id_Venda = @id_Venda And Pago = @Pago";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txt_N_Venda.Text));
            cmd.Parameters.AddWithValue("@Pago", "Não");
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txt_ValorVendaSel.Text = String.Format("{0:C}", reader["Total"]);
                }
            }
            con.FecharCon();
        }

        private void FiltrarVenda()
        {
            con.AbrirCon();
            sql = "SELECT v.id_Venda, v.id_Cliente, c.nome, v.dataVenda, v.valorCompra, v.valorDesconto, v.valorDeEntrada, v.valorPago, v.formaDePagamento, v.formadeEntrada, v.id_Usuario FROM tb_venda as v INNER JOIN tbcliestes as c ON v.id_Cliente = c.id where c.nome LIKE @nome order by v.id_Venda desc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txt_Cliente.Text + "%");
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
        }

        private void FormatarDG_Vendas()
        {
            grid.Columns[0].HeaderText = "Nº da Venda";
            grid.Columns[1].HeaderText = "Nº do Cliente";
            grid.Columns[2].HeaderText = "Cliente";
            grid.Columns[3].HeaderText = "Data da Venda";
            grid.Columns[4].HeaderText = "Valor da Compra";
            grid.Columns[5].HeaderText = "Desconto";
            grid.Columns[6].HeaderText = "Entrada";
            grid.Columns[7].HeaderText = "Sub Total";
            grid.Columns[8].HeaderText = "Forma de Pagamento";
            grid.Columns[9].HeaderText = "Forma de Entrada";
            grid.Columns[10].HeaderText = "Nº do Vendedor";
            grid.Columns[0].Width = 80;
            grid.Columns[1].Width = 80;
            grid.Columns[2].Width = 250;
            grid.Columns[4].DefaultCellStyle.Format = "C2";
            grid.Columns[5].DefaultCellStyle.Format = "C2";
            grid.Columns[6].DefaultCellStyle.Format = "C2";
            grid.Columns[7].DefaultCellStyle.Format = "C2";
        }

        private void ListarParcelas()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_Parcelas where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txt_N_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Parcelas.DataSource = dt;
            con.FecharCon();
            FormatarParcelas();
        }


        private void ConsultaParcelas()
        {
            if (txt_N_Venda.Text != String.Empty)
            {
                if (rbtn_Nao.Checked == true)
                {
                    ListarParcelasNaoPagas();
                }

                if (rbtn_Sim.Checked == true)
                {
                    ListarParcelasSimPagas();
                }

                if (rbtn_Todas.Checked == true)
                {
                    ListarParcelas();
                }
            }
            else
            {
                MessageBox.Show("Selecione um Cliente", "ATENÇÂO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ListarParcelasNaoPagas()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_Parcelas WHERE id_Venda = @id_Venda And Pago = 'Não'";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txt_N_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Parcelas.DataSource = dt;
            con.FecharCon();
            FormatarParcelas();
        }

        private void ListarParcelasSimPagas()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_Parcelas WHERE id_Venda = @id_Venda And Pago = 'Sim'";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(txt_N_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Parcelas.DataSource = dt;
            con.FecharCon();
            FormatarParcelas();
        }

        private void FormatarParcelas()
        {
            grid_Parcelas.Columns[0].Visible = false;
            grid_Parcelas.Columns[1].Width = 60;
            grid_Parcelas.Columns[2].Width = 81;
            grid_Parcelas.Columns[3].Width = 75;
            grid_Parcelas.Columns[4].Width = 80;
            grid_Parcelas.Columns[5].Width = 60;
            grid_Parcelas.Columns[1].HeaderText = "Nº";
            grid_Parcelas.Columns[2].HeaderText = "Valor";
            grid_Parcelas.Columns[3].HeaderText = "Vencimento";
            grid_Parcelas.Columns[2].DefaultCellStyle.Format = "C2";
            grid_Parcelas.Columns[4].HeaderText = "Data do Pagamento";
            grid_Parcelas.Columns[5].HeaderText = "Paga";
        }

        private void FrmParcelas_Load(object sender, EventArgs e)
        {
            FiltrarVenda();
            FormatarDG_Vendas();
            rbtn_Nao.Checked = true;
        }

        private void txt_Cliente_TextChanged(object sender, EventArgs e)
        {
            ConsultarLimite();
            ValorTotalParcelasEmAberto();
            ValorTotalParcelasEmAbertoVendaSelecionada();
            SaldoDeLimite();
            ConsultaParcelas();
            FiltrarVenda();
        }

        private void LimparCampos()
        {
            txt_ID_Cliente.Text = "0";
            txt_N_Venda.Text = "0";
            lbl_Dia.Visible = false;
            txt_DataPagamento.Visible = false;
            ck_Pagar.Visible = false;
            txt_DataPagamento.Clear();
            txt_Parcela.Clear();
            txt_Valor.Clear();
            txt_Vencimento.Clear();
            txt_Paga.Clear();
            btnConfirmar.Visible = false;
            txt_Saldo.Clear();
            txt_TotalEAberto.Clear();
            txt_Limite.Clear();
            txt_ValorVendaSel.Clear();
            rbtn_Nao.Checked = true;
            txt_Cliente.Clear();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_N_Venda.Text = grid.CurrentRow.Cells[0].Value.ToString();
            lblidVenda.Text = grid.CurrentRow.Cells[0].Value.ToString();
            txt_ID_Cliente.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txt_Cliente.Text = grid.CurrentRow.Cells[2].Value.ToString();
            ConsultarLimite();
            ValorTotalParcelasEmAberto();
            ValorTotalParcelasEmAbertoVendaSelecionada();
            SaldoDeLimite();
            ConsultaParcelas();
        }

        private void grid_Parcelas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_Parcela.Text = grid_Parcelas.CurrentRow.Cells[1].Value.ToString();
            txt_Valor.Text = grid_Parcelas.CurrentRow.Cells[2].Value.ToString();
            txt_Vencimento.Text = grid_Parcelas.CurrentRow.Cells[3].Value.ToString();
            txt_Paga.Text = grid_Parcelas.CurrentRow.Cells[5].Value.ToString();
            txt_Valor.Text = String.Format("{0:C}", txt_Valor.Text);

            if (txt_Paga.Text == "Sim")
            {
                txt_DataPagamento.Text = grid_Parcelas.CurrentRow.Cells[4].Value.ToString();
                lbl_Dia.Visible = true;
                txt_DataPagamento.Visible = true;
                ck_Pagar.Visible = false;
                btnConfirmar.Visible = false;
            }
            else
            {
                lbl_Dia.Visible = false;
                txt_DataPagamento.Visible = false;
                ck_Pagar.Visible = true;
                btnConfirmar.Visible = true;
            }  
        }
        private void rbtn_ParcelasNaoPagas(object sender, EventArgs e)
        {
          ConsultaParcelas();
        }

        private void rbtn_Todas_Click(object sender, EventArgs e)
        {
            ListarParcelas();
        }

        private void rbtn_Sim_Click(object sender, EventArgs e)
        {
            ListarParcelasSimPagas();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            MarcarParcelaComoPagaSim();
            lbl_Dia.Visible = false;
            txt_DataPagamento.Visible = false;
            ck_Pagar.Visible = false;
            txt_DataPagamento.Clear();
            txt_Parcela.Clear();
            txt_Valor.Clear();
            txt_Vencimento.Clear();
            txt_Paga.Clear();
            btnConfirmar.Visible = false;
            ConsultaParcelas();
            ConsultarLimite();
            ValorTotalParcelasEmAberto();
            ValorTotalParcelasEmAbertoVendaSelecionada();
            SaldoDeLimite();
            ck_Pagar.Checked = false;
        }

        private void ck_Pagar_Click(object sender, EventArgs e)
        {
            if (ck_Pagar.Checked == true)
            {
                txt_Paga.Text = "Sim";
            }
            else
            {
                txt_Paga.Text = "Não";
            }
        }

        private void txt_Cliente_Enter(object sender, EventArgs e)
        {
            LimparCampos();
            txt_Saldo.BackColor = Color.White;
        }
    }
}
