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
    public partial class FrmCaixa : Form
    {

        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        Double valor;
        int qtde;
        Double total;
        int quantidade;
        String LiberarConsutaParcelas = "Não";
        public FrmCaixa()
        {
            InitializeComponent();
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

        private void NovaVenda()
        {
            GerarCodigoVenda();
            Listar();
            cbx_Qtde.Text = "1";
            cbx_QtdeParcelas.Text = "1";
            lbl_id_Cli.Text = "0";
            lbl_nomeCliente.Text = "Selecione o Cliente";
            lbl_Valor_da_Compra.Text = "R$ 0";
            lbl_Desconto.Text = "R$ 0";
            lbl_Sub_Total.Text = "R$ 0";
            lbl_Troco.Text = "R$ 0";
            lbl_V_Entrada.Text = "R$ 0";
            lbl_Qtd_Itens.Text = "0";
            txtCodBarras.Text = "";
            txt_Data_Primeira_Parce.Text = DateTime.Now.ToShortDateString();
            ExcluirParcelas();
            ListarParcelas();
            btn_Editar.Enabled = false;
            btn_Excluir.Enabled = false;
            HabilitarCampos();
            txtCodBarras.Focus();
        }

        private void BloquearComandos()
        {
            txtCodBarras.Enabled = false;
            txt_ID_Produto.Enabled = false;
            txtPoduto.Enabled = false;
            btn_Produto.Enabled = false;
            txtValor.Enabled = false;
            cbx_Qtde.Enabled = false;
            txt_Valor_Total.Enabled = false;
            btn_Inserir.Enabled = false;
        }

        private void HabilitarCampos()
        {
            btn_Editar.Enabled = false;
            btn_Excluir.Enabled = false;
            btn_Inserir.Enabled = true;
            txtCodBarras.Enabled = true;
            txt_ID_Produto.Enabled = true;
            txtPoduto.Enabled = true;
            txtValor.Enabled = true;
            txt_Valor_Total.Enabled = true;
            btn_Produto.Enabled = true;
            btn_Produto.Enabled = true;
            cbx_Qtde.Enabled = true;
            btn_Inserir.Enabled = true;
            cbx_QtdeParcelas.Enabled = true;
            txtCodBarras.Focus();
        }

        private void AtualizarEstoque()
        {
            int total = 0;
            int estoque = 0;
            foreach (DataGridViewRow dt in grid.Rows)
            {
                int id = Convert.ToInt16(dt.Cells[0].Value);
                int qtdSaida = Convert.ToInt16(dt.Cells[2].Value);

                MySqlDataReader reader;
                con.AbrirCon();
                sql = "SELECT * FROM tbprodutos where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        estoque = Convert.ToInt16(reader["estoque"]);
                    }
                }
                con.FecharCon();
                total = estoque - qtdSaida;

                con.AbrirCon();
                sql = "UPDATE tbprodutos SET estoque = @estoque where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@estoque", total);
                cmd.ExecuteNonQuery();
                con.FecharCon();
            }
        }

        private void ExcluirParcelas()
        {
            con.AbrirCon();
            sql = "DELETE FROM tb_Parcelas where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
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
            con.AbrirCon();
            sql = "SELECT * FROM tb_Parcelas where id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Parcelas.DataSource = dt;
            con.FecharCon();
            FormatarDG_Parcelas();
        }

        private void FormatarDG_Parcelas()
        {
            grid_Parcelas.Columns[0].Visible = false;
            grid_Parcelas.Columns[3].Visible = false;
            grid_Parcelas.Columns[5].Visible = false;
            grid_Parcelas.Columns[6].Visible = false;
            grid_Parcelas.Columns[1].Width = 60;
            grid_Parcelas.Columns[2].Width = 81;
            grid_Parcelas.Columns[4].Width = 75;
            grid_Parcelas.Columns[1].HeaderText = "Nº";
            grid_Parcelas.Columns[2].HeaderText = "Valor";
            grid_Parcelas.Columns[4].HeaderText = "Vencimento";
            grid_Parcelas.Columns[2].DefaultCellStyle.Format = "C2";
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

        private void GerarCodigoVenda()
        {
            con.AbrirCon();
            sql = "SELECT max(id_Venda) from tb_venda";

            try
            {
                cmd = new MySqlCommand(sql, con.con);
                if (cmd.ExecuteScalar() == DBNull.Value)
                {
                    lbl_ID_Venda.Text = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    lbl_ID_Venda.Text = ra.ToString();
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
            grid.DataSource = dt;
            con.FecharCon();
            dt.Columns.Add("Total", typeof(Double));
            FormatarDG();
        }

        private void ValorTotalItem()
        {
            int qt_Itens = 0;
            decimal total = 0;
            decimal totalCusto = 0;
            foreach (DataGridViewRow dt in grid.Rows)
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

                lbl_Sub_Total.Text = String.Format("{0:C}", total);
                lbl_Valor_da_Compra.Text = String.Format("{0:C}", total);
                txt_CustoTotal.Text = Convert.ToString(totalCusto);
                lbl_Qtd_Itens.Text = Convert.ToString(qt_Itens);
            }
        }

        private void FormatarDG()
        {
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Produto";
            grid.Columns[2].HeaderText = "Qtde.";
            grid.Columns[3].HeaderText = "Valor Un.";
            grid.Columns[4].HeaderText = "Custo.";
            grid.Columns[5].HeaderText = "Total";
            grid.Columns[3].DefaultCellStyle.Format = "C2";
            grid.Columns[4].DefaultCellStyle.Format = "C2";
            grid.Columns[5].DefaultCellStyle.Format = "C2";
            grid.Columns[0].Width = 80;
            grid.Columns[1].Width = 336;
            grid.Columns[2].Width = 60;

            grid.Columns[4].Visible = false;
        }

        private void FrmCaixa_Load(object sender, EventArgs e)
        {
            Program.CodBarras = "NT";
            Program.forAberto = "caixa";
            Program.ExecultadoVenda = "Não";
            LimparTudo();
        }

        private void LimparTudo()
        {
            lbl_Usuario.Text = Program.nomeUsuario;
            lbl_id_User.Text = Program.idUsuario;
            Listar();
            cbx_Qtde.Text = "1";
            cbx_QtdeParcelas.Text = "1";
            lbl_id_Cli.Text = "0";
            lbl_ID_Venda.Text = "0";
            lbl_nomeCliente.Text = "Selecione o Cliente";
            lbl_Valor_da_Compra.Text = "R$ 0";
            lbl_Desconto.Text = "R$ 0";
            lbl_Sub_Total.Text = "R$ 0";
            lbl_Troco.Text = "R$ 0";
            lbl_Qtd_Itens.Text = "0";
            lbl_V_Entrada.Text = "R$ 0";
            lbl_Sub_TotalA.Text = "R$ 0";
            txtCodBarras.Text = "";
            txt_Data_Primeira_Parce.Text = DateTime.Now.ToShortDateString();
            BloquearComandos();
            btn_Editar.Enabled = false;
            btn_Excluir.Enabled = false;
            btn_OK_Cliente.Enabled = false;
            txt_Entrada.Enabled = false;
            cbx_QtdeParcelas.Enabled = false;

            grid_Parcelas.DataSource = null; //Remover a datasource
            grid_Parcelas.Columns.Clear(); //Remover as colunas
            grid_Parcelas.Rows.Clear();    //Remover as linhas
            grid_Parcelas.Refresh();    //Para a grid se actualizar*/

            grid.DataSource = null;
            grid.Columns.Clear();
            grid.Rows.Clear();
            grid.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cadastros.FrmVendedor frmVendedor = new Cadastros.FrmVendedor();
            frmVendedor.Show();
        }

        private void FrmCaixa_Activated(object sender, EventArgs e)
        {
            if (Program.vendedorAcionado == "Sim")
            {
                lbl_id_User.Text = Program.idUsuario;
                lbl_Usuario.Text = Program.vendedor;
            }
            else
            {
                lbl_id_Cli.Text = Program.idcliente;
                lbl_nomeCliente.Text = Program.nomecliente;
                btn_OK_Cliente.Enabled = true;
            }

            if (Program.CodBarras != "NT")
            {
                txtCodBarras.Text = Program.CodBarras;
                Program.CodBarras = "NT";
                ConsultaCodigoDeBarras();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Filtros.FrmFiltrarCliente frmFiltrarCliente = new Filtros.FrmFiltrarCliente();
            frmFiltrarCliente.Show();
        }

        private void btn_Inserir_Click(object sender, EventArgs e)
        {
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

            // Verificar se o item já está na venda
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
                Limpar();
            }
            else
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
                Listar();
                ValorTotalItem();
                Limpar();
                con.FecharCon();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
        }

        private void btn_Finalizar_Venda_Click(object sender, EventArgs e)
        {
            Program.ExecultadoVenda = "Sim";
            cbx_QtdeParcelas.Text = "";
            var resultado = MessageBox.Show("Deseja Finalizar está Venda?", "Finalizar Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {

                if (int.Parse(lbl_id_Cli.Text) == 0)
                {
                    MessageBox.Show("Selecione um Cliente!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbx_FormPagamento.Text == String.Empty)
                {
                    MessageBox.Show("Selecione a forma de Pagamento!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AtualizarValoresDoCaixa();
                AtualizarEstoque();

                Decimal desconto = 0;
                Decimal valorEntrada = 0;
                String formaEndrada = "";

                if (txt_Desconto.Text != String.Empty)
                {
                    desconto = Convert.ToDecimal(txt_Desconto.Text);
                }
                if (txt_Entrada.Text != String.Empty)
                {
                    valorEntrada = Convert.ToDecimal(txt_Entrada.Text.Replace(",", "."));
                }
                if (cbx_Entrada.Text != String.Empty)
                {
                    formaEndrada = cbx_Entrada.Text;
                }

                {
                    con.AbrirCon();
                    sql = "UPDATE tb_Venda SET valorCompra = @valorCompra, valorDesconto = @valorDesconto, valorPago = @valorPago, valorCustoTotal = @valorCustoTotal, formaDePagamento = @formaDePagamento, valorDeEntrada = @valorDeEntrada, formadeEntrada = @formadeEntrada where id_Venda = @id_Venda";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                    cmd.Parameters.AddWithValue("@valorCompra", Convert.ToDouble(lbl_Valor_da_Compra.Text.Replace("R$", "")));
                    cmd.Parameters.AddWithValue("@valorDesconto", desconto);
                    cmd.Parameters.AddWithValue("@valorPago", Convert.ToDouble(lbl_Sub_Total.Text.Replace("R$", "")));
                    cmd.Parameters.AddWithValue("@valorCustoTotal", Convert.ToDouble(txt_CustoTotal.Text));
                    cmd.Parameters.AddWithValue("@formaDePagamento", cbx_FormPagamento.Text);
                    cmd.Parameters.AddWithValue("@valorDeEntrada", txt_Entrada.Text.Replace(",", "."));
                    cmd.Parameters.AddWithValue("@formadeEntrada", formaEndrada);

                    cmd.ExecuteNonQuery();
                    con.FecharCon();
                    MessageBox.Show("Venda Finalizada com Sucesso!", "Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NovaVenda();
                    LimparTudo();
                }
            }
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

        private void txt_Desconto_TextChanged(object sender, EventArgs e)
        {
            Desconto();
        }

        private void txt_Dinheiro_TextChanged(object sender, EventArgs e)
        {
            Troco();
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
                            //FormatarDG_Parcelas();   
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

        private void btn_Teste_Click(object sender, EventArgs e)
        {
            ExcluirParcelas();
            GeradorDeParcelas();
            ListarParcelas();
            //FormatarDG_Parcelas();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ID_Produto.Text = grid.CurrentRow.Cells[0].Value.ToString();
            txtPoduto.Text = grid.CurrentRow.Cells[1].Value.ToString();
            cbx_Qtde.Text = grid.CurrentRow.Cells[2].Value.ToString();
            txtValor.Text = grid.CurrentRow.Cells[3].Value.ToString();
            txt_Custo.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txt_Valor_Total.Text = grid.CurrentRow.Cells[5].Value.ToString();
            btn_Editar.Enabled = true;
            btn_Excluir.Enabled = true;
            btn_Inserir.Enabled = false;
            txtCodBarras.Enabled = false;
            txt_ID_Produto.Enabled = false;
            txtPoduto.Enabled = false;
            txtValor.Enabled = false;
            txt_Valor_Total.Enabled = false;
            btn_Produto.Enabled = false;

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
            sql = "UPDATE tb_itensVenda SET quantidade= @quantidade where id_Produto = @id_Produto And id_Venda = @id_Venda";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
            cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
            cmd.Parameters.AddWithValue("@quantidade", int.Parse(cbx_Qtde.Text));
            cmd.ExecuteNonQuery();
            Listar();
            ValorTotalItem();
            HabilitarCampos();
            Limpar();
            con.FecharCon();
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja Realmente Excluir o Item da Venda?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                con.AbrirCon();
                sql = "DELETE FROM tb_itensVenda where id_Produto = @id_Produto And id_Venda = @id_Venda";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Produto", int.Parse(txt_ID_Produto.Text));
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                cmd.ExecuteNonQuery();
                con.FecharCon();
                Listar();
                HabilitarCampos();
                ValorTotalItem();
                Limpar();
                lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
            }
        }

        private void btn_OK_Cliente_Click(object sender, EventArgs e)
        {
            Program.ExecultadoVenda = "Não";
            FiltarVenda();
            btn_OK_Cliente.Enabled = false;
            HabilitarCampos();
            FiltrarFormaDEPagamento();
            LiberarConsutaParcelas = "Sim";
        }

        private void FiltrarFormaDEPagamento()
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

        private void FiltarVenda()
        {
            // Verificar se o cliente esta com uma venda em aberto
            MySqlCommand cmdItem;
            cmdItem = new MySqlCommand("SELECT * FROM tb_Venda where id_Cliente = @id_Cliente And valorPago = 00.0", con.con);
            cmdItem.Parameters.AddWithValue("@id_Cliente", int.Parse(lbl_id_Cli.Text));
            MySqlDataAdapter dat = new MySqlDataAdapter();
            dat.SelectCommand = cmdItem;
            DataTable dta = new DataTable();
            dat.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                if (Program.forVendas != "Sim")
                {
                    MessageBox.Show("Cliente já tem uma venda em aberto!", "VENDA ABERTA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                Program.forVendas = "Não";
                con.AbrirCon();
                sql = "SELECT* FROM tb_Venda where id_Cliente = @id_Cliente And valorPago = 00.0";
                cmd = new MySqlCommand(sql, con.con);
                MySqlDataReader reader;
                cmd.Parameters.AddWithValue("@id_Cliente", int.Parse(lbl_id_Cli.Text));
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lbl_ID_Venda.Text = Convert.ToString(reader["id_Venda"]);
                    }
                    con.FecharCon();
                    Listar();
                    ValorTotalItem();
                    ListarParcelas();
                }
            }
            else
            {
                GerarCodigoVenda();
                con.AbrirCon();
                sql = "INSERT INTO tb_venda (id_Venda, id_Cliente, id_Usuario, dataVenda) VALUES (@id_Venda, @id_Cliente, @id_Usuario, now())";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Venda", int.Parse(lbl_ID_Venda.Text));
                cmd.Parameters.AddWithValue("@id_Cliente", int.Parse(lbl_id_Cli.Text));
                cmd.Parameters.AddWithValue("@id_Usuario", int.Parse(lbl_id_User.Text));
                cmd.ExecuteNonQuery();
                lbl_Usuario.Text = Program.nomeUsuario;
                lbl_id_User.Text = Program.idUsuario;
                Listar();
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

                grid_Parcelas.DataSource = null; //Remover a datasource
                grid_Parcelas.Columns.Clear(); //Remover as colunas
                grid_Parcelas.Rows.Clear();    //Remover as linhas
                grid_Parcelas.Refresh();    //Para a grid se actualizar*/

                grid.DataSource = null;
                grid.Columns.Clear();
                grid.Rows.Clear();
                grid.Refresh();
            }
            lbl_Sub_TotalA.Text = lbl_Sub_Total.Text;
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

        private void cbx_Entrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Entrada.Enabled = true;
        }

        private void cbx_Qtde_DropDownClosed(object sender, EventArgs e)
        {
            if (txtCodBarras.Text == String.Empty)
            {
                MessageBox.Show("Campo do Produto está Vazio!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbx_Qtde.Text = "1";
                txtCodBarras.Focus();
                return;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LiberarConsutaParcelas = "Não";
            Filtros.FrmFiltrarVenda frmFiltrarVenda = new Filtros.FrmFiltrarVenda();
            frmFiltrarVenda.Show();
        }

        private void btn_Produto_Click(object sender, EventArgs e)
        {
            Filtros.FrmFiltrarProduto frmFiltrarProduto = new Filtros.FrmFiltrarProduto();
            frmFiltrarProduto.Show();
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

        private void button2_Click_1(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "Promissória";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cbx_FormPagamento.Text = "PIX";
        }
    }
}
