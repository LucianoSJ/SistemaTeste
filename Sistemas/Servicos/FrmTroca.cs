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
    public partial class FrmTroca : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmTroca()
        {
            InitializeComponent();
        }

        private void GerarCodigoTroca()
        {
            con.AbrirCon();
            sql = "SELECT max(id_Venda) from tb_itenstroca";

            try
            {
                cmd = new MySqlCommand(sql, con.con);
                if (cmd.ExecuteScalar() == DBNull.Value)
                {
                    lb_IdTroca.Text = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    lb_IdTroca.Text = ra.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                con.FecharCon();
            }
        }

        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT p.id_Produto, f.nome, p.quantidade, p.valorVenda, p.valorCusto FROM tb_itensVenda as p INNER JOIN tbprodutos as f ON p.id_Produto = f.id where id_Venda = @id_Venda order by p.id_Produto asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridC.DataSource = dt;
            con.FecharCon();
            dt.Columns.Add("Total", typeof(Double));
            FormatarDG();
        }

        private void FormatarDG()
        {
            gridC.Columns[0].HeaderText = "ID";
            gridC.Columns[1].HeaderText = "Produto";
            gridC.Columns[2].HeaderText = "Qtde.";
            gridC.Columns[3].HeaderText = "Valor Un.";
            gridC.Columns[4].HeaderText = "Custo.";
            gridC.Columns[5].HeaderText = "Total";
            gridC.Columns[3].DefaultCellStyle.Format = "C2";
            gridC.Columns[4].DefaultCellStyle.Format = "C2";
            gridC.Columns[5].DefaultCellStyle.Format = "C2";
            gridC.Columns[0].Width = 80;
            gridC.Columns[1].Width = 336;
            gridC.Columns[2].Width = 60;

            gridC.Columns[4].Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Filtros.FrmFiltrarVenda filtrarVenda = new Filtros.FrmFiltrarVenda();
            filtrarVenda.Show();
        }

        private void FrmTroca_Load(object sender, EventArgs e)
        {
            Program.forAberto = "troca";
            lbl_Usuario.Text = Program.nomeUsuario;
            lbl_id_User.Text = Program.idUsuario;
            rbtn_VoltarTroca.Checked = true;
            Bolquear();
            btn_Editar.Enabled = false;
            cbx_Qtde.Enabled = false;
            rbtn_SaidaTroca.Enabled = false;
        }

        private void FrmTroca_Activated(object sender, EventArgs e)
        {
            lbl_ID_Venda.Text = Program.idVenda;
            lbl_id_Cli.Text = Program.idcliente;
            lbl_nomeCliente.Text = Program.nomecliente;
        }

        private void btn_OK_Cliente_Click(object sender, EventArgs e)
        {
            FiltarTroca();
            //Listar();
            ValorTotalItem();
        }

        private void ValorTotalItem()
        {
            int qt_Itens = 0;
            decimal total = 0;
            decimal totalCusto = 0;
            foreach (DataGridViewRow dt in gridC.Rows)
            {
                decimal v1 = Convert.ToDecimal(dt.Cells[2].Value);
                decimal v2 = Convert.ToDecimal(dt.Cells[3].Value);
                decimal subtotal = v1 * v2;
                qt_Itens = (qt_Itens + Convert.ToInt16(dt.Cells[2].Value));

                decimal c1 = Convert.ToDecimal(dt.Cells[2].Value);
                decimal c2 = Convert.ToDecimal(dt.Cells[4].Value);
                decimal custo = c1 * c2;

                dt.Cells[5].Value = subtotal;
                total = total + subtotal;
                totalCusto = totalCusto + custo;

                txt_ValorPago.Text = String.Format("{0:C}", total);
                txt_ValorCompra.Text = String.Format("{0:C}", total);
                //txt_CustoTotal.Text = Convert.ToString(totalCusto);
                txt_QtdItens.Text = Convert.ToString(qt_Itens);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Bolquear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Habilitar();
        }

        private void Bolquear()
        {
            btn_Editar.Enabled = true;
            btn_Inserir.Enabled = false;
            txtCodBarras.Enabled = false;
            txt_ID_Produto.Enabled = false;
            txtPoduto.Enabled = false;
            txtValor.Enabled = false;
            txt_Valor_Total.Enabled = false;
            btn_Produto.Enabled = false;
        }

        private void Habilitar()
        {
            btn_Editar.Enabled = false;
            btn_Inserir.Enabled = true;
            txtCodBarras.Enabled = true;
            txt_ID_Produto.Enabled = true;
            txtPoduto.Enabled = true;
            txtValor.Enabled = true;
            txt_Valor_Total.Enabled = true;
            btn_Produto.Enabled = true;
        }

        private void gridC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ID_Produto.Text = gridC.CurrentRow.Cells[0].Value.ToString();
            txtPoduto.Text = gridC.CurrentRow.Cells[1].Value.ToString();
            cbx_Qtde.Text = gridC.CurrentRow.Cells[2].Value.ToString();
            txtValor.Text = gridC.CurrentRow.Cells[3].Value.ToString();
            txt_Custo.Text = gridC.CurrentRow.Cells[4].Value.ToString();
            txt_Valor_Total.Text = gridC.CurrentRow.Cells[5].Value.ToString();
            Bolquear();
            btn_Editar.Enabled = true;
            cbx_Qtde.Enabled = true;

            MySqlDataReader reader;
            con.AbrirCon();
            sql = "SELECT * FROM tbprodutos where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txt_ID_Produto.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtCodBarras.Text = Convert.ToString(reader["codBarras"]);
                    txtEstoque.Text = Convert.ToString(reader["estoque"]);
                }
            }
            con.FecharCon();
            ValorTotalItem();
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
                con.AbrirCon();
                sql = "INSERT INTO tb_itenstroca (id_troca, id_Venda, id_Cliente, id_Produto, quantidade, valorVenda, movimentacao, dataDaTroca, Status) VALUES (@id_troca, @id_Venda, @id_Cliente, @id_Produto, @quantidade, @valorVenda, 'Entrada', now(), 'Aberta')";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                cmd.Parameters.AddWithValue("@id_Cliente", int.Parse(lbl_id_Cli.Text));
                cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
                cmd.Parameters.AddWithValue("@quantidade", int.Parse(cbx_Qtde.Text));
                cmd.Parameters.AddWithValue("@valorVenda", Convert.ToDouble(txtValor.Text.Replace("R$", "")));

                cmd.ExecuteNonQuery();
                Listar();
                ValorTotalItem();
                con.FecharCon();
                ListarTroca();
                rbtn_SaidaTroca.Enabled = true;
        }
        private void ListarTroca()
        {
            con.AbrirCon();
            sql = "SELECT p.id_Produto, f.nome, p.quantidade, p.valorVenda, p.movimentacao FROM tb_itenstroca as p INNER JOIN tbprodutos as f ON p.id_Produto = f.id where id_Venda = @id_Venda order by p.id_Produto asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridT.DataSource = dt;
            con.FecharCon();
            FormatarDGTrocca();
        }

        private void FormatarDGTrocca()
        {
            gridT.Columns[0].HeaderText = "ID";
            gridT.Columns[1].HeaderText = "Produto";
            gridT.Columns[2].HeaderText = "Qtde.";
            gridT.Columns[3].HeaderText = "Valor";
            gridC.Columns[3].DefaultCellStyle.Format = "C2";
            gridT.Columns[4].HeaderText = "Movimentação.";
            gridT.Columns[0].Width = 80;
            gridT.Columns[1].Width = 336;
            gridT.Columns[2].Width = 60;
            gridT.Columns[3].Width = 100;
        }

        private void txt_Desconto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_Dinheiro_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_Entrada_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cbx_Qtde_TextChanged(object sender, EventArgs e)
        {
            if (txtValor.Text != string.Empty)
            {
                decimal valor = Convert.ToDecimal(txtValor.Text.Replace("R$", ""));
                int quantidade = int.Parse(cbx_Qtde.Text);
                decimal total = valor * quantidade;

                txt_Valor_Total.Text = String.Format("{0:C}", total);
            }
        }

        private void FiltarTroca()
        {
            // Verificar se a troca esta em aberto
            con.AbrirCon();
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_itenstroca where id_Venda = @id_Venda And Status = 'Aberta'", con.con);
            cmdItem.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                if (Program.forVendas != "Sim")
                {
                    MessageBox.Show("Cliente já tem uma venda em aberto!", "VENDA ABERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Program.forVendas = "Não";
                con.AbrirCon();
                sql = "SELECT * FROM tb_itenstroca where id_Venda = @id_Venda And Status = 'Aberta'";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataReader reader;
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lb_IdTroca.Text = Convert.ToString(reader["id_troca"]);
                    }
                    Listar();
                    ValorTotalItem();
                    ListarTroca();
                    rbtn_SaidaTroca.Enabled = true;
                    con.FecharCon();
                }
            }
            else
            {
                GerarCodigoTroca();
                cbx_Qtde.Text = "1";
                cbx_QtdeParcelas.Text = "1";
                lbl_Valor_da_Compra.Text = "R$ 0";
                lbl_Desconto.Text = "R$ 0";
                lbl_Sub_Total.Text = "R$ 0";
                lbl_Troco.Text = "R$ 0";
                lbl_Qtd_Itens.Text = "0";
                lbl_V_Entrada.Text = "R$ 0";
                lbl_Sub_TotalA.Text = "R$ 0";
                txtCodBarras.Text = "";
                txt_Data_Primeira_Parce.Text = DateTime.Now.ToShortDateString();
                btn_Editar.Enabled = false;
                btn_Excluir.Enabled = false;
                btn_OK_Cliente.Enabled = false;
                txt_Entrada.Enabled = false;
                cbx_QtdeParcelas.Enabled = false;

                gridC.DataSource = null;
                gridC.Columns.Clear();
                gridC.Rows.Clear();
                gridC.Refresh();

                gridT.DataSource = null;
                gridT.Columns.Clear();
                gridT.Rows.Clear();
                gridT.Refresh();

                gridN.DataSource = null;
                gridN.Columns.Clear();
                gridN.Rows.Clear();
                gridN.Refresh();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
        }

        private void btn_Finalizar_Venda_Click(object sender, EventArgs e)
        {

        }
    }
 }
