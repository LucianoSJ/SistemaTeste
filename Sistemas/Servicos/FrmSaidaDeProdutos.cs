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
    public partial class FrmSaidaDeProdutos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;

        public FrmSaidaDeProdutos()
        {
            InitializeComponent();
        }

        private void InserirNaTabelatb_MovimentacaoDeEstoque()
        {
            con.AbrirCon();
            sql = "INSERT INTO tb_MovimentacaoDeEstoque (id_Produto, valorVenda, custo, qtd, data, notafiscal, observações, Tipo) VALUES (@id_Produto, @valorVenda, @custo, @qtd, curDate(), @notafiscal, @observações, 'Saída')";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@id_Produto", int.Parse(id));
            cmd.Parameters.AddWithValue("@valorVenda", Convert.ToDouble(txtValor.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@custo", Convert.ToDouble(txtCusto.Text.Replace("R$", "")));
            cmd.Parameters.AddWithValue("@qtd", int.Parse(txtQuantidade.Text));
            cmd.Parameters.AddWithValue("@notafiscal", int.Parse(txt_NF.Text));
            cmd.Parameters.AddWithValue("@observações", txt_Obs.Text);

            cmd.ExecuteNonQuery();
        }

        private void CarregarCombobox()
        {
            con.AbrirCon();
            sql = "SELECT * FROM tbfornecedores order by nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxFrornecedores.DataSource = dt;
            cbxFrornecedores.ValueMember = "id";
            cbxFrornecedores.DisplayMember = "nome";
            con.FecharCon();
        }

        private void habilitarCampos()
        {
            // txtProduto.Enabled = true;
            cbxFrornecedores.Enabled = true;
            //txtEstoque.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValor.Enabled = true;
            txtCusto.Enabled = true;
            btnSalvar.Enabled = true;
            txt_NF.Enabled = true;
            txt_Obs.Enabled = true;
            txtQuantidade.Focus();
        }

        private void desabilitarCampos()
        {
            txtProduto.Enabled = false;
            cbxFrornecedores.Enabled = false;
            txtEstoque.Enabled = false;
            txtQuantidade.Enabled = false;
            txtValor.Enabled = false;
            txtCusto.Enabled = false;
            btnSalvar.Enabled = false;
            txt_NF.Enabled = false;
            txt_Obs.Enabled = false;
        }

        private void limparCampos()
        {
            txtProduto.Text = "";
            cbxFrornecedores.Text = "";
            txtEstoque.Text = "";
            txtQuantidade.Text = "";
            txtValor.Text = "";
            txtCusto.Text = "";
        }

        private void FrmSaidaDeProdutos_Load(object sender, EventArgs e)
        {
            CarregarCombobox();
            desabilitarCampos();
            txt_NF.Text = "0";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            desabilitarCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            desabilitarCampos();
        }

        private void FrmSaidaDeProdutos_Activated(object sender, EventArgs e)
        {
            id = Program.idProdutos;
            txt_Id.Text = Program.idProdutos;
            txtProduto.Text = Program.nomeProdutos;
            cbxFrornecedores.Text = Program.fornecedoorProduto;
            txtEstoque.Text = Program.estoqueProdutos;
            txtValor.Text = Program.valorProdutos;
            txtCusto.Text = Program.custoProdutos;
        }

        private void btnProduto_Click_1(object sender, EventArgs e)
        {
            habilitarCampos();
            limparCampos();

            Program.chamadaProdutos = "estoque";
            Cadastros.FrmProdutos frmProdutos = new Cadastros.FrmProdutos();
            frmProdutos.Show();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            {
                if (txtProduto.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Selecione um Produto!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProduto.Text = "";
                    txtProduto.Focus();
                    return;
                }

                if (txtQuantidade.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha a Quantidade!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQuantidade.Text = "";
                    txtQuantidade.Focus();
                    return;
                }

                if (Convert.ToDouble(txtEstoque.Text) < Convert.ToDouble(txtQuantidade.Text))
                {
                    MessageBox.Show("A Quantidade da Saída não Pode ser Maior que Estoque!", "VERIFIQUE A QUANTIDADE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantidade.Text = "";
                    txtQuantidade.Focus();
                    return;
                }

                if (int.Parse(txtQuantidade.Text) == 0)
                {
                    MessageBox.Show("A Quantidade da Saída não Pode ser Zero!", "VERIFIQUE A QUANTIDADE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantidade.Text = "";
                    txtQuantidade.Focus();
                    return;
                }


                //Código do botão para editar os produtos
                con.AbrirCon();
                sql = "UPDATE tbprodutos SET id_fornec = @id_fornec, valor_venda = @valor_venda, valor_compra = @valor_compra, estoque = @estoque where id = @id";
                cmd = new MySqlCommand(sql, con.con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@id_fornec", cbxFrornecedores.SelectedValue);
                cmd.Parameters.AddWithValue("@valor_venda", txtValor.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@valor_compra", txtCusto.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@estoque",  Convert.ToDouble(txtEstoque.Text) - Convert.ToDouble(txtQuantidade.Text));

                cmd.ExecuteNonQuery();

                InserirNaTabelatb_MovimentacaoDeEstoque();
                con.FecharCon();
                MessageBox.Show("Lançamento Feito com Sucesso!", "SAIDA DE ESTOQUE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limparCampos();
                desabilitarCampos();
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Se a tecla digitada não for número e nem backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                e.Handled = true;
            }
        }
    }
}
