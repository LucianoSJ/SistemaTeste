using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaLoja
{
    public partial class FrmMenu : Form
    {
        DateTime hora;
        Conexao con = new Conexao();

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void FrmMenu_Resize(object sender, EventArgs e)
        {
           // this.WindowState = FormWindowState.Maximized;
        }

        private void saírToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            pnlTopo.BackColor = Color.FromArgb(230, 230, 230);
            pnlRight.BackColor = Color.FromArgb(170, 170, 170);

            lbl_Usuario.Text = Program.nomeUsuario;
            lbl_Cargo.Text = Program.cargoUsuario;
            lbl_Data.Text = DateTime.Now.ToShortDateString();
        }

        private void MenuUsuarios_Click(object sender, EventArgs e)
        {
            Cadastros.FrmUsuarios frmUsuarios = new Cadastros.FrmUsuarios();
            frmUsuarios.Show();
        }

        private void MenuCategorias_Click(object sender, EventArgs e)
        {
            Cadastros.FrmCategoria frmCategoria = new Cadastros.FrmCategoria();
            frmCategoria.Show();
        }

        private void cargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastros.FrmCargos frmCargos = new Cadastros.FrmCargos();
            frmCargos.Show();
        }

        private void MenuProdutos_Click(object sender, EventArgs e)
        {
            Cadastros.FrmProdutos frmProdutos = new Cadastros.FrmProdutos();
            frmProdutos.Show();
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            Cadastros.FrmClientes frmClientes = new Cadastros.FrmClientes();
            frmClientes.Show();
        }

        private void MenuFornecedores_Click(object sender, EventArgs e)
        {
            Cadastros.FrmFornecedores frmFornecedores = new Cadastros.FrmFornecedores();
            frmFornecedores.Show();
        }

        private void entradaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Servicos.FrmEntradaDeProtuto frm = new Servicos.FrmEntradaDeProtuto();
            frm.Show();
        }

        private void btnNovaVenda_Click(object sender, EventArgs e)
        {
            Servicos.FrmCaixa frmCaixa = new Servicos.FrmCaixa();
            frmCaixa.Show();
        }

        private void novaVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmCaixa frmCaixa = new Servicos.FrmCaixa();
            frmCaixa.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hora = DateTime.Now;
            bl_Hora.Text = hora.ToLongTimeString();
        }

        private void entradaDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmAberturaDeCaixa frmAberturaDeCaixa = new Servicos.FrmAberturaDeCaixa();
            frmAberturaDeCaixa.Show();
        }

        private void fechamentoDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmFechamentoCaixa frmFechamentoCaixa = new Servicos.FrmFechamentoCaixa();
            frmFechamentoCaixa.Show();
        }

        private void cupomNãoFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Relatorios.Frm_Rel_CupomNFiscal frm_Rel_Cupom = new Relatorios.Frm_Rel_CupomNFiscal();
            frm_Rel_Cupom.ShowDialog();
            frm_Rel_Cupom.Dispose();
        }

        private void retiradaDeCaixaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Servicos.FrmMovimentacaoDeCaixa frmMovDeCaixa = new Servicos.FrmMovimentacaoDeCaixa();
            frmMovDeCaixa.Show();
        }

        private void saídaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmSaidaDeProdutos frmSaida = new Servicos.FrmSaidaDeProdutos();
            frmSaida.Show();
        }

        private void excluirVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmExcluirVendas frmExcluir = new Servicos.FrmExcluirVendas();
            frmExcluir.Show();
        }

        private void parcelasEmAbertoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmParcelas frmParcelas = new Servicos.FrmParcelas();
            frmParcelas.Show();
        }

        private void trocaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Servicos.FrmTroca frmTroca = new Servicos.FrmTroca();
            frmTroca.Show();
        }
    }
}
