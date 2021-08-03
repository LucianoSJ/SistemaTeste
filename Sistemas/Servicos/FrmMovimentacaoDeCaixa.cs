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
    public partial class FrmMovimentacaoDeCaixa : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmMovimentacaoDeCaixa()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tb_fechamentoCaixa order by id DESC";
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
            grid.Columns[0].HeaderText = "Nº";
            grid.Columns[1].HeaderText = "Id User -->";
            grid.Columns[2].HeaderText = "Abertura";
            grid.Columns[3].HeaderText = "Id User -->";
            grid.Columns[4].HeaderText = "Fechamento";
            grid.Columns[5].HeaderText = "Dinheiro";
            grid.Columns[6].HeaderText = "Entrada";
            grid.Columns[7].HeaderText = "Retirada";
            grid.Columns[8].HeaderText = "Saldo Din.";
            grid.Columns[9].HeaderText = "Total";
            grid.Columns[0].Width = 40;
            grid.Columns[1].Width = 40;
            grid.Columns[2].Width = 135;
            grid.Columns[3].Width = 40;
            grid.Columns[4].Width = 135;
            grid.Columns[5].DefaultCellStyle.Format = "C2";
            grid.Columns[6].DefaultCellStyle.Format = "C2";
            grid.Columns[7].DefaultCellStyle.Format = "C2";
            grid.Columns[8].DefaultCellStyle.Format = "C2";
            grid.Columns[9].DefaultCellStyle.Format = "C2";
            grid.Columns[10].Visible = false;
        }

        private void FrmMovimentacaoDeCaixa_Load(object sender, EventArgs e)
        {
            txt_Obs.Text = "Movimentação";
            BuscarValoresBD();
            Calculos();
            lbl_Retirado.Text = "0";
            Listar();
        }
        private void BuscarValoresBD()
        {
            MySqlCommand cmdVerificar;
            MySqlDataReader reader;
            con.AbrirCon();
            sql = "SELECT max(id) from tb_fechamentoCaixa";

            try
            {
                cmd = new MySqlCommand(sql, con.con);
                if (cmd.ExecuteScalar() == DBNull.Value)
                {
                    txt_ValorInicial.Text = "1";
                    txt_IDFechamento.Text = "1";
                }
                else
                {
                    con.AbrirCon();
                    cmdVerificar = new MySqlCommand("SELECT * FROM tb_fechamentoCaixa WHERE id=(SELECT MAX(id) FROM tb_fechamentoCaixa)", con.con);
                    reader = cmdVerificar.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txt_ValorInicial.Text = Convert.ToString(String.Format("{0:C}", reader["valorInicialEmDinheiro"]));
                            txt_IDFechamento.Text = Convert.ToString(reader["id"]);
                            txt_DataHora.Text = Convert.ToString(reader["dataHoraAbertura"]);
                            txt_Entrada.Text = Convert.ToString(String.Format("{0:C}", reader["valorEntradaEmDinheiro"]));
                            txt_Retirada.Text = Convert.ToString(String.Format("{0:C}", reader["valorRetiradoEmDinheiro"]));
                            txt_Saldo.Text = Convert.ToString(String.Format("{0:C}", reader["valorSaldoEmDinheiro"]));
                            txt_TotalVendido.Text = Convert.ToString(String.Format("{0:C}", reader["totalVendido"]));

                            if (txt_Saldo.Text == "R$ 0,00")
                            {
                                Program.saldoCaixa = Convert.ToDecimal(reader["valorInicialEmDinheiro"]);
                            }
                            else
                            {
                                Program.saldoCaixa = Convert.ToDecimal(reader["valorSaldoEmDinheiro"]);
                            }

                            if (Convert.ToString(reader["status"]) == "Fechado")
                            {
                                txt_NovoValor.Enabled = false;
                                btnSalvar.Enabled = false;
                                lbl_CxFechado.Visible = true;
                                lbl_DgValor.Visible = false;
                            }

                            txt_NovoValor.Text = Convert.ToString(Convert.ToDecimal(Program.saldoCaixa));
                        }
                    }
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

        private void Calculos()
        {
            decimal saldo = Convert.ToDecimal(Program.saldoCaixa);
            try
            {
                if (txt_ValorInicial.Text != String.Empty)
                {
                    decimal valorDigitado = Convert.ToDecimal(txt_NovoValor.Text);

                    if (valorDigitado <= saldo)
                    {
                        decimal resultado = saldo - valorDigitado;
                        lbl_Retirado.Text = String.Format("{0:C}", resultado);
                    }
                    else
                    {
                        lbl_Retirado.Text = "0";

                        if (valorDigitado > saldo)
                        {
                            decimal resultado = valorDigitado - saldo;
                            lbl_ValorAcres.Text = String.Format("{0:C}", resultado);
                        }
                        else
                        {
                            lbl_ValorAcres.Text = "0";
                        }
                    }
                    lbl_Saldo.Text = String.Format("{0:C}", Convert.ToDecimal(txt_NovoValor.Text));
                }
                else
                {
                    txt_NovoValor.Clear();
                    lbl_Retirado.Text = "0";
                    lbl_Saldo.Text = "0";
                }
            }
            catch (Exception ex)
            {
                lbl_Retirado.Text = Convert.ToString(Convert.ToDecimal(Program.saldoCaixa));
                lbl_Saldo.Text = "0";
                lbl_ValorAcres.Text = "0";
            }
        }

        private void VerificaSeTemEntradaOuSaida()
        {
            con.AbrirCon();
            if (lbl_Retirado.Text != "0")
            {
                sql = "INSERT INTO tb_movimentacaoCaixa (id_Fechamento, id_Usuario, valor, dataHora, Tipo, descricao) VALUES (@id_Fechamento, @id_Usuario, @valor, now(), 'Saida', @descricao)";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Fechamento", int.Parse(txt_IDFechamento.Text));
                cmd.Parameters.AddWithValue("@id_Usuario", int.Parse(Program.idUsuario));
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(lbl_Retirado.Text.Replace("R$", "")));
                cmd.Parameters.AddWithValue("@descricao", txt_Obs.Text);
                cmd.ExecuteNonQuery();
            }
            else
            {
                if (lbl_ValorAcres.Text != "0")
                {
                    sql = "INSERT INTO tb_movimentacaoCaixa (id_Fechamento, id_Usuario, valor, dataHora, Tipo, descricao) VALUES (@id_Fechamento, @id_Usuario, @valor, now(), 'Entrada', @descricao)";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id_Fechamento", int.Parse(txt_IDFechamento.Text));
                    cmd.Parameters.AddWithValue("@id_Usuario", int.Parse(Program.idUsuario));
                    cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(lbl_ValorAcres.Text.Replace("R$", "")));
                    cmd.Parameters.AddWithValue("@descricao", txt_Obs.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            VerificaSeTemEntradaOuSaida();

            lbl_Retirado.Text = Convert.ToString((Convert.ToDouble(txt_Retirada.Text.Replace("R$", "")) + Convert.ToDouble(lbl_Retirado.Text.Replace("R$", ""))));
            con.AbrirCon();
            sql = "UPDATE tb_fechamentoCaixa SET valorSaldoEmDinheiro = @valorSaldoEmDinheiro, valorRetiradoEmDinheiro = @valorRetiradoEmDinheiro where id = @id";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id", int.Parse(txt_IDFechamento.Text));
            cmd.Parameters.AddWithValue("@valorSaldoEmDinheiro", Convert.ToDouble(lbl_Saldo.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@valorRetiradoEmDinheiro", Convert.ToDouble(lbl_Retirado.Text.Replace("R$", "")));

            cmd.ExecuteNonQuery();
            con.FecharCon();
            MessageBox.Show("Movimentação realizada com Sucesso!", "MOVIMENTAÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void txt_NovoValor_TextChanged_1(object sender, EventArgs e)
        {
            Calculos();
        }

        private void txt_NovoValor_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
