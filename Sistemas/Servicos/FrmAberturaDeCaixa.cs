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
    public partial class FrmAberturaDeCaixa : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        String idf = "1";
        public FrmAberturaDeCaixa()
        {
            InitializeComponent();
        }

        private void Calculos()
        {
            decimal saldo = Convert.ToDecimal(Program.saldoCaixa);
            try
            {
                if (txt_ValorInicial.Text != String.Empty)
                {
                    decimal valorDigitado = Convert.ToDecimal(txt_ValorInicial.Text);

                    if (valorDigitado <= saldo)
                    {
                        decimal resultado = saldo - valorDigitado;
                        lbl_Retirado.Text = String.Format("{0:C}", resultado);
                        lbl_ValorAcres.Text = "0";
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
                    lbl_Saldo.Text = String.Format("{0:C}", Convert.ToDecimal(txt_ValorInicial.Text));
                }
                else
                {
                    txt_ValorInicial.Clear();
                    lbl_Saldo.Text = "0";
                    lbl_Retirado.Text = String.Format("{0:C}", Convert.ToDecimal(Program.saldoCaixa));
                    lbl_ValorAcres.Text = "0";
                }
            }
            catch (Exception ex)
            {
                lbl_Retirado.Text = Convert.ToString(Convert.ToDecimal(Program.saldoCaixa));
                lbl_Saldo.Text = "0";
                lbl_ValorAcres.Text = "0";
            }
        }

        private void FrmAberturaDeCaixa_Load(object sender, EventArgs e)
        {
            VerificaValorInicial();
            lbl_TitValorRetirado.Visible = false;
            lbl_ValorRetirado.Visible = false;
        }

        private void VerificaValorInicial()
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
                    Program.valorInicial = 0;
                    txt_ValorInicial.Text = Convert.ToString(Program.valorInicial);
                    lbl_Retirado.Text = "0";
                    lbl_ValorAcres.Text = "0";
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
                            if (reader["dataHoraFechamento"] == String.Empty)
                            {
                                MessageBox.Show("O caixa já está aberto!", "Aberto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Close();
                            }
                            Program.valorInicial = Convert.ToDecimal(reader["valorSaldoEmDinheiro"]);
                            txt_ValorInicial.Text = String.Format("{0:C}", Program.valorInicial);
                            Program.saldoCaixa = Convert.ToDecimal(reader["valorSaldoEmDinheiro"]);
                            lbl_Retirado.Text = "0";
                            lbl_ValorAcres.Text = "0";
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

        private void ConsultaProximoID()
        {
            con.AbrirCon();
            sql = "SELECT max(id) from tb_fechamentoCaixa";
            try
            {
                cmd = new MySqlCommand(sql, con.con);
                if (cmd.ExecuteScalar() == DBNull.Value)
                {
                    idf = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                    idf = ra.ToString();
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

        private void VerificaSeTemEntradaOuSaida()
        {
            con.AbrirCon();
            if (lbl_Retirado.Text != "0")
            {
                sql = "INSERT INTO tb_movimentacaoCaixa (id_Fechamento, id_Usuario, valor, dataHora, Tipo, descricao) VALUES (@id_Fechamento, @id_Usuario, @valor, now(), 'Saída', 'Abertura')";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id_Fechamento", int.Parse(idf));
                cmd.Parameters.AddWithValue("@id_Usuario", int.Parse(Program.idUsuario));
                cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(lbl_Retirado.Text.Replace("R$", "")));
                cmd.ExecuteNonQuery();
            }
            else
            {
                if (lbl_ValorAcres.Text != "0")
                {
                    sql = "INSERT INTO tb_movimentacaoCaixa (id_Fechamento, id_Usuario, valor, dataHora, Tipo, descricao) VALUES (@id_Fechamento, @id_Usuario, @valor, now(), 'Entrada', 'Abertura')";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id_Fechamento", int.Parse(idf));
                    cmd.Parameters.AddWithValue("@id_Usuario", int.Parse(Program.idUsuario));
                    cmd.Parameters.AddWithValue("@valor", Convert.ToDouble(lbl_ValorAcres.Text.Replace("R$", "")));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ConsultaProximoID();
            VerificaSeTemEntradaOuSaida();
            InsereNoBanco();
        }

        private void InsereNoBanco()
        {
            //Insere novo fechamento de caixa "Abertura"
            if (txt_ValorInicial.Text == String.Empty)
            {
                txt_ValorInicial.Text = "0";
            }
            sql = "INSERT INTO tb_fechamentoCaixa (id_UsuarioAbertura, valorInicialEmDinheiro, dataHoraAbertura) VALUES (@id_UsuarioAbertura, @valorInicialEmDinheiro, now())";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_UsuarioAbertura", int.Parse(Program.idUsuario));
            cmd.Parameters.AddWithValue("@valorInicialEmDinheiro", Convert.ToDouble(txt_ValorInicial.Text.Replace("R$", "")));

            cmd.ExecuteNonQuery();
            con.FecharCon();
            MessageBox.Show("Caixa Aberto com Sucesso", "Aberto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void txt_ValorInicial_TextChanged(object sender, EventArgs e)
        {
            Calculos();
        }

        private void txt_ValorInicial_KeyPress(object sender, KeyPressEventArgs e)
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
            }            //Se a tecla digitada não for número e nem backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}
