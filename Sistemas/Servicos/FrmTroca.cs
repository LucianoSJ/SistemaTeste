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
        int quantidade;
        String LiberarConsutaParcelas = "Não";

        decimal valorCompraOriginal = 0;
        decimal valorPagoOriginal = 0;
        decimal valorDescontoOriginal = 0;
        decimal valorDeEntradaOriginal = 0;
        String id_TrocaOriginal = "";
        String formadeEntradaOriginal = "";

        public FrmTroca()
        {
            InitializeComponent();
        }

        private void GerarCodigoTroca()
        {
            con.AbrirCon();
            sql = "SELECT max(id_troca) from tb_itenstroca";

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            IniciarNovaTrocaLimpaTudo();
            Filtros.FrmFiltrarVenda filtrarVenda = new Filtros.FrmFiltrarVenda();
            filtrarVenda.Show();
        }

        private void IniciarNovaTrocaLimpaTudo()
        {
            Limpar();
            LiberarConsutaParcelas = "Não";
            lbl_valorCompraOriginal.Text = "0";
            lb_IdTroca.Text = "0";
            txt_ValorPago.Clear();
            txt_QtdItens.Clear();
            txt_QtdTotal.Clear();
            txt_Valor_Troca.Clear();
            txt_ValorItensNovos.Clear();
            txt_ValorSaldo.Clear();
            txt_Q_Troca.Clear();
            txt_Q_Novos.Clear();
            txt_Q_Total.Clear();
            btn_OK_Cliente.Enabled = true;
            Bolquear();
            cbx_FormPagamento.Text = "";
            btn_Editar.Enabled = false;

            gridC.DataSource = null;
            gridC.Columns.Clear();
            gridC.Rows.Clear();
            gridC.Refresh();

            gridT.DataSource = null;
            gridT.Columns.Clear();
            gridT.Rows.Clear();
            gridT.Refresh();
        }

        private void FrmTroca_Load(object sender, EventArgs e)
        {
            Program.forAberto = "troca";
            lbl_ID_Venda.Text = "0";
            lbl_Usuario.Text = Program.nomeUsuario;
            lbl_id_User.Text = Program.idUsuario;
            rbtn_VoltarTroca.Checked = true;
            Bolquear();
            btn_Editar.Enabled = false;
            cbx_Qtde.Enabled = false;
            rbtn_SaidaTroca.Enabled = false;
            grid_Parcelas.Visible = false;
            lbl_Parcelas.Visible = false;
            img.Visible = true;
            txt_Data_Primeira_Parce.Text = DateTime.Now.ToShortDateString();
            IniciarNovaTrocaLimpaTudo();
        }

        private void FrmTroca_Activated(object sender, EventArgs e)
        {
            lbl_ID_Venda.Text = Program.idVenda;
            lbl_id_Cli.Text = Program.idcliente;
            lbl_nomeCliente.Text = Program.nomecliente;

            if (Program.vendedorAcionado == "Sim")
            {
                lbl_id_User.Text = Program.idUsuario;
                lbl_Usuario.Text = Program.vendedor;
            }

            if (Program.CodBarras != "NT")
            {
                txtCodBarras.Text = Program.CodBarras;
                Program.CodBarras = "NT";
                ConsultaCodigoDeBarras();
            }
        }

        private void btn_OK_Cliente_Click(object sender, EventArgs e)
        {
            if (lbl_ID_Venda.Text == "0")
            {
                MessageBox.Show("Selecione uma venda para efetuar a troca!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FiltarValoresAntesDeClicarEmInserirNovosItens();
            VerificarSeJaIniciouAinsercaoDeItensNovos();
            if (Program.troca == "Sim")
            {
                FiltarTrocaAtualJaIniciadoInsercaodeNovosItens();
                rbtn_VoltarTroca.Checked = false;
                rbtn_SaidaTroca.Checked = true;
                gridT.Enabled = false;
                cbx_Qtde.Enabled = true;
                btn_Editar.Enabled = false;
                btn_Inserir.Enabled = true;
                Habilitar();
                rbtn_VoltarTroca.Enabled = false;
                rbtn_SaidaTroca.Enabled = false;
            }
            VerificaOSaldoDoValorDeTroca();
            VerificaSeExiteParcelasNaTroca();
            cbx_QtdeParcelas.Text = Convert.ToString(grid_Parcelas.RowCount);
            FiltrarFormaDEPagamento();
            LiberarConsutaParcelas = "Sim";
        }

        private void FiltrarFormaDEPagamento()
        {
            try
            {
                MySqlDataReader reader;
                con.AbrirCon();
                sql = "SELECT * FROM tb_venda where id_Venda = @id_Venda";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cbx_FormPagamento.Text = Convert.ToString(reader["formaDePagamento"]);
                    }
                }
                con.FecharCon();
            }
            catch (Exception)
            {
            }
        }

        private void VerificaSeExiteParcelasNaTroca()
        {
            ListarParcelas();
            if (grid_Parcelas.RowCount != 0)
            {
                grid_Parcelas.Visible = true;
                lbl_Parcelas.Visible = true;
                img.Visible = false;
            }
            else
            {
                grid_Parcelas.Visible = false;
                lbl_Parcelas.Visible = false;
                img.Visible = true;
                cbx_QtdeParcelas.Text = "0";
                cbx_FormPagamento.Text = "";
            }
        }

        private void VerificarSeJaIniciouAinsercaoDeItensNovos()
        {
            FiltarValoresAntesDeClicarEmInserirNovosItens();
            con.AbrirCon();
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_trocaatual where id_troca = @id_troca", con.con);
            cmdItem.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                Program.troca = "Sim";
            }
        }

        private void FiltarTrocaAtualJaIniciadoInsercaodeNovosItens()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_trocaatual where id_troca = @id_troca";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lbl_valorCompraOriginal.Text = Convert.ToString(String.Format("{0:C}", reader["ValorDaCompra"]));
                    txt_ValorPago.Text = Convert.ToString(String.Format("{0:C}", reader["ValorCompraAtual"]));
                    txt_QtdItens.Text = Convert.ToString(reader["QtdeItens"]);
                    txt_QtdTotal.Text = Convert.ToString(reader["QtdItensMaisNovo"]);
                    txt_Valor_Troca.Text = Convert.ToString(String.Format("{0:C}", reader["Valor_Troca"]));
                    txt_ValorItensNovos.Text = Convert.ToString(String.Format("{0:C}", reader["ValorNovosItens"]));
                    txt_ValorSaldo.Text = Convert.ToString(String.Format("{0:C}", reader["Saldo"]));
                    txt_Q_Troca.Text = Convert.ToString(reader["QtdTroca"]);
                    txt_Q_Novos.Text = Convert.ToString(reader["QtdeNovos"]);
                    txt_Q_Total.Text = Convert.ToString(reader["QtdeTotal"]);
                }
            }
            con.FecharCon();
        }

        private void FiltarValoresAntesDeClicarEmInserirNovosItens()
        {
            FiltarTroca();
            //Listar();
            ValorTotalItem();
            btn_OK_Cliente.Enabled = false;
            ValorOriginaDaCompra();
        }

        private void ValorOriginaDaCompra()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_venda where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lbl_valorCompraOriginal.Text = String.Format("{0:C}", Convert.ToDecimal(reader["valorPago"]));
                }
            }
            con.FecharCon();
        }

        private void ValorTotalItem()
        {
            int qt_Itens = 0;
            decimal total = 0;
            decimal totalCusto = 0;
            foreach (DataGridViewRow dt in gridC.Rows)
            {
                decimal qt = Convert.ToDecimal(dt.Cells[2].Value);
                decimal v = Convert.ToDecimal(dt.Cells[3].Value);
                decimal subtotal = v * qt;
                qt_Itens = (qt_Itens + Convert.ToInt16(dt.Cells[2].Value));

                decimal qt2 = Convert.ToDecimal(dt.Cells[2].Value);
                decimal v2 = Convert.ToDecimal(dt.Cells[4].Value);
                decimal custo = v2 * qt2;

                dt.Cells[5].Value = subtotal;
                total = total + subtotal;
                totalCusto = totalCusto + custo;

                txt_ValorPago.Text = String.Format("{0:C}", total);
                txt_CustoTotal.Text = Convert.ToString(totalCusto);

                if (rbtn_VoltarTroca.Checked == true)
                {
                    txt_QtdItens.Text = Convert.ToString(qt_Itens);
                    txt_QtdTotal.Text = Convert.ToString(qt_Itens);
                }
                else
                {
                    txt_QtdTotal.Text = Convert.ToString(qt_Itens);
                }
            }

            if (gridC.RowCount == 0)
            {
                txt_ValorPago.Clear();
                txt_CustoTotal.Clear();
                txt_QtdItens.Clear();
            }
        }

        private void ValorTotalItemTroca()
        {
            int qt_Itens = 0;
            decimal total = 0;
            foreach (DataGridViewRow dt in gridT.Rows)
            {
                decimal qt = Convert.ToDecimal(dt.Cells[3].Value);
                decimal v = Convert.ToDecimal(dt.Cells[4].Value);
                decimal subtotal = v * qt;
                qt_Itens = (qt_Itens + Convert.ToInt16(dt.Cells[3].Value));

                dt.Cells[6].Value = subtotal;
                total = total + subtotal;
                if (Program.troca == "Não")
                {
                    txt_Valor_Troca.Text = String.Format("{0:C}", total);

                    if (rbtn_SaidaTroca.Checked != true)
                    {
                        txt_ValorSaldo.Text = String.Format("{0:C}", total);
                    }

                    if (rbtn_VoltarTroca.Checked == true)
                    {
                        txt_Q_Troca.Text = Convert.ToString(qt_Itens);
                        txt_Q_Total.Text = Convert.ToString(qt_Itens);

                    }
                    else
                    {
                        txt_Q_Total.Text = Convert.ToString(qt_Itens);
                    }
                }
                txt_Valor_Troca.Text = String.Format("{0:C}", txt_Valor_Troca.Text);
                txt_ValorSaldo.Text = String.Format("{0:C}", txt_ValorSaldo.Text);
            }

            if (gridT.RowCount == 0)
            {
                txt_Valor_Troca.Clear();
                txt_Q_Troca.Clear();
                txt_ValorSaldo.Clear();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Bolquear();
            gridC.Enabled = true;
            gridT.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.troca == "Não")
            {
                var resultado = MessageBox.Show("Já inseriu todos os itens que serão trocados?", "ATENÇÂO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    SalvarValoresNaTabelaTrocaAtual();
                    Habilitar();
                    gridT.Enabled = false;
                    cbx_Qtde.Enabled = true;
                    rbtn_VoltarTroca.Enabled = false;
                    rbtn_SaidaTroca.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Insira primeiramente todos os itens que serão trocados, antes de inserir os novos itens!", "ATENÇÂO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                FiltarValoresAntesDeClicarEmInserirNovosItens();
                VerificarSeJaIniciouAinsercaoDeItensNovos();
            }
        }

        private void SalvarValoresNaTabelaTrocaAtual()
        {
            decimal valor;
            int qtde;
            int qtdeT;
            if (gridC.RowCount == 0)
            {
                valor = 0;
                qtde = 0;
                qtdeT = 0;
            }
            else
            {
                valor = Convert.ToDecimal(txt_ValorPago.Text.Replace("R$", ""));
                qtde = int.Parse(txt_QtdItens.Text);
                qtdeT = int.Parse(txt_QtdTotal.Text);
            }

            con.AbrirCon();
            sql = "INSERT INTO tb_trocaatual (id_troca, ValorDaCompra, ValorCompraAtual, QtdeItens, QtdItensMaisNovo, Valor_Troca, ValorNovosItens, Saldo, QtdTroca, QtdeNovos, QtdeTotal) VALUES (@id_troca, @ValorDaCompra, @ValorCompraAtual, @QtdeItens, @QtdItensMaisNovo, @Valor_Troca, 0, @Saldo, @QtdTroca, 0, @QtdeTotal)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.Parameters.AddWithValue("@ValorDaCompra", Convert.ToDouble(lbl_valorCompraOriginal.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@ValorCompraAtual", valor);
            cmd.Parameters.AddWithValue("@QtdeItens", qtde);
            cmd.Parameters.AddWithValue("@QtdItensMaisNovo", qtdeT);
            cmd.Parameters.AddWithValue("@Valor_Troca", Convert.ToDouble(txt_Valor_Troca.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@Saldo", Convert.ToDouble(txt_ValorSaldo.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@QtdTroca", int.Parse(txt_Q_Troca.Text));
            cmd.Parameters.AddWithValue("@QtdeTotal", int.Parse(txt_Q_Total.Text));
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private void AtualizarValoresNaTabelaTrocaAtual()
        {
            decimal valorPago = 0;
            decimal valorItensNovos = 0;
            int QtdItens = 0;
            int QtdTotal = 0;
            int QtdQ_Novos = 0;

            if (txt_ValorPago.Text != String.Empty)
            {
                valorPago = Convert.ToDecimal(txt_ValorPago.Text.Replace("R$", ""));
            }

            if (txt_ValorItensNovos.Text != String.Empty)
            {
                valorItensNovos = Convert.ToDecimal(txt_ValorItensNovos.Text.Replace("R$", ""));
            }

            if (txt_QtdItens.Text != String.Empty)
            {
                QtdItens = int.Parse(txt_QtdItens.Text);
            }

            if (txt_QtdTotal.Text != String.Empty)
            {
                QtdTotal = int.Parse(txt_QtdTotal.Text);
            }

            if (txt_Q_Novos.Text != String.Empty)
            {
                QtdQ_Novos = int.Parse(txt_Q_Novos.Text);
            }

            con.AbrirCon();
            sql = "UPDATE tb_trocaatual SET ValorDaCompra= @ValorDaCompra, ValorCompraAtual= @ValorCompraAtual, QtdeItens= @QtdeItens, QtdItensMaisNovo= @QtdItensMaisNovo, Valor_Troca= @Valor_Troca, ValorNovosItens= @ValorNovosItens, Saldo= @Saldo, QtdTroca= @QtdTroca, QtdeNovos= @QtdeNovos, QtdeTotal= @QtdeTotal where id_troca = @id_troca";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.Parameters.AddWithValue("@ValorDaCompra", Convert.ToDouble(lbl_valorCompraOriginal.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@ValorCompraAtual", valorPago);
            cmd.Parameters.AddWithValue("@QtdeItens", QtdItens);
            cmd.Parameters.AddWithValue("@QtdItensMaisNovo", int.Parse(txt_QtdTotal.Text));
            cmd.Parameters.AddWithValue("@Valor_Troca", Convert.ToDouble(txt_Valor_Troca.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@ValorNovosItens", valorItensNovos);
            cmd.Parameters.AddWithValue("@Saldo", Convert.ToDouble(txt_ValorSaldo.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@QtdTroca", int.Parse(txt_Q_Troca.Text));
            cmd.Parameters.AddWithValue("@QtdeNovos", QtdQ_Novos);
            cmd.Parameters.AddWithValue("@QtdeTotal", QtdTotal);
            cmd.ExecuteNonQuery();
            con.FecharCon();
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
            txt_Q_orig.Text = gridC.CurrentRow.Cells[2].Value.ToString();
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
            btn_Editar.Enabled = true;
            btn_Excluir.Enabled = false;
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (int.Parse(cbx_Qtde.Text) > int.Parse(txt_Q_orig.Text))
            {
                MessageBox.Show("Não é permitido trocar uma quantidade maior do que foi comprada pelo cliente!", "ATENÇÂO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbx_Qtde.Text = "1";
                return;
            }
            else
            {
                AtualizarEstoque();
                VerificaSeOItemJáEstaNaTroca();
            }

            if (gridC.RowCount == 0)
            {
                txt_QtdTotal.Clear();
            }
            Limpar();
        }

        private void VerificaSeOItemJáEstaNaTroca()
        {
            String Status = "Saída";
            if (Program.troca == "Não")
            {
                Status = "Entrada";
            }

            con.AbrirCon();
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_itenstroca where id_troca= @id_troca And id_Produto= @id_Produto And movimentacao= @movimentacao", con.con);
            cmdItem.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmdItem.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmdItem.Parameters.AddWithValue("@movimentacao", Status);
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                if (Program.troca == "Não")
                {
                    if (rbtn_SaidaTroca.Checked != true)
                    {
                        MessageBox.Show("O Item Já foi inserido na troca, caso precise altear a quantidade, exclua e insira novamente!", "ITEM JÁ INSERIDO NA TROCA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    AtualizaQtdeDoItemDeSaidaNaTabelaTroca();
                }
            }
            else
            {
                if (Program.troca == "Não")
                {
                    InsereNaTabelaTroca();
                }
                else
                {
                    InserirItensNaTabelaTrocaSaidaDeProdutos();
                }
            }
        }

        private void AtualizaQtdeDoItemDeSaidaNaTabelaTroca()
        {
            int quantidade = 0;
            MySqlDataReader reader;
            con.AbrirCon();
            sql = "SELECT * FROM tb_itenstroca where id_troca = @id_troca And id_Produto = @id_Produto And movimentacao= 'Saída'";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    quantidade = Convert.ToInt32(reader["quantidade"]);
                }
            }
            quantidade = quantidade + int.Parse(cbx_Qtde.Text);
            con.FecharCon();

            con.AbrirCon();
            sql = "UPDATE tb_itenstroca SET quantidade= @quantidade where id_troca = @id_troca And id_Produto = @id_Produto And movimentacao= 'Saída'";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }
        private void InsereNaTabelaTroca()
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

            if (int.Parse(cbx_Qtde.Text) == int.Parse(txt_Q_orig.Text) && rbtn_VoltarTroca.Checked == true)
            {
                ExcluirItemDaVenda();
            }
            else
            {
                EditarQuantidadeEmVendasParaMenos();
            }
            FechaAConexaoECalculaValores();
            rbtn_SaidaTroca.Enabled = true;
            btn_Editar.Enabled = false;
        }

        private void InserirItensNaTabelaTrocaSaidaDeProdutos()
        {
            con.AbrirCon();
            sql = "INSERT INTO tb_itenstroca (id_troca, id_Venda, id_Cliente, id_Produto, quantidade, valorVenda, movimentacao, dataDaTroca, Status) VALUES (@id_troca, @id_Venda, @id_Cliente, @id_Produto, @quantidade, @valorVenda, 'Saída', now(), 'Aberta')";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@id_Cliente", int.Parse(lbl_id_Cli.Text));
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@quantidade", int.Parse(cbx_Qtde.Text));
            cmd.Parameters.AddWithValue("@valorVenda", Convert.ToDouble(txtValor.Text.Replace("R$", "")));
            cmd.ExecuteNonQuery();
        }

        private void EditarQuantidadeEmVendasParaMenos()
        {
            int quantidade = int.Parse(txt_Q_orig.Text) - int.Parse(cbx_Qtde.Text);
            con.AbrirCon();
            sql = "UPDATE tb_itensVenda SET quantidade= @quantidade where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.ExecuteNonQuery();
        }

        private void EditarQuantidadeEmVendasParaMais()
        {
            int quantidade = int.Parse(txt_Q_orig.Text) + int.Parse(cbx_Qtde.Text);
            con.AbrirCon();
            sql = "UPDATE tb_itensVenda SET quantidade= @quantidade where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.ExecuteNonQuery();
        }

        private void ExcluirItemDaVenda()
        {
            con.AbrirCon();
            sql = "DELETE FROM tb_itensVenda where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.ExecuteNonQuery();
        }
        private void ListarTroca()
        {
            con.AbrirCon();
            sql = "SELECT p.id, p.id_Produto, f.nome, p.quantidade, p.valorVenda, p.movimentacao FROM tb_itenstroca as p INNER JOIN tbprodutos as f ON p.id_Produto = f.id where id_Venda = @id_Venda order by p.id_Produto asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridT.DataSource = dt;
            con.FecharCon();
            dt.Columns.Add("Total", typeof(Double));
            FormatarDGTrocca();
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
            gridC.Columns[0].Width = 110;
            gridC.Columns[1].Width = 420;
            gridC.Columns[2].Width = 110;
            gridC.Columns[3].Width = 110;
            gridC.Columns[4].Visible = false;
        }

        private void FormatarDGTrocca()
        {
            gridT.Columns[1].HeaderText = "ID Prod.";
            gridT.Columns[2].HeaderText = "Produto";
            gridT.Columns[3].HeaderText = "Qtde.";
            gridT.Columns[4].HeaderText = "Valor";
            gridT.Columns[4].DefaultCellStyle.Format = "C2";
            gridT.Columns[5].HeaderText = "Movimentação.";
            gridT.Columns[6].DefaultCellStyle.Format = "C2";
            gridT.Columns[1].Width = 110;
            gridT.Columns[2].Width = 420;
            gridT.Columns[3].Width = 110;
            gridT.Columns[4].Width = 110;
            gridT.Columns[5].Width = 120;
            gridT.Columns[0].Visible = false;
            gridT.Columns[5].DisplayIndex = 6;
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
                    FechaAConexaoECalculaValores();
                    rbtn_SaidaTroca.Enabled = true;
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

                Listar();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
        }

        private void btn_Finalizar_Venda_Click(object sender, EventArgs e)
        {

        }

        private void gridT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_Grid.Text = gridT.CurrentRow.Cells[0].Value.ToString();
            txt_ID_Produto.Text = gridT.CurrentRow.Cells[1].Value.ToString();
            txtPoduto.Text = gridT.CurrentRow.Cells[2].Value.ToString();
            cbx_Qtde.Text = gridT.CurrentRow.Cells[3].Value.ToString();
            txtValor.Text = gridT.CurrentRow.Cells[4].Value.ToString();
            txt_Custo.Text = gridT.CurrentRow.Cells[5].Value.ToString();
            txt_Valor_Total.Text = gridT.CurrentRow.Cells[6].Value.ToString();
            Bolquear();
            btn_Editar.Enabled = true;
            cbx_Qtde.Enabled = false;

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
                    txt_Custo.Text = Convert.ToString(reader["valor_compra"]);
                }
            }

            txt_CustoTotal.Text = Convert.ToString((Convert.ToDecimal(txt_Custo.Text.Replace("R$", "")) * int.Parse(cbx_Qtde.Text)));

            FiltrarQuantidadeDoItemNaTabelaVenda();
            //FechaAConexaoECalculaValores();
            btn_Editar.Enabled = false;
            btn_Excluir.Enabled = true;
            con.FecharCon();
        }

        private void FiltrarQuantidadeDoItemNaTabelaVenda()
        {
            MySqlDataReader reader;
            con.AbrirCon();
            sql = "SELECT * FROM tb_itensvenda where id_Venda = @id_Venda And id_Produto = @id_Produto";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txt_Q_orig.Text = Convert.ToString(reader["quantidade"]);
                }
            }
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Item da Troca?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                VerificaSeOItemJáFoiExcluidoDaVenda();
                DeletaItemDaTroca();
            }
            FechaAConexaoECalculaValores();
            Limpar();
        }

        private void DeletaItemDaTroca()
        {
            con.AbrirCon();
            sql = "DELETE FROM tb_itenstroca where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txt_Grid.Text));
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private void VerificaSeOItemJáFoiExcluidoDaVenda()
        {
            con.AbrirCon();
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_itensvenda where id_Venda = @id_Venda And id_Produto = @id_Produto", con.con);
            cmdItem.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmdItem.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                EditarQuantidadeEmVendasParaMais();
            }
            else
            {
                InsereItensNaTabelaVendas();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
            btn_Excluir.Enabled = false;
        }

        private void FechaAConexaoECalculaValores()
        {
            con.FecharCon();
            Listar();
            ListarTroca();
            ValorTotalItem();
            ValorTotalItemTroca();
        }

        private void InsereItensNaTabelaVendas()
        {
            con.AbrirCon();
            sql = "INSERT INTO tb_itensVenda (id_Venda, id_Produto, quantidade, valorVenda, valorCusto) VALUES (@id_Venda, @id_Produto, @quantidade, @valorVenda, @valorCusto)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", lbl_ID_Venda.Text);
            cmd.Parameters.AddWithValue("@id_Produto", txt_ID_Produto.Text);
            cmd.Parameters.AddWithValue("@quantidade", int.Parse(cbx_Qtde.Text));
            cmd.Parameters.AddWithValue("@valorVenda", Convert.ToDouble(txtValor.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@valorCusto", Convert.ToDouble(txt_Custo.Text.Replace("R$", "")));
            cmd.ExecuteNonQuery();
        }

        private void Limpar()
        {
            txtCodBarras.Clear();
            txt_ID_Produto.Clear();
            txtEstoque.Clear();
            txtPoduto.Clear();
            txtValor.Clear();
            cbx_Qtde.Text = "1";
            txt_Valor_Total.Clear();
            txtCodBarras.Focus();
        }
        private void CalculaSaldo()
        {
            decimal valoritensNovo = 0;
            if (txt_ValorItensNovos.Text != String.Empty)
            {
                valoritensNovo = Convert.ToDecimal(txt_ValorItensNovos.Text.Replace("R$", ""));
            }
            txt_ValorSaldo.Text = Convert.ToString((Convert.ToDecimal(txt_ValorSaldo.Text.Replace("R$", "")) - (valoritensNovo + Convert.ToDecimal(txt_Valor_Total.Text.Replace("R$", "")))));
        }
        private void btn_Inserir_Click(object sender, EventArgs e)
        {
            if (txt_ValorSaldo.Text.Substring(0, 1) == "-")
            {
                MessageBox.Show("O saldo já está negativo, encerre a troca!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int qt_Itens = 0;
            int qt_prod = 0;
            int total_Itens = 0;
            if (txtCodBarras.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Digite o Código de Barras", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodBarras.Text = "";
                txtCodBarras.Focus();
                return;
            }

            // Verificar se o código de barras existe
            con.AbrirCon();
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM tbprodutos where codBarras = @codBarras", con.con);
            cmdVerificar.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                MessageBox.Show("Código de Barras não Cadastrado!", "Sem Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodBarras.Text = "";
                txtCodBarras.Focus();
                return;
            }
            con.FecharCon();
            CalcularItensNovos();
            //CalculaSaldo();
            AtualizarEstoque();
            VerificaSeOItemJáEstaNaTroca();
            ListarTroca();

            // Verificar se o item já está na venda
            con.AbrirCon();
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_itensVenda where id_Produto = @id_Produto And id_Venda = @id_Venda", con.con);
            cmdItem.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmdItem.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                ConsultaQuantidade();
                ValorTotalItem();
            }
            else
            {
                con.FecharCon();
                con.AbrirCon();
                sql = "INSERT INTO tb_itensVenda (id_Venda, id_Produto, quantidade, valorVenda, valorCusto) VALUES (@id_Venda, @id_Produto, @quantidade, @valorVenda, @valorCusto)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", lbl_ID_Venda.Text);
                cmd.Parameters.AddWithValue("@id_Produto", txt_ID_Produto.Text);
                cmd.Parameters.AddWithValue("@quantidade", int.Parse(cbx_Qtde.Text));
                cmd.Parameters.AddWithValue("@valorVenda", Convert.ToDouble(txtValor.Text.Replace("R$", "")));
                cmd.Parameters.AddWithValue("@valorCusto", Convert.ToDouble(txt_Custo.Text.Replace("R$", "")));

                cmd.ExecuteNonQuery();
                Listar();
                ValorTotalItem();
                con.FecharCon();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
            AtualizarValoresNaTabelaTrocaAtual();
            ValorTotalItemTroca();
            VerificaOSaldoDoValorDeTroca();
            Limpar();
        }

        private void CalcularItensNovos()
        {
            int qt_Itens = int.Parse(txt_Q_Total.Text);
            int qt_prod = int.Parse(cbx_Qtde.Text);
            int total_Itens = qt_Itens + qt_prod;

            txt_Q_Total.Text = Convert.ToString(total_Itens);

            if (txt_Q_Novos.Text != String.Empty)
            {
                qt_Itens = int.Parse(txt_Q_Novos.Text);
            }
            else
            {
                qt_Itens = 0;
            }

            qt_prod = int.Parse(cbx_Qtde.Text);
            total_Itens = qt_Itens + qt_prod;

            txt_Q_Novos.Text = Convert.ToString(total_Itens);

            if (txt_ValorItensNovos.Text != String.Empty)
            {
                txt_ValorItensNovos.Text = String.Format("{0:C}", Convert.ToString((Convert.ToDecimal(txt_ValorItensNovos.Text.Replace("R$", "")) + Convert.ToDecimal(txt_Valor_Total.Text.Replace("R$", "")))));
            }
            else
            {
                txt_ValorItensNovos.Text = String.Format("{0:C}", txt_Valor_Total.Text);
            }
            txt_ValorSaldo.Text = String.Format("{0:C}", Convert.ToString((Convert.ToDecimal(txt_Valor_Troca.Text.Replace("R$", "")) - Convert.ToDecimal(txt_ValorItensNovos.Text.Replace("R$", "")))));
        }

        private void ConsultaQuantidade()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_itensVenda where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    quantidade = Convert.ToInt32(reader["quantidade"]);
                    quantidade = quantidade + (int.Parse(cbx_Qtde.Text));
                }
            }
            con.FecharCon();
            con.AbrirCon();
            sql = "UPDATE tb_itensVenda SET quantidade= @quantidade where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@quantidade", quantidade);
            cmd.ExecuteNonQuery();
            con.FecharCon();
            Listar();
            return;
        }

        private void txtCodBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (txtCodBarras.Text != String.Empty)
                    {
                        Program.Botao = "codBarras";
                        ConsultaCodigoDeBarras();
                    }
                    else
                    {
                        txtCodBarras.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConsultaCodigoDeBarras()
        {
            con.AbrirCon();
            if (Program.Botao == "codBarras")
            {
                sql = "SELECT * FROM tbprodutos where codBarras = @codBarras";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataReader reader;
                cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txt_ID_Produto.Text = Convert.ToString(reader["id"]);
                        txtPoduto.Text = Convert.ToString(reader["nome"]);
                        txtValor.Text = Convert.ToString(reader["valor_venda"]);
                        txtEstoque.Text = Convert.ToString(reader["estoque"]);
                        txt_Valor_Total.Text = Convert.ToString(reader["valor_venda"]);
                        txt_Custo.Text = Convert.ToString(reader["valor_compra"]);
                    }

                    if (txt_Valor_Total.Text != String.Empty && cbx_Qtde.Text != String.Empty)
                    {
                        txt_Valor_Total.Text = (float.Parse(txtValor.Text) * int.Parse(cbx_Qtde.Text)).ToString();
                        txt_Valor_Total.Text = double.Parse(txt_Valor_Total.Text).ToString("C2");
                    }

                    txtValor.Text = double.Parse(txtValor.Text).ToString("C2");
                }
                else
                {
                    MessageBox.Show("Produto Não Encontrado!", "Verifique o Código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.FecharCon();
            }

            if (Program.Botao == "IDProduto")
            {
                sql = "SELECT * FROM tbprodutos where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataReader reader;
                cmd.Parameters.AddWithValue("@id", txt_ID_Produto.Text);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtCodBarras.Text = Convert.ToString(reader["codBarras"]);
                        txtPoduto.Text = Convert.ToString(reader["nome"]);
                        txtValor.Text = Convert.ToString(reader["valor_venda"]);
                        txtEstoque.Text = Convert.ToString(reader["estoque"]);
                        txt_Valor_Total.Text = Convert.ToString(reader["valor_venda"]);
                        txt_Custo.Text = Convert.ToString(reader["valor_compra"]);
                    }

                    if (txt_Valor_Total.Text != String.Empty && cbx_Qtde.Text != String.Empty)
                    {
                        txt_Valor_Total.Text = (float.Parse(txtValor.Text) * int.Parse(cbx_Qtde.Text)).ToString();
                        txt_Valor_Total.Text = double.Parse(txt_Valor_Total.Text).ToString("C2");
                    }

                    txtValor.Text = double.Parse(txtValor.Text).ToString("C2");
                }
                else
                {
                    MessageBox.Show("Produto Não Encontrado!", "Verifique o Código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.FecharCon();
            }
        }

        private void txt_ID_Produto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (txtCodBarras.Text != String.Empty)
                    {
                        Program.Botao = "IDProduto";
                        ConsultaCodigoDeBarras();
                    }
                    else
                    {
                        txtCodBarras.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Produto_Click(object sender, EventArgs e)
        {
            Filtros.FrmFiltrarProduto frmFiltrarProduto = new Filtros.FrmFiltrarProduto();
            frmFiltrarProduto.Show();
        }

        private void VerificaOSaldoDoValorDeTroca()
        {
            try
            {
                if (txt_ValorSaldo.Text.Substring(0, 1) == "-" || Convert.ToDecimal(txt_ValorSaldo.Text) == 0 && txt_ValorSaldo.Text != String.Empty)
                {
                    MessageBox.Show("Não há mais saldo positivo!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    lbl_Sub_TotalA.Text = txt_ValorSaldo.Text.Replace("-", "");
                    lbl_Sub_Total.Text = txt_ValorSaldo.Text.Replace("-", "");
                    lbl_Valor_da_Compra.Text = txt_ValorSaldo.Text.Replace("-", "");
                    lbl_Qtd_Itens.Text = txt_Q_Total.Text;
                    Bolquear();
                    cbx_Qtde.Enabled = false;
                    gridC.Enabled = false;
                    btn_Editar.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
        }

        private void AtualizarEstoque()
        {
            int total = 0;
            int estoque = 0;

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
                    estoque = Convert.ToInt16(reader["estoque"]);
                }
            }
            con.FecharCon();

            if (Program.troca == "Não")
            {
                total = estoque + int.Parse(cbx_Qtde.Text);
            }
            else
            {
                total = estoque - int.Parse(cbx_Qtde.Text);
            }

            con.AbrirCon();
            sql = "UPDATE tbprodutos SET estoque = @estoque where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@estoque", total);
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private void txt_Desconto_TextChanged(object sender, EventArgs e)
        {
            Desconto();
        }

        private void Desconto()
        {
            String desc = "0";
            try
            {
                desc = txt_Desconto.Text;
                decimal total = Convert.ToDecimal(lbl_Valor_da_Compra.Text.Replace("R$", ""));
                decimal desconto = Convert.ToDecimal(desc);
                decimal subTotal = total - desconto;
                lbl_Sub_TotalA.Text = String.Format("{0:C}", subTotal);
                lbl_Sub_Total.Text = String.Format("{0:C}", subTotal);
                lbl_Desconto.Text = String.Format("{0:C}", desconto);
            }
            catch (Exception ex)
            {
                desc = "0";
                decimal total = Convert.ToDecimal(lbl_Valor_da_Compra.Text.Replace("R$", ""));
                decimal desconto = Convert.ToDecimal(desc);
                decimal subTotal = total - desconto;
                lbl_Sub_TotalA.Text = String.Format("{0:C}", subTotal);
                lbl_Sub_Total.Text = String.Format("{0:C}", subTotal);
                lbl_Desconto.Text = String.Format("{0:C}", desconto);
            }
        }

        private void txt_Dinheiro_TextChanged(object sender, EventArgs e)
        {
            Troco();
        }

        private void Troco()
        {
            String valor = "0";
            try
            {
                valor = txt_Dinheiro.Text;
                decimal total = Convert.ToDecimal(lbl_Sub_TotalA.Text.Replace("R$", ""));
                decimal dinheiro = Convert.ToDecimal(valor);

                if (dinheiro > total)
                {
                    decimal troco = dinheiro - total;
                    lbl_Troco.Text = String.Format("{0:C}", troco);
                }
            }
            catch (Exception ex)
            {
                lbl_Troco.Text = String.Format("{0:C}", 0);
            }
        }

        private void cbx_Entrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Entrada.Enabled = true;
        }

        private void txt_Entrada_TextChanged(object sender, EventArgs e)
        {
            String ent = "0";
            try
            {
                if (cbx_Entrada.Text != String.Empty)
                {
                    ent = txt_Entrada.Text.Replace("R$", "");
                    decimal entrada = Convert.ToDecimal(ent);
                    decimal valorTotal = Convert.ToDecimal(lbl_Sub_Total.Text.Replace("R$", ""));
                    decimal saldo = valorTotal - entrada;

                    lbl_Sub_TotalA.Text = String.Format("{0:C}", saldo);
                    lbl_V_Entrada.Text = String.Format("{0:C}", entrada);
                }
                else
                {
                    MessageBox.Show("Selecione Primeiramente a Forma de Emtrada!", "Preencha a Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_Entrada.Text = "";
                    cbx_Entrada.Focus();
                }
            }
            catch (Exception ex)
            {
                if (cbx_Entrada.Text != String.Empty)
                {
                    ent = "0";
                    decimal entrada = Convert.ToDecimal(ent);
                    decimal valorTotal = Convert.ToDecimal(lbl_Sub_Total.Text.Replace("R$", ""));
                    decimal saldo = valorTotal - entrada;

                    lbl_Sub_TotalA.Text = String.Format("{0:C}", saldo);
                    lbl_V_Entrada.Text = String.Format("{0:C}", entrada);
                }
                else
                {
                    MessageBox.Show("Selecione Primeiramente a Forma de Emtrada!", "Preencha a Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txt_Entrada.Text = "";
                    cbx_Entrada.Focus();
                }
                //MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbx_QtdeParcelas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (LiberarConsutaParcelas == "Sim")
            {
                if (cbx_QtdeParcelas.Text == "0")
                {
                    ExcluirParcelas();
                    cbx_FormPagamento.Text = "";
                    cbx_QtdeParcelas.Text = "0";
                    VerificaSeExiteParcelasNaTroca();
                }
                else
                {
                    if (Program.ExecultadoVenda == "Sim")
                    {
                        return;
                    }

                    if (cbx_FormPagamento.Text == String.Empty && lbl_ID_Venda.Text != "0")
                    {
                        MessageBox.Show("Selecinoe o forma de Pagamento", "ATENÇÂO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (lbl_ID_Venda.Text != "0" && cbx_QtdeParcelas.Text != string.Empty)
                    {
                        var resultado = MessageBox.Show("Confirma o Pagamendo da 1º Parcela para o Dia: " + txt_Data_Primeira_Parce.Text, "1º PARCELA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Yes)
                        {
                            AtualizaFormaDePagamento();
                            ExcluirParcelas();
                            GeradorDeParcelas();
                            ListarParcelas();
                            FormatarDG_Parcelas();
                            grid_Parcelas.Visible = true;
                            lbl_Parcelas.Visible = true;
                            img.Visible = false;
                        }
                    }
                }
            }

        }

        private void AtualizaFormaDePagamento()
        {
            con.AbrirCon();
            sql = "UPDATE tb_Venda SET formaDePagamento = @formaDePagamento where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@formaDePagamento", cbx_FormPagamento.Text);
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private void ExcluirParcelas()
        {
            con.AbrirCon();
            sql = "DELETE FROM tb_Parcelas where id_Venda = @id_Venda And id_troca= @id_troca";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private double Truncar(double valor)
        {
            valor *= 100;
            valor = Math.Truncate(valor);
            valor /= 100;
            return valor;
        }

        private void GeradorDeParcelas()
        {
            double x = 0;
            int y = 0;
            int z = 0;
            double r = 0;
            double w = 0;
            double t = 0;
            double valorParcela = 0;
            //Exemplo
            x = Convert.ToDouble(lbl_Sub_Total.Text.Replace("R$", "")); //(5003,15)
            y = int.Parse(cbx_QtdeParcelas.Text);                      //(10)
            z = y - 1;                                                //(10 - 1 = 9)
            r = Truncar((x / y));                                    //Quebra em duas casas depois da vírgula
            w = x - (z * r);                                        //(w = 5003,15 - (9 * 500,31) -> (w = 5003,15 - (4.502,79)) -> (w = 500,36)
            t = w + (z * r);                                       //t = 500,36 + (9 * 500,31) -> t = 500,36 + 4.502,79 -> t = 5.003,15
            for (int i = 0; i < int.Parse(cbx_QtdeParcelas.Text); i++)
            {
                if (i == 0)
                {
                    valorParcela = w;
                }
                else
                {
                    valorParcela = r;
                }

                DateTime dataPrimeiroVencimento = DateTime.Now;
                String dataPagamento = DateTime.Now.ToString("dd/MM/yyyy");
                String pago = "Não";
                String DataPrimeira = DateTime.Now.ToString(txt_Data_Primeira_Parce.Text);
                string DataPaga = Convert.ToString(dataPagamento);
                dataPrimeiroVencimento = DateTime.Parse(txt_Data_Primeira_Parce.Text);

                if (i == 0 && DataPrimeira == DataPaga)
                {
                    pago = "Sim";
                }
                else
                {
                    dataPagamento = "";
                }

                con.AbrirCon();
                sql = "INSERT INTO tb_Parcelas (id_Venda, N_da_Parcela, Valor_Parcela, Data_Vencimento, Data_Pagamento, Pago) VALUES (@id_Venda, @N_da_Parcela, @Valor_Parcela, @Data_Vencimento, @Data_Pagamento, @Pago)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                cmd.Parameters.AddWithValue("@N_da_Parcela", Convert.ToString((i + 1)) + " / " + cbx_QtdeParcelas.Text);
                cmd.Parameters.AddWithValue("@Valor_Parcela", valorParcela);
                cmd.Parameters.AddWithValue("@Data_Vencimento", dataPrimeiroVencimento.AddMonths(i).ToShortDateString());
                cmd.Parameters.AddWithValue("@Data_Pagamento", Convert.ToString(dataPagamento));
                cmd.Parameters.AddWithValue("@Pago", pago);


                cmd.ExecuteNonQuery();
            }
            con.FecharCon();
            ListarParcelas();
        }

        private void ListarParcelas()
        {
            try
            {
                con.AbrirCon();
                sql = "SELECT * FROM tb_Parcelas where id_Venda = @id_Venda And id_troca= @id_troca";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid_Parcelas.DataSource = dt;
                con.FecharCon();
                FormatarDG_Parcelas();
            }
            catch (Exception)
            {
            }
        }

        private void FormatarDG_Parcelas()
        {
            grid_Parcelas.Columns[0].Visible = false;
            grid_Parcelas.Columns[3].Visible = false;
            grid_Parcelas.Columns[5].Visible = false;
            grid_Parcelas.Columns[1].Width = 60;
            grid_Parcelas.Columns[2].Width = 81;
            grid_Parcelas.Columns[4].Width = 75;
            grid_Parcelas.Columns[1].HeaderText = "Nº";
            grid_Parcelas.Columns[2].HeaderText = "Valor";
            grid_Parcelas.Columns[4].HeaderText = "Vencimento";
            grid_Parcelas.Columns[2].DefaultCellStyle.Format = "C2";
        }

        private void btn_Finalizar_Troca_Click(object sender, EventArgs e)
        {
            Program.ExecultadoVenda = "Sim";
            cbx_QtdeParcelas.Text = "";
            var resultado = MessageBox.Show("Deseja Finalizar está Troca?", "Finalizar Troca", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {

                if (int.Parse(lbl_id_Cli.Text) == 0)
                {
                    MessageBox.Show("Selecione um Cliente!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                AtualizarValoresDoCaixa();
                FecharTroca();
                ConsultarValoresDaVendaOriginal();
                AlterarValoresDaVendaOriginal();
                DeletaItemDatb_trocaatual();
                IniciarNovaTrocaLimpaTudo();
            }
        }

        private void FecharTroca()
        {
            con.AbrirCon();
            sql = "UPDATE tb_itenstroca SET Status= 'Fechada' where id_troca = @id_troca";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }
        private void AtualizarValoresDoCaixa()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;

            con.AbrirCon();
            cmdVerificar = new MySqlCommand("SELECT * FROM tb_fechamentoCaixa WHERE id=(SELECT MAX(id) FROM tb_fechamentoCaixa)", con.con);
            reader = cmdVerificar.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    decimal valorInicial = Convert.ToDecimal(reader["valorInicialEmDinheiro"]);
                    decimal entrada = Convert.ToDecimal(reader["valorEntradaEmDinheiro"]);
                    decimal saldoDinheiro = Convert.ToDecimal(reader["valorSaldoEmDinheiro"]);
                    decimal totalVendido = Convert.ToDecimal(reader["totalVendido"]);

                    if (cbx_Entrada.Text == "Dinheiro")
                    {
                        entrada = (entrada + Convert.ToDecimal(txt_Entrada.Text.Replace("R$", "")));
                        saldoDinheiro = saldoDinheiro + (valorInicial + entrada);
                    }
                    if (cbx_FormPagamento.Text == "Dinheiro")
                    {
                        entrada = (entrada + Convert.ToDecimal(lbl_Sub_TotalA.Text.Replace("R$", "")));
                        saldoDinheiro = saldoDinheiro + (valorInicial + entrada);
                    }
                    totalVendido = totalVendido + Convert.ToDecimal(lbl_Sub_Total.Text.Replace("R$", ""));

                    con.AbrirCon();
                    sql = "UPDATE tb_fechamentoCaixa SET valorEntradaEmDinheiro = @valorEntradaEmDinheiro, valorSaldoEmDinheiro = @valorSaldoEmDinheiro, totalVendido = @totalVendido where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@valorEntradaEmDinheiro", entrada);
                    cmd.Parameters.AddWithValue("@valorSaldoEmDinheiro", saldoDinheiro);
                    cmd.Parameters.AddWithValue("@totalVendido", totalVendido);

                    cmd.ExecuteNonQuery();
                    con.FecharCon();
                }
            }
        }

        private void ConsultarValoresDaVendaOriginal()
        {
            con.AbrirCon();
            sql = "SELECT* FROM tb_Venda where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataReader reader;
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    valorCompraOriginal = Convert.ToDecimal(reader["valorCompra"]);
                    valorPagoOriginal = Convert.ToDecimal(reader["valorPago"]);
                    valorDescontoOriginal = Convert.ToDecimal(reader["valorDesconto"]);
                    valorDeEntradaOriginal = Convert.ToDecimal(reader["valorDeEntrada"]);
                    id_TrocaOriginal = Convert.ToString(reader["id_Troca"]);
                    formadeEntradaOriginal = Convert.ToString(reader["formadeEntrada"]);
                }
                con.FecharCon();
            }

            decimal Valorlbl_Sub_Total = 0;
            decimal Valorlbl_Sub_TotalA = 0;
            decimal Valortxt_Desconto = 0;
            decimal Valortxt_Entrada = 0;

            if (lbl_Sub_Total.Text != String.Empty)
            {
                Valorlbl_Sub_Total = Convert.ToDecimal(lbl_Sub_Total.Text.Replace("R$", ""));
            }

            if (lbl_Sub_TotalA.Text != String.Empty)
            {
                Valorlbl_Sub_TotalA = Convert.ToDecimal(lbl_Sub_TotalA.Text.Replace("R$", ""));
            }

            if (txt_Desconto.Text != String.Empty)
            {
                Valortxt_Desconto = Convert.ToDecimal(txt_Desconto.Text.Replace("R$", ""));
            }

            if (txt_Entrada.Text != String.Empty)
            {
                Valortxt_Entrada = Convert.ToDecimal(txt_Entrada.Text.Replace("R$", ""));
            }

            valorCompraOriginal = valorCompraOriginal + Valorlbl_Sub_Total;
            valorPagoOriginal = valorPagoOriginal + Valorlbl_Sub_TotalA;
            valorDescontoOriginal = valorDescontoOriginal + Valortxt_Desconto;
            valorDeEntradaOriginal = valorDeEntradaOriginal + Valortxt_Entrada;
            if (int.Parse(id_TrocaOriginal) != 0)
            {
                id_TrocaOriginal = id_TrocaOriginal + "/" + lb_IdTroca.Text;
            }
            else
            {
                id_TrocaOriginal = lb_IdTroca.Text;
            }

            if (formadeEntradaOriginal == String.Empty)
            {
                formadeEntradaOriginal = cbx_Entrada.Text;
            }
        }

        private void AlterarValoresDaVendaOriginal()
        {
            con.AbrirCon();
            sql = "UPDATE tb_Venda SET valorCompra= @valorCompra, valorDesconto= @valorDesconto, formadeEntrada= @formadeEntrada, valorPago= @valorPago, valorCustoTotal= @valorCustoTotal, valorDeEntrada= @valorDeEntrada, id_Troca= @id_Troca  where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@valorCompra", valorCompraOriginal);
            cmd.Parameters.AddWithValue("@valorDesconto", valorDescontoOriginal);
            cmd.Parameters.AddWithValue("@formadeEntrada", formadeEntradaOriginal);
            cmd.Parameters.AddWithValue("@valorPago", valorPagoOriginal);
            cmd.Parameters.AddWithValue("@valorCustoTotal", Convert.ToDouble(txt_CustoTotal.Text));
            cmd.Parameters.AddWithValue("@valorDeEntrada", valorDeEntradaOriginal);
            cmd.Parameters.AddWithValue("@id_Troca", id_TrocaOriginal);
            cmd.ExecuteNonQuery();
            con.FecharCon();
            MessageBox.Show("Troca Finalizada com Sucesso!", "Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeletaItemDatb_trocaatual()
        {
            con.AbrirCon();
            sql = "DELETE FROM tb_trocaatual where id_troca = @id_troca";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_troca", int.Parse(lb_IdTroca.Text));
            cmd.ExecuteNonQuery();
            con.FecharCon();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "Crédito";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "Dinheiro";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "Débito";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "Promissória";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "PIX";
        }
    }
}

