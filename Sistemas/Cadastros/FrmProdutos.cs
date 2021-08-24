using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaLoja.Cadastros
{
    public partial class FrmProdutos : Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;
        string id;
        string codAntigo;
        string foto;
        string alterou;

        public FrmProdutos()
        {
            InitializeComponent();
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

        private void Listar()
        {
            con.AbrirCon();
            sql = "SELECT p.id, p.nome, p.descricao, p.id_fornec, p.valor_venda, p.valor_compra, p.codBarras, p.DataCad, p.estoque, f.nome, p.imagem, p.desconto FROM tbprodutos as p INNER JOIN tbfornecedores as f ON p.id_fornec = f.id order by p.nome asc";
            cmd = new MySqlCommand(sql, con.con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharCon();
            FormatarDG();
        }

        private void BuscarNome()
        {
            con.AbrirCon();
            sql = "SELECT p.id, p.nome, p.descricao, p.id_fornec, p.valor_venda, p.valor_compra, p.codBarras, p.DataCad, p.estoque, f.nome, p.imagem, p.desconto FROM tbprodutos as p INNER JOIN tbfornecedores as f ON p.id_fornec = f.id where p.nome LIKE @nome order by p.nome asc";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtBuscar.Text + "%");
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
            grid.Columns[0].HeaderText = "ID";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Descrição";
            grid.Columns[3].HeaderText = "Fornecedor";
            grid.Columns[4].HeaderText = "Valor Venda";
            grid.Columns[5].HeaderText = "Valor Compra";
            grid.Columns[6].HeaderText = "Cod. Barras";
            grid.Columns[7].HeaderText = "Data Cad.";
            grid.Columns[8].HeaderText = "Estoque";
            grid.Columns[9].HeaderText = "Fornecedor";
            grid.Columns[10].HeaderText = "Imagem";
            grid.Columns[11].HeaderText = "Desconto";
            grid.Columns[0].Visible = false;
            grid.Columns[3].Visible = false;
         // Formatar coluna para moeda
            grid.Columns[4].DefaultCellStyle.Format = "C2";
            grid.Columns[5].DefaultCellStyle.Format = "C2";
            grid.Columns[1].Width = 350;
            grid.Columns[9].Width = 300;
        }

        private void habilitarCampos()
        {
            txtNome.Focus();
            txtNome.Enabled = true;
            txtDescricao.Enabled = true;
            txtValor.Enabled = true;
            txtCusto.Enabled = true;
            txtCodBarras.Enabled = true;
            txtdesconto.Enabled = true;
            //txtEstoque.Enabled = true;
            cbxFrornecedores.Enabled = true;
        }

        private void desabilitarCampos()
        {
            limparCampos();
            txtNome.Enabled = false;
            txtNome.Focus();
            btnImg.Enabled = false;
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            txtValor.Enabled = false;
            txtCusto.Enabled = false;
            txtCodBarras.Enabled = false;
            txtEstoque.Enabled = false;
            cbxFrornecedores.Enabled = false;

        }

        private void limparCampos()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtValor.Text = "";
            txtCusto.Text = "";
            txtCodBarras.Text = "";
            txtEstoque.Text = "";
            cbxFrornecedores.Text = "";
        }

        private void limparFoto()
        {
            img.Image = Properties.Resources.sem_foto;
            foto = "img/sem-foto.jpg";
        }

        private void FrmProdutos_Load(object sender, EventArgs e)
        {
            limparFoto();
            CarregarCombobox();
            cbxFrornecedores.Text = "";
            Listar();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();
            limparFoto();
            habilitarCampos();
            btnImg.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            grid.Enabled = false;
            btnSalvar.Enabled = true;
            img.Enabled = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            {
                var resultado = MessageBox.Show("Deseja Realmente Excluir o Registro", "Excluir Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //Código do botão para Excluir
                    con.AbrirCon();
                    sql = "DELETE FROM tbprodutos where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    con.FecharCon();


                    MessageBox.Show("Registro Excluido com Sucesso!", "Dados Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnNovo.Enabled = true;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    limparCampos();
                    desabilitarCampos();
                    Listar();
                }
            }
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Imagens JPG(*.jpg;*.png)|*.jpg;*.png)|Todos os Arquivos(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString();
                img.ImageLocation = foto;
                alterou = "1";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Preencha o nome!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }

            //Código do botão para salvar
            con.AbrirCon();
            sql = "INSERT INTO tbprodutos (nome, descricao, id_fornec ,valor_venda, valor_compra, codBarras, DataCad, imagem, desconto) VALUES (@nome, @descricao, @fornecedores, @valor_venda, @valor_compra, @codBarras, curDate(), @imagem, @desconto)";
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("@fornecedores", cbxFrornecedores.SelectedValue);
            cmd.Parameters.AddWithValue("@valor_venda", txtValor.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@valor_compra", txtCusto.Text.Replace(",", "."));
            cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
            cmd.Parameters.AddWithValue("@imagem", Img());
            cmd.Parameters.AddWithValue("@desconto", txtdesconto.Text.Replace(",", "."));
            //cmd.Parameters.AddWithValue("@estoque",txtEstoque.Text);

            // Verificar se o código de barras já exeiste
            MySqlCommand cmdVerificar;
            cmdVerificar = new MySqlCommand("SELECT * FROM tbprodutos where codBarras = @codBarras", con.con);
            cmdVerificar.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmdVerificar;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Código de Barras já Registrado!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodBarras.Text = "";
                txtCodBarras.Focus();
                return;
            }

            cmd.ExecuteNonQuery();
            con.FecharCon();
            Listar();

            MessageBox.Show("Registro Salvo com Sucesso!", "Dados Salvos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            limparCampos();
            desabilitarCampos();
            Listar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            {
                if (txtNome.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Preencha o nome!", "Campo Vazio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Text = "";
                    txtNome.Focus();
                    return;
                }


                //Código do botão para editar
                con.AbrirCon(); 

                if(alterou == "1")
                {
                    sql = "UPDATE tbprodutos SET nome = @nome, descricao = @descricao, id_fornec = @id_fornec, valor_venda = @valor_venda, valor_compra = @valor_compra, codBarras = @codBarras, imagem = @imagem, desconto = @desconto  where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                    cmd.Parameters.AddWithValue("@imagem", Img());
                }
                else
                {
                    sql = "UPDATE tbprodutos SET nome = @nome, descricao = @descricao, id_fornec = @id_fornec, valor_venda = @valor_venda, valor_compra = @valor_compra, codBarras = @codBarras, desconto = @desconto  where id = @id";
                    cmd = new MySqlCommand(sql, con.con);
                }

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                cmd.Parameters.AddWithValue("@id_fornec", cbxFrornecedores.SelectedValue);
                cmd.Parameters.AddWithValue("@valor_venda", txtValor.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@valor_compra", txtCusto.Text.Replace(",", "."));
                cmd.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                cmd.Parameters.AddWithValue("@desconto", txtdesconto.Text.Replace(",", "."));
                // Verifica se o código de barras já existe no Banco
                if (txtCodBarras.Text != codAntigo)
                {
                    MySqlCommand cmdVerificar;
                    cmdVerificar = new MySqlCommand("SELECT * FROM tbprodutos where codBarras = @codBarras", con.con);
                    cmdVerificar.Parameters.AddWithValue("@codBarras", txtCodBarras.Text);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmdVerificar;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Código de Barras já Registrado!", "Já Restrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodBarras.Text = "";
                        txtCodBarras.Focus();
                        return;
                    }
                }

                cmd.ExecuteNonQuery();
                con.FecharCon();

                MessageBox.Show("Registro Editado com Sucesso!", "Dados Editado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNovo.Enabled = true;
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                limparCampos();
                desabilitarCampos();
                Listar();
                alterou = "0";
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            btnImg.Enabled = true;
            habilitarCampos();

            id = grid.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = grid.CurrentRow.Cells[1].Value.ToString();
            txtDescricao.Text = grid.CurrentRow.Cells[2].Value.ToString();
            cbxFrornecedores.Text = grid.CurrentRow.Cells[9].Value.ToString();
            txtValor.Text = grid.CurrentRow.Cells[4].Value.ToString();
            txtCusto.Text = grid.CurrentRow.Cells[5].Value.ToString();
            txtCodBarras.Text = grid.CurrentRow.Cells[6].Value.ToString();
            txtEstoque.Text = grid.CurrentRow.Cells[8].Value.ToString();
            codAntigo = grid.CurrentRow.Cells[6].Value.ToString();
            txtdesconto.Text = grid.CurrentRow.Cells[11].Value.ToString();

            if (grid.CurrentRow.Cells[10].Value != DBNull.Value)
            {
                byte[] imagem = (byte[])grid.CurrentRow.Cells[10].Value;
                MemoryStream ms = new MemoryStream(imagem);
                img.Image = System.Drawing.Image.FromStream(ms);
            }
            else
            {
                img.Image = Properties.Resources.sem_foto;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNome();
        }

        private void grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Program.chamadaProdutos == "estoque")
            {
                Program.idProdutos = grid.CurrentRow.Cells[0].Value.ToString();
                Program.nomeProdutos = grid.CurrentRow.Cells[1].Value.ToString();
                Program.fornecedoorProduto = grid.CurrentRow.Cells[9].Value.ToString();
                Program.valorProdutos = grid.CurrentRow.Cells[4].Value.ToString();
                Program.custoProdutos = grid.CurrentRow.Cells[5].Value.ToString();
                Program.estoqueProdutos = grid.CurrentRow.Cells[8].Value.ToString();
                //img.Text = grid.CurrentRow.Cells[9].Value.ToString();
                Close();
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCusto_KeyPress(object sender, KeyPressEventArgs e)
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

        private byte[] Img()
        {
            byte[] imagem_byte = null;
            if (foto == "")
            {
                return null;
            }

            FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imagem_byte = br.ReadBytes((int)fs.Length);
            return imagem_byte;
        }

    }
}
